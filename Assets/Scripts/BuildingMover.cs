using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMover : MonoBehaviour
{
    public float speed, jumpAt;
    public Vector2 dir, jumpTo;
    public bool y;

    void Update()
    {
        
        if(!y)
            transform.Translate(dir * speed * Time.deltaTime);
        else
            if(App.Instance.isPlaying)
            {
                float a = 0.045f, b = 5f;
                speed = (a * ((App.Instance.Score + 1) * 0.01f) + b) / 2;

                transform.Translate(dir * speed * Time.deltaTime);
            }
               
                

        if (y)
        {
            if (transform.localPosition.y < jumpAt)
            {
                transform.localPosition = jumpTo;
            }
        }
        else
        {
            if (transform.position.x < jumpAt)
            {
                transform.position = jumpTo;
            }
        }

    }
}
