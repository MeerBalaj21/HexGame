using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyButton : MonoBehaviour, IItemPurchase
{
    public RewardHandler RewardGiver;
    public IAPItems Items;
    public IAPStore Store;
    public TMP_Text TextPrice;
    public TMP_Text TextBanner;
    public PopUpsSO PopUp;

    public void Start()
    {
        TextPrice.SetText(Items.Price.ToString());
        TextBanner.SetText(Items.Title);
    }
    public void PurchaseFail(IAPItems iAPItem)
    {
        PopUp.EnablePopUp("Purchase Fail");
    }

    public void PurchaseSuccess(IAPItems iAPItem)
    {
        PopUp.EnablePopUp("Purchase Success");
        RewardGiver.GiveReward(iAPItem);
        if(iAPItem.ProductType == UnityEngine.Purchasing.ProductType.NonConsumable)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void Buy()
    {
        Store.PurchaseItemWithId(Items.SKU, this);
    }
}
