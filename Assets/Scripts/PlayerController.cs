using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juto.Audio;
//using SpriteShatter; <- paid asset

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rig;

    private int dir = 1;

    private int last_walldir;

    bool wallCollision;

    public Animal animal;

    SpriteRenderer render;

    private float downSpeedMult = 0f;

    //private Shatter shatter; <- paid asset

    private PlayerInfo info;


    public struct PlayerInfo
    {
        public string char_name;
        public int score, coins, jumps, jumpInAir;
    }

    bool _WallCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0.51f*dir,0,0), Vector2.right * dir, 0.01f);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "wall")
            {
                if (last_walldir != dir)
                {
                    App.Instance.AddScore(App.Instance.wallScoreIncrease);
                    rig.velocity = new Vector2(rig.velocity.x, Time.deltaTime * 0.75f);
                }
                  

                last_walldir = dir;

                return true;

            }
               
        }
        return false;
    }


    public void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        render = GetComponentInChildren<SpriteRenderer>();
        //shatter = GetComponentInChildren<Shatter>(); <- paid asset
    }
    
    public void Unshatter()
    {
        //shatter.reset(); <- paid asset
        render.sprite = animal.jumpSprite;

    }


    public void Update()
    {
        if(!App.Instance.isPlaying)
        {
            return;
        }

        wallCollision = _WallCollision();

        if(Input.GetMouseButtonDown(0))
        {
            if (dir == 1)
                dir = -1;
            else
                dir = 1;

            render.flipX = (dir != 1) ? true : false;

            render.sprite = animal.jumpSprite;
            


            AudioController.PlaySound(animal.GetJumpClip());

            if (wallCollision)
                rig.AddForce(new Vector2(0, speed * 12f));
            else
            {
                rig.velocity = Vector2.zero;
                rig.AddForce(new Vector2(0, -speed * 20f));
            }

            downSpeedMult = 0;
        }
        else
        {
            if (wallCollision)
            {
                downSpeedMult = Mathf.Clamp(downSpeedMult += Time.deltaTime * 0.75f, 0, 0.6f);
            }
               
        }


    }

    public void FixedUpdate()
    {

        if (!App.Instance.isPlaying)
        {
            rig.velocity = new Vector2(0, -speed);
            return;
        }

        if (!wallCollision)
        {

            rig.velocity = new Vector2(speed * dir * 2, rig.velocity.y);
        }
        else
        {
            render.sprite = animal.idle;
            rig.velocity = Vector2.zero;

            rig.velocity = new Vector2(rig.velocity.x, -speed * downSpeedMult);
        }
    }


    public void Die()
    {
        render.sprite = animal.dieSprite;
        AudioController.PlaySound(animal.GetDieClip());
        //shatter.shatterDetails.explosionForce = new Vector2(15 * -dir, -5); <- paid asset
        //shatter.shatter(); -||-

    }

}
