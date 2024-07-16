using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : Ads
{
    public override void Show()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(p_data.AdUnitId, options);
    }

    private void OnBannerLoaded()
    {
        Advertisement.Banner.Show(p_data.AdUnitId);
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }
}