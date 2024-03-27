using UnityEngine.Events;

public class Localization
{
    private static Localization _instance;
    public static Localization Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Localization();
            return _instance;
        }
    }

    private Langs _lang;
    public Langs Lang { get { return _lang; } }

    public UnityEvent LanguageChanged = new UnityEvent();

    private Localization()
    {
    }

    public void Init(Langs lang)
    {
        _lang = lang;

        LanguageChanged.Invoke();
    }
}
