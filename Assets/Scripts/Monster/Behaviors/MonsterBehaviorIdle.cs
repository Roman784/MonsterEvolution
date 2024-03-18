using UnityEngine;

public class MonsterBehaviorIdle : IMonsterBehavior
{
    private readonly Monster _monster;

    // Время простоя.
    private readonly float _time;
    private float _timer;

    public MonsterBehaviorIdle(Monster monster, float time)
    {
        _monster = monster;
        _time = time;
    }

    public void Enter()
    {
        _timer = Random.Range(_time - _time / 2f, _time + _time / 2f);
    }

    public void Exit()
    {
    }

    public void Update()
    {
        if (_timer <= 0)
        {
            _monster.Behavior.SetBehaviorWalking();
        }

        _timer -= Time.deltaTime;
    }
}
