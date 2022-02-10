using System.Collections;
using UnityEngine;

public class PlayerCharacteristic : MonoBehaviour
{
    public int mana;
    public int maxMana;
    public int health;
    public int maxHealth;
    public GameObject gunPlace;

    private Joystick joystick;
    private bool facingRight = false;
    private new Camera camera;
    private GameOverMenuScript gameOverMenuScript;
    private bool immortalityOn = false;
    // Start is called before the first frame update

    void Start()
    {
        camera = Camera.main;
        mana = maxMana;
        if (StaticClass.typeOfDevice == StaticClass.TypeOfDevice.Phone)
        {
            joystick = GameObject.FindGameObjectWithTag("JoystickMove").GetComponent<FixedJoystick>();
        }
        gameOverMenuScript = GameObject.FindGameObjectWithTag("GameOverMenu").GetComponent<GameOverMenuScript>();
        gameOverMenuScript.StartGameOverMenu();
    }

    void FixedUpdate()
    {
        FlipMethod();
    }

    private void Flip()
    {
        facingRight = !facingRight;

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

            Vector3 objectPos = camera.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
        }
        if (angle >= 90 && angle <= 180 || angle <= -90 && angle >= -180)
        {
            if (facingRight)
            {
                Flip();
            }
        }
        else
        if (angle >= 0 && angle < 90 || angle < 0 && angle > -90)
        {
            if (!facingRight)
            {
                Flip();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!immortalityOn)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                StaticClass.mainScript.SetToAllEnemiesAlivePlayerOrDead(false);
                gameOverMenuScript.gameObject.SetActive(true);
                gameOverMenuScript.OpenGameOverMenu(gameObject.GetComponent<PlayerCharacteristic>());
                StartCoroutine(PlayerSetFalse(0.1f));
            }
        }
    }

    public void OnImmortality(float i)
    {
        StartCoroutine(ImmortalityCoroutine(i));
    }

    private IEnumerator ImmortalityCoroutine(float i)
    {
        immortalityOn = true;
        yield return new WaitForSeconds(i);
        immortalityOn = false;
    }

    private IEnumerator PlayerSetFalse(float i)
    {
        yield return new WaitForSeconds(i);
        gameObject.SetActive(false);
    }
}
