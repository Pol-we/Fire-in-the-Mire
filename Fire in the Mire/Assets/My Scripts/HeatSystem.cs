using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeatSystem : MonoBehaviour
{
    public float heatPercent = 100f;
    private float heatDecreaseInterval = 25f;
    private float saturationDecreaseInterval = 25f;
    private float startTimeThreshold = 60f;

    private float nextDecreaseTime;
    private PlayerController playerController;
    private SaturationSystem saturationSystem;

    public bool nearCamin = false;

    private void Start()
    {
        nextDecreaseTime = Time.time + startTimeThreshold + heatDecreaseInterval;
        playerController = FindObjectOfType<PlayerController>();
        saturationSystem = FindObjectOfType<SaturationSystem>();
        StartCoroutine(HeaterRespawnRoutine());
    }

    private void Update()
    {
        if (Time.time >= nextDecreaseTime)
        {
            if (!nearCamin)
                ChangeHeat(-1f);

            if (saturationSystem != null)
                saturationSystem.DecreaseExternally(1f); // доступный внешний метод

            nextDecreaseTime = Time.time + heatDecreaseInterval;
        }

        if (heatPercent <= 40f && playerController != null)
        {
            playerController._speedPl = 0.9f;
        }

        if (heatPercent <= 0 || (saturationSystem != null && saturationSystem.GetSaturation() <= 0))
        {
            Debug.Log("Игра окончена: Один из параметров достиг 0%");
            Time.timeScale = 0;
        }
    }

    public void ChangeHeat(float amount)
    {
        heatPercent = Mathf.Clamp(heatPercent + amount, 0, 100);
    }

    // Вызов при соприкосновении с предметами (дрова, свечи и т.д.)
    public void OnHeatItemPicked()
    {
        float restore = Random.Range(5f, 35f);
        ChangeHeat(restore);
        Debug.Log($"Тепло увеличено на {restore}%");
    }

    private IEnumerator HeaterRespawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(40f);
            RespawnHeaterItems(); // здесь можно активировать/респавнить предметы
        }
    }

    private void RespawnHeaterItems()
    {
        // Здесь логика появления новых подогревающих предметов
        Debug.Log("Респавн предметов тепла");
    }

    public float GetHeat()
    {
        return heatPercent;
    }
}
