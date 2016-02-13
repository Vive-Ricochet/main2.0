using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStamina : MonoBehaviour {
	public int startingStamina = 100;
	public int currentStamina;
	public Slider StaminaSlider;
	public AudioClip tiredClip;
	public AudioClip fatigueClip;

	Animator anim;
	AudioSource playerAudio;
	bool isTired;
	bool fatigue;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
		currentStamina = startingStamina;
		playerAudio = GetComponent <AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (fatigue) {
			//onHit effects
		} else {
			//reset effects
		}

		//reset flag
		fatigue = false;
	}

	public void ApplyFatigue(int amount){
		fatigue = true;
		currentStamina -= amount;
		StaminaSlider.value = currentStamina;

		//hurt sound
		playerAudio.clip = fatigueClip;
		playerAudio.Play();

		if(currentStamina <= 0 && !isTired){
			//kill player
			Tired();
		}
	}

	void Tired(){
		isTired = true;
		anim.SetTrigger ("Tired");
		playerAudio.clip = tiredClip;
		playerAudio.Play ();
	}
}
