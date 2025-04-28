using UnityEngine;
using UnityEngine.UI;

public class RandomEventSystem : MonoBehaviour
{
    private float timer = 0f;
    private bool eventsStarted = false;
    private float nextEventTime = 0f;
    private float eventInterval = 5f;

    [SerializeField] private RectTransform eventImageTransform; // !!! RectTransform вместо RawImage
    [SerializeField] private FridgeInteract fridge;

    private Vector2 hiddenPosition = new Vector2(0, -200f); // Скрытая снизу
    private Vector2 visiblePosition = new Vector2(0, 100f); // Видимая по центру

    private void Start()
    {
        eventImageTransform.anchoredPosition = hiddenPosition;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!eventsStarted && timer >= 5f)
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
        int eventType = Random.Range(0, 2); // 0 - потеря тепла, 1 - еда

        ShowEventImage(); // Показать с анимацией

        switch (eventType)
        {
            case 0:
                int heatLoss = Random.Range(1, 6);
                HeatSystem.Instance?.DecreaseHeat(heatLoss);
                TextManager.Instance?.ShowMessage($"The audience is unhappy! Warmth -{heatLoss}%", 2f);
                break;

            case 1:
                int foodCount = Random.Range(1, 3);
                fridge.AddRandomFood(foodCount);
                TextManager.Instance?.ShowMessage("The audience rewarded you with food!", 2f);
                break;
        }
    }

    private void ShowEventImage()
    {
        if (eventImageTransform != null)
        {
            // Появление с отскоком
            eventImageTransform.gameObject.SetActive(true);
            LeanTween.move(eventImageTransform, visiblePosition, 0.6f).setEase(LeanTweenType.easeOutBounce);

            // Через 2.5 секунды спрятать
            CancelInvoke(nameof(HideEventImage));
            Invoke(nameof(HideEventImage), 2.5f);
        }
    }

    private void HideEventImage()
    {
        if (eventImageTransform != null)
        {
            // Уход вверх с отскоком
            LeanTween.move(eventImageTransform, new Vector2(0, 500f), 0.6f).setEase(LeanTweenType.easeInBounce)
                .setOnComplete(() => {
                    eventImageTransform.gameObject.SetActive(false);
                    eventImageTransform.anchoredPosition = hiddenPosition; // Вернуть вниз для следующего раза
                });
        }
    }
}
