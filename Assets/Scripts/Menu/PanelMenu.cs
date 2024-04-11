using System.Collections;
using UnityEngine;

public class PanelMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private float _animationSpeed;

    public void Open()
    {
        StopAllCoroutines();
        StartCoroutine(SetScale(Vector2.zero, Vector2.one, true));

        SoundPlayer.Instance.PlayOpenMenuSound();
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(SetScale(Vector2.one, Vector2.zero, false));

        SoundPlayer.Instance.PlayOpenMenuSound();
    }

    private IEnumerator SetScale(Vector2 from, Vector2 to, bool isActive)
    {
        _panel.transform.localScale = from;

        if (isActive)
            _panel.SetActive(true);

        do
        {
            _panel.transform.localScale = Vector2.Lerp(_panel.transform.localScale, to, _animationSpeed * Time.deltaTime);

            yield return null;
        }
        while ((Vector2)_panel.transform.localScale != to);

        if (!isActive)
            _panel.SetActive(false);
    }
}
