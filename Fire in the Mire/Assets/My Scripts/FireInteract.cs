using UnityEngine;
using UnityEngine.InputSystem;

public class FireInteract : MonoBehaviour
{
    private bool playerNear = false;
    private float gameTime = 0f;
    private float nextTick = 85f; // 60 + 25
    private float tickInterval = 25f;

    private void Update()
    {
        gameTime += Time.deltaTime;

        // Взаимодействие по кнопке E
        if (playerNear && Keyboard.current.eKey.wasPressedThisFrame)
        {
            // Дополнительные действия, если нужно
        }

        // Каждые 25 сек. после 1 мин
        if (gameTime >= nextTick)
        {
            HeatSystem.Instance?.DecreaseHeat(1);
            SaturationSystem.Instance?.DecreaseSaturation(1);

            nextTick = gameTime + tickInterval;
        }

        // Проверка завершения игры
        if (HeatSystem.Instance != null && HeatSystem.Instance.CurrentHeat <= 0 ||
            SaturationSystem.Instance != null && SaturationSystem.Instance.CurrentSaturation <= 0)
        {
            EndGame();
        }

        // Замедление при 40%
        if (PlayerController.Instance != null)
        {
            bool lowHeat = HeatSystem.Instance != null && HeatSystem.Instance.CurrentHeat <= 40;
            bool lowSat = SaturationSystem.Instance != null && SaturationSystem.Instance.CurrentSaturation <= 40;
            PlayerController.Instance.SetSpeedMultiplier((lowHeat || lowSat) ? 0.9f : 1f);
        }
    }

    private void EndGame()
    {
        Debug.Log("Game over: one of the stats reached 0%.");
        Time.timeScale = 0;
        Debug.Log("You died. All energy depleted.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            Debug.Log("You approached the fireplace.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
