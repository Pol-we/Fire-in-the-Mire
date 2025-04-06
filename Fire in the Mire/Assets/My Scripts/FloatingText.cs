using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public string message = "Нажмите [E]"; // Текст сообщения
    public GameObject textPrefab; // Префаб текста (TextMeshPro)

    private GameObject textInstance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && textInstance == null)
        {
            ShowText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && textInstance != null)
        {
            HideText();
        }
    }

    private void ShowText()
    {
        if (textPrefab == null) return;

        // Находим Canvas в сцене
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas не найден в сцене!");
            return;
        }

        // Создаем объект текста и делаем его дочерним к Canvas
        textInstance = Instantiate(textPrefab, canvas.transform);

        // Устанавливаем текст
        textInstance.GetComponent<TMP_Text>().text = message;

             // Отладочное сообщение
        Debug.Log("Текст создан в позиции: " + textInstance.transform.position);
    }

    public void HideText()
    {
        if (textInstance != null)
        {
            Destroy(textInstance);
            textInstance = null;
        }
    }
}