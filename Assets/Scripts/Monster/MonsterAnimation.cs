using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        InitIntensities();
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
}
