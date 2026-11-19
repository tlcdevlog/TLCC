using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.25f;

    [Header("Recoil")]
    public Transform gun;
    public float recoilAmount = 0.15f;
    public float recoilRecovery = 15f;
    private float nextFireTime;
    private Camera cam;
    private Vector3 gunBasePos;
    void Start()
    {
        cam = Camera.main;
        gunBasePos = gun.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) 
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
        gun.localPosition = Vector3.Lerp(gun.localPosition, gunBasePos, Time.deltaTime * recoilRecovery);
    }
    void Shoot()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));

        gun.localPosition = gunBasePos - new Vector3(recoilAmount, 0f, 0f);
    }
}
