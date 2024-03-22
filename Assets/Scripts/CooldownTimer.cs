using UnityEngine;
using UnityEngine.Events;

public class CooldownTimer
{
    private float _timer;
    private float _cooldown;

    public UnityEvent OnTicked = new UnityEvent();

    public CooldownTimer(float cooldown, UnityAction action = null)
    {
        _cooldown = cooldown;
        _timer = _cooldown;

        if (action != null)
            OnTicked.AddListener(action);
    }

    public void Update()
    {
        if (_timer <= 0)
        {
            _timer = _cooldown;

            OnTicked.Invoke();
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    public void SetCooldown(float value)
    {
        _cooldown = value;
    }
}