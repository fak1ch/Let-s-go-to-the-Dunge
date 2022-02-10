using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationButton : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ClickAtButtonPlay()
    {
        StartCoroutine(EventPlay());
    }

    public void ClickAtButtonSettings()
    {
        StartCoroutine(EventSettings());
    }

    public void ClickAtButtonExit()
    {
        StartCoroutine(EventExit());
    }

    IEnumerator EventPlay()
    {
        animator.SetBool("isClick", true);
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene("FirstLevel");
    }

    IEnumerator EventSettings()
    {
        animator.SetBool("isClick", true);
        yield return new WaitForSeconds(0.01f);
        GameObject.Find("MainMenuScript").GetComponent<MainMenu>().SetActiveSettings();
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isClick", false);
    }

    IEnumerator EventExit()
    {
        animator.SetBool("isClick", true);
        yield return new WaitForSeconds(0.01f);
        Application.Quit();
    }
}
