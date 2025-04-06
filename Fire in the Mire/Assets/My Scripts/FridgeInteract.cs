using UnityEngine;

public class FridgeInteract : MonoBehaviour
{
    private bool playerInZone = false;

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            SaturationSystem saturation = FindObjectOfType<SaturationSystem>();
            if (saturation != null)
            {
                float restore = Random.Range(10f, 40f);
                saturation.IncreaseSaturation(restore);
                Debug.Log("Взаимодействие с холодильником");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInZone = false;
    }
}
