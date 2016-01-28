using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    // private fields editable by inspector
    [SerializeField] private float speed = 20;  // player movement speed
    [SerializeField] private float jump = 5;   // player intitial jump velocity
    [SerializeField] private float rotationSpeed = 100; // player rotation speed

    // private fields
    private bool grounded;
    private Rigidbody rb;

    public bool dashing;
    private bool canDash = false;//to be used with stamina

    void Start() {

        // rigid body used for in-engine physics 
        rb = GetComponent<Rigidbody>();
        dashing = false;

        // gravity is intended to be used for this object
        rb.useGravity = true;
        Physics.gravity = new Vector3(0f, -10.0f, 0f);

    }

    void FixedUpdate() {

        // get normalized camera forward direction. Ignor Y transformation
        Vector3 camDir = Camera.main.transform.forward;
        camDir.y = 0;
        camDir.Normalize();

        // get camera's righthand direction for move perpendicular to camera
        Vector3 camRight = new Vector3(camDir.z, 0f, -camDir.x);
        
        // the new velocity to be applied
        Vector3 newVel = Vector3.zero;

        // calculate movement transformations
        // (apply only if not dashing currently)
        if (!dashing) {

            // Get movement inputs
            newVel += Input.GetAxis("Horizontal") * camRight * speed;
            newVel += Input.GetAxis("Vertical") * camDir * speed;

            // check input for "Jump" and execute if grounded
            if ((Input.GetAxis("Jump") == 1) && grounded) {
                // shoot velocity upwards. 
                rb.velocity = new Vector3(0.0f, Input.GetAxis("Jump") * jump, 0.0f);
            }

            // apply new velocity
            transform.position += (newVel * Time.fixedDeltaTime);

            // rotate rigid body to face direction they are moving
            if (newVel != Vector3.zero) {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(newVel),
                    Time.deltaTime * rotationSpeed
                    );
            }

        }

    }

	// on 'entering' a collision: grounded = true
	void OnCollisionEnter(Collision collisionInfo){
        if(collisionInfo.gameObject.CompareTag("Floor"))
            grounded = true;
	}
	
    // on 'exiting' a collision: grounded = true
	void OnCollisionExit(Collision collisionInfo){
        if(collisionInfo.gameObject.CompareTag("Floor"))
		    grounded = false;
	}

   
    
    //Getters and setters
    public void setDashing(bool input) {
        dashing = input;
    }

    public bool isDashing() {
        return dashing;
    }
    
    public float getSpeed() {
        return speed;
    }

    public float getRotationSpeed() {
        return rotationSpeed;
    }
}