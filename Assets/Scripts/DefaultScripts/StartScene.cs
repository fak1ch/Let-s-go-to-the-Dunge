using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour, IScript
{
    public float speed;
    public Vector2 moveInput;
    private Rigidbody2D rb;
    public Vector2 MoveInput { get { return moveInput; } set { moveInput = value; } }
    public GameObject gm1;
    public GameObject gm2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput.normalized * speed;
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("FirstLevel");
        }
    }

    IEnumerator TalkWithKing()
    {
        gm1.SetActive(true);
        yield return new WaitForSeconds(10f);
        gm1.SetActive(false);
        gm2.SetActive(true);

        yield return new WaitForSeconds(4f);
        gm2.SetActive(false);
        GetComponent<SpriteRenderer>().flipX = false;
        moveInput = new Vector3(-1, 0, 0);
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene("FirstLevel");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            moveInput = new Vector3(0, 0, 0);
            StartCoroutine(TalkWithKing());
            Destroy(collision.gameObject);
        }
    }
}
