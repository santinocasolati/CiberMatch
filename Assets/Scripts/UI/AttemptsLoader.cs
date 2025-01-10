using TMPro;
using UnityEngine;

public class AttemptsLoader : MonoBehaviour
{
    private void Start()
    {
        int attempts = PlayerPrefs.GetInt("Attempts");
        GetComponent<TMP_Text>().text = $"Intentos: {attempts}";
    }
}
