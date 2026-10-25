using UnityEngine;
using UnityEngine.Events;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float invincibilityTime = 0.5f;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;
    public UnityEvent<int, int> onHealthChanged;
    public UnityEvent onDeath;
    private float lastHitTime = -999f;
    private bool isDead;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }
    private void Start()
    {
        onHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }
    public void TakeDamage(int amount)
    {
        if (isDead) return;
        if (Time.time < lastHitTime + invincibilityTime) return;
        lastHitTime = Time.time;
        CurrentHealth = Mathf.Max(CurrentHealth -  amount, 0);
        onHealthChanged?.Invoke(CurrentHealth, maxHealth);
        if (CurrentHealth == 0) Die();
    }
    private void Die()
    {
        isDead = true;
        onDeath?.Invoke();
        gameObject.SetActive(false); 
    }
}
