using UnityEngine;

public class HeatItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HeatSystem heatSystem = FindObjectOfType<HeatSystem>();
            if (heatSystem != null)
            {
                heatSystem.OnHeatItemPicked();
                Destroy(gameObject);
            }
        }
    }
}
