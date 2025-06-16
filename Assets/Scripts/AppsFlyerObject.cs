using UnityEngine;
using AppsFlyerSDK;

public class AppsFlyerObject : MonoBehaviour, IAppsFlyerConversionData
{
    void Start()
    {
        AppsFlyer.setIsDebug(true); // Включаем лог
        AppsFlyer.setCustomerUserId(SystemInfo.deviceUniqueIdentifier); // Уникальный ID юзера

        AppsFlyer.initSDK(
            "e2GAFEK3u92ZRGPjhAq3r8",                  // Dev Key
            "com.AppsFlyerTestProjectForDocs.MiniGame", // Android package name
            this);

        AppsFlyer.startSDK();

        Debug.Log("[AppsFlyer] SDK инициализирован");
    }

    public void onConversionDataSuccess(string conversionData)
    {
        Debug.Log("Conversion data: " + conversionData);
    }

    public void onConversionDataFail(string error)
    {
        Debug.LogError("Conversion data error: " + error);
    }

    public void onAppOpenAttribution(string attributionData)
    {
        Debug.Log("App open attribution: " + attributionData);
    }

    public void onAppOpenAttributionFailure(string error)
    {
        Debug.LogError("App open attribution error: " + error);
    }
}