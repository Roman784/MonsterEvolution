using UnityEngine;

[RequireComponent(typeof(Monster))]
public class MonsterMerging : MonoBehaviour
{
    [Header("Monster detection")]
    [SerializeField] private float _detectRange; // Область, в которой мы ищем монстра для объединения.
    [SerializeField] private LayerMask _detectLayerMask;

    private Monster _monster;

    private void Awake()
    {
        _monster = GetComponent<Monster>();
    }

    private void Start()
    {
        _monster.Dragging.Lowered.AddListener(Detection);
    }

    private void Detection()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _detectRange, _detectLayerMask);

        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent<Monster>(out Monster monster))
            {
                if (TryMerge(monster))
                {
                    break;
                }
            }
        }
    }

    public bool TryMerge(Monster monster)
    {
        if (!CanMerge(monster)) return false;

        monster.Destroy();
        _monster.Destroy();

        MonsterSpawner.Instance.Spawn(_monster.TypeNumber + 1, GetMergedMonsterPosition(monster));

        DataContext.Instance.RemoveСoupleMonster(monster.TypeNumber);

        return true;
    }

    private bool CanMerge(Monster monster)
    {
        return monster != _monster &&
               monster.TypeNumber == _monster.TypeNumber && 
               monster.TypeNumber + 1 <= _monster.MaxTypeNumber;
    }

    private Vector2 GetMergedMonsterPosition(Monster monster)
    {
        float x = (_monster.transform.position.x + monster.transform.position.x) / 2f;
        float y = (_monster.transform.position.y + monster.transform.position.y) / 2f;

        return new Vector2(x, y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }
}
