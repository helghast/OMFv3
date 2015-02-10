using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlAudioVolume : MonoBehaviour {

    public Slider volumeSlider = null;

	// Use this for initialization
	void Start () {
        if(volumeSlider == null)
        {
            Debug.LogWarning("Falta asignar el slider");
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void changeVolume()
    {
        gameObject.audio.volume = volumeSlider.value / 100f;
        //Debug.Log(volumeSlider.value);
    }

    public void soundClickButton(AudioSource soundButton_GO)
    {
        soundButton_GO.PlayOneShot(soundButton_GO.clip);
    }

    public void toggleMuteAll(Toggle toggle)
    {
        AudioSource[] arrayOfAudioSource_MM = FindObjectsOfType<AudioSource>();
        for(int i = 0; i < arrayOfAudioSource_MM.Length; i++)
        {
            arrayOfAudioSource_MM[i].mute = toggle.isOn;
        }
    }
}
