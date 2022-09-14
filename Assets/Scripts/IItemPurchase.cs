using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemPurchase
{
    void PurchaseSuccess(IAPItems iAPItem);
    void PurchaseFail(IAPItems iAPItem);
}
