using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    // private fields
    private int health;
    private bool dashing; // NOT USED

    void Start() {
        health = GetComponent<PlayerHealth>().GetCurrentHealth();
    }

    // is this player currently dashing? ******** NOT USED CURRENTLY (USE PLAYER MOVEMENT'S DASH VALUE FOR NOW)
    public bool isDashing() {
        return dashing;
    }
}
