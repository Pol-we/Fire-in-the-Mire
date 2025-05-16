using UnityEngine;

public class HeatSystem : MonoBehaviour
{
    public static HeatSystem Instance;

    public float CurrentHeat { get; set; } = 100f;
    private float timer = 0f;
    private bool decayStarted = false;
    private float decayInterval = 1f;
    private float nextDecayTime = 0f;
    public bool nearCamin = false;

    [SerializeField] public float startDecayTime = 30f;


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

        if (!decayStarted && timer >= startDecayTime)
        {
            decayStarted = true;
            nextDecayTime = Time.time + decayInterval;
            HeatText();

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

        LowHeat();
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

    private void HeatText()
    {
        //int randText = Random.Range(0, 4);
        int randText = 1;

        switch (randText)
        {
            case 0:
                TextManager.Instance?.ShowMessage("Cold...");
                break;
            case 1:
                TextManager.Instance?.ShowMessage("I need to warm up.");
                break;
            case 2:
                TextManager.Instance?.ShowMessage("I think I'm freezing.");
                break;
            case 3:
                TextManager.Instance?.ShowMessage("It's getting colder...");
                break;

        }
    }

    private void LowHeat()
    {
        if (CurrentHeat == 50 || CurrentHeat == 10)
        {
            HeatText();
        }
    }

}
