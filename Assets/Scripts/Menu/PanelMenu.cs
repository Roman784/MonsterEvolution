using UnityEngine;

public class PanelMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void Open()
    {
        _panel.SetActive(true);

        SoundPlayer.Instance.PlayOpenMenuSound();
    }

    public void Close()
    {
        _panel.SetActive(false);

        SoundPlayer.Instance.PlayOpenMenuSound();
    }
}
