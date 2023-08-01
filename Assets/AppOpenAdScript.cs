using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

public class RewardedIntersitial : MonoBehaviour
{
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-1701446681412302/6293948905";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/5662855259";
#else
  private string _adUnitId = "unused";
#endif

    private AppOpenAd appOpenAd;

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            LoadAppOpenAd();
        });
    }
    public void LoadAppOpenAd()
    {
        // Clean up the old ad before loading a new one.
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        Debug.Log("Loading the app open ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        AppOpenAd.Load(_adUnitId, ScreenOrientation.Portrait, adRequest,
            (AppOpenAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("App open ad loaded with response : "
                          + ad.GetResponseInfo());

                appOpenAd = ad;
                ShowAppOpenAd();
            });
    }

    public void ShowAppOpenAd()
    {
        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            Debug.Log("Showing app open ad.");
            appOpenAd.Show();
        }
        else
        {
            Debug.LogError("App open ad is not ready yet.");
        }
    }

    private void RegisterReloadHandler(AppOpenAd ad)
    {
        
    // Raised when the ad closed full screen content.
    ad.OnAdFullScreenContentClosed += () =>
    {
            Debug.Log("App open ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadAppOpenAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadAppOpenAd();
        };
    }

    private void RegisterEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("App open ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("App open ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("App open ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("App open ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("App open ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
        };
    }
}