using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Juto.Audio;

public class OfferUI : MonoBehaviour
{
    public Offer offer;
    public Image icon, salePicture;
    public TextMeshProUGUI name,desc,price, oldPrice;

    public void UpdateUI(Offer o)
    {
        offer = o;

        name.text = o.name;
        desc.text = o.desc;

        oldPrice.gameObject.SetActive(o.sale);
        if (o.sale)
        {
            oldPrice.text = o.FormatPrice();
        }
        price.text = (o.sale) ? o.FormatDiscount() : o.FormatPrice();

        icon.sprite = o.icon;
        salePicture.gameObject.SetActive(o.sale);
        
            
    }

    public void OnClick()
    {
        float price = (offer.sale) ? offer.discountPrice : offer.price;

        if(offer.currency == Offer.Currency.Coin)
        {

            if(App.Instance.coins >= price)
            {
                AudioController.PlaySound(App.Instance.soundDB.Purchase());
                App.Instance.coins -= (int)price;
                App.Instance.ui.UpdateShopCoin();
            }
            else
            {
                AudioController.PlaySound(App.Instance.soundDB.NoPurchase());
            }
           
        }
        else
        {
            //Open appstore
        }

        App.Instance.Save();
    }
}
