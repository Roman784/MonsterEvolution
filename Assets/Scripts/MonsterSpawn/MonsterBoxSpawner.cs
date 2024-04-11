using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBoxSpawner : MonoBehaviour
{
    public static MonsterBoxSpawner Instance {  get; private set; }

    [SerializeField] private MonsterBox _monsterBoxPrefab;

    public int InitialTypeNumber {  get; private set; }
    [Min(1)] private int _typeNumber;

    private List<MonsterBox> _spawnedBoxes = new List<MonsterBox>();

    public float InitialCooldown { get; private set; }
    public float InitialReductionStep { get; private set; }
    private float _cooldown;
    [Range(0f, 1f)] private float _timeReductionStep;
    private CooldownTimer _timer;

    [Space]

    [SerializeField] private Image _indicator;

    [Space]

    [SerializeField] private int _maxCount;

    [Space]

    [SerializeField] private GameObject _spawnEffect;

    private void Awake()
    {
        Instance = Singleton.Get<MonsterBoxSpawner>();
    }

    public void Init(int initialTypeNumber, float initialCooldown, float initialTimeReductionStep)
    {
        InitialTypeNumber = initialTypeNumber;
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
        DisposeNullableBoxes();

        if (_spawnedBoxes.Count >= _maxCount) return;

        MonsterBox spawnedBox = Instantiate(_monsterBoxPrefab);
        spawnedBox.Init(typeNumber, position);

        _spawnedBoxes.Add(spawnedBox);

        Instantiate(_spawnEffect, position + new Vector2(0f, 0.5f), Quaternion.identity);

        SoundPlayer.Instance.PlayMergeSound();
    }

    private void DisposeNullableBoxes()
    {
        for (int i = 0; i < _spawnedBoxes.Count; i++)
        {
            if (_spawnedBoxes[i] == null)
                _spawnedBoxes.RemoveAt(i);
        }
    }

    public void ReduceTime()
    {
        _timer.Time -= _timeReductionStep;

        SoundPlayer.Instance.PlayButtoClickSound();
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
        if (_timer == null) return;

        _indicator.fillAmount = _timer.Time / _cooldown;
    }

    [ContextMenu("Spawn test box")]
    private void SpawnTestBox()
    {
        Vector2 position = CorralArea.Instance.GetRandomPosition();
        Spawn(1, position);
    }
}
