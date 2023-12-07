using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;
    [SerializeField] string volumeSliderName;
    public List<AudioSource> audioSources;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider = gameObject.transform.GetComponent<Slider>();
        if(!PlayerPrefs.HasKey(volumeSliderName)) PlayerPrefs.SetFloat(volumeSliderName, 1);
        Load();
    }

    public void ChangeVolume()
    {
        foreach(AudioSource audioSource in audioSources) audioSource.volume = volumeSlider.value;
        Save();
    }

    public void ChangeMasterVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(volumeSliderName);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(volumeSliderName, volumeSlider.value);
    }
}
