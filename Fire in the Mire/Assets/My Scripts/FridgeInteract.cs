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
            TextManager.Instance?.ShowMessage($"Added {added} food to the fridge.");
        }
        else
        {
            TextManager.Instance?.ShowMessage("There's no more space for food in the fridge!");
        }
    }




    private void Update()
    {


        foodItemsCount = foodItems.Length;

        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (currentFoodIndex < foodItems.Length)
            {
                float currentSat = SaturationSystem.Instance?.CurrentSaturation ?? 0f;

                if (currentSat >= 90f)
                {
                    TextManager.Instance?.ShowMessage("I'm not hungry.");
                    return;
                }
                else
                {


                    GameObject food = foodItems[currentFoodIndex];
                    EatItem eatItem = food.GetComponent<EatItem>();
                    if (eatItem != null)
                    {
                        eatItem.EatFromFridge(); // специальный метод для холодильника
                    }
                    currentFoodIndex++;
                }
            }
            else
            {
                TextManager.Instance?.ShowMessage("The fridge is empty.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            TextManager.Instance?.ShowMessage("Press E to take food from the fridge.");
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
