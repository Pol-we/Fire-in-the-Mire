using UnityEngine;
using UnityEngine.InputSystem;

public class PickItem : MonoBehaviour
{
    private static PickItem _currentActiveItem;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && _currentActiveItem != null)
        {
            SaturationSystem.Instance?.AddSaturation(Random.Range(5, 36));
            TextManager.Instance?.ShowMessage("Вы поели. Насыщение +...");
            TextManager.Instance?.HideMessage(); // <-- скрываем текст сразу
            Destroy(gameObject); // если нужно удалить объект
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentActiveItem = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _currentActiveItem == this)
        {
            _currentActiveItem = null;
        }
    }

    // Позволяет потомкам переопределять поведение
    protected virtual void OnPickedUp()
    {
        Destroy(gameObject);
        _currentActiveItem = null;
    }
}
