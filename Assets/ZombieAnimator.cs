using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    public float bobSpeed = 6f;
    public float bobAmount = 0.05f;
    public float squashAmount = 0.1f;

    private Vector3 startPos;
    private Vector3 startScale;
    private float offset;
    void Start()
    {
        startPos = transform.localPosition;
        startScale = transform.localScale;
        offset = Random.Range(0f, 10f);

    }

    // Update is called once per frame
    void Update()
    {
        float wave = Mathf.Sin( Time.time * bobSpeed + offset);
        transform.localPosition = new Vector3(startPos.x, startPos.y + wave * bobAmount, startPos.z);
        transform.localScale = new Vector3(startScale.x - wave * squashAmount, startScale.y + wave * squashAmount, startScale.z);
    }
}
