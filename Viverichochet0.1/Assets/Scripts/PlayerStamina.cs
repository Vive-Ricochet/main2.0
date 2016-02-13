using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStamina : MonoBehaviour {
	public int startingStamina = 100;
	public int currentStamina;
	public Slider StaminaSlider;
	public AudioClip tiredClip;
	public AudioClip fatigueClip;

	int dashAttack = 60;
	int timer  = 40;

	Animator anim;
	AudioSource playerAudio;
	bool isTired;
	bool fatigue = false;
	bool canDash;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
		currentStamina = startingStamina;
		playerAudio = GetComponent <AudioSource> ();

	}

	// Update is called once per frame
	void Update () {
		if (currentStamina < dashAttack || fatigue == false) {
			fatigue = true;
			resetFatigue ();
		} else if (currentStamina >= dashAttack  && canDash) {
			ApplyFatigue (dashAttack);
		}

		//reset flag
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

	void resetFatigue(){
		if (fatigue || currentStamina <100){
			for (int i = 0; currentStamina <= 100; i++) {
				timer -= 1;
				if (timer == 0) { currentStamina += 1; }
				if (currentStamina >= dashAttack){
					fatigue = false;
					canDash = true;

				}
			}
		}
	}
	void Tired(){
		isTired = true;
		anim.SetTrigger ("Tired");
		playerAudio.clip = tiredClip;
		playerAudio.Play ();
	}
}
