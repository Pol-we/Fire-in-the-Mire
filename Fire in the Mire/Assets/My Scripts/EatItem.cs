using UnityEngine;

public class EatItem : PickItem
{
    public AudioClip eatSound;
    public int randomSaturation;

    public override void Interact()
    {
        float currentSat = SaturationSystem.Instance?.CurrentSaturation ?? 0f;

        if (currentSat >= 90f)
        {
            TextManager.Instance?.ShowMessage("I'm not hungry.");
            return;
        }
        else
        {
            Eat();
        }
    }

    // Используется, если еда берётся из холодильника
    public void EatFromFridge()
    {
        Eat();
        gameObject.SetActive(false);
    }

    private void Eat()
    {

        if (_currentActiveItem == this)
            _currentActiveItem = null;

        randomSaturation = Random.Range(5, 36);

        TextManager.Instance?.HideMessage();

        AudioSource.PlayClipAtPoint(eatSound, transform.position);
        SaturationSystem.Instance?.AddSaturation(randomSaturation);
        TextManager.Instance?.ShowMessage($"You ate. Satiety + {randomSaturation}", 2f);
        
        gameObject.SetActive(false);

    }
}
