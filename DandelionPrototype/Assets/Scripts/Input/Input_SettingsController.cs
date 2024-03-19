using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Input_SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] AudioMixer mixer;
    private int currentVolume;
    private bool canChangeVolume;
    [SerializeField] private int volumeIncrement;
    [SerializeField] private Slider volumeSlider;

    private void OnSettingsMenu(InputValue inputValue)
    {
        if (settingsPanel.activeInHierarchy == true)
        {
            settingsPanel.SetActive(false);
            canChangeVolume = false;
        }
        else
        {
            settingsPanel.SetActive(true);
            canChangeVolume = true;
        }
    }

    private void OnVolumeUp(InputValue inputValue)
    {
        if (canChangeVolume == true)
            SetVolume(currentVolume + volumeIncrement, true);
    }
    private void OnVolumeDown(InputValue inputValue)
    {
        if (canChangeVolume == true)
            SetVolume(currentVolume - volumeIncrement, false);
    }

    private void SetVolume(int volumeToSet, bool isIncreasing)
    {
        volumeToSet = Mathf.Clamp(volumeToSet, -30, 20);
        currentVolume = volumeToSet;

        mixer.SetFloat("MasterVol", volumeToSet);

        if (isIncreasing == true)
            volumeSlider.value += 1;
        else
            volumeSlider.value -= 1;
    }
}
