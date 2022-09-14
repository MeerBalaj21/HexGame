using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

[CreateAssetMenu(menuName = "ScriptableObject/IAPStoreSO", fileName = "IAPStore")]
public class IAPStore : ScriptableObject, IStoreListener
{
    public bool IAPInProgress;
    [SerializeField] public List<IAPItems> IAPItemss;
    [SerializeField] private FakeStoreUIMode FakeStoreUIMode;
    IStoreController _controller;
    IExtensionProvider _extensionProvider;
    [NonSerialized] private ConfigurationBuilder _configurationBuilder;
    [NonSerialized] private IStoreController _storeController; //_controller
    [NonSerialized] private StandardPurchasingModule _purchasingModule;
    [NonSerialized] private IItemPurchase _purchaseListener;

    public bool IsInitialized
    {
        get
        {
            return _storeController != null && _extensionProvider != null;
        }
    }

    public void Initialize()
    {
        Debug.Log("[INFO] Initializing IAP store");
        SetupStoreConfigurationBuilder();
        if (!IsInitialized)
        {
            UnityPurchasing.Initialize(this, _configurationBuilder);
        }
    }

    public IAPItems GetIAPItems(string id)
    {
        return IAPItemss.Where(x => x.SKU == id).FirstOrDefault();
    }

    public Product GetProduct(string id)
    {
        var item = GetIAPItems(id);
        return item != null ? item.Product : null;
    }

    public bool PurchaseItemWithId(string id, IItemPurchase purchaseListener)
    {
        if (!IsInitialized) return false;
        if (_purchaseListener != null) return false;
        _purchaseListener = purchaseListener;
        _storeController.InitiatePurchase(id);
        IAPInProgress = true;
        return true;
    }

    private void SetupStoreConfigurationBuilder()
    {
        if (_configurationBuilder == null)
        {
            _purchasingModule = StandardPurchasingModule.Instance();
            _purchasingModule.useFakeStoreUIMode = FakeStoreUIMode;
            _configurationBuilder = ConfigurationBuilder.Instance(_purchasingModule);
            _configurationBuilder.AddProducts(IAPItemss.Select(x => x.ProductDefinition));
        }
    }

    private void AssignProductsToItems()
    {
        for (int i = 0; i < IAPItemss.Count; i++)
        {
            var item = IAPItemss[i];
            item.Product = _storeController.products.WithID(item.SKU);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("[INFO][IAP] IAP Store initialized");
        _storeController = controller;
        _extensionProvider = extensions;
        AssignProductsToItems();
        RestorePurchases();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"[ERROR][IAP] Purchasing failed to initialize. Reason: {error}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError($"[ERROR][IAP] Product purchase failed. Product: {product.definition.id}. Reason: {failureReason}");
        if (failureReason == PurchaseFailureReason.DuplicateTransaction)
        {
            var prod = GetIAPItems(product.definition.id);
            _purchaseListener?.PurchaseSuccess(prod);
        }
        else
        {
            _purchaseListener?.PurchaseFail(GetIAPItems(product.definition.id));
        }
        _purchaseListener = null;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Debug.LogError($"[INFO][IAP] Product purchased. Product: {purchaseEvent.purchasedProduct.definition.id}.");
        _purchaseListener?.PurchaseSuccess(GetIAPItems(purchaseEvent.purchasedProduct.definition.id));

        _purchaseListener = null;
        IAPInProgress = false;
        return PurchaseProcessingResult.Complete;
    }

    public void RestorePurchases()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                _extensionProvider.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(OnTransactionsRestored);
                break;
            default:
                Debug.LogWarning($"[WARN][IAP] {Application.platform} is not supported.");
                break;
        }
    }

    void OnTransactionsRestored(bool success)
    {
        Debug.Log("[INFO][IAP] Restore Transactions completed.");
    }

    public ProductCollection products => throw new NotImplementedException();

    public void PurchaseIAP(string id)
    {
        _controller.InitiatePurchase(id);
    }
}
