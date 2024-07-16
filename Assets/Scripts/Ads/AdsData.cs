using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AdType
{
    Banner,
    Interstitial,
    Rewarded
}
[CreateAssetMenu(menuName = "Ads Data/Data", fileName = "AdsData")]
public class AdsData : ScriptableObject
{
    [SerializeField] private AdType _adType;
    [SerializeField] private string _androidID = "Android";
    [SerializeField] private string _iosID = "IOS";

#if UNITY_IOS
    public string AdUnitId { get { return _iosID; } }
#elif UNITY_ANDROID
    public string AdUnitId { get { return _androidID; } }
#else
    public string AdUnitId = "";
#endif 

    public AdType AdType { get { return _adType; } }
}