using UnityEngine;
using UnityEngine.InputSystem;

public class PickItem : MonoBehaviour
{
    protected static PickItem _currentActiveItem;

    protected virtual void Update()
    {
        if (_currentActiveItem == this && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
        // Скрываем текст
        TextManager.Instance?.HideMessage();

        // Отключаем объект, но не удаляем
        gameObject.SetActive(false);

        if (_currentActiveItem == this)
            _currentActiveItem = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentActiveItem = this;
            TextManager.Instance?.ShowMessage("Press E to interact.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _currentActiveItem == this)
        {
            TextManager.Instance?.HideMessage();
            _currentActiveItem = null;
        }
    }
}
