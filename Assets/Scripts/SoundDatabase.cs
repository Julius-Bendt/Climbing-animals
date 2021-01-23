using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDatabase : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] click;

    [SerializeField]
    private AudioClip[] menuTracks;

    [SerializeField]
    private AudioClip[] gameTracks;

    [SerializeField]
    private AudioClip[] coinPickup;
    [SerializeField]
    private AudioClip[] purchase;

    [SerializeField]
    private AudioClip[] noPurchase;

    public AudioClip Click()
    {
        return click[Random.Range(0, click.Length)];
    }

    public AudioClip MenuTrack()
    {
        return menuTracks[Random.Range(0, menuTracks.Length)];
    }

    public AudioClip GameTrack()
    {
        return gameTracks[Random.Range(0, gameTracks.Length)];
    }

    public AudioClip Purchase()
    {
        return purchase[Random.Range(0, purchase.Length)];
    }

    public AudioClip NoPurchase()
    {
        return noPurchase[Random.Range(0, noPurchase.Length)];
    }

    public AudioClip CoinPickup()
    {
        return coinPickup[Random.Range(0, coinPickup.Length)];
    }

}
