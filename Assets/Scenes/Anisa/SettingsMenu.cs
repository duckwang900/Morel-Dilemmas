using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioSource buttonAudioSource;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject MainMenu;

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    /** TODO: Create AudioManager that handles all the BG Music/SFX 
    (this is a temporary working version to test out the sliders) 
    Probably also use PlayerPrefs to save/load audio settings in AudioManager
    */
    public void OnButtonClick()
    {
        StartCoroutine(PlayButtonSFX());
    }

    public IEnumerator PlayButtonSFX()
    {
        buttonAudioSource.Play();

        yield return new WaitForSeconds(buttonAudioSource.clip.length);
        this.gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFX(float sliderValue)
    {
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }
}
