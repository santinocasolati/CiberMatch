using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Phrase DB")]
public class PhraseDatabaseSO : ScriptableObject
{
    [SerializeField] private List<PhrasePair> _phrases = new();

    public List<PhrasePair> Phrases { get { return _phrases; } }
}

[System.Serializable]
public class PhrasePair
{
    [SerializeField, TextArea] private string _groupOnePhrase;
    [SerializeField, TextArea] private string _groupTwoPhrase;
    private int _id;

    public string GroupOnePhrase { get { return _groupOnePhrase; } }
    public string GroupTwoPhrase { get { return _groupTwoPhrase; } }
}
