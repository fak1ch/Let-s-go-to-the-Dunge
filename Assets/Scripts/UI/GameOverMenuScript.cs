using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuScript : MonoBehaviour
{
    private Image[] imageChildren;
    private MoveHeroe moveHeroe;

    public void RestartScene()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    public void CloseGameOverMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RespawnPlayerAfterAds()
    {
        StaticClass.mainScript.SetToAllEnemiesAlivePlayerOrDead(true);
        CloseGameOverMenuCoroutineAfterAds();
    }

    public void OpenGameOverMenu(MoveHeroe mh)
    {
        this.moveHeroe = mh;
        StartCoroutine(OpenGameOverMenuCoroutine());
    }

    IEnumerator OpenGameOverMenuCoroutine()
    {
        Vector3 vec = gameObject.GetComponent<RectTransform>().position;
        vec.z = 0;
        gameObject.GetComponent<RectTransform>().position = vec;
        Color color = gameObject.GetComponent<Image>().color;

        for (float i=0f; i < 1; i += 0.05f)
        {
            color.a += i;
            for(int k =0;k< imageChildren.Length; k++)
            {
                imageChildren[k].color = color;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    private void CloseGameOverMenuCoroutineAfterAds()
    {
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 0;
        for (int k = 0; k < imageChildren.Length; k++)
        {
            imageChildren[k].color = color;
        }

        Vector3 vec = gameObject.GetComponent<RectTransform>().position;
        vec.z = -100;
        gameObject.GetComponent<RectTransform>().position = vec;

        moveHeroe.gameObject.SetActive(true);
        moveHeroe.health = moveHeroe.GetComponent<HealthBar>().numberOfLives;
        moveHeroe.OnImmortality(2);

        gameObject.SetActive(false);
    }

    public void StartGameOverMenu()
    {
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 0f;
        gameObject.GetComponent<Image>().color = color;

        imageChildren = gameObject.transform.GetComponentsInChildren<Image>();
        
        for(int i = 0; i < imageChildren.Length; i++)
        {
            imageChildren[i].color = color;
        }

        gameObject.SetActive(false);
    }
}
