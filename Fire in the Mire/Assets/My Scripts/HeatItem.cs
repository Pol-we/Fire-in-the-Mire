using UnityEngine;

public class HeatItem : PickItem
{
    public int randomHeat;
    public override void Interact()
    {
        randomHeat = Random.Range(5, 36);
        TextManager.Instance?.HideMessage();

        HeatSystem.Instance?.AddHeat(randomHeat);

        TextManager.Instance?.ShowMessage($"Вы согрелись. Тепло + {randomHeat}", 2f);

        gameObject.SetActive(false);

        if (_currentActiveItem == this)
            _currentActiveItem = null;
    }
}
