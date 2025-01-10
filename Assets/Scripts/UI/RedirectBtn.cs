using UnityEngine;
using UnityEngine.SceneManagement;

public class RedirectBtn : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private AudioClip clickSfx;

    public void Click()
    {
        ServiceLocator.Instance.AccessService<AudioService>().PlayAudio(clickSfx);
        SceneManager.LoadScene(sceneName);
    }
}
