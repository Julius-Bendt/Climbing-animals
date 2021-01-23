using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juto.UI;

public class GameUI : MonoBehaviour
{

    public RectTransform screenFill;
    UIAnimationFramework anim;

    private void Start()
    {
        anim = GetComponent<UIAnimationFramework>();
        AnimateFill();
    }

    public void AnimateFill(bool _out = true)
    {
        Vector3 v = Vector3.zero;
        if (_out)
            v = Vector3.zero;
        else
            v = new Vector3(15, 15, 1);

        anim.Scale(screenFill, v, 0.5f, 0f);
    }
}
