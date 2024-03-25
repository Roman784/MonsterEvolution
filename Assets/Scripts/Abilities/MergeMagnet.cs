using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MergeMagnet : Ability
{
    public static MergeMagnet Instance { get; private set; }

    public int _coupleCountAtTime;

    private List<Monster> _selectedMonsters = new List<Monster>();

    [Space]

    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        Instance = Singleton.Get<MergeMagnet>();
    }

    public void Init(float initialCooldown, int initialCoupleCount)
    {
        base.Init(initialCooldown);

        _coupleCountAtTime = initialCoupleCount;
    }

    private new void Update()
    {
        base.Update();
    }

    protected override void Use()
    {
        for (int i = 0; i < _coupleCountAtTime; i++)
        {
            Monster[] couple = GetCouple();

            if (couple == null) break;

            StartCoroutine(PushMonsters(couple));
        }
    }

    private Monster[] GetCouple()
    {
        List<Monster> monsters = MonsterRegistry.Instance.Get().ToList();
        Monster[] couple = new Monster[2];

        for (int i = 0; i < monsters.Count; i++)
        {
            Monster first = monsters[i];

            if (first.Dragging.IsLifted || first.TypeNumber >= first.MaxTypeNumber || _selectedMonsters.Contains(first))
                continue;

            for (int j = i + 1; j < monsters.Count; j++)
            {
                Monster second = monsters[j];

                if (first.TypeNumber != second.TypeNumber || second.Dragging.IsLifted || first == second || _selectedMonsters.Contains(second))
                    continue;

                couple[0] = first;
                couple[1] = second;

                _selectedMonsters.Add(couple[0]);
                _selectedMonsters.Add(couple[1]);

                return couple;

            }
        }

        return null;
    }

    private IEnumerator PushMonsters(Monster[] couple)
    {
        Vector2 position = (couple[0].transform.position + couple[1].transform.position) / 2f;

        while (!IsEqualPositions(couple[0].transform.position, couple[1].transform.position, 0.1f))
        {
            couple[0].transform.position = Vector2.MoveTowards(couple[0].transform.position, position, _moveSpeed * Time.deltaTime);
            couple[1].transform.position = Vector2.MoveTowards(couple[1].transform.position, position, _moveSpeed * Time.deltaTime);

            yield return null;
        }

        _selectedMonsters.Remove(couple[0]);
        _selectedMonsters.Remove(couple[1]);

        couple[0].Merging.TryMerge(couple[1]);
    }

    private bool IsEqualPositions(Vector2 position1, Vector2 position2, float spread)
    {
        return position1.x - spread < position2.x && position1.x + spread > position2.x &&
               position1.y - spread < position2.y && position1.y + spread > position2.y;
    }

    public void SetCoupleCountAtTime(int value)
    {
        _coupleCountAtTime = value;
    }
}
