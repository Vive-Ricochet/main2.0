using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {

	// Private fields editable by inspector
    [SerializeField] private GameObject Player01_obj;
    [SerializeField] private GameObject Player02_obj;

    // Public fields
    private PlayerAccesor Player01;
    private PlayerAccesor Player02;

    // On scene load, do this
    void Start() {

        // Get player respective accesors
        if (Player01_obj != null && Player02_obj != null) {
            Player01 = Player01_obj.GetComponent<PlayerAccesor>();
            Player02 = Player02_obj.GetComponent<PlayerAccesor>();
        }

    }

    // On every update, do this
    void FixedUpdate() {

        // Check for collision between the players
        if (Player01 != null && Player02 != null) {
            //print(Player01.isDashing());

        }
    }
}
