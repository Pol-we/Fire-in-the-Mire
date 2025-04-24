using UnityEngine;

public class EatItem : PickItem
{
    public int randomSaturation;
    public override void Interact()
    {
        Eat();
    }

    // Используется, если еда берётся из холодильника
    public void EatFromFridge()
    {
        Eat();
        gameObject.SetActive(false);
    }

    private void Eat()
    {
        gameObject.SetActive(false);

        if (_currentActiveItem == this)
            _currentActiveItem = null;

        randomSaturation = Random.Range(5, 36);

        TextManager.Instance?.HideMessage();

        SaturationSystem.Instance?.AddSaturation(randomSaturation);
        TextManager.Instance?.ShowMessage($"Вы поели. Насыщение + {randomSaturation}", 2f);
    }
}
