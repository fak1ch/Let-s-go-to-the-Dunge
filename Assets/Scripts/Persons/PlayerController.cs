using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IScript
{
    private Joystick joystick;
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    public Vector2 MoveInput { get { return moveInput; } set { moveInput = value; } }

    private void Awake()
    {
        joystick = GameObject.FindGameObjectWithTag("JoystickMove").GetComponent<FixedJoystick>();
        if (StaticClass.typeOfDevice == StaticClass.TypeOfDevice.Phone)
        {
            joystick.gameObject.SetActive(true);
        }
        else
        {
            joystick.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("JoystickAttack").SetActive(false);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        moveInput = Vector2.zero;
        if (StaticClass.typeOfDevice == StaticClass.TypeOfDevice.PC)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        }

        moveInput.Normalize();

        moveVelocity = speed * moveInput;

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
    }
}
