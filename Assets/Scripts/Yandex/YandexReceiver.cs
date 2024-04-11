using UnityEngine;
using UnityEngine.Events;

public class YandexReceiver : MonoBehaviour
{
    public static UnityEvent OnSDKInited = new UnityEvent();
    public static UnityEvent<string> OnDataLoaded = new UnityEvent<string>();
    public static UnityEvent Rewarded = new UnityEvent();

    public void InvokeYSDKInitEvent() => OnSDKInited.Invoke();
    public void InvokeDataLoadEvent(string data) => OnDataLoaded.Invoke(data);
    public void OnRewarded() => Rewarded.Invoke();

    public void StopGame()
    {
        AudioListener.volume = 0f;
        Time.timeScale = 0f;
    }
    public void ContinueGame()
    {
        AudioListener.volume = 1f;
        Time.timeScale = 1f;
        MusicPlayer.Instance?.ContinuePlay();
    }
}
