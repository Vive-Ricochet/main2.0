using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour {

    [SerializeField] private int dashDuration = 30;

    private int dashRecovery = 0;
    private Vector3 dashDir;

    private float speed;
    private float rotationSpeed;
    private PlayerMovement PM;

    // Use this for initialization
    void Start() {
        PM = GetComponent<PlayerMovement>();
        speed = PM.getSpeed();
        rotationSpeed = PM.getRotationSpeed();
    }

    // Update is called once per frame
    void FixedUpdate() {

        //camera setting: get the direction camera is facing. 
        Vector3 camDir = Camera.main.transform.forward;
        camDir.y = 0;
        camDir.Normalize();


        Vector3 newVel = Vector3.zero;
        
        if (PM) {
            if (Input.GetButton("Dash")) {
                newVel = Vector3.zero;
                dashDir = camDir;
                GetComponent<PlayerMovement>().setDashing(true);

            }
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