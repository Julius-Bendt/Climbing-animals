using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferManager : MonoBehaviour
{
    public Offer[] items;

    public Transform parent;
    public GameObject prefab;

    public void Init()
    {
        foreach (Offer item in items)
        {
            Instantiate(prefab, parent).GetComponent<OfferUI>().UpdateUI(item);
        }
    }
}

[System.Serializable]
public struct Offer
{
    public string name, desc;
    public float price, discountPrice;
    public Currency currency;
    public bool sale;
    public Sprite icon;

    public string FormatDiscount()
    {
        string _currency = (currency == Currency.Coin) ? "<sprite=6>" : "$";

        return _currency + discountPrice;
    }

    public string FormatPrice()
    {
        string _currency = (currency == Currency.Coin) ? "<sprite=6>" : "$";

        return _currency + price;
    }

    public enum Currency
    {
        Coin,
        Dollar
    };
}
