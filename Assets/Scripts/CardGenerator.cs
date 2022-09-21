using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
    public IAPStore Store;
    public GameObject Parent;
    public NoAds Ads;
    public GameObject RestoreIAP;
    public GameObject RV;
    [SerializeField] private ScrollRect ScrollRect;

    public void Start()
    {
        CardBuilder();
    }

   

    public void CardBuilder()
    {
        GameObject RVCard = (GameObject)Instantiate(RV, new Vector2(0, 0), Quaternion.identity);
        RVCard.transform.SetParent(Parent.transform);

        foreach (var j in Store.IAPItemss)
        {
            if(Ads.ShowAds == false)
            {
                if(j.ProductType == UnityEngine.Purchasing.ProductType.NonConsumable)
                {
                    continue;
                }
               
            }
            GameObject Card = (GameObject)Instantiate(j.Prefab, new Vector2(0, 0), Quaternion.identity);
            Card.transform.SetParent(Parent.transform);
            
        }
        GameObject RestoreIAPCard = (GameObject)Instantiate(RestoreIAP, new Vector2(0, 0), Quaternion.identity);
        RestoreIAPCard.transform.SetParent(Parent.transform);

        ScrollRect.normalizedPosition = new Vector2(0, 1);
    }
}
