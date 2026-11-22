using UnityEngine;
using TMPro;
public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float elapsed;
    bool running = true;

    void Update()
    {
        if (!running) return;
        elapsed += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsed / 60f);
        int seconds = Mathf.FloorToInt(elapsed % 60f);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    
    public void StopTimer()
    {
        running = false;
    }

    public float GetTime()
    {
        return elapsed;
    }
}
