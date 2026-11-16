using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int contactDamage = 10;
    public float speed = 2f;
    public int health = 3;

    private Transform player;
    private Rigidbody2D rb;
    private Vector2 knockbackVelocity;
    private int maxHealth;
    public float strongKnockback = 3f;
    public float weakKnockback = 1f;
    public float knockbackDuration = 0.15f;
    private float knockbackTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHealth = health;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeHit(int damage, Vector2 direction)
    {

        bool isFirstHit = health == maxHealth;

        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }
        float force = isFirstHit ? strongKnockback : weakKnockback;
        ApplyKnockback(direction, force, knockbackDuration);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(contactDamage);
        }
    }

    public void ApplyKnockback(Vector2 direction, float force, float duration)
    {
        knockbackVelocity = direction.normalized * force;
        knockbackTimer = duration;
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector2 move;
        if (knockbackTimer > 0f)
        {
            move = knockbackVelocity;
            knockbackTimer -= Time.fixedDeltaTime;
        }
        else
        {
            move = ((Vector2)player.position - rb.position).normalized * speed;
        }

        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }
}