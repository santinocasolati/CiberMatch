using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhraseService : BaseService
{
    [SerializeField] private PhraseDatabaseSO _phraseDatabase;

    public List<string> GetGroupPhrases(int groupNumber)
    {
        return _phraseDatabase.Phrases.Select(p => groupNumber == 1 ? p.GroupOnePhrase : p.GroupTwoPhrase).ToList();
    }
}