using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    private AudioSource source;

    public void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = true;
    }
    public void Stop()
    {
        source.Stop();
    }

    public void ChangePitch(float p,float time)
    {
        StartCoroutine(_ChangePitch(time,p));
    }

    public void ChangePitch(float p)
    {
        source.pitch = p;
    }

    public void DieEffect()
    {
        StartCoroutine(_DieEffect());
    }


    public void ChangeTrack(AudioClip clip)
    {
        if (!App.Instance.settings.musicEnabled)
            return;

        if (source.clip == clip && source.isPlaying)
            return;

        source.clip = clip;
        source.Play();
    }


    private IEnumerator _ChangePitch(float time, float _to)
    {
        float elapsedTime = 0, start = source.pitch;

        while(elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            source.pitch = Mathf.Lerp(start, _to, elapsedTime / time);

            yield return null;
        }

        yield return null;
    }

    private IEnumerator _DieEffect()
    {
        ChangePitch(0.3f, 0.3f);
        yield return new WaitForSeconds(0.4f);
        ChangePitch(1f, 1f);

    }


}
