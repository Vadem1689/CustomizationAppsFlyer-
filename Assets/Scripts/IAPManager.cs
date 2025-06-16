using UnityEngine;
using UnityEngine.Purchasing;
using System.Globalization;
using AppsFlyerSDK;
using System.Collections.Generic;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;

    [Header("Product Settings")]
    public string productId = "test_product_1"; // Укажи свой ID из Google Play / App Store

    void Start()
    {
        if (storeController == null)
        {
            InitializePurchasing();
        }
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(productId, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProduct()
    {
        // 🔥 Отправка тестового ивента перед покупкой
        SendTestEventToAppsFlyer();

        if (storeController != null && storeController.products != null)
        {
            Product product = storeController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log($"Покупка продукта: {product.definition.id}");
                storeController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("Продукт не найден или недоступен");
            }
        }
        else
        {
            Debug.Log("IAP ещё не инициализирован");
        }
    }

    void SendTestEventToAppsFlyer()
    {
        Dictionary<string, string> eventValues = new Dictionary<string, string>
        {
            { "source", "iap_button" },
            { "test_key", "test_value" }
        };

        AppsFlyer.sendEvent("af_test_event", eventValues);
        Debug.Log("[AF DEBUG] af_test_event отправлен из BuyProduct");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("IAP успешно инициализирован");
        storeController = controller;
        extensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"Ошибка инициализации IAP: {error}");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError($"Ошибка инициализации IAP: {error}. Дополнительно: {message}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log($"Покупка завершена: {args.purchasedProduct.definition.id}");

        // Ивент покупки
        IAPAnalytics.SendPurchaseEvent(args.purchasedProduct);

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogWarning($"Покупка не удалась: {product.definition.id} | Причина: {failureReason}");
    }
}
