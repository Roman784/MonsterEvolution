using UnityEngine;

public class MonsterBehaviorIdle : MonsterBehavior
{
    private readonly float _time; // Время простоя.
    private readonly float _timeOffset;
    private float _timer;

    public MonsterBehaviorIdle(Monster monster, float time, float timeOffset) : base(monster)
    {
        _time = time;
        _timeOffset = timeOffset;
    }

    public override void Enter()
    {
        IsFinished = false;

        _timer = Random.Range(_time - _timeOffset, _time + _timeOffset);
    }

    public override void Update()
    {
        if (_timer <= 0)
        {
            _monster.BehaviorHandler.SetBehaviorWalking();

            IsFinished = true;
        }

        _timer -= Time.deltaTime;
    }
}
