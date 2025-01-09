using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PhraseManager : MonoBehaviour
{
    [SerializeField] private PhraseContainerController[] _phraseContainers;

    private Dictionary<int, string> _groupOnePhrases = new();
    private Dictionary<int, string> _groupTwoPhrases = new();

    private IEnumerator Start()
    {
        while (ServiceLocator.Instance == null)
        {
            yield return null;
        }

        PhraseService phraseService = null;

        while (phraseService == null)
        {
            phraseService = ServiceLocator.Instance.AccessService<PhraseService>();
            yield return null;
        }

        LoadPhrases(phraseService);
    }

    private void LoadPhrases(PhraseService phraseService)
    {
        List<string> groupOne = phraseService.GetGroupPhrases(1);
        List<string> groupTwo = phraseService.GetGroupPhrases(2);

        for (int i = 0; i < groupOne.Count; i++)
        {
            _groupOnePhrases[i] = groupOne[i];
            _groupTwoPhrases[i] = groupTwo[i];
        }

        CreatePhrases();
    }

    private void CreatePhrases()
    {
        AddPhrasesToContainer(_groupOnePhrases, _phraseContainers[0]);
        AddPhrasesToContainer(_groupTwoPhrases, _phraseContainers[1]);
    }

    private void AddPhrasesToContainer(Dictionary<int, string> phrases, PhraseContainerController container)
    {
        foreach (KeyValuePair<int, string> kvp in phrases)
        {
            container.AddPhrase(kvp.Key, kvp.Value);
        }
    }
}
