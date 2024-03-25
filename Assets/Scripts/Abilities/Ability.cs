using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    protected bool _isOpen;

    protected float _cooldown;
    protected CooldownTimer _timer;

    [Space]

    [SerializeField] private Image _indicator;

    public void Init(bool isOpen, float cooldown)
    {
        _isOpen = isOpen;
        _cooldown = cooldown;

        if (isOpen)
            Enable();
        else
            Disable();
    }

    protected void Update()
    {
        UpdateIndicator();
    }

    private void Enable()
    {
        gameObject.SetActive(true);

        _timer = new GameObject(GetType().ToString() + "Timer", typeof(CooldownTimer)).GetComponent<CooldownTimer>();
        _timer.Init(_cooldown, Use);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    protected abstract void Use();

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

    protected abstract void Save();

    private void UpdateIndicator()
    {
        _indicator.fillAmount = (_cooldown - _timer.Time) / _cooldown;
    }
}
