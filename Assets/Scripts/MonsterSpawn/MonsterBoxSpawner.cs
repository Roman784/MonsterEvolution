using UnityEngine;
using UnityEngine.UI;

public class MonsterBoxSpawner : MonoBehaviour
{
    public static MonsterBoxSpawner Instance {  get; private set; }

    [SerializeField] private MonsterBox _monsterBoxPrefab;

    [Space]

    [Min(1)] private int _typeNumber;

    public float InitialCooldown { get; private set; }
    public float InitialReductionStep { get; private set; }
    private float _cooldown;
    [Range(0f, 1f)] private float _timeReductionStep;
    private CooldownTimer _timer;

    [Space]

    [SerializeField] private Image _indicator;

    private void Awake()
    {
        Instance = Singleton.Get<MonsterBoxSpawner>();
    }

    public void Init(int initialTypeNumber, float initialCooldown, float initialTimeReductionStep)
    {
        InitialCooldown = initialCooldown;
        InitialReductionStep = initialTimeReductionStep;

        _typeNumber = initialTypeNumber;
        _cooldown = initialCooldown;
        _timeReductionStep = initialTimeReductionStep;

        _timer = new GameObject("MonsterBoxSpawnTimer", typeof(CooldownTimer)).GetComponent<CooldownTimer>();
        _timer.Init(_cooldown, Spawn);
    }

    private void Update()
    {
        UpdateIndicator();
    }

    private void Spawn()
    {
        Vector2 position = CorralArea.Instance.GetRandomPosition();
        Spawn(_typeNumber, position);
    }

    private void Spawn(int typeNumber, Vector2 position)
    {
        MonsterBox spawnedBox = Instantiate(_monsterBoxPrefab);
        spawnedBox.Init(typeNumber, position);
    }

    public void ReduceTime()
    {
        _timer.Time -= _timeReductionStep;
    }

    public void SetTypeNumber(int value)
    {
        _typeNumber = value;
    }

    public void SetCooldown(float value)
    {
        _cooldown = value;
        _timer.SetCooldown(_cooldown);
    }

    public void SetTimeReductionStep(float value)
    {
        _timeReductionStep = value;
    }

    private void UpdateIndicator()
    {
        _indicator.fillAmount = (_cooldown - _timer.Time) / _cooldown;
    }

    [ContextMenu("Spawn test box")]
    private void SpawnTestBox()
    {
        Vector2 position = CorralArea.Instance.GetRandomPosition();
        Spawn(1, position);
    }
}
