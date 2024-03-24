using UnityEngine;
using UnityEngine.Events;

public class CooldownTimer : MonoBehaviour
{
    public float Time;

    private float _cooldown;

    public UnityEvent OnTicked { get; private set; } = new UnityEvent();

    public void Init(float cooldown, UnityAction action = null)
    {
        _cooldown = cooldown;
        Time = _cooldown;

        if (action != null)
            OnTicked.AddListener(action);
    }

    private void Update()
    {
        if (Time <= 0)
        {
            Time = _cooldown;

            OnTicked.Invoke();
        }
        else
        {
            Time -= UnityEngine.Time.deltaTime;
        }
    }

    public void SetCooldown(float value)
    {
        _cooldown = value;
    }
}