using System.Collections.Generic;
using UnityEngine;

public class PhraseContainerController : MonoBehaviour
{
    [SerializeField] private bool _isGroupOne = false;
    [SerializeField] private Canvas _canvas;

    private Dictionary<int, GameObject> _phrases = new Dictionary<int, GameObject>();

    public void AddPhrase(int id, string text)
    {
        GameObject phrase = ServiceLocator.Instance.AccessService<PhraseFactoryService>().CreatePhrase(id, text, _isGroupOne, _canvas);
        phrase.transform.SetParent(transform, false);

        _phrases[id] = phrase;

        ShufflePhrases();
    }

    private void ShufflePhrases()
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        for (int i = 0; i < children.Count; i++)
        {
            int randomIndex = Random.Range(i, children.Count);
            Transform temp = children[i];
            children[i] = children[randomIndex];
            children[randomIndex] = temp;
        }

        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }
}
