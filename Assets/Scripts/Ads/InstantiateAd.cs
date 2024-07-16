using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InstantiateAd : Ads, IUnityAdsLoadListener, IUnityAdsShowListener
{
    bool _adLoaded = false;
    public event Action adEnded;

    internal void Initialize()
    {
        Advertisement.Load(p_data.AdUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        _adLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Rewarded: Error loading Ad Unit: {p_data.AdUnitId} - {error.ToString()} - {message}");
    }

    public override void Show()
    {
        if (_adLoaded)
            Advertisement.Show(p_data.AdUnitId, this);
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Debug.Log($"Showing Ad Unit {p_data.AdUnitId}");
    }

    public void OnUnityAdsShowClick(string _adUnitId)
    {
        Debug.Log($"Clicked Ad Unit {p_data.AdUnitId}");
    }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ad Unit {p_data.AdUnitId} was ended");
        adEnded?.Invoke();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {p_data.AdUnitId}: {error.ToString()} - {message}");
    }
}