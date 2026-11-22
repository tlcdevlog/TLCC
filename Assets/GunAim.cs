using UnityEngine;

public class GunAim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 direction = mousePos - transform.position;
        float deadZone = 1f;
        if (direction.magnitude < deadZone) return;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, angle);

        Vector3 scale = transform.localScale;
        bool lookingLeft = angle > 90f || angle < -90f;
        scale.y = Mathf.Abs(scale.y) * (lookingLeft ? -1 : 1f);
        transform.localScale = scale;
    }
}
