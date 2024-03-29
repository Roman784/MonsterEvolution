using UnityEngine;

[RequireComponent(typeof(MonsterBehaviorHandler), typeof(MonsterDragging), typeof(MonsterMerging))]
[RequireComponent(typeof(MonsterMovement))]
public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterInfo _info;

    public int MaxTypeNumber {  get; private set; }

    [SerializeField] private int _typeNumber;
    public int TypeNumber { get { return _typeNumber; } }

    private int _revenue;
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

        _revenue = _info.CPS;
    }

    public void Init(int maxTypeNumber, Vector2 position)
    {
        MaxTypeNumber = maxTypeNumber;
        transform.position = position;
    }

    public void Destroy()
    {
        MonsterRegistry.Instance.Remove(this);

        Destroy(gameObject);
    }
}
