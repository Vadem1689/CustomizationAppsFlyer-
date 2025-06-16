using UnityEngine.Purchasing;
using System.Collections.Generic;
using System.Globalization;
using AppsFlyerSDK;
using UnityEngine;

public static class IAPAnalytics
{
    public static void SendPurchaseEvent(Product product)
    {
        var price = product.metadata.localizedPrice;
        var currency = product.metadata.isoCurrencyCode;

        Dictionary<string, string> iapData = new Dictionary<string, string>
        {
            { AFInAppEvents.REVENUE, price.ToString(CultureInfo.InvariantCulture) },
            { AFInAppEvents.CURRENCY, currency },
            { AFInAppEvents.CONTENT_ID, product.definition.id }
        };

        AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, iapData);
        Debug.Log($"[AppsFlyer] af_purchase: {price} {currency} — Отправлено");
    }
}