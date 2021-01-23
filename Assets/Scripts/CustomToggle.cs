using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CustomToggle : MonoBehaviour
{
    private bool value;
    public Sprite on, off;
    public Sprite iconOn, iconOff;

    public Image state, icon;

    public ToggleClickEvent _event;

    public void Pressed()
    {
        value = !value;
        UpdateUI();

        _event.Invoke(value);

    }

    public void SetValue(bool val)
    {
        value = val;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //Update ui
        state.sprite = (value) ? on : off;
        icon.sprite = (value) ? iconOn : iconOff;
    }

    [System.Serializable]
    public class ToggleClickEvent : UnityEvent<bool>
    {
    }
}
