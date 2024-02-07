using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializerButtonIntertitial : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [SerializeField] InterstitialAdsButton IntertitialAd;

    void Awake()
    { InitializeAds(); }

    public void InitializeAds()
    {    _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);}

    public void OnInitializationComplete(){IntertitialAd.LoadAd();}

    public void OnInitializationFailed(UnityAdsInitializationError error, string message){Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");}
}


