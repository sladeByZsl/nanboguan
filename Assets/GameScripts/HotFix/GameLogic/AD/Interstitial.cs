using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

namespace GameLogic
{
    public class Interstitial : MonoBehaviour
    {
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        private const string _adUnitId = "ca-app-pub-4486370518400185/4098967075";
#elif UNITY_IPHONE
        private const string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        private const string _adUnitId = "unused";
#endif

        private InterstitialAd _interstitialAd;

        public void Start()
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
            });

            LoadAd();
            ShowAd();
        }

        /// <summary>
        /// Loads the ad.
        /// </summary>
        public void LoadAd()
        {
            // Clean up the old ad before loading a new one.
            if (_interstitialAd != null)
            {
                DestroyAd();
            }

            Debug.Log("Loading interstitial ad.");

            // Create our request used to load the ad.
            var adRequest = new AdRequest();

            // Send the request to load the ad.
            InterstitialAd.Load(_adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
            {
                // If the operation failed with a reason.
                if (error != null)
                {
                    Debug.LogError("Interstitial ad failed to load an ad with error : " + error);
                    return;
                }
                // If the operation failed for unknown reasons.
                // This is an unexpected error, please report this bug if it happens.
                if (ad == null)
                {
                    Debug.LogError("Unexpected error: Interstitial load event fired with null ad and null error.");
                    return;
                }

                // The operation completed successfully.
                Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());
                _interstitialAd = ad;

            });
        }

        /// <summary>
        /// Shows the ad.
        /// </summary>
        public void ShowAd()
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
            }
        }

        /// <summary>
        /// Destroys the ad.
        /// </summary>
        public void DestroyAd()
        {
            if (_interstitialAd != null)
            {
                Debug.Log("Destroying interstitial ad.");
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }
        }

        /// <summary>
        /// Logs the ResponseInfo.
        /// </summary>
        public void LogResponseInfo()
        {
            if (_interstitialAd != null)
            {
                var responseInfo = _interstitialAd.GetResponseInfo();
                UnityEngine.Debug.Log(responseInfo);
            }
        }
    }
}
