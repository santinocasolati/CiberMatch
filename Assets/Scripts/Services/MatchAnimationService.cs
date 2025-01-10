using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class MatchAnimationService : BaseService
{
    [SerializeField] private CanvasGroup backgroundCanvasGroup;
    [SerializeField] private RectTransform leftElement;
    [SerializeField] private RectTransform rightElement;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float waitDuration = 1f;
    [SerializeField] private float fadeDuration = 1f;

    private Vector3 leftOriginalPosition;
    private Vector3 rightOriginalPosition;

    private bool _canDrag = true;

    public bool CanDrag { get { return _canDrag; } }

    private void Awake()
    {
        leftOriginalPosition = leftElement.position;
        rightOriginalPosition = rightElement.position;
    }

    public void AnimateMatch(GameObject leftBox, GameObject rightBox)
    {
        _canDrag = false;
        SetupAnimation(leftBox.GetComponentInChildren<TMP_Text>().text, rightBox.GetComponentInChildren<TMP_Text>().text);
        PlayAnimation(leftBox, rightBox);
    }

    private void PlayAnimation(GameObject leftBox, GameObject rightBox)
    {
        backgroundCanvasGroup.gameObject.SetActive(true);

        backgroundCanvasGroup.DOFade(1f, fadeDuration).OnComplete(() =>
        {
            Destroy(leftBox);
            Destroy(rightBox);

            float leftHalfWidth = leftElement.rect.width / 2;
            float rightHalfWidth = rightElement.rect.width / 2;

            Vector2 leftTargetPosition = new Vector2(0 - leftHalfWidth, leftElement.position.y);
            Vector2 rightTargetPosition = new Vector2(0 + rightHalfWidth, rightElement.position.y);

            Sequence animationSequence = DOTween.Sequence();

            animationSequence
                .Join(leftElement.DOAnchorPos(leftTargetPosition, animationDuration))
                .Join(rightElement.DOAnchorPos(rightTargetPosition, animationDuration))
                .OnComplete(() => Invoke(nameof(EndAnimation), waitDuration));
        });
    }

    private void EndAnimation()
    {
        backgroundCanvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
        {
            backgroundCanvasGroup.gameObject.SetActive(false);
            _canDrag = true;
        });
    }

    private void SetupAnimation(string text1, string text2)
    {
        backgroundCanvasGroup.gameObject.SetActive(false);
        backgroundCanvasGroup.alpha = 0f;
        leftElement.position = leftOriginalPosition;
        rightElement.position = rightOriginalPosition;
        leftElement.GetComponentInChildren<TMP_Text>().text = text1;
        rightElement.GetComponentInChildren<TMP_Text>().text = text2;
    }
}
