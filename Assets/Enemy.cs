using UnityEngine;
using System.Collections;
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
    [Header("Hit Flash")]
    public Color flashColor = Color.red;
    public float flashDuration = 0.05f;

    private SpriteRenderer sr;
    private Color baseColor;
    private Coroutine flashRoutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHealth = health;
        sr = GetComponentInChildren<SpriteRenderer>();
        baseColor = sr.color;
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
            if (CameraFollow.Instance != null)
                CameraFollow.Instance.Shake(0.1f, 0.15f);
            if (HitStop.Instance != null)
                HitStop.Instance.Stop(0.15f);
            Destroy(gameObject);
            return;
        }
        Flash();
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
    private void Flash()
    {
        if (flashRoutine != null) StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(FlashRoutine());
    }
    private IEnumerator FlashRoutine()
    {
        sr.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        sr.color = baseColor;
        flashRoutine = null;
    }
}