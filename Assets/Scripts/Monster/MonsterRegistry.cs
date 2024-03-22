using System.Collections.Generic;
using UnityEngine.Events;

public class MonsterRegistry
{
    private static MonsterRegistry _instance;
    public static MonsterRegistry Instance
    {
        get
        {
            if (_instance == null)
                _instance = new MonsterRegistry();
            return _instance;
        }
    }

    private HashSet<Monster> _monsters = new HashSet<Monster>();

    public UnityEvent OnChanged = new UnityEvent();

    private MonsterRegistry()
    {
    }

    public IEnumerable<Monster> Get()
    {
        return _monsters;
    }

    public void Add(Monster monster)
    {
        _monsters.Add(monster);

        OnChanged.Invoke();
    }

    public void Remove(Monster monster)
    {
        _monsters.Remove(monster);

        OnChanged.Invoke();
    }
}
