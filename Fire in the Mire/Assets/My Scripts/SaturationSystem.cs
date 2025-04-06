using UnityEngine;

public class SaturationSystem : MonoBehaviour
{
    public static SaturationSystem Instance;

    public float CurrentSaturation { get; private set; } = 100f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        // Замедление при 40% и ниже
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.SetSpeedMultiplier(CurrentSaturation <= 40f ? 0.9f : 1f);
        }

        // Завершение игры
        if (CurrentSaturation <= 0)
        {
            EndGame();
        }
    }

    public void AddSaturation(float amount)
    {
        CurrentSaturation = Mathf.Clamp(CurrentSaturation + amount, 0f, 100f);
        Debug.Log($"Насыщение увеличено на {amount}%. Текущее: {CurrentSaturation}%");
    }

    public void DecreaseSaturation(float amount)
    {
        CurrentSaturation = Mathf.Clamp(CurrentSaturation - amount, 0f, 100f);
        Debug.Log($"Насыщение уменьшено на {amount}%. Текущее: {CurrentSaturation}%");
    }

    private void EndGame()
    {
        Debug.Log("Игра окончена. Голод победил.");
        Time.timeScale = 0;
        TextManager.Instance?.ShowMessage("Вы истощены. Конец игры.");
    }
    public void IncreaseSaturation(float amount)
    {
        AddSaturation(amount);
    }

}
