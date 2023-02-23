using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;
using System.Collections;

public class RewardedAdsButton : MonoBehaviour
{
    public event Action AdsShowComplete;
    public event Action AdsShow;

    [SerializeField] private Text _textError;

    public void EarnedReward()
    {
        _textError.gameObject.SetActive(true);
        AdsShowComplete?.Invoke();
    }

    public void ShowText()
    {
        _textError.gameObject.SetActive(true);
    }

    public void ShowAds()
    {
        AdsShow?.Invoke();
    }
}