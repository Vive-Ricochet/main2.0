using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public AudioClip deathClip;
	public AudioClip damagedClip;

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
			//onHit effects
		} else {
			//reset effects
		}

		//reset flag
		damaged = false;
	}

	public void TakeDamage(int amount){
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

    public int GetCurrentHealth() {
        return currentHealth;
    }

	void Death(){
		isDead = true;
		anim.SetTrigger ("Die");
		playerAudio.clip = deathClip;
		playerAudio.Play ();
	}
}
