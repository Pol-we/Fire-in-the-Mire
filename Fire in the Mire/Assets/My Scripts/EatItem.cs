using UnityEngine;

public class EatItem : PickItem
{
    public override void Interact()
    {
        Debug.Log("+10 Eat");

        SaturationSystem.Instance?.AddSaturation(Random.Range(5, 36));

        gameObject.SetActive(false);

        if (_currentActiveItem == this)
            _currentActiveItem = null;
    }
}
