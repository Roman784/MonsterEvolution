using UnityEngine;

[System.Serializable]
public class LocalizedText
{
    [TextArea(1, 10)]
    public string En;
    [TextArea(1, 10)]
    public string Ru;

    public string Translate()
    {
        if (Localization.Instance.Lang == Langs.Ru) 
            return Ru;

        return En;
    }
}
