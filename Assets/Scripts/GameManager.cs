using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string winSceneName;
    [SerializeField] private TMP_Text attemptsText;

    private int _attempts = 0;

    private int _toGuess;
    private int _guessed = 0;

    private IEnumerator Start()
    {
        while (ServiceLocator.Instance == null)
        {
            yield return null;
        }

        MatchAnimationService matchAnimationService = null;
        PhraseService phraseService = null;
        while (matchAnimationService == null || phraseService == null)
        {
            matchAnimationService = ServiceLocator.Instance.AccessService<MatchAnimationService>();
            phraseService = ServiceLocator.Instance.AccessService<PhraseService>();
            yield return null;
        }

        _toGuess = phraseService.GetGroupPhrases(1).Count;

        matchAnimationService.OnAnimationEnded += MatchGuessed;
        phraseService.OnAttemptPerformed += AddAttempt;
    }

    private void AddAttempt()
    {
        _attempts++;
        attemptsText.text = $"Attempts: {_attempts}";
    }

    private void MatchGuessed()
    {
        _guessed++;

        if (_guessed >= _toGuess)
        {
            PlayerPrefs.SetInt("Attempts", _attempts);
            SceneManager.LoadScene(winSceneName);
        }
    }
}
