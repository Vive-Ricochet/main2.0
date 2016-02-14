using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float startingHealth = 100;
	public float currentHealth;
	public Slider healthSlider;
	public AudioClip deathClip;
	public AudioClip damagedClip;

	//float damage = System.Math.Floor(

	Animator anim;
	AudioSource playerAudio;
	bool isDead;
	bool damaged;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
		currentHealth = startingHealth;
		playerAudio = GetComponent <AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (damaged) {
			
		} else {
			//reset effects
		}

		//reset flag
		damaged = false;
	}

	public void TakeDamage(float amount){
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		//hurt sound
		playerAudio.clip = damagedClip;
		playerAudio.Play();

		if(currentHealth <= 0 && !isDead){
			//kill player
			Death();
		}
	}

    public float GetCurrentHealth() {
        return currentHealth;
    }

	void Death(){
		isDead = true;
		anim.SetTrigger ("Die");
		playerAudio.clip = deathClip;
		playerAudio.Play ();
	}
}
