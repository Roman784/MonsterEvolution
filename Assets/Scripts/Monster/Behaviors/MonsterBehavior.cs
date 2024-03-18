public abstract class MonsterBehavior
{
    public bool IsFinished { get; protected set; }

    protected readonly Monster _monster;

    public MonsterBehavior(Monster monster)
    {
        _monster = monster;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public abstract void Update();
}
