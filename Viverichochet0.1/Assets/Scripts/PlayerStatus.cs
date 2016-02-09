using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    // private fields
    private int health;
    private bool dashing;

    void Start() {
        health = GetComponent<PlayerHealth>().GetCurrentHealth();
    }

    // is this player currently dashing?
    public bool isDashing() {
        return dashing;
    }
}
