using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    // private fields
    private int health;

    void Start() {

        health = GetComponent<PlayerHealth>().GetCurrentHealth();
 
    }
}
