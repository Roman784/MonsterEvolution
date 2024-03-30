using UnityEngine;
using UnityEngine.Events;

public class Sound
{
    private static Sound _instance;
    public static Sound Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Sound();
            return _instance;
        }
    }

    public float Volume { get; private set; }

    public UnityEvent OnVolumeChanged = new UnityEvent();

    private Sound()
    {
    }

    public void Init(float volume)
    {
        Volume = volume;

        UpdateRenderers();
    }

    public void SetVolume(float volume)
    {
        Volume = volume;
        OnVolumeChanged.Invoke();

        DataContext.Instance.SetSoundVolume(Volume);
    }

    private void UpdateRenderers()
    {
        ChangeSoundVolumeButton[] buttons = GameObject.FindObjectsOfType<ChangeSoundVolumeButton>();

        foreach (ChangeSoundVolumeButton button in buttons) 
        {
            button?.UpdateIcon(Volume);
        }
    }
}
