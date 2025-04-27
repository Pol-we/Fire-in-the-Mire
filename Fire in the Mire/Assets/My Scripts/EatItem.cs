using UnityEngine;

public class EatItem : PickItem
{
    public AudioClip eatSound;
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

        AudioSource.PlayClipAtPoint(eatSound, transform.position);
        SaturationSystem.Instance?.AddSaturation(randomSaturation);
        TextManager.Instance?.ShowMessage($"You ate. Satiety + {randomSaturation}", 2f);

    }
}
