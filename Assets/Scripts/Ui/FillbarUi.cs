using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FillbarUi : MonoBehaviour
{
    [SerializeField] private Canvas loadingScreen;
    [SerializeField] private Image loadingBarFill;
    [SerializeField] private float fillDuration = .25f;
    [SerializeField] private float _fillDeathZone = .24f;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void Awake()
    {
    }

    private void EnableLoadingScreen()
    {
        loadingScreen.enabled = true;
    }

    private void DisableLoadingScreen()
    {
        Invoke(nameof(TurnOffLoadingScreen), fillDuration);
    }

    private void TurnOffLoadingScreen()
    {
        loadingBarFill.fillAmount = 0.0f;
        loadingScreen.enabled = false;
    }

    private void UpdateLoadBarFill(float percentage)
    {
        if (percentage < _fillDeathZone)
        {
            loadingBarFill.fillAmount = 0f;
            return;
        }

        StartCoroutine(LerpFill(loadingBarFill.fillAmount, percentage));
    }

    private IEnumerator LerpFill(float from, float to)
    {
        var start = Time.time;
        var now = start;
        while (start + fillDuration > now)
        {
            loadingBarFill.fillAmount = Mathf.Lerp(from, to, (now - start) / fillDuration);
            yield return null;
            now = Time.time;
        }

        loadingBarFill.fillAmount = to;
    }

}