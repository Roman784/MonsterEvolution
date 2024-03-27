[System.Serializable]
public class LocalizedText
{
    public string En;
    public string Ru;

    public string Translate()
    {
        if (Localization.Instance.Lang == Langs.Ru) 
            return Ru;

        return En;
    }
}
