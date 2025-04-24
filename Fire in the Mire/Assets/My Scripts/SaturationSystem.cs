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
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.SetSpeedMultiplier(CurrentSaturation <= 40f ? 0.9f : 1f);
        }

        if (CurrentSaturation <= 0)
        {
            EndGame();
        }
    }

    public void AddSaturation(float amount)
    {
        CurrentSaturation = Mathf.Clamp(CurrentSaturation + amount, 0f, 100f);
        TextManager.Instance?.ShowMessage($"Насыщение +{amount}%. Сейчас: {CurrentSaturation}%");
    }

    public void DecreaseSaturation(float amount)
    {
        CurrentSaturation = Mathf.Clamp(CurrentSaturation - amount, 0f, 100f);
        TextManager.Instance?.ShowMessage($"Насыщение -{amount}%. Сейчас: {CurrentSaturation}%");
    }

    private void EndGame()
    {
        TextManager.Instance?.ShowMessage("Вы истощены. Конец игры.");
        Time.timeScale = 0;
    }

    public void IncreaseSaturation(float amount)
    {
        AddSaturation(amount);
    }
}
