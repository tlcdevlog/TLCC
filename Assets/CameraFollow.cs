using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;

    public Transform target;
    public float smoothSpeed = 5f;

    private Vector3 followPos;
    private float shakeTimer;
    private float shakeDuration;
    private float shakeMagnitude;

    void Awake()
    {
        Instance = this;
        followPos = transform.position;
    }
    void LateUpdate()
    {
        if (target == null) return;
        
            Vector3 hedef = new Vector3(target.position.x, target.position.y, -10f);
            followPos = Vector3.Lerp(followPos, hedef, smoothSpeed * Time.deltaTime);
            
            Vector3 offset = Vector3.zero;
        
        if(shakeTimer > 0f)
        {
            shakeTimer -= Time.unscaledDeltaTime;
            float fade = shakeTimer / shakeDuration;
            offset = (Vector3)(Random.insideUnitCircle * shakeMagnitude * fade);
        }
        transform.position = followPos + offset;
    }
    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeTimer = duration;
        shakeMagnitude = magnitude;
    }
}
