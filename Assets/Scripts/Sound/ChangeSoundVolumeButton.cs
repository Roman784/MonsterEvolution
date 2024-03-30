using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundVolumeButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    [Space]

    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;

    private void Awake()
    {
        _button.onClick.AddListener(ChangeVolume);
    }

    private void Start()
    {
        Sound.Instance.OnVolumeChanged.AddListener(UpdateIcon);
    }

    private void ChangeVolume()
    {
        float volume = Sound.Instance.Volume > 0 ? 0 : 1;

        UpdateIcon(volume);

        Sound.Instance.SetVolume(volume);
    }

    public void UpdateIcon(float volume)
    {
        _icon.sprite = volume > 0 ? _soundOn : _soundOff;
    }

    public void UpdateIcon()
    {
        float volume = Sound.Instance.Volume;
        UpdateIcon(volume);
    }
}
