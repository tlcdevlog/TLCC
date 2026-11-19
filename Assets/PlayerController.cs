using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarý")]
    public float moveSpeed = 5f;

    [Header("Bob Animasyonu")]
    public float bobSpeed = 5f;
    public float bobAmount = 0.03f;
    
    [Header("Squash Stretch")]
    
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 mousePos;
    private SpriteRenderer sr;
    private Transform visual;
    private float baseY;
    private float bobTimer;
    public float squashAmount = 0.04f;
    private Vector3 baseScale;


    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        visual = sr.transform;
        baseY = visual.localPosition.y;
        baseScale = visual.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        sr.flipX = mousePos.x < transform.position.x;

        Bob();
        Squash();
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position +  moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }
    void Bob()
    {
        float targetY = baseY;
        
        if (moveInput.sqrMagnitude > 0.01f)
        {
            bobTimer += Time.deltaTime * bobSpeed;
            targetY = baseY + Mathf.Sin(bobTimer) * bobAmount;
        }
        else
        {
            bobTimer = 0f;
        }

        Vector3 pos = visual.localPosition;
        pos.y = Mathf.Lerp(pos.y, targetY, 0.5f);
        visual.localPosition = pos;
    }
    void Squash()
    {
        Vector3 targetScale = baseScale;
        
        if (moveInput.sqrMagnitude > 0.01f)
        {
            float s = Mathf.Sin(bobTimer) * squashAmount;
            targetScale = new Vector3(baseScale.x - s, baseScale.y + s, baseScale.z);
        }

        visual.localScale = Vector3.Lerp(visual.localScale, targetScale, 0.3f);
    }
}
