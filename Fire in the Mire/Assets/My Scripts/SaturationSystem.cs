using UnityEngine;
using UnityEngine.UI;

public class SaturationSystem : MonoBehaviour
{
    public static SaturationSystem Instance;

    public float CurrentSaturation { get; set; } = 100f;

    [SerializeField] private Slider saturationSlider;

    private float timer = 0f;
    private bool decayStarted = false;
    private float decayInterval = 1f;
    private float nextDecayTime = 0f;

    [SerializeField] public float startDecayTime = 20f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateSaturationSlider();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!decayStarted && timer >= startDecayTime)
        {
            
            decayStarted = true;
            nextDecayTime = Time.time + decayInterval;
            HungryText();
        }

        if (decayStarted && Time.time >= nextDecayTime)
        {
            DecreaseSaturation(1f);
            nextDecayTime = Time.time + decayInterval;
        }

        UpdateSaturationSlider();

        if (CurrentSaturation <= 0)
        {
            EndGame();
        }

        LowSaturation();
    }

    private void UpdateSaturationSlider()
    {
        if (saturationSlider != null)
        {
            saturationSlider.value = CurrentSaturation;
        }
    }

    public void AddSaturation(float amount)
    {
        CurrentSaturation = Mathf.Clamp(CurrentSaturation + amount, 0f, 100f);
        UpdateSaturationSlider();
    }

    public void DecreaseSaturation(float amount)
    {
        CurrentSaturation = Mathf.Clamp(CurrentSaturation - amount, 0f, 100f);
        UpdateSaturationSlider();
    }

    public void IncreaseSaturation(float amount)
    {
        AddSaturation(amount);
    }

    private void EndGame()
    {
        TextManager.Instance?.ShowMessage("You are exhausted. Game over.");
        Time.timeScale = 0;
    }

    private void HungryText()
    {
        //int randText = Random.Range(0, 4);
        int randText = 0;

        switch (randText)
        {
            case 0: TextManager.Instance?.ShowMessage("I need to find something to eat...");
                break;
            case 1:
                TextManager.Instance?.ShowMessage("Eat...");
                break;
            case 2:
                TextManager.Instance?.ShowMessage("Eat. Eat. Eat.");
                break;
            case 3:
                TextManager.Instance?.ShowMessage("How hungry...");
                break;

        }
    }

    private void LowSaturation()
    {
        if ( CurrentSaturation == 50 || CurrentSaturation == 10)
        {
            HungryText();
        }
    }

    public float GetSaturation()
    {
        return CurrentSaturation;
    }

}
