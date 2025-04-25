using UnityEngine;
using TMPro;

public class WorldTemperatureSystem : MonoBehaviour
{
    public static WorldTemperatureSystem Instance;

    public TMP_Text temperatureText; // перетащи сюда TextMeshPro из Canvas
    public float currentTemperature = -20f;
    private float changeInterval = 35f;
    private float nextChangeTime;

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
    }

    private void ChangeTemperature()
    {
        float delta = Random.Range(-10f, 10f);
        currentTemperature += delta;
        currentTemperature = Mathf.Clamp(currentTemperature, -50f, 50f); // ограничение, если нужно

        UpdateTemperatureUI();
        ApplySpeedModifier();
    }

    public void AdjustTemperature(float amount)
    {
        currentTemperature += amount;
        currentTemperature = Mathf.Clamp(currentTemperature, -50f, 50f); // сохраняем в пределах
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
                PlayerController.Instance.SetSpeedMultiplier(0.95f);
            else if (currentTemperature > 20f)
                PlayerController.Instance.SetSpeedMultiplier(1.15f);
            else
                PlayerController.Instance.SetSpeedMultiplier(1f);
        }
    }
}
