using UnityEngine;

[RequireComponent(typeof(MonsterBehaviorHandler), typeof(MonsterDragging), typeof(MonsterMerging))]
[RequireComponent(typeof(MonsterMovement))]
public class Monster : MonoBehaviour
{
    public static readonly int MaxTypeNumber = 2;

    [SerializeField] private int _typeNumber;
    public int TypeNumber { get { return _typeNumber; } }

    [SerializeField] private int _revenue;
    public int Revenue {  get { return _revenue; } }

    public MonsterBehaviorHandler BehaviorHandler { get; private set; }
    public MonsterDragging Dragging { get; private set; }
    public MonsterMerging Merging { get; private set; }
    public MonsterMovement Movement { get; private set; }

    private void Awake()
    {
        BehaviorHandler = GetComponent<MonsterBehaviorHandler>();
        Dragging = GetComponent<MonsterDragging>();
        Merging = GetComponent<MonsterMerging>();
        Movement = GetComponent<MonsterMovement>();
    }

    public void Init(Vector2 position)
    {
        transform.position = position;
    }

    public void Destroy()
    {
        MonsterRegistry.Instance.Remove(this);

        Destroy(gameObject);
    }
}
