using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextLocalization : MonoBehaviour
{
    [SerializeField] private string _en;
    [SerializeField] private string _ru;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

        Localization.Instance.LanguageChanged.AddListener(UpdateText);
    }

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = Localization.Instance.Lang == Langs.Ru ? _ru : _en;
    }
}
