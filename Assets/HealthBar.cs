using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    public void UpdateBar(int current, int max)
    {
        slider.maxValue = max;
        slider.value = current;
    }
}
