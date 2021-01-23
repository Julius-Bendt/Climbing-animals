using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public int diesSinceLastAd = 3;
    public const int deathPrAd = 3;

    public GameObject window;

    public void ShowAd()
    {
        diesSinceLastAd++;

        if(diesSinceLastAd >= deathPrAd)
        {
            diesSinceLastAd = 0;
            window.SetActive(true);
        }
    }
}
