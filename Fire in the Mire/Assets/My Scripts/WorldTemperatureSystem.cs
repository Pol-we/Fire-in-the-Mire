using UnityEngine;
using TMPro;

public class WorldTemperatureSystem : MonoBehaviour
{
    public static WorldTemperatureSystem Instance;

    public TMP_Text temperatureText; // перетащи сюда TextMeshPro из Canvas
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
            if (currentTemperature < -29f)
                PlayerController.Instance.SetSpeedMultiplier(0.15f);
            else if (currentTemperature > 19f)
                PlayerController.Instance.SetSpeedMultiplier(88f);
            else
                PlayerController.Instance.SetSpeedMultiplier(1f);
        }
    }
}
