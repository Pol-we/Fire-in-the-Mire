using UnityEngine;

public class CaminZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HeatSystem heatSystem = FindObjectOfType<HeatSystem>();
            if (heatSystem != null)
            {
                heatSystem.nearCamin = true;
                Debug.Log($"Тепло у камина: {heatSystem.GetHeat():0.0}%");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HeatSystem heatSystem = FindObjectOfType<HeatSystem>();
            if (heatSystem != null)
            {
                heatSystem.nearCamin = false;
            }
        }
    }
}
