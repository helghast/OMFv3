using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlAudioVolume : MonoBehaviour {

    public Slider volumeMusicSlider = null;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void changeVolumeBSO()
    {
       audio.volume = volumeMusicSlider.value / 100f;
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
