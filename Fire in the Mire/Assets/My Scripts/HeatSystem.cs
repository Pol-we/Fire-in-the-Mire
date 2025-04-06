using UnityEngine;

public class HeatSystem : MonoBehaviour
{
    public static HeatSystem Instance;

    public float CurrentHeat { get; private set; } = 100f;
    private float timer = 0f;
    private bool decayStarted = false;
    private float decayInterval = 1f;
    private float nextDecayTime = 0f;
    public bool nearCamin = false;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Начинаем убывание через 1.5 минуты
        if (!decayStarted && timer >= 90f)
        {
            decayStarted = true;
            nextDecayTime = Time.time + decayInterval;
        }

        // Каждую секунду убываем на 1%
        if (decayStarted && Time.time >= nextDecayTime)
        {
            DecreaseHeat(1);
            nextDecayTime = Time.time + decayInterval;
        }

        // Снижаем скорость игрока при 40%
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.SetSpeedMultiplier(CurrentHeat <= 40f ? 0.9f : 1f);
        }

        // Конец игры
        if (CurrentHeat <= 0)
        {
            EndGame();
        }
    }

    public void AddHeat(int amount)
    {
        CurrentHeat = Mathf.Clamp(CurrentHeat + amount, 0f, 100f);
        Debug.Log("Тепло увеличено: " + amount + "%. Сейчас: " + CurrentHeat + "%");
    }

    public void DecreaseHeat(int amount)
    {
        CurrentHeat = Mathf.Clamp(CurrentHeat - amount, 0f, 100f);
        Debug.Log($"Тепло уменьшено на {amount}%. Текущее: {CurrentHeat}%");

        if (CurrentHeat <= 40f)
        {
            PlayerController.Instance?.SetSpeedMultiplier(0.9f);
        }
        else
        {
            PlayerController.Instance?.SetSpeedMultiplier(1f);
        }

        if (CurrentHeat <= 0)
        {
            EndGame();
        }
    }


    public float GetHeat()
    {
        return CurrentHeat;
    }

    public void ChangeHeat(float addHeat)
    {
        CurrentHeat += addHeat;
    }

    private void EndGame()
    {
        Debug.Log("Игра окончена. Вы замёрзли.");
        Time.timeScale = 0;
        TextManager.Instance?.ShowMessage("Вы замёрзли. Конец игры.");
    }
}
