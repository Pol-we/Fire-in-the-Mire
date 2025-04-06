using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController _controller;
    public float _speedPl = 1;
    public float _rotationSpeed = 120;

    private Vector3 rotation;

    public void Start()
    {

    }

    public void Update()
    {
        if (Time.timeScale == 1)
        {
            this.rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * _rotationSpeed * Time.deltaTime, 0);

            Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime);
            move = this.transform.TransformDirection(move);
            _controller.Move(move * _speedPl);
            this.transform.Rotate(this.rotation);
        }
    }
}