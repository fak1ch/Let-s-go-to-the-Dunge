using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private RewardedAdsButton _button;
    [SerializeField] private string _adUnitId;
    [SerializeField] private string _videoAdMobId;
    [SerializeField] bool _testMode = true;

    private InterstitialAd _interstitial;
    private RewardedAd _rewardedAd;

    private void OnEnable()
    {
        _rewardedAd = new RewardedAd(_adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(request);

        _button.AdsShow += ShowRewardedAd;
        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }

    void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs e)
    {

    }

    private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {

    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        _button.EarnedReward();
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {

    }

    public void ShowRewardedAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
        else
        {
            _button.ShowText();
        }
    }
}