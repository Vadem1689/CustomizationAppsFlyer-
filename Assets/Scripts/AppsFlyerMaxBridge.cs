/*using UnityEngine;


using System.Collections.Generic;


using AppsFlyerSDK;



using MaxSdkBase;





/// <summary>


/// Единая точка инициализации AppsFlyer + передача ad revenue (MAX) и IAP-дохода.


/// Поместите на объект в первой сцене (DontDestroyOnLoad).


/// </summary>


public class AppsFlyerMaxBridge : MonoBehaviour, IAppsFlyerConversionData


{


   [Header("AppsFlyer")]


   [SerializeField] private string appsFlyerDevKey = "YOUR_AF_DEV_KEY";


   [SerializeField] private string iosAppId        = "123456789";      // без ‘id’-префикса


   [SerializeField] private bool   enableDebugLogs = true;





#if UNITY_IOS


   private const AppsFlyerConnector.Store CURRENT_STORE = AppsFlyerConnector.Store.APPLE;


#else


   private const AppsFlyerConnector.Store CURRENT_STORE = AppsFlyerConnector.Store.GOOGLE;


#endif





   // --------------------------------------------------------------------


   // 1. Инициализация


   // --------------------------------------------------------------------


   private void Awake()


   {


       DontDestroyOnLoad(gameObject);





       // --- AppsFlyer SDK ---


       AppsFlyer.setIsDebug(enableDebugLogs);


       AppsFlyer.initSDK(appsFlyerDevKey, iosAppId, this);   // this = deep-link колбэки





       // --- Purchase Connector (IAP & подписки) ---


       AppsFlyerPurchaseConnector.init(this, CURRENT_STORE);


       AppsFlyerPurchaseConnector.setAutoLogPurchaseRevenue(


           AppsFlyerAutoLogPurchaseRevenueOptions


               .AppsFlyerAutoLogPurchaseRevenueOptionsInAppPurchases |


           AppsFlyerAutoLogPurchaseRevenueOptions


               .AppsFlyerAutoLogPurchaseRevenueOptionsAutoRenewableSubscriptions);


       AppsFlyerPurchaseConnector.build();


       AppsFlyerPurchaseConnector.startObservingTransactions();





       // Запускаем отправку событий в AppsFlyer


       AppsFlyer.startSDK();


   }





   // --------------------------------------------------------------------


   // 2. Доход от рекламы (impression-level)


   // --------------------------------------------------------------------


   private void OnEnable()


   {


       MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdRevenuePaid;


       MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent    += OnAdRevenuePaid;


       MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent      += OnAdRevenuePaid;


   }





   private void OnDisable()


   {


       MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent -= OnAdRevenuePaid;


       MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent    -= OnAdRevenuePaid;


       MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent      -= OnAdRevenuePaid;


   }





   private static void OnAdRevenuePaid(string adUnitId, AdInfo adInfo)


   {


       // Обязательные поля


       var data = new AFAdRevenueData(


           adInfo.NetworkName,               // network


           MediationNetwork.AppLovinMAX,     // mediation


           "USD",                            // валюта (MAX всегда в USD)


           adInfo.Revenue);                  // доход за показ





       // Дополнительные атрибуты


       var extras = new Dictionary<string, string>


       {


           [AdRevenueScheme.AD_UNIT]       = adUnitId,


           [AdRevenueScheme.AD_TYPE]       = adInfo.AdFormat,


           [AdRevenueScheme.PLACEMENT]     = adInfo.Placement,


           [AdRevenueScheme.IMPRESSION_ID] = adInfo.CreativeId      // не обязателен


       };





       AppsFlyer.logAdRevenue(data, extras);


   }





   // --------------------------------------------------------------------


   // 3. (Необязательно) колбэки deep-link/атрибуции


   // --------------------------------------------------------------------


   public void onConversionDataSuccess(string conversionData) { }


   public void onConversionDataFail(string error)              { }


   public void onAppOpenAttribution(string attributionData)    { }


   public void onAppOpenAttributionFailure(string error)       { }


}
*/