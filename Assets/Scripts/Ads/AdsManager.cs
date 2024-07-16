using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private Button _rewardButton;
    [SerializeField] private GameManager _manager;
    [SerializeField] private AdsData[] _adsData = new AdsData[3];
    [Tooltip("This value most be between 0 to 100, 100 is for always the game was ended a ad start.")]
    [SerializeField] private int _adChances = 100;

    //Ads
    private BannerAd _bannerAd = new();
    private InstantiateAd _interstitialAd = new();
    private InstantiateAd _rewardedAd = new();

    private int _currentAdChances;

    private string _gameId;

    public event Action rewardEvent;

    private void OnEnable()
    {
        _manager.startAd += HandleEndGame;
        _manager.endGame += HandleReactiveRewardButton;
        _rewardButton.onClick.AddListener(HandleStartReward);
    }

    private void OnDisable()
    {
        _manager.startAd -= HandleEndGame;
        _manager.endGame -= HandleReactiveRewardButton;
        _rewardButton.onClick.RemoveListener(HandleStartReward);
    }

    private void Awake()
    {
#if UNITY_IOS
        _gameId = "5629781";
#elif UNITY_ANDROID
        _gameId = "5629780";
#elif UNITY_EDITOR
        _gameId = "5629780";
#else
        gameObject.SetActive(false);
#endif

        Validate();

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, true, this);
        }

        CheckAdsData(_adsData);
        _currentAdChances = _adChances;
    }

    private void Start()
    {
        _bannerAd.Show();
    }

    private void DataConverter()
    {
        for (int i = 0; i < _adsData.Length; i++)
        {
            switch (_adsData[i].AdType)
            {
                case AdType.Banner:
                    _bannerAd.Data = _adsData[i];
                    break;
                case AdType.Interstitial:
                    _interstitialAd.Data = _adsData[i];
                    _interstitialAd.adEnded += HandleEndInterstitial;
                    break;
                case AdType.Rewarded:
                    _rewardedAd.Data = _adsData[i];
                    _rewardedAd.adEnded += HandleReward;
                    break;
                default:
                    break;
            }
        }
    }

    private void CheckAdsData(AdsData[] adsDataArray)
    {
        HashSet<AdsData> seen = new HashSet<AdsData>();
        List<AdsData> duplicates = new List<AdsData>();

        foreach (AdsData ad in adsDataArray)
        {
            if (!seen.Add(ad))
            {
                duplicates.Add(ad);
            }
        }

        if (duplicates.Count > 0)
        {
            Debug.LogError($"{name}: There are duplicate types of ads, check the SO\nDisabling component.");
            enabled = false;
            return;
        }
        else
        {
            DataConverter();
        }
    }

    private void HandleStartReward()
    {
        _rewardedAd.Show();
        _rewardButton.gameObject.SetActive(false);
    }

    private void HandleReward()
    {
        rewardEvent?.Invoke();
        _rewardedAd.Initialize();
    }

    private void HandleEndInterstitial()
    {
        _currentAdChances = _adChances;
        _interstitialAd.Initialize();
    }

    private void HandleEndGame()
    {
        int chance = UnityEngine.Random.Range(0, 100);

        if (chance < _currentAdChances)
            _interstitialAd.Show();
        else
            _currentAdChances += 10;

        HandleReactiveRewardButton();
    }

    private void HandleReactiveRewardButton()
    {
        _rewardButton.gameObject.SetActive(true);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        _bannerAd.Show();
        _interstitialAd.Initialize();
        _rewardedAd.Initialize();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    private void Validate()
    {
        if (!_rewardButton)
        {
            Debug.LogError($"{name}: RewardButton is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_manager)
        {
            Debug.LogError($"{name}: GameManager is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }
}