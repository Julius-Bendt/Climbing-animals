using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoyer : MonoBehaviour
{
    public bool top = false;
    private void OnCollisionEnter2D(Collision2D o)
    {
        if(App.Instance.isPlaying)
        {
            if (o.gameObject.CompareTag("Player"))
            {
                App.Instance.Die();
                return;
            }

            if(!top)
            {
                if (o.gameObject.CompareTag("Obstacle"))
                {
                    Destroy(o.gameObject);
                }
            }

        }
    }
}
