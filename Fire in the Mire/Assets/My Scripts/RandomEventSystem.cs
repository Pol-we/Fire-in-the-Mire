using UnityEngine;
using UnityEngine.UI;

public class RandomEventSystem : MonoBehaviour
{
    private float timer = 0f;
    private bool eventsStarted = false;
    private float nextEventTime = 0f;
    private float eventInterval = 5f;

    public RawImage eventImage;
    [SerializeField] private FridgeInteract fridge;
    [SerializeField] private Texture heatLossTexture;
    [SerializeField] private Texture foodRewardTexture;

    private Vector3 heatLossHiddenPosition = new Vector3(-1220, -8, 0);
    private Vector3 heatLossShownPosition = new Vector3(-723, -8, 0);

    private Vector3 foodRewardHiddenPosition = new Vector3(1229, -8, 0);
    private Vector3 foodRewardShownPosition = new Vector3(728, -8, 0);

    private void Start()
    {
        if (eventImage != null)
        {
            eventImage.enabled = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!eventsStarted && timer >= 5)
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
        int eventType = Random.Range(0, 2); // 0 - негативное тепло, 1 - еда

        switch (eventType)
        {
            case 0:
                int heatLoss = Random.Range(1, 6);
                HeatSystem.Instance?.DecreaseHeat(heatLoss);
                TextManager.Instance?.ShowMessage($"The audience is unhappy! Warmth -{heatLoss}%", 2f);
                ShowEventSprite(heatLossTexture, heatLossHiddenPosition, heatLossShownPosition, 0f); // без поворота
                break;

            case 1:
                int foodCount = Random.Range(1, 3);
                fridge.AddRandomFood(foodCount);
                TextManager.Instance?.ShowMessage("The audience rewarded you with food!", 2f);
                ShowEventSprite(foodRewardTexture, foodRewardHiddenPosition, foodRewardShownPosition, 180f); // поворот по Y
                break;
        }
    }

    private void ShowEventSprite(Texture texture, Vector3 hiddenPos, Vector3 shownPos, float rotationY)
    {
        if (eventImage != null)
        {
            eventImage.texture = texture;
            eventImage.enabled = true;

            eventImage.rectTransform.anchoredPosition3D = hiddenPos;

            // Сбросить масштаб и поворот
            eventImage.rectTransform.localEulerAngles = new Vector3(0, rotationY, 0);

            LeanTween.cancel(eventImage.gameObject);

            // Появление за 0.5 секунды
            LeanTween.moveLocal(eventImage.gameObject, shownPos, 0.5f)
                .setEase(LeanTweenType.easeOutBack)
                .setOnComplete(() =>
                {
                    // Пауза 0.5 секунды
                    LeanTween.delayedCall(eventImage.gameObject, 0.2f, () =>
                    {
                        // Исчезновение обратно за 0.5 секунды
                        LeanTween.moveLocal(eventImage.gameObject, hiddenPos, 0.5f)
                            .setEase(LeanTweenType.easeInBack)
                            .setOnComplete(() =>
                            {
                                eventImage.enabled = false;
                            });
                    });
                });
        }
    }
}
