public class BoxOpener : Ability
{
    public static BoxOpener Instance { get; private set; }

    private int _countAtTime;

    private void Awake()
    {
        Instance = Singleton.Get<BoxOpener>();
    }

    public void Init(float initialCooldown, int initialCountAtTime)
    {
        base.Init(initialCooldown);

        _countAtTime = initialCountAtTime;
    }

    private new void Update()
    {
        base.Update();
    }

    protected override void Use()
    {
        MonsterBox[] boxes = FindObjectsOfType<MonsterBox>();

        for (int i = 0; i < _countAtTime; i++)
        {
            if (i >= boxes.Length) return;

            boxes[i].Open();
        }
    }

    public void SetCountAtTime(int value)
    {
        _countAtTime = value;
    }
}
