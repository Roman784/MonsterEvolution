public class BoxOpener : Ability
{
    public static BoxOpener Instance { get; private set; }

    private void Awake()
    {
        Instance = Singleton.Get<BoxOpener>();
    }

    private new void Update()
    {
        base.Update();
    }

    protected override void Use()
    {
        MonsterBox[] boxes = FindObjectsOfType<MonsterBox>();

        if (boxes.Length == 0) return;

        boxes[0].Open();
    }

    protected override void Save()
    {
        BoxOpenerData data = new BoxOpenerData()
        {
            IsOpen = _isOpen,
            Cooldown = _cooldown
        };

        DataContext.Instance.SetBoxOpenerData(data);
    }
}
