using UnityEngine;

public class MonsterBehaviorWalking : IMonsterBehavior
{
    private readonly Monster _monster;

    private Vector2 _position;

    public MonsterBehaviorWalking(Monster monster)
    {
        _monster = monster;
    }

    public void Enter()
    {
        _monster.Movement.SetSpeed(SpeedTypes.Walk);
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
    }
}
