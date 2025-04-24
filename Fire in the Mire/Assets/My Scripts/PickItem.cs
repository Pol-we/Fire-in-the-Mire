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
        Debug.Log("+10 Eat");

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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _currentActiveItem == this)
        {
            _currentActiveItem = null;
        }
    }
}
