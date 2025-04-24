using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;

    public GameObject tutorialTextPrefab; // Префаб текста (с TMP_Text внутри)
    private GameObject currentTextInstance;
    private TMP_Text textComponent;

    [SerializeField] private float defaultDuration = 3f;
    private float hideTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (tutorialTextPrefab != null)
        {
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                currentTextInstance = Instantiate(tutorialTextPrefab, canvas.transform);
                textComponent = currentTextInstance.GetComponent<TMP_Text>();
                currentTextInstance.SetActive(false);
            }
            else
            {
                Debug.LogError("TextManager: На сцене не найден Canvas.");
            }
        }
        else
        {
            Debug.LogError("TextManager: Не присвоен prefab текста (tutorialTextPrefab).");
        }
    }

    private void Update()
    {
        if (currentTextInstance != null && currentTextInstance.activeSelf && Time.time >= hideTime)
        {
            currentTextInstance.SetActive(false);
        }
    }


    public void ShowMessage(string message, float duration = -1f)
    {
        if (textComponent == null || currentTextInstance == null) return;

        textComponent.text = message;
        currentTextInstance.SetActive(true);
        hideTime = Time.time + (duration > 0 ? duration : defaultDuration);
    }

    public void HideMessage()
    {
        if (currentTextInstance != null)
        {
            currentTextInstance.SetActive(false);
        }
    }
}
