using UnityEngine;

public class MonsterBehaviorReentry : IMonsterBehavior
{
    private readonly Monster _monster;

    private Vector2 _position;

    public MonsterBehaviorReentry(Monster monster)
    {
        _monster = monster;
    }

    public void Enter()
    {
        _monster.Movement.SetSpeed(SpeedTypes.Run);
        _position = CorralArea.Instance.GetRandomPosition();
    }

    public void Exit()
    {
    }

    public void Update()
    {
        _monster.Movement.Move(_position);

        if ((Vector2)_monster.transform.position == _position)
        {
            _monster.Behavior.SetBehaviorIdle();
        }

        if (!CorralArea.Instance.IsWithin(_position)) 
        {
            _position = CorralArea.Instance.GetRandomPosition();
        }
    }
}
