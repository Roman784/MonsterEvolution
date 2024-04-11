using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
        YandexSender.Instance.InitYSDK();
        YandexReceiver.OnSDKInited.AddListener(OpenLevel);
    }

    private void OpenLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
