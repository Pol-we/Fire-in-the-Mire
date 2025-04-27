using UnityEngine;
using UnityEngine.UI;

public class SaturationSystem : MonoBehaviour
{
    public static SaturationSystem Instance;

    public float CurrentSaturation { get; private set; } = 100f;

    [SerializeField] private Slider saturationSlider;

    private float timer = 0f;
    private bool decayStarted = false;
    private float decayInterval = 1f;
    private float nextDecayTime = 0f;

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

        if (!decayStarted && timer >= 60f)
        {
            decayStarted = true;
            nextDecayTime = Time.time + decayInterval;
        }

        if (decayStarted && Time.time >= nextDecayTime)
        {
            DecreaseSaturation(1f);
            nextDecayTime = Time.time + decayInterval;
        }

        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.SetSpeedMultiplier(CurrentSaturation <= 40f ? 0.9f : 1f);
        }

        UpdateSaturationSlider();

        if (CurrentSaturation <= 0)
        {
            EndGame();
        }
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
}
