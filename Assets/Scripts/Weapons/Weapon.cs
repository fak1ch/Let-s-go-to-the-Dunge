using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public float startTimeBtwShots;
    public int manacoast;
    public GameObject bullet;
    public Transform shotPoint;
    public AudioSource audioSource;
    public bool greenZone = false;

    public bool IsDropped { get { return isDropped; } set { isDropped = value; } }

    private bool isDropped = true;
    private Joystick joystick;
    private Image image;
    private Text manaText;
    private float timeBtwShots;
    private MainScript mainScript;
    private GameObject player;
    private PlayerCharacteristic playerCharacteristic;
    private bool isFlipped = true;

    public void StartMethod()
    {
        mainScript = StaticClass.mainScript;
        player = StaticClass.player;
        playerCharacteristic = StaticClass.playerCharacteristic;
        joystick = mainScript.attackJoystick;
        manaText = GameObject.FindGameObjectWithTag("ManaText").GetComponent<Text>();
        image = GameObject.Find("Mana123").GetComponent<Image>();
        manaText.text = $"{playerCharacteristic.mana}/{playerCharacteristic.maxMana}";
    }

    protected void UpdateMethod()
    {
        RotateWeapon();
        ClickButtonAttack();

    }

    protected void FixedUpdateMethod()
    {
    }

    protected void ClickButtonAttack()
    {
        if (!isDropped)
        {
            if (timeBtwShots <= 0 && !greenZone)
            {
                if ((joystick.Direction != Vector2.zero) && playerCharacteristic.mana >= manacoast)
                {
                    AttackFromWeapon();
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    public virtual void AttackFromWeapon()
    {
        timeBtwShots = startTimeBtwShots;
        ShootPlay();
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        ChangeManaBar(-manacoast);
    }

    public void RotateWeapon()
    {
        if (!isDropped)
        {
            float angle;
            if (StaticClass.typeOfDevice == StaticClass.TypeOfDevice.PC)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 5.23f;

                Vector3 objectPos = mainScript.camera.WorldToScreenPoint(transform.position);
                mousePos.x = mousePos.x - objectPos.x;
                mousePos.y = mousePos.y - objectPos.y;

                angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            }
            else
            {
                angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            }

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (angle >= 90 && angle <= 180 || angle <= -90 && angle >= -180)
            {
                if (isFlipped)
                {
                    Vector3 vec = gameObject.transform.localScale;
                    vec.y *= -1;

                    gameObject.transform.localScale = vec;
                    isFlipped = !isFlipped;
                }
            }
            else
            if (angle >= 0 && angle < 90 || angle < 0 && angle > -90)
            {
                if (!isFlipped)
                {
                    Vector3 vec = gameObject.transform.localScale;
                    vec.y *= -1;

                    gameObject.transform.localScale = vec;
                    isFlipped = !isFlipped;
                }
            }
        }
    }

    public void ChangeManaBar(int manac)
    {
        if (playerCharacteristic.mana + manac > playerCharacteristic.maxMana)
        {
            playerCharacteristic.mana = playerCharacteristic.maxMana;
        }
        else
        {
            playerCharacteristic.mana += manac;
        }
        manaText.text = $"{playerCharacteristic.mana}/{playerCharacteristic.maxMana}";
        float k = playerCharacteristic.mana;
        float j = playerCharacteristic.maxMana;
        float z = k / j;
        image.fillAmount = z;
    }

    public void ShootPlay()
    {
        audioSource.Stop();
        audioSource.Play();
    }
}
