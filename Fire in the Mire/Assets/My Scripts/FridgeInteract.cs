using UnityEngine;
using UnityEngine.InputSystem;

public class FridgeInteract : MonoBehaviour
{
    public GameObject[] foodItems; // Присвой сюда 4 еды в инспекторе
    private bool playerInRange = false;
    private int currentFoodIndex = 0;

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (currentFoodIndex < foodItems.Length)
            {
                GameObject food = foodItems[currentFoodIndex];
                EatItem eatItem = food.GetComponent<EatItem>();
                if (eatItem != null)
                {
                    eatItem.EatFromFridge(); // специальный метод для холодильника
                }
                currentFoodIndex++;
            }
            else
            {
                TextManager.Instance?.ShowMessage("Холодильник пуст.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            TextManager.Instance?.ShowMessage("Нажмите E, чтобы взять еду из холодильника");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            TextManager.Instance?.HideMessage();
        }
    }
}
