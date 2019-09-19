using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuCanvasController : MonoBehaviour {

	public void OnButtonPlay () {
        
        SceneManager.LoadScene(AppScenes.LOADING_SCENE);
        MusicManager.Instance.PlaySound(AppSounds.BUTTON_SFX);
        MusicManager.Instance.PlayBackgroundMusic(AppSounds.GAME_MUSIC);
    }
    public GameObject m_mainMenu;
    public GameObject m_options;
    public void OnButtonOptions(bool isOptions)
    {
        m_mainMenu.SetActive(!isOptions);
        m_options.SetActive(isOptions);
        if (!isOptions)
        {
            MusicManager.Instance.MusicVolumeSave = m_musicSlider.value;
            MusicManager.Instance.SfxVolumeSave = m_sfxSlider.value;
        }
        MusicManager.Instance.PlaySound(AppSounds.BUTTON_SFX);
    }
    public void OnButtonExit()
    {
        Application.Quit();
        MusicManager.Instance.PlaySound(AppSounds.BUTTON_SFX);
    }
    public void OnButtonReturnToMenu()
    {
        SceneManager.LoadScene(AppScenes.MAIN_SCENE);
        MusicManager.Instance.PlayBackgroundMusic(AppSounds.MAIN_TITLE_MUSIC);
    }

    public Slider m_musicSlider;
    public Slider m_sfxSlider;
    public void OnMusicValueChanged()
    {
        MusicManager.Instance.MusicVolume = m_musicSlider.value;
    }
    public void OnSfxValueChanged()
    {
        MusicManager.Instance.SfxVolume = m_sfxSlider.value;
    }
}
