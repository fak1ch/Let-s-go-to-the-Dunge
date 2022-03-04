using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GameOverMenuScript : MonoBehaviour
{
    [SerializeField] private RewardedAdsButton _button;

    private Image[] _imageChildren;

    private void OnEnable()
    {
        _button.AdsShowComplete += RespawnPlayerAfterAds;
    }

    private void OnDisable()
    {
        _button.AdsShowComplete -= RespawnPlayerAfterAds;
    }

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
        Destroy(_button);
        CloseGameOverMenuCoroutineAfterAds();
        StaticClass.playerCharacteristic.gameObject.SetActive(true);
        StaticClass.playerCharacteristic.HealHp(999);
        StaticClass.playerCharacteristic.OnImmortality(2);
    }

    public void OpenGameOverMenu()
    {
        gameObject.SetActive(true);
        StartCoroutine(OpenGameOverMenuCoroutine());
    }

    private IEnumerator OpenGameOverMenuCoroutine()
    {
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 0f;
        gameObject.GetComponent<Image>().color = color;

        _imageChildren = gameObject.transform.GetComponentsInChildren<Image>();

        for (int i = 0; i < _imageChildren.Length; i++)
        {
            _imageChildren[i].color = color;
        }

        Vector3 vec = gameObject.GetComponent<RectTransform>().position;
        vec.z = 100;
        gameObject.GetComponent<RectTransform>().position = vec;

        color = gameObject.GetComponent<Image>().color;

        for (float i=0f; i < 1; i += 0.05f)
        {
            color.a += i;
            for(int k =0;k< _imageChildren.Length; k++)
            {
                _imageChildren[k].color = color;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    private void CloseGameOverMenuCoroutineAfterAds()
    {
        Color color = gameObject.GetComponent<Image>().color;
        color.a = 0;
        for (int k = 0; k < _imageChildren.Length; k++)
        {
            _imageChildren[k].color = color;
        }

        Vector3 vec = gameObject.GetComponent<RectTransform>().position;
        vec.z = -100;
        gameObject.GetComponent<RectTransform>().position = vec;

        gameObject.SetActive(false);
    }
}
