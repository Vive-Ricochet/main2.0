﻿using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour {

    // Private fields editable by inspector
    [SerializeField] private int dashDuration = 20;

    // Private fields
    private int dashRecovery = 0;
    private Vector3 dashDir;
    private float speed;
    private float rotationSpeed;
    private PlayerMovement PM;
    private Camera PlayerCamera;

    // Use this for initialization
    void Start() {

        // Get Player movement
        PM = GetComponent<PlayerMovement>();

        // Get values from player movement
        speed = PM.getSpeed();
        rotationSpeed = PM.getRotationSpeed();
        PlayerCamera = PM.getPlayerCamera();
    }

    // Update is called once per frame
    void FixedUpdate() {

        //camera setting: get the direction camera is facing. 
        Vector3 camDir = PlayerCamera.transform.forward;
        camDir.y = 0;
        camDir.Normalize();


        Vector3 newVel = Vector3.zero;
        if (PM) {
            if (Input.GetAxis("Dash_P1") == 1) {
                //newVel = Vector3.zero;
                //dashDir = camDir;
                //print("getbutton");
                //print(PM.isCharging());
                PM.setCharging(true);
                //print(PM.isCharging());
            }
            if (Input.GetAxis("Dash_P1") != 1 && PM.isCharging()) {
                newVel = Vector3.zero;
                dashDir = camDir;
                GetComponent<PlayerMovement>().setCharging(false);
                //print(PM.isCharging());
                GetComponent<PlayerMovement>().setDashing(true);
            }
        }
        if (PM.isCharging() == true) {
            transform.rotation = Quaternion.LookRotation(camDir);
        }
        if (PM.isDashing()) {
            //transform.position = (newVel);
            dashRecovery++;
            newVel += dashDir * (speed * 2f);

            //transform.position += (newVel * Time.fixedDeltaTime);
            transform.position += (newVel * Time.fixedDeltaTime);

            //transform camera
            if (newVel != Vector3.zero) {
                transform.rotation = Quaternion.LookRotation(dashDir);
            }

            //check if dash is over
            if (dashRecovery >= dashDuration) {
                GetComponent<PlayerMovement>().setDashing(false);
                dashRecovery = 0;
            }
        }
    }

    // check on collision with another collider
    /*** must be trigger enabled ***/
    void OnTriggerEnter(Collider other) {

        GameObject otherObject = other.gameObject;

        // if other object is a "Pickup":
        if (otherObject.CompareTag("Player")) {

            otherObject.GetComponent<PlayerAccesor>().DamageThis(transform);

        }
    }

}