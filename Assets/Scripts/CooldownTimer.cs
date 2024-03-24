using UnityEngine;
using UnityEngine.Events;

public class CooldownTimer : MonoBehaviour
{
    private float _timer;
    private float _cooldown;

    public UnityEvent OnTicked { get; private set; } = new UnityEvent();

    public void Init(float cooldown, UnityAction action = null)
    {
        _cooldown = cooldown;
        _timer = _cooldown;

        if (action != null)
            OnTicked.AddListener(action);
    }

    public void SetCooldown(float value)
    {
        _cooldown = value;
    }

    private void Update()
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
}