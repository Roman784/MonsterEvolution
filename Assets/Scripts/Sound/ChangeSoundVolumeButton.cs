using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundVolumeButton : MonoBehaviour
{
    [SerializeField] private GameObject _soundOn;
    [SerializeField] private GameObject _soundOff;

    private void Start()
    {
        Sound.Instance.OnVolumeChanged.AddListener(UpdateIcon);
    }

    public void ChangeVolume()
    {
        float volume = Sound.Instance.Volume > 0 ? 0 : 1;

        UpdateIcon(volume);

        Sound.Instance.SetVolume(volume);
    }

    public void UpdateIcon(float volume)
    {
        _soundOn.SetActive(volume > 0);
        _soundOff.SetActive(volume <= 0);
    }

    public void UpdateIcon()
    {
        float volume = Sound.Instance.Volume;
        UpdateIcon(volume);
    }
}
