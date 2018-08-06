using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class StoreManager : MonoBehaviour {
    MarketEvents events;

	public void OnPurchaseCompleted(Product product)
    {
        if (product != null)
        {
         if(product.definition.id=="coin5000price")
            { PlayerPrefs.SetInt("totalCoin", PlayerPrefs.GetInt("totalCoin")+5000);
                events.updateStocks();
            }
         else if (product.definition.id == "coin2500price")
            {
                PlayerPrefs.SetInt("totalCoin", PlayerPrefs.GetInt("totalCoin")+2500);
                events.updateStocks();
            }
            else if (product.definition.id == "coin1000price")
            {
                PlayerPrefs.SetInt("totalCoin", PlayerPrefs.GetInt("totalCoin")+1000);
                events.updateStocks();
            }
            else if (product.definition.id == "coin500price")
            {
                PlayerPrefs.SetInt("totalCoin", PlayerPrefs.GetInt("totalCoin")+500);
                events.updateStocks();
            }
            else if (product.definition.id == "removeadsprice")
            {
                PlayerPrefs.SetInt("canAd", 0);
            }
        }
        else
        {

        }



    }

}
