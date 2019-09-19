using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStart : MonoBehaviour
{
    [SerializeField] MusicStartEnum musictype;
    void Start()
    {
        switch (musictype)
        {
            case MusicStartEnum.menu:
                MusicManager.Instance.PlayBackgroundMusic(AppSounds.MAIN_TITLE_MUSIC);
                break;
            case MusicStartEnum.initialGame:
                MusicManager.Instance.PlayBackgroundMusic(AppSounds.GAME_MUSIC);
                break;
        }
    }  
}
