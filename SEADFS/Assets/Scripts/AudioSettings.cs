using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;

    [SerializeField] private Slider sfxSlider,musicSlider,weldSlider,masterSlider;

    private void Start()
    {
        SetByPlayerPrefs();
        AsignSliders();
    }

    private void SetByPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("PlayerMusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("PlayerMusicVolume");
            MusicValueChanged();
        }

        if (PlayerPrefs.HasKey("PlayerSFXVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("PlayerSFXVolume");
            SFXValueChanged();
        }

        if (PlayerPrefs.HasKey("PlayerMasterVolume"))
        {
            masterSlider.value = PlayerPrefs.GetFloat("PlayerMasterVolume");
            MasterValueChanged();
        }

        if (PlayerPrefs.HasKey("PlayerWeldVolume"))
        {
            weldSlider.value = PlayerPrefs.GetFloat("PlayerWeldVolume");
            WeldValueChanged();
        }
    }

    private void AsignSliders()
    {
        musicSlider.onValueChanged.AddListener(delegate { MusicValueChanged(); });
        sfxSlider.onValueChanged.AddListener(delegate { SFXValueChanged(); });
        weldSlider.onValueChanged.AddListener(delegate { WeldValueChanged(); });
        masterSlider.onValueChanged.AddListener(delegate { MasterValueChanged(); });


    }

    private void MasterValueChanged()
    {
        if (masterSlider.value == 0f)
        {
            masterMixer.SetFloat("MasterVolume", -80);
        }
        else
        {
            masterMixer.SetFloat("MasterVolume", Mathf.Log10(masterSlider.value) * 20);
        }

        PlayerPrefs.SetFloat("PlayerMasterVolume", masterSlider.value);
    }

    private void MusicValueChanged()
    {
        if (musicSlider.value == 0f)
        {
            masterMixer.SetFloat("MusicVolume", -80);
        }
        else
        {
            masterMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
        }

        PlayerPrefs.SetFloat("PlayerMusicVolume", musicSlider.value);
    }

    private void SFXValueChanged()
    {
        if (sfxSlider.value == 0f)
        {
            masterMixer.SetFloat("SFXVolume", -80);
        }
        else
        {
            masterMixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20);
        }

        PlayerPrefs.SetFloat("PlayerSFXVolume", sfxSlider.value);
    }

    private void WeldValueChanged()
    {
        if (weldSlider.value == 0f)
        {
            masterMixer.SetFloat("WeldVolume", -80);
        }
        else
        {
            masterMixer.SetFloat("WeldVolume", Mathf.Log10(weldSlider.value) * 20);
        }

        PlayerPrefs.SetFloat("PlayerWeldVolume", weldSlider.value);
    }

}
