using UnityEngine.InputSystem;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    private static PickItem _currentActiveItem;
    private FloatingText floatingText;

    private void Start()
    {
        // Получаем ссылку на компонент FloatingText, если он есть на объекте
        floatingText = GetComponent<FloatingText>();
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && _currentActiveItem != null)
        {
            // Скрываем текст перед удалением
            if (_currentActiveItem.floatingText != null)
            {
                _currentActiveItem.floatingText.HideText();
            }

            Destroy(_currentActiveItem.gameObject);
            _currentActiveItem = null;
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
}
