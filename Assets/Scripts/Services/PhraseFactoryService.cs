using UnityEngine;

public class PhraseFactoryService : BaseService
{
    [SerializeField] private GameObject _phrasePrefab;

    public GameObject CreatePhrase(int id, string text, bool isDraggable, Canvas canvas)
    {
        GameObject phrase = Instantiate(_phrasePrefab);
        phrase.GetComponent<PhraseController>().SetValues(id, text, isDraggable, canvas);
        return phrase;
    }
}
