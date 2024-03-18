using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Monster))]
public class MonsterBehavior : MonoBehaviour
{
    private Dictionary<Type, IMonsterBehavior> _behaviorsMap;
    private IMonsterBehavior _currentBehavior;

    private Monster _monster;

    [SerializeField] private float _idleTime;

    private void Awake()
    {
        _monster = GetComponent<Monster>();
    }

    private void Start()
    {
        InitBehaviors();
        DefaultBehavior();
    }

    private void Update()
    {
        if (_currentBehavior != null)
            _currentBehavior.Update();
    }

    private void InitBehaviors()
    {
        _behaviorsMap = new Dictionary<Type, IMonsterBehavior>();

        _behaviorsMap[typeof(MonsterBehaviorIdle)] = new MonsterBehaviorIdle(_monster, _idleTime);
        _behaviorsMap[typeof(MonsterBehaviorWalking)] = new MonsterBehaviorWalking(_monster);
        _behaviorsMap[typeof(MonsterBehaviorLifting)] = new MonsterBehaviorLifting();
        _behaviorsMap[typeof(MonsterBehaviorReentry)] = new MonsterBehaviorReentry();
    }

    private void SetBehavior(IMonsterBehavior newBehavior)
    {
        if (_currentBehavior != null)
            _currentBehavior.Exit();

        _currentBehavior = newBehavior;
        _currentBehavior.Enter();
    }

    private IMonsterBehavior GetBehavior<T>() where T : IMonsterBehavior
    {
        return _behaviorsMap[typeof(T)];
    }

    private void DefaultBehavior()
    {
        SetBehaviorIdle();
    }

    public void SetBehaviorIdle()
    {
        var behavior = GetBehavior<MonsterBehaviorIdle>();
        SetBehavior(behavior);
    }

    public void SetBehaviorWalking()
    {
        var behavior = GetBehavior<MonsterBehaviorWalking>();
        SetBehavior(behavior);
    }

    public void SetBehaviorLifting()
    {
        var behavior = GetBehavior<MonsterBehaviorLifting>();
        SetBehavior(behavior);
    }

    public void SetBehaviorReentry()
    {
        var behavior = GetBehavior<MonsterBehaviorReentry>();
        SetBehavior(behavior);
    }
}
