using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Animal
{
    public string name;
    public AudioClip[] jump, die;
    public Sprite idle, jumpSprite, dieSprite;

    public AudioClip GetJumpClip()
    {
        return jump[Random.Range(0, jump.Length)];
    }

    public AudioClip GetDieClip()
    {
        return die[Random.Range(0, die.Length)];
    }
}
