using UnityEngine;
using TMPro;

public class WorldTemperatureSystem : MonoBehaviour
{
    public static WorldTemperatureSystem Instance;

    public TMP_Text temperatureText;
    public float currentTemperature = -20f;
    private float changeInterval = 5f;
    private float nextChangeTime;
    private float lastTemperatureChecked;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        nextChangeTime = Time.time + changeInterval;
        UpdateTemperatureUI();
        ApplySpeedModifier();
    }

    private void Update()
    {
        if (Time.time >= nextChangeTime)
        {
            ChangeTemperature();
            nextChangeTime = Time.time + changeInterval;
        }

        if (PlayerController.Instance != null && Mathf.Abs(currentTemperature - lastTemperatureChecked) > 0.1f)
        {
            ApplySpeedModifier();
            lastTemperatureChecked = currentTemperature;
        }
    }

    private void ChangeTemperature()
    {
        float delta = Random.Range(-10f, 10f);
        currentTemperature += delta;
        currentTemperature = Mathf.Clamp(currentTemperature, -50f, 50f);

        UpdateTemperatureUI();
        ApplySpeedModifier();
    }

    public void AdjustTemperature(float amount)
    {
        currentTemperature += amount;
        currentTemperature = Mathf.Clamp(currentTemperature, -50f, 50f);
        UpdateTemperatureUI();
        ApplySpeedModifier();
    }

    private void UpdateTemperatureUI()
    {
        if (temperatureText != null)
        {
            temperatureText.text = $"{currentTemperature:F1}°C";
        }
    }

    private void ApplySpeedModifier()
    {
        if (PlayerController.Instance != null)
        {
            if (currentTemperature < -30f)
            {
                PlayerController.Instance._speedPl = 1.6f; // 2 * 0.8
                Debug.Log("[WorldTemperatureSystem] Температура ниже -30°C. Установлена скорость 0.8x.");
            }
            else if (currentTemperature > 20f)
            {
                PlayerController.Instance._speedPl = 2.4f; // 2 * 1.2
                Debug.Log("[WorldTemperatureSystem] Температура выше 20°C. Установлена скорость 1.2x.");
            }
            else
            {
                PlayerController.Instance._speedPl = 2.0f; // нормальная скорость
                Debug.Log("[WorldTemperatureSystem] Нормальная температура. Скорость 1x.");
            }
        }
    }
}
