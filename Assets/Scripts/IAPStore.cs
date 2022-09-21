using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

[CreateAssetMenu(menuName = "ScriptableObject/IAPStoreSO", fileName = "IAPStore")]
public class IAPStore : ScriptableObject, IStoreListener
{
    public PopUpsSO PopUp;
    public bool IAPInProgress;
    public InterstitialAds InterAds;
    IStoreController _controller;
    IExtensionProvider _extensionProvider;
    [SerializeField] public List<IAPItems> IAPItemss;
    [SerializeField] private FakeStoreUIMode FakeStoreUIMode;
    [NonSerialized] private ConfigurationBuilder _configurationBuilder;
    [NonSerialized] private IStoreController _storeController; //_controller
    [NonSerialized] private StandardPurchasingModule _purchasingModule;
    [NonSerialized] private IItemPurchase _purchaseListener;
    [SerializeField] private RewardHandler RewardHandler;
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
        var reward = GetIAPItems(purchaseEvent.purchasedProduct.definition.id);
        Debug.LogError($"[INFO][IAP] Product purchased. Product: {purchaseEvent.purchasedProduct.definition.id}.");

        if (_purchaseListener == null)
        {
            RewardHandler.GiveReward(reward);
        }
        else
        {
            _purchaseListener?.PurchaseSuccess(reward);
        }

        _purchaseListener = null;
        IAPInProgress = false;
        var time = InterAds.GetTimer();
        InterAds.SetLastAdTimer(time);
        return PurchaseProcessingResult.Complete;
    }

    public void RestorePurchases()
    {

        //_purchaseListener = purchaseListener;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                if (!IsInitialized)
                {
                    PopUp.EnablePopUp("Restore unsuccessful");
                    return;
                }
                else
                {
                    Debug.LogError("RESTORE SUCCESSFULL");
                    _extensionProvider.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(OnTransactionsRestored);
                    PopUp.EnablePopUp("Restore Successful");
                }

                break;
            default:
                Debug.LogWarning($"[WARN][IAP] {Application.platform} is not supported.");
                break;
        }
    }

    void OnTransactionsRestored(bool success)
    {
        Debug.LogError($"[INFO][IAP] Restore Transactions completed. {success}");
    }

    public ProductCollection products => throw new NotImplementedException();

    public void PurchaseIAP(string id)
    {
        _controller.InitiatePurchase(id);
    }
}
