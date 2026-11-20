using UnityEngine;
using System.Collections;

public class HitStop : MonoBehaviour
{
    public static HitStop Instance;
    private Coroutine routine;

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    public void Stop(float duration)
    {
        if (routine != null) return;
        routine = StartCoroutine(StopRoutine(duration));
    }
    private IEnumerator StopRoutine(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        routine = null;
    }
}
