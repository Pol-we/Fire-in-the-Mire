using UnityEngine;

public class HeatItem : PickItem
{
    public AudioClip heatSound;
    public int randomHeat;


    //public HeatSystem heatSystem;
    public override void Interact()
    {
        float currentSat = HeatSystem.Instance?.GetHeat() ?? 0;

        if (currentSat >= 90)
        {
            TextManager.Instance?.ShowMessage("It's warm now, I don't need it.");
            return;
        }
        else
        {
            if (_currentActiveItem == this)
                _currentActiveItem = null;

            randomHeat = Random.Range(5, 36);
            TextManager.Instance?.HideMessage();

            HeatSystem.Instance?.AddHeat(randomHeat);

            AudioSource.PlayClipAtPoint(heatSound, transform.position);

            TextManager.Instance?.ShowMessage($"You warmed up. Warmth + {randomHeat}", 2f);

            Destroy(gameObject);

        }
    }
}   
