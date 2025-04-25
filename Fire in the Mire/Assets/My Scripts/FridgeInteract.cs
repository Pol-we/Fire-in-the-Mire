using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FridgeInteract : MonoBehaviour
{
    public GameObject[] foodItems; // Присвой сюда 4 еды в инспекторе
    private bool playerInRange = false;
    public int currentFoodIndex = 0;
    public int foodItemsCount;
    public GameObject randomFood;

    public void AddRandomFood(int count)
    {
        int added = 0;

        for (int i = 0; i < foodItems.Length; i++)
        {
            if (!foodItems[i].activeSelf)
            {
                foodItems[i].SetActive(true); // активируем объект еды в этом слоте
                added++;

                if (added >= count)
                    break;
            }
        }

        if (added > 0)
        {
            currentFoodIndex = 0;
            TextManager.Instance?.ShowMessage($"Добавлено {added} еды в холодильник.");
        }
        else
        {
            TextManager.Instance?.ShowMessage("В холодильнике больше нет места для еды!");
        }
    }




    private void Update()
    {
        foodItemsCount = foodItems.Length;

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
