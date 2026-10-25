using JetBrains.Annotations;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifeTime = 2f;   
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            
            Vector2 dir = other.transform.position - transform.position;
            enemy.ApplyKnockback(dir, 6f, 0.1f);
            enemy.TakeDamage(1);
            Destroy(gameObject);
        }
        
    }
}
