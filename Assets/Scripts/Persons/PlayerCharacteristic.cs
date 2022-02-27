using System;
using System.Collections;
using UnityEngine;

public class PlayerCharacteristic : MonoBehaviour, IEntity
{
    public int mana;
    public int maxMana;
    public int health;
    public int maxHealth;
    public GameObject gunPlace;
    public GameOverMenuScript gameOverMenuScript;
    public event Action<int,int> OnHealthChange;
    public event Action<int> OnAmethystChange;

    private Joystick _joystick;
    private bool _facingRight = false;
    private Camera _camera;
    private bool _immortalityOn = false;
    public int Amethists { get; set; }

    void Start()
    {
        _camera = Camera.main;
        mana = maxMana;
        if (StaticClass.typeOfDevice == StaticClass.TypeOfDevice.Phone)
        {
            _joystick = GameObject.FindGameObjectWithTag("JoystickMove").GetComponent<FixedJoystick>();
        }
    }

    void FixedUpdate()
    {
        FlipMethod();
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        Vector3 vec = gunPlace.transform.localScale;
        vec.x *= -1;

        gunPlace.transform.localScale = vec;

        Vector3 vec1 = transform.localScale;
        vec1.x *= -1;

        transform.localScale = vec1;
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
            angle = Mathf.Atan2(_joystick.Vertical, _joystick.Horizontal) * Mathf.Rad2Deg;
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

    public void TakeDamage(int damage)
    {
        if (!_immortalityOn)
        {
            health -= damage;
            OnHealthChange?.Invoke(health,maxHealth);
            if (health <= 0)
            {
                health = 0;
                StaticClass.mainScript.SetToAllEnemiesAlivePlayerOrDead(false);
                gameOverMenuScript.OpenGameOverMenu();
                StartCoroutine(PlayerSetFalse(0.1f));
            }
        }
    }


    public void TakeAmethyst(int value)
    {
        Amethists += value;
        OnAmethystChange?.Invoke(Amethists);
    }

    public void HealHp(int hpCount)
    {
        if (health + hpCount <= maxHealth)
        {
            health += hpCount;
        }
        else
        {
            health = maxHealth;
        }
        OnHealthChange?.Invoke(health, maxHealth);
    }

    public void OnImmortality(float i)
    {
        StartCoroutine(ImmortalityCoroutine(i));
    }

    private IEnumerator ImmortalityCoroutine(float i)
    {
        _immortalityOn = true;
        yield return new WaitForSeconds(i);
        _immortalityOn = false;
    }

    private IEnumerator PlayerSetFalse(float i)
    {
        yield return new WaitForSeconds(i);
        gameObject.SetActive(false);
    }
}
