using System.Collections.Generic;
using UnityEngine;

public class PhraseContainerController : MonoBehaviour
{
    [SerializeField] private bool _isGroupOne = false;

    private Dictionary<int, GameObject> _phrases = new Dictionary<int, GameObject>();

    public void AddPhrase(int id, string text)
    {
        GameObject phrase = ServiceLocator.Instance.AccessService<PhraseFactoryService>().CreatePhrase(id, text, _isGroupOne);
        phrase.transform.parent = transform;

        _phrases[id] = phrase;
    }
}
