using UnityEngine;
using UnityEngine.UI;

public class MonsterBoxSpawner : MonoBehaviour
{
    public static MonsterBoxSpawner Instance {  get; private set; }

    [SerializeField] private MonsterBox _monsterBoxPrefab;

    [Space]

    [Min(1)] private int _typeNumber;

    [Space]

    private float _cooldown;
    [Range(0f, 1f)] private float _timeReductionStep;
    private CooldownTimer _timer;

    [Space]

    [SerializeField] private Image _indicator;

    private void Awake()
    {
        Instance = Singleton.Get<MonsterBoxSpawner>();
    }

    public void Init(int typeNumber, float cooldown, float timeReductionStep)
    {
        _typeNumber = typeNumber;
        _cooldown = cooldown;
        _timeReductionStep = timeReductionStep;

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

        Save();
    }

    public void SetCooldown(float value)
    {
        _cooldown = value;
        _timer.SetCooldown(_cooldown);

        Save();
    }

    public void SetTimeReductionStep(float value)
    {
        _timeReductionStep = value;

        Save();
    }

    private void Save()
    {
        MonsterSpawnerData data = new MonsterSpawnerData()
        {
            TypeNumber = _typeNumber,
            Cooldown = _cooldown,
            TimeReductionStep = _timeReductionStep,
        };

        DataContext.Instance.SetMonsterSpawnerData(data);
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
