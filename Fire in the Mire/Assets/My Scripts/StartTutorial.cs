using System.Collections;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(PlayIntroMessages());
    }

    IEnumerator PlayIntroMessages()
    {
        yield return new WaitForSeconds(3f);

        TextManager.Instance?.ShowMessage("Where am I?", 3f);
        yield return new WaitForSeconds(3f);

        TextManager.Instance?.ShowMessage("I don't recognize this place...", 3f);
        yield return new WaitForSeconds(3f);

        TextManager.Instance?.ShowMessage("I need to look around...", 3f);
        yield return new WaitForSeconds(3f);

        TextManager.Instance?.HideMessage();
    }
}
