using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuScript : MonoBehaviour
{
    private Image[] imageChildren;

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

    public void OpenGameOverMenu()
    {
        StartCoroutine(OpenGameOverMenuCoroutine());
    }

    IEnumerator OpenGameOverMenuCoroutine()
    {
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 0f;
        gameObject.GetComponent<Image>().color = color;

        imageChildren = gameObject.transform.GetComponentsInChildren<Image>();

        for (int i = 0; i < imageChildren.Length; i++)
        {
            imageChildren[i].color = color;
        }

        Vector3 vec = gameObject.GetComponent<RectTransform>().position;
        vec.z = 100;
        gameObject.GetComponent<RectTransform>().position = vec;

        color = gameObject.GetComponent<Image>().color;

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

        StaticClass.playerCharacteristic.gameObject.SetActive(true);
        StaticClass.playerCharacteristic.health = StaticClass.playerCharacteristic.maxHealth;
        StaticClass.playerCharacteristic.OnImmortality(2);

        gameObject.SetActive(false);
    }
}
