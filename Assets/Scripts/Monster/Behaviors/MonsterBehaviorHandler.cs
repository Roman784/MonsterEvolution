using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Monster))]
public class MonsterBehaviorHandler : MonoBehaviour
{
    private Dictionary<Type, MonsterBehavior> _behaviorsMap;
    private MonsterBehavior _currentBehavior;

    private Monster _monster;

    [Header("Idle")]
    [SerializeField] private float _idleTime;
    [SerializeField] private float _idleTimeOffset;

    private void Awake()
    {
        _monster = GetComponent<Monster>();
    }

    private void Start()
    {
        InitBehaviors();
        DefaultBehavior();

        _monster.Dragging.Lifted.AddListener(SetBehaviorLifting);
    }

    private void InitBehaviors()
    {
        _behaviorsMap = new Dictionary<Type, MonsterBehavior>();

        _behaviorsMap[typeof(MonsterBehaviorIdle)] = new MonsterBehaviorIdle(_monster, _idleTime, _idleTimeOffset);
        _behaviorsMap[typeof(MonsterBehaviorWalking)] = new MonsterBehaviorWalking(_monster);
        _behaviorsMap[typeof(MonsterBehaviorLifting)] = new MonsterBehaviorLifting(_monster);
        _behaviorsMap[typeof(MonsterBehaviorReentry)] = new MonsterBehaviorReentry(_monster);
    }

    private void Update()
    {
        if (_currentBehavior != null && !_currentBehavior.IsFinished)
        {
            _currentBehavior.Update();
        }
        else
        {
            if (_currentBehavior == GetBehavior<MonsterBehaviorIdle>())
                SetBehaviorWalking();
            else
                DefaultBehavior();
        }

        if (!CorralArea.Instance.IsWithin(transform.position) && _currentBehavior != GetBehavior<MonsterBehaviorReentry>())
        {
            SetBehaviorReentry();
        }
    }

    private void SetBehavior(MonsterBehavior newBehavior)
    {
        if (_currentBehavior != null)
            _currentBehavior.Exit();

        _currentBehavior = newBehavior;
        _currentBehavior.Enter();
    }

    private MonsterBehavior GetBehavior<T>() where T : MonsterBehavior
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
