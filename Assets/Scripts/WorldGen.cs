using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    public Obstacle[] obstacles;
    public float y;
    public float waittime;
    public Obstacle coin, pottedPlant;
    [System.Serializable]
    public struct Obstacle
    {
        public GameObject prefab;
        public float min, max, space;
        public bool randomBetween;
    }

    private void Start()
    {
        StartCoroutine(GameWorldGen());
        StartCoroutine(Falling());
    }

    IEnumerator GameWorldGen()
    {
        while(true)
        {

            if(App.Instance.isPlaying)
            {

                if(Random.Range(0,100) >= 90)
                {
                    Instantiate(coin.prefab, new Vector3(Random.Range(coin.min, coin.max), y, 0), Quaternion.identity);
                }

                Obstacle o = obstacles[Random.Range(0, obstacles.Length)];
                float x = (Random.Range(0, 100) >= 50f) ? o.min : o.max;
                int rot = (x == o.min) ? 0 : 180;

                Instantiate(o.prefab, new Vector3(x, y, 0), Quaternion.Euler(rot,0,rot));
                yield return new WaitForSeconds(Wait());
                
            }
            yield return null;
        }
    }

    public IEnumerator Falling()
    {
        while(true)
        {
            if (App.Instance.isPlaying)
            {
                int random = Random.Range(0, 100);
                if (random >= 90)
                {
                    Instantiate(coin.prefab, new Vector3(Random.Range(coin.min, coin.max), y, 0), Quaternion.identity);
                }
                else if (random >= 70)
                {
                    Instantiate(pottedPlant.prefab, new Vector3(Random.Range(pottedPlant.min, pottedPlant.max), y, 0), Quaternion.identity);
                }

                yield return new WaitForSeconds(Wait() * Random.Range(0.5f, 1.5f));

            }
            yield return null;
        }

    }


    public float Wait()
    {
        //y = ax+b
        float a = -0.0055f, b = 2.5f;

        if (App.Instance.Score > 30000)
            a = -0.0035f;

        return (a * ((App.Instance.Score + 1) * 0.007f) + b) - Random.Range(0,0.5f);
    }



}
