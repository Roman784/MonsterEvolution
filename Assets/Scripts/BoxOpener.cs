using UnityEngine;
using UnityEngine.UI;

public class BoxOpener : MonoBehaviour
{
    public static BoxOpener Instance { get; private set; }

    private bool _isOpen;

    private float _cooldown;
    private CooldownTimer _timer;

    [Space]

    [SerializeField] private Image _indicator;

    private void Awake()
    {
        Instance = Singleton.Get<BoxOpener>();
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

        _timer = new GameObject("BoxOpenerTimer", typeof(CooldownTimer)).GetComponent<CooldownTimer>();
        _timer.Init(_cooldown, OpenBox);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OpenBox()
    {
        MonsterBox[] boxes = FindObjectsOfType<MonsterBox>();

        if (boxes.Length == 0) return;

        boxes[0].Open();
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
        BoxOpenerData data = new BoxOpenerData()
        {
            IsOpen = _isOpen,
            Cooldown = _cooldown
        };

        DataContext.Instance.SetBoxOpenerData(data);
    }

    private void UpdateIndicator()
    {
        _indicator.fillAmount = (_cooldown - _timer.Time) / _cooldown;
    }
}
