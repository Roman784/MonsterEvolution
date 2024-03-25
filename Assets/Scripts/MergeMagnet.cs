using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MergeMagnet : MonoBehaviour
{
    public static MergeMagnet Instance { get; private set; }

    private bool _isOpen;

    private float _cooldown;
    private CooldownTimer _timer;

    [Space]

    [SerializeField] private float _moveSpeed;

    [Space]

    [SerializeField] private Image _indicator;

    private void Awake()
    {
        Instance = Singleton.Get<MergeMagnet>();
    }

    public void Init(bool isOpen, float cooldown)
    {
        _isOpen = isOpen;
        _cooldown = cooldown;

        if (isOpen)
            Enable();
        else
            Disable();
    }

    private void Update()
    {
        UpdateIndicator();
    }

    private void Enable()
    {
        gameObject.SetActive(true);

        _timer = new GameObject("MergeMagnetTimer", typeof(CooldownTimer)).GetComponent<CooldownTimer>();
        _timer.Init(_cooldown, Merge);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void Merge()
    {
        Monster[] couple = GetCouple();

        if (couple == null) return;

        StopAllCoroutines();
        StartCoroutine(PushMonsters(couple));
    }

    private Monster[] GetCouple()
    {
        List<Monster> monsters = MonsterRegistry.Instance.Get().ToList();
        Monster[] couple = new Monster[2];

        for (int i = 0; i < monsters.Count; i++)
        {
            Monster first = monsters[i];

            if (first.Dragging.IsLifted || first.TypeNumber >= first.MaxTypeNumber)
                continue;

            for (int j = i + 1; j < monsters.Count; j++)
            {
                Monster second = monsters[j];

                if (first.TypeNumber == second.TypeNumber && !second.Dragging.IsLifted && first != second)
                {
                    couple[0] = first;
                    couple[1] = second;

                    return couple;
                }
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

        couple[0].Merging.TryMerge(couple[1]);
    }

    private bool IsEqualPositions(Vector2 position1, Vector2 position2, float spread)
    {
        return position1.x - spread < position2.x && position1.x + spread > position2.x &&
               position1.y - spread < position2.y && position1.y + spread > position2.y;
    }

    public void Open()
    {
        _isOpen = true;
        Enable();

        Save();
    }

    public void SetCooldown(float value)
    {
        _cooldown = value;
        _timer.SetCooldown(value);

        Save();
    }

    private void Save()
    {
        MergeMagnetData data = new MergeMagnetData()
        {
            IsOpen = _isOpen,
            Cooldown = _cooldown
        };

        DataContext.Instance.SetMergeMagnedData(data);
    }

    private void UpdateIndicator()
    {
        _indicator.fillAmount = (_cooldown - _timer.Time) / _cooldown;
    }
}
