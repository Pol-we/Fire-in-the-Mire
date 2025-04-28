using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public CharacterController _controller;
    public float _speedPl = 2f;
    public float _rotationSpeed = 160f;
    private Vector3 rotation;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * _rotationSpeed * Time.deltaTime, 0);

            Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
            move = transform.TransformDirection(move);
            _controller.Move(move * _speedPl * Time.deltaTime);

            transform.Rotate(rotation);
        }
    }
}
