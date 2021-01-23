using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementDatabase : MonoBehaviour
{
    public Achievement[] achievements;

    public void CheckForUnlocks(PlayerController.PlayerInfo playerInfo)
    {

    }

    public void CheckForUnlocks(PlayerStats stats)
    {

    }
}

[System.Serializable]
public struct Achievement
{
    public string name;
    public Sprite icon;
    public PlayerStats stats;
    public PlayerController.PlayerInfo playerInfo;
    public int coinIncrease;
    public Animal skinUnlock;
}
