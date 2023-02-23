using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationButton : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
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
        _animator.SetBool("isClick", true);
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene("FirstLevel");
    }

    IEnumerator EventSettings()
    {
        _animator.SetBool("isClick", true);
        yield return new WaitForSeconds(0.01f);
        GameObject.Find("MainMenuScript").GetComponent<MainMenu>().SetActiveSettings();
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("isClick", false);
    }

    IEnumerator EventExit()
    {
        _animator.SetBool("isClick", true);
        yield return new WaitForSeconds(0.01f);
        Application.Quit();
    }
}
