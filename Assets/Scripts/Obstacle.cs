using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juto.Audio;

public class Obstacle : MonoBehaviour
{

    private Rigidbody2D rig;
    private float speed;

    public Vector2 speedOffset = new Vector2(-0.75f, 0.75f);
    public bool coin;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if(!App.Instance.isPlaying)
        {
            return;
        }

        speed = CalcSpeed();
        speed += Random.Range(speedOffset.x,speedOffset.y);
    }

    public float CalcSpeed()
    {
        //y = ax+b
        float a = 0.045f, b = 5f;

        return (a * ((App.Instance.Score + 1)*0.01f) + b) / 2;
    }

    public void FixedUpdate()
    {
        if (!App.Instance.isPlaying)
        {
            return;
        }


        rig.velocity = new Vector2(0, -speed);
    }

    private void OnTriggerEnter2D(Collider2D o)
    {
        if(o.tag == "Player")
        {
            if(!coin)
            {
                o.GetComponent<PlayerController>().Die();
                App.Instance.Die();
            }
            else
            {
                AudioController.PlaySound(App.Instance.soundDB.CoinPickup());
                App.Instance.coinsPickedup += 1;
                Destroy(gameObject);
            }
        }
    }
}
