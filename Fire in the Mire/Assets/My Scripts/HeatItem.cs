using UnityEngine;

public class HeatItem : PickItem
{
    public AudioClip heatSound;
    public int randomHeat;
    public override void Interact()
    {
        randomHeat = Random.Range(5, 36);
        TextManager.Instance?.HideMessage();

        HeatSystem.Instance?.AddHeat(randomHeat);

        AudioSource.PlayClipAtPoint(heatSound, transform.position);

        TextManager.Instance?.ShowMessage($"You warmed up. Warmth + {randomHeat}", 2f);

        Destroy(gameObject);

        if (_currentActiveItem == this)
            _currentActiveItem = null;
    }
}
