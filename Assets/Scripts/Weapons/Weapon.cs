using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _startTimeBtwShots;
    [SerializeField] private int _manacoast;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private GameObject _whoseWeapon;

    private AudioSource _audioSource;
    private bool _greenZone = false;
    private Joystick _joystick;
    private float _timeBtwShots;
    private MainScript _mainScript;
    private PlayerCharacteristic _playerCharacteristic;
    private bool _isFlipped = true;

    public bool IsDropped { get; set; } = true;
    public bool GreenZone { set { _greenZone = value; } }

    protected void StartMethod()
    {
        _mainScript = FindObjectOfType<MainScript>();
        _playerCharacteristic = FindObjectOfType<PlayerCharacteristic>();
        _joystick = _mainScript.JoystickAttack;
        TryGetComponent(out _audioSource);
    }

    protected void UpdateMethod()
    {
        RotateWeapon();
        ClickButtonAttack();
    }

    private void ClickButtonAttack()
    {
        if (!IsDropped)
        {
            if (_timeBtwShots <= 0 && !_greenZone)
            {
                if (_joystick != null)
                {
                    if ((_joystick.Direction != Vector2.zero) && _playerCharacteristic.Mana >= _manacoast)
                    {
                        AttackFromWeapon();
                    }
                }
            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
            }
        }
    }

    public virtual void AttackFromWeapon()
    {
        _timeBtwShots = _startTimeBtwShots;
        ShootPlay();
        var bullet = Instantiate(_bullet, _shotPoint.position, _shotPoint.rotation);
        bullet.GetComponentInChildren<Bullet>().SetWhoseBullet(_whoseWeapon);
        if(_manacoast>0)
        _playerCharacteristic.ChangeManaBar(-_manacoast);
        _whoseWeapon.GetComponent<PlayerStatistics>().AddShot();
    }

    private void RotateWeapon()
    {
        if (!IsDropped)
        {
            float angle = 0;
            if (StaticClass.typeOfDevice == StaticClass.TypeOfDevice.PC)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 5.23f;

                Vector3 objectPos = _mainScript.GetComponent<Camera>().WorldToScreenPoint(transform.position);
                mousePos.x = mousePos.x - objectPos.x;
                mousePos.y = mousePos.y - objectPos.y;

                angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            }
            else if (_joystick != null)
            {
                angle = Mathf.Atan2(_joystick.Vertical, _joystick.Horizontal) * Mathf.Rad2Deg;
            }

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (angle >= 90 && angle <= 180 || angle <= -90 && angle >= -180)
            {
                if (_isFlipped)
                {
                    Vector3 vec = gameObject.transform.localScale;
                    vec.y *= -1;

                    gameObject.transform.localScale = vec;
                    _isFlipped = !_isFlipped;
                }
            }
            else
            if (angle >= 0 && angle < 90 || angle < 0 && angle > -90)
            {
                if (!_isFlipped)
                {
                    Vector3 vec = gameObject.transform.localScale;
                    vec.y *= -1;

                    gameObject.transform.localScale = vec;
                    _isFlipped = !_isFlipped;
                }
            }
        }
    }

    public void TakeWeapon(GameObject gunPlace)
    {
        _whoseWeapon = gunPlace.transform.root.gameObject;
        transform.position = gunPlace.transform.position;
        transform.SetParent(gunPlace.transform);
        IsDropped = false;
        GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    public void DropWeapon()
    {
        _whoseWeapon = null;
        transform.SetParent(default);
        IsDropped = true;
        GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    private void ShootPlay()
    {
        _audioSource.Stop();
        _audioSource.Play();
    }
}
