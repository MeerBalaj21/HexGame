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
    //public TMP_Text TextAmount;



    public void Start()
    {
        TextPrice.SetText(Items.Price.ToString());
        TextBanner.SetText(Items.Title);
        //TextAmount.SetText(I)
    }
    public void PurchaseFail(IAPItems iAPItem)
    {
        
    }

    public void PurchaseSuccess(IAPItems iAPItem)
    {
        RewardGiver.GiveReward(iAPItem);
    }

    public void Buy()
    {
        Store.PurchaseItemWithId(Items.SKU, this);
    }
}
