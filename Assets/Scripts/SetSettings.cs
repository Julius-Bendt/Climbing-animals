using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSettings : MonoBehaviour
{
    public void SetMusic(bool val)
    {
        App.Instance.settings.musicEnabled = val;

        if (!val)
            App.Instance.music.Stop();
        else
            App.Instance.music.ChangeTrack(App.Instance.soundDB.MenuTrack());


        App.Instance.Save();
    }
    public void SetSound(bool val)
    {
        App.Instance.settings.sfxEnabled = val;

        App.Instance.Save();
    }

    public void SetNotification(bool val)
    {
        App.Instance.settings.notification = val;

        App.Instance.Save();
    }
}
