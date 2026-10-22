using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void LateUpdate()
    {
        if (target == null) return;
        {
            Vector3 hedef = new Vector3(target.position.x, target.position.y, -10f);
            transform.position = Vector3.Lerp(transform.position, hedef, smoothSpeed * Time.deltaTime);
        }
    }
}
