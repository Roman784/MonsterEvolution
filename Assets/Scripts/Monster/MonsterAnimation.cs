using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[RequireComponent(typeof(Monster))]
public class MonsterAnimation : MonoBehaviour
{
    private float _intensity;
    [SerializeField] private float _height;

    [Space]

    [SerializeField] private float _idleIntensity;
    [SerializeField] private float _walkIntensity;
    [SerializeField] private float _runIntensity;

    private Dictionary<SpeedTypes, float> _intensitiesMap;

    [Space]

    [SerializeField] private Animator _coinFetchAnimator;
    private CooldownTimer _coinFetchTimer;

    private void Awake()
    {
        InitIntensities();

        _coinFetchTimer = new GameObject(GetType().ToString() + "Timer", typeof(CooldownTimer)).GetComponent<CooldownTimer>();
        _coinFetchTimer.Init(Random.Range(1.5f, 3f), FetchCoin);
    }

    private void OnDestroy()
    {
        if (_coinFetchTimer != null)
            Destroy(_coinFetchTimer.gameObject);
    }

    private void InitIntensities()
    {
        _intensitiesMap = new Dictionary<SpeedTypes, float>();

        _intensitiesMap[SpeedTypes.Idle] = _idleIntensity;
        _intensitiesMap[SpeedTypes.Walk] = _walkIntensity;
        _intensitiesMap[SpeedTypes.Run] = _runIntensity;
    }

    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        float y = 1 + Mathf.Sin(Time.time * _intensity) * _height;
        transform.localScale = new Vector2(transform.localScale.x, y);
    }

    public void SetIntensity(SpeedTypes speed)
    {
        if (!_intensitiesMap.ContainsKey(speed)) return;

        _intensity = _intensitiesMap[speed];
    }

    private void FetchCoin()
    {
        _coinFetchAnimator.SetTrigger("Fetch");
    }
}
