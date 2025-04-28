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

        if (!decayStarted && timer >= 60f)
        {
            decayStarted = true;
            nextDecayTime = Time.time + decayInterval;
        }

        if (decayStarted && Time.time >= nextDecayTime)
        {
            DecreaseHeat(1);
            nextDecayTime = Time.time + decayInterval;
        }


        if (CurrentHeat <= 0)
        {
            EndGame();
        }
    }

    public void AddHeat(int amount)
    {
        CurrentHeat = Mathf.Clamp(CurrentHeat + amount, 0f, 100f);
        //TextManager.Instance?.ShowMessage($"Тепло +{amount}%. Сейчас: {CurrentHeat}%");
    }

    public void DecreaseHeat(int amount)
    {
        CurrentHeat = Mathf.Clamp(CurrentHeat - amount, 0f, 100f);
        //TextManager.Instance?.ShowMessage($"Тепло -{amount}%. Сейчас: {CurrentHeat}%");

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
        CurrentHeat = Mathf.Clamp(CurrentHeat + addHeat, 0f, 100f);
    }

    private void EndGame()
    {
        TextManager.Instance?.ShowMessage("You froze. Game over.");
        Time.timeScale = 0;
    }
}
