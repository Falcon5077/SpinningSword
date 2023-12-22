using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class BottomBanner : MonoBehaviour
{
    private BannerView bannerView;
    #if UNITY_ANDROID
        // 실전용 공고ID
        private string adUnitId = "ca-app-pub-5504078506585326/5274944577";
        // 데모용 광고ID
    //    private string adUnitId = "ca-app-pub-3940256099942544/6300978111";
    #elif UNITY_IPHONE
        private string adUnitId = "ca-app-pub-3940256099942544/2934735716";
    #else
        private string adUnitId = "unused";
    #endif

    // Start is called before the first frame update
    void Start()
    {
        // MobileAds.RaiseAdEventsOnUnityMainThread = true;

        // 테스트 기기 설정
        // List<string> deviceIds = new List<string>();
        // deviceIds.Add("08D488AF882B169DCA4EB790F975FF4C");
        // RequestConfiguration requestConfiguration = new RequestConfiguration
        //     .Builder()
        //     .SetTestDeviceIds(deviceIds)
        //     .build();
        // MobileAds.SetRequestConfiguration(requestConfiguration);
        
        // MobileAds.Initialize(initStatus => { });
        // this.RequestBanner();
        
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            LoadAd();
        });
    }

    private void CreateBannerView(){
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (bannerView != null)
        {
            bannerView.Destroy();
            //DestroyAd();
            
        }

        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);
    }

    private void LoadAd(){
        
        // create an instance of a banner view first.
        if (bannerView == null)
        {
            CreateBannerView();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);
        
    }


    private void RequestBanner()
    {
        // if(this.bannerView != null){
        //     this.bannerView.Destroy();
        // }

        // AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        // this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);
        
        AdRequest request = new AdRequest.Builder()
        //.setTest("08D488AF882B169DCA4EB790F975FF4C")
        .Build();

        List<string> deviceIds = new List<string>();
        deviceIds.Add("08D488AF882B169DCA4EB790F975FF4C");
        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        this.bannerView.LoadAd(request);
    }

    // public void LoadAd()
    // {
    //     if(_bannerView == null)
    //     {
    //         CreateBannerView();
    //     }
    //     // create our request used to load the ad.
    //     var adRequest = new AdRequest.Builder()
    //         .AddKeyword("unity-admob-sample")
    //         .Build();

    //     _bannerView.LoadAd(adRequest);
    // }

    public void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        LoadAdError loadAdError = args.LoadAdError;

        // Gets the domain from which the error came.
        string domain = loadAdError.GetDomain();

        // Gets the error code. See
        // https://developers.google.com/android/reference/com/google/android/gms/ads/AdRequest
        // and https://developers.google.com/admob/ios/api/reference/Enums/GADErrorCode
        // for a list of possible codes.
        int code = loadAdError.GetCode();

        // Gets an error message.
        // For example "Account not approved yet". See
        // https://support.google.com/admob/answer/9905175 for explanations of
        // common errors.
        string message = loadAdError.GetMessage();

        // Gets the cause of the error, if available.
        AdError underlyingError = loadAdError.GetCause();

        // All of this information is available via the error's toString() method.
        Debug.Log("Load error string: " + loadAdError.ToString());

        // Get response information, which may include results of mediation requests.
        ResponseInfo responseInfo = loadAdError.GetResponseInfo();
        Debug.Log("Response info: " + responseInfo.ToString());
    }
}
