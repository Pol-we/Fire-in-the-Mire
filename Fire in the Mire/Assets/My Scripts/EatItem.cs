using UnityEngine;

public class EatItem : PickItem
{
    protected override void OnPickedUp()
    {
        base.OnPickedUp();

        SaturationSystem saturationSystem = FindObjectOfType<SaturationSystem>();
        if (saturationSystem != null)
        {
            int restore = Random.Range(5, 36); // Целое значение от 5 до 35
            saturationSystem.AddSaturation(restore);

            if (TextManager.Instance != null)
            {
                TextManager.Instance.ShowMessage("Вы подобрали еду: +" + restore + "% насыщения");
                Invoke(nameof(HideText), 2f); // Скрыть через 2 секунды
            }
        }
    }

    private void HideText()
    {
        TextManager.Instance?.HideMessage();
    }
}
