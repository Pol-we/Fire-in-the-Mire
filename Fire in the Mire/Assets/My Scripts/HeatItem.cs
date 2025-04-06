using UnityEngine;

public class HeatItem : PickItem
{
    protected override void OnPickedUp()
    {
        base.OnPickedUp(); // Удалит объект и сбросит активный item

        HeatSystem heatSystem = FindObjectOfType<HeatSystem>();
        if (heatSystem != null)
        {
            float restoreAmount = Random.Range(5f, 35f);
            heatSystem.ChangeHeat(restoreAmount);
            Debug.Log($"Тепло увеличено на {restoreAmount}%");
        }
    }
}
