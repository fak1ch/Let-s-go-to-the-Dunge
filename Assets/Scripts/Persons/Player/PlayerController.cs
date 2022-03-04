using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _gunPlace;
    [SerializeField] private float _speed = 250;
    [SerializeField] private bool _movePc = false;

    private Joystick _joystickMove;
    private Joystick _joystickAttack;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Vector2 _moveVelocity;
    private Camera _camera;
    private bool _facingRight = false;

    public Vector2 MoveInput { get { return _moveInput; } }

    private void Awake()
    {
        _camera = Camera.main;
        if (StaticClass.typeOfDevice == StaticClass.TypeOfDevice.Phone)
        {
            _joystickMove = GameObject.FindGameObjectWithTag("JoystickMove").GetComponent<FixedJoystick>();
            _joystickAttack = GameObject.FindGameObjectWithTag("JoystickAttack").GetComponent<FixedJoystick>();
        }
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveInput = Vector2.zero;
        if (StaticClass.typeOfDevice == StaticClass.TypeOfDevice.PC || _movePc)
        {
            _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            _moveInput = new Vector2(_joystickMove.Horizontal, _joystickMove.Vertical);
        }

        _moveInput.Normalize();
        _moveVelocity = _speed * _moveInput;

        FlipMethod();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveVelocity * Time.fixedDeltaTime);
    }

    private void FlipMethod()
    {
        float angle;
        if (StaticClass.typeOfDevice == StaticClass.TypeOfDevice.PC)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;

            Vector3 objectPos = _camera.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(_joystickMove.Vertical, _joystickMove.Horizontal) * Mathf.Rad2Deg;
        }
        if (angle >= 90 && angle <= 180 || angle <= -90 && angle >= -180)
        {
            if (_facingRight)
            {
                Flip();
            }
        }
        else
        if (angle >= 0 && angle < 90 || angle < 0 && angle > -90)
        {
            if (!_facingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        Vector3 vec = _gunPlace.transform.localScale;
        vec.x *= -1;

        _gunPlace.transform.localScale = vec;

        Vector3 vec1 = transform.localScale;
        vec1.x *= -1;

        transform.localScale = vec1;
    }
}
