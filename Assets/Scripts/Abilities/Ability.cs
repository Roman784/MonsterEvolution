using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    public float InitialCooldown { get; private set; }
    protected float _cooldown;
    protected CooldownTimer _timer;

    [Space]

    [SerializeField] private Image _indicator;

    public void Init(float initialCooldown)
    {
        InitialCooldown = initialCooldown;
        _cooldown = initialCooldown;

        _timer = new GameObject(GetType().ToString() + "Timer", typeof(CooldownTimer)).GetComponent<CooldownTimer>();
        _timer.Init(_cooldown, Use);

        Disable();
    }

    protected void Update()
    {
        UpdateIndicator();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _timer.gameObject.SetActive(true);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
        _timer.gameObject.SetActive(false);
    }

    protected abstract void Use();

    public void SetCooldown(float value)
    {
        _cooldown = value;
        _timer.SetCooldown(value);
    }

    private void UpdateIndicator()
    {
        if (_timer == null) return;

        _indicator.fillAmount = (_cooldown - _timer.Time) / _cooldown;
    }
}
