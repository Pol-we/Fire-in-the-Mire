using UnityEngine;

public class HeatItem : PickItem
{
    public override void Interact()
    {
        Debug.Log("+10 Heat");

        HeatSystem.Instance?.AddHeat(Random.Range(5, 36));

        gameObject.SetActive(false);

        if (_currentActiveItem == this)
            _currentActiveItem = null;
    }
}
