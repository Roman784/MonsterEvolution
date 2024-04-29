using UnityEngine;

public class AdPanel : PanelMenu
{
    [SerializeField] private GameObject _bgPanel;

    public void ShowAd()
    {
        YandexSender.Instance.ShowRewardedVideo();
    }

    public new void Open()
    {
        _bgPanel.SetActive(true);
        base.Open();
    }

    public new void Close()
    {
        _bgPanel.SetActive(false);
        base.Close();
    }
}
