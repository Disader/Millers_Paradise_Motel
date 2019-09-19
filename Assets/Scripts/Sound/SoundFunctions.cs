using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFunctions : MonoBehaviour
{
    public void PlayButtonSound()
    {
        MusicManager.Instance.PlaySound(AppSounds.BUTTON_SFX);
    }
    public void PlayDespacito2Sound()
    {
        MusicManager.Instance.PlaySound("despacito2");
    }
}
