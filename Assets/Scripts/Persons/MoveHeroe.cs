using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveHeroe : MonoBehaviour
{
    public enum TypeOfDevice { PC, Phone }
    private TypeOfDevice typeOfDevice;
    private Joystick joystick;
    public int mana;
    public int maxMana;
    public int health;
    public GameObject gunPlace;
    private int activeGun = 0;
    private bool allowDrop = true;
    private float timer = 0.5f;
    private float allowDropTime = 0.5f;
    public bool allowPick = true;
    private float timer1 = 0.5f;
    private float allowPickTime = 0.5f;

    private bool facingRight = false;
    public bool FacingRight { get { return facingRight; } }

    private List<GameObject> weapons = new List<GameObject>();
    public List<GameObject> Weapons { get { return weapons; } }
    public int ActiveGun { get { return activeGun; } }
    private WeaponsPanel weaponsPanel;

    private new Camera camera;

    public bool androidClickAction = false;

    private GameOverMenuScript gameOverMenuScript;
    private bool immortalityOn = false;
    // Start is called before the first frame update

    void Start()
    {
        camera = Camera.main;

        typeOfDevice = (TypeOfDevice)StaticClass.typeOfDevice;
        if (typeOfDevice == TypeOfDevice.Phone)
        {
            joystick = GameObject.FindGameObjectWithTag("JoystickMove").GetComponent<FixedJoystick>();
        }
        mana = maxMana;

        weaponsPanel = GameObject.Find("WeaponsPanel").GetComponent<WeaponsPanel>();
        gameOverMenuScript = GameObject.FindGameObjectWithTag("GameOverMenu").GetComponent<GameOverMenuScript>();
        gameOverMenuScript.StartGameOverMenu();
    }

    void FixedUpdate()
    {
        if (!allowDrop)
        {
            if (timer <= 0) 
            {
                timer = allowDropTime;
                allowDrop = true;
            }
            timer -= Time.deltaTime;
        }
        if (!allowPick)
        {
            if (timer1 <= 0)
            {
                timer1 = allowPickTime;
                allowPick = true;
            }
            timer1 -= Time.deltaTime;
        }
        DropGun();
        ChangeGun();

        FlipMethod();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (allowPick)
            {
                if (Input.GetKey(KeyCode.E) || androidClickAction)
                {
                    if (weapons.Count == 2)
                    {
                        weapons[activeGun].transform.SetParent(default);
                        //weapons[activeGun].GetComponent<PistolPrefabs>().IsDropped = true;
                        weapons.RemoveAt(activeGun);
                        weapons.Add(collision.gameObject);
                        collision.gameObject.transform.position = gunPlace.transform.position;
                        collision.gameObject.transform.SetParent(gunPlace.transform);
                        //weapons[weapons.Count - 1].GetComponent<PistolPrefabs>().IsDropped = false;
                        SelectGun(weapons.Count);
                        weapons[activeGun].GetComponent<SpriteRenderer>().sortingOrder = 3;
                        allowPick = false;
                    }
                    else
                    if (weapons.Count == 1 || weapons.Count == 0)
                    {
                        weapons.Add(collision.gameObject);
                        collision.gameObject.transform.position = gunPlace.transform.position;
                        collision.gameObject.transform.SetParent(gunPlace.transform);
                        //weapons[weapons.Count - 1].GetComponent<PistolPrefabs>().IsDropped = false;
                        SelectGun(weapons.Count);
                        weapons[activeGun].GetComponent<SpriteRenderer>().sortingOrder = 3;
                        allowPick = false;
                    }

                    weaponsPanel.ChangeSprite(weapons[activeGun].GetComponent<SpriteRenderer>().sprite);
                }
            }
        }

        if (collision.CompareTag("Portal"))
        {
            SceneManager.LoadScene("FirstLevel");
        }
    }

    private void DropGun()
    {
        if (allowDrop)
        {
            if (weapons.Count > 0)
            {
                if (weapons.Count == 2)
                {
                    //if (!weapons[activeGun].GetComponent<PistolPrefabs>().IsDropped && Input.GetKey(KeyCode.Q))
                    //{
                    //    weapons[activeGun].GetComponent<SpriteRenderer>().sortingOrder = 1;
                    //    weapons[activeGun].transform.SetParent(default);
                    //    weapons[activeGun].GetComponent<PistolPrefabs>().IsDropped = true;
                    //    weapons.RemoveAt(activeGun);
                    //    SelectGun(weapons.Count);
                    //    allowDrop = false;

                    //    weaponsPanel.ChangeSprite(weapons[activeGun].GetComponent<SpriteRenderer>().sprite);
                    //}
                }
                else
                if (weapons.Count == 1)
                {
                    //if (!weapons[activeGun].GetComponent<PistolPrefabs>().IsDropped && Input.GetKey(KeyCode.Q))
                    //{
                    //    weapons[activeGun].GetComponent<SpriteRenderer>().sortingOrder = 1;
                    //    weapons[activeGun].transform.SetParent(default);
                    //    weapons[activeGun].GetComponent<PistolPrefabs>().IsDropped = true;
                    //    weapons.RemoveAt(activeGun);

                    //    activeGun = -1;
                    //    allowDrop = false;
                    //    weaponsPanel.ChangeSprite(null);
                    //}
                }
            }
        }
        
    }

    public void SelectGun(int i)
    {
        if (i == 1)
        {
            activeGun = 0;
            weapons[0].SetActive(true);  
        }
        if (i == 2)
        {
            activeGun = 1;
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
        }
        if (i == 0)
        {
            activeGun = -1;
        }
        if (i == 10)
        {
            activeGun = 0;
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
        }
        if (i == 11)
        {
            activeGun = 1;
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
        }
    }

    private void ChangeGun()
    {
        if (weapons.Count == 2)
        {
            if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1))
            {
                SelectGun(10);
            }
            if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2))
            {
                SelectGun(11);
            }
            weaponsPanel.ChangeSprite(weapons[activeGun].GetComponent<SpriteRenderer>().sprite);
        }
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
        if (typeOfDevice == TypeOfDevice.PC)
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
                gameOverMenuScript.OpenGameOverMenu(gameObject.GetComponent<MoveHeroe>());
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

    public void PickBadPistol(GameObject pistol)
    {
        weapons.Add(pistol); 
        pistol.gameObject.transform.position = gunPlace.transform.position;
        pistol.gameObject.transform.SetParent(gunPlace.transform);
        weapons[0].GetComponent<SpriteRenderer>().sortingOrder = 3;
        activeGun = 0;
    }
}
