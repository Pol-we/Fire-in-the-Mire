using UnityEngine;
using UnityEngine.UI;

public class SaturationSystem : MonoBehaviour
{
    public Slider saturationSlider;
    public float startDelay = 90f; // 1.5 минуты = 90 секунд
    public float decreaseInterval = 1f; // интервал уменьшения (сек)
    public float decreaseAmount = 1f;   // сколько убавляется за раз

    private float timer;
    private float nextDecreaseTime;
    private bool decreasing = false;

    private void Start()
    {
        saturationSlider.value = saturationSlider.maxValue;
        timer = 0f;
        nextDecreaseTime = startDelay + decreaseInterval;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!decreasing && timer >= startDelay)
        {
            decreasing = true;
        }

        if (decreasing && timer >= nextDecreaseTime)
        {
            DecreaseSaturation();
            nextDecreaseTime += decreaseInterval;
        }
    }

    private void DecreaseSaturation()
    {
        saturationSlider.value = Mathf.Max(0, saturationSlider.value - decreaseAmount);

        // Здесь можно добавить действия при нуле
        if (saturationSlider.value <= 0)
        {
            Debug.Log("Сытость на нуле!");
            // Например, смерть игрока или снижение скорости
        }
    }
}
