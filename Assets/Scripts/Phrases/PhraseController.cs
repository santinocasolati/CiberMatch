using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class PhraseController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private float returnDuration = .5f;

    private int _id;
    private bool _isDraggable = false;
    private bool _isMoving = false;

    private RectTransform _rectTransform;
    private Canvas _canvas;
    private GraphicRaycaster _graphicRaycaster;
    private CanvasGroup _canvasGroup;

    private Vector2 _originalAnchoredPosition;

    private PhraseController _hoveredPhrase = null;

    public int Id { get { return _id; } }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetValues(int id, string text, bool isDraggable, Canvas canvas)
    {
        _id = id;
        _isDraggable = isDraggable;
        _canvas = canvas;
        _graphicRaycaster = _canvas.GetComponent<GraphicRaycaster>();

        GetComponentInChildren<TMP_Text>().text = text;
    }

    public void OnDrag(PointerEventData eventData)
    {
        MatchAnimationService matchAnimationService = ServiceLocator.Instance.AccessService<MatchAnimationService>();
        if (!_isDraggable || _isMoving || !matchAnimationService.CanDrag) return;

        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

        PhraseController otherPhrase = TryMatch(eventData.position);
        if (otherPhrase != _hoveredPhrase)
        {
            if (_hoveredPhrase != null)
            {
                _hoveredPhrase.gameObject.GetComponent<RectTransform>().localScale = Vector2.one;
                _hoveredPhrase = null;
            }

            if (otherPhrase != null)
            {
                otherPhrase.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
                _hoveredPhrase = otherPhrase;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MatchAnimationService matchAnimationService = ServiceLocator.Instance.AccessService<MatchAnimationService>();
        if (!_isDraggable || _isMoving || !matchAnimationService.CanDrag) return;

        _hoveredPhrase = null;
        _originalAnchoredPosition = _rectTransform.anchoredPosition;
        transform.localScale = new Vector3(.85f, .85f, .85f);
        _canvasGroup.alpha = .6f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MatchAnimationService matchAnimationService = ServiceLocator.Instance.AccessService<MatchAnimationService>();
        if (!_isDraggable || _isMoving || !matchAnimationService.CanDrag) return;

        if (_hoveredPhrase != null)
        {
            _hoveredPhrase.gameObject.GetComponent<RectTransform>().localScale = Vector2.one;
            _hoveredPhrase = null;
        }

        PhraseController otherPhrase = TryMatch(eventData.position);

        if (otherPhrase == null)
        {
            MoveToOriginalPosition();
        }
        else if (_id != otherPhrase.Id)
        {
            MoveToOriginalPosition();
            ServiceLocator.Instance.AccessService<PhraseService>().PerformAttempt(false);
        }
        else
        {
            matchAnimationService.AnimateMatch(gameObject, otherPhrase.gameObject);

            ServiceLocator.Instance.AccessService<PhraseService>().PerformAttempt(true);
        }
    }

    private void MoveToOriginalPosition()
    {
        _isMoving = true;
        _rectTransform.DOAnchorPos(_originalAnchoredPosition, returnDuration).OnComplete(() =>
        {
            _isMoving = false;
        });
        transform.localScale = Vector3.one;
        _canvasGroup.alpha = 1f;
    }

    private PhraseController TryMatch(Vector2 position)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = position
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        _graphicRaycaster.Raycast(pointerEventData, raycastResults);

        PhraseController otherPhrase = null;

        foreach (RaycastResult result in raycastResults)
        {
            PhraseController tempPhrase = result.gameObject.GetComponent<PhraseController>();

            if (tempPhrase != null && tempPhrase != this)
            {
                otherPhrase = tempPhrase;
            }
        }

        return otherPhrase;
    }
}
