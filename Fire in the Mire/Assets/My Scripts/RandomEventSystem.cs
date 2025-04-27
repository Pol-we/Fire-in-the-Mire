using UnityEngine;

public class RandomEventSystem : MonoBehaviour
{
    private float timer = 0f;
    private bool eventsStarted = false;
    private float nextEventTime = 0f;
    private float eventInterval = 35f;

    [SerializeField] private FridgeInteract fridge; // Ссылка на скрипт холодильника
    [SerializeField] private WorldTemperatureSystem temperatureManager; // Скрипт, где управляется температура

    private void Update()
    {
        timer += Time.deltaTime;

        if (!eventsStarted && timer >= 90f) // 1.5 минуты
        {
            eventsStarted = true;
            nextEventTime = Time.time + eventInterval;
        }

        if (eventsStarted && Time.time >= nextEventTime)
        {
            TriggerRandomEvent();
            nextEventTime = Time.time + eventInterval;
        }
    }

    private void TriggerRandomEvent()
    {
        int eventType = Random.Range(0, 3); // 0 - негативное тепло, 1 - падение температуры, 2 - еда

        switch (eventType)
        {
            case 0:
                int heatLoss = Random.Range(1, 6);
                HeatSystem.Instance?.DecreaseHeat(heatLoss);
                TextManager.Instance?.ShowMessage($"The audience is unhappy! Warmth -{heatLoss}%", 2f);
                break;

            case 1:
                float tempDrop = Random.Range(-3f, -1f);
                WorldTemperatureSystem.Instance?.AdjustTemperature(tempDrop);
                TextManager.Instance?.ShowMessage($"The audience is unhappy! Temperature {tempDrop}°C", 2f);
                break;

            case 2:
                int foodCount = Random.Range(1, 3);
                fridge.AddRandomFood(foodCount); // тут ты должен реализовать добавление еды в холодильник
                TextManager.Instance?.ShowMessage($"The audience rewarded you with food!", 2f);
                break;
        }
    }

}
