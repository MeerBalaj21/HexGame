using UnityEngine;
using UnityEngine.Purchasing;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObject/IAPSO", order = 1, fileName = "IAP")]
public class IAPItems : ScriptableObject
{
    public string SKU = "";
    public string Title = "";
    public float Price = 0.00f;
    public ProductType ProductType;
    public List<RewardIAP> RewardType;
    public GameObject Prefab;

    [NonSerialized] public Product Product;
    ProductDefinition _productDefinition;
    public ProductDefinition ProductDefinition
    {
        get
        {
            if (_productDefinition == null)
            {
                _productDefinition = new ProductDefinition(SKU, ProductType);
            }
            return _productDefinition;
        }
    }
}
