using UnityEngine;
using UnityEngine.SceneManagement;

public class RedirectBtn : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void Click()
    {
        SceneManager.LoadScene(sceneName);
    }
}
