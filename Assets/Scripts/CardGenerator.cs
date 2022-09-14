using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    public IAPStore Store;
    public GameObject Parent;


    public void Start()
    {
        CardBuilder();
    }

    public void CardBuilder()
    {
        foreach (var j in Store.IAPItemss)
        {
            GameObject Card = (GameObject)Instantiate(j.Prefab, new Vector2(0, 0), Quaternion.identity);
            Card.transform.SetParent(Parent.transform);

        }      
    }

}
