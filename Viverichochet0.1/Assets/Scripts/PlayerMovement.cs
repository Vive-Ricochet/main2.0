using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    // private fields editable by inspector
    [SerializeField] private float speed = 20;  // player movement speed
    [SerializeField] private float jump = 5;   // player intitial jump velocity
    [SerializeField] private float rotationSpeed = 100; // player rotation speed
	[SerializeField] private Camera PlayerCamera;

    // private fields
    PlayerAccesor myStats;
    private bool grounded;
    private Rigidbody rb;

    public bool dashing;
    private bool canDash = false;//to be used with stamina
	private bool charging;

    // public fields
    public string Horizontal;
    public string Vertical;

    void Start() {

        // initialize my accessor
        myStats = GetComponent<PlayerAccesor>();
        myStats.setDashing(false);
        myStats.setCharging(false);

        // rigid body used for in-engine physics 
        rb = GetComponent<Rigidbody>();
        dashing = false;
		charging = false;


		
        // gravity is intended to be used for this object
        rb.useGravity = true;
        Physics.gravity = new Vector3(0f, -30.0f, 0f);

    }

    void FixedUpdate() {

        //print(dashing);

        // get normalized camera forward direction. Ignor Y transformation
		Vector3 camDir = PlayerCamera.transform.forward;
        camDir.y = 0;
        camDir.Normalize();

        // get camera's righthand direction for move perpendicular to camera
        Vector3 camRight = new Vector3(camDir.z, 0f, -camDir.x);
        
        // the new velocity to be applied
        Vector3 newVel = Vector3.zero;

        // calculate movement transformations
        // (apply only if not dashing currently)
        //if (dashing==false && charging == false) {
        if ( myStats.isDashing() == false && myStats.isCharging() == false) {


            // Get movement inputs
            newVel += Input.GetAxis(Horizontal) * camRight * speed;
			newVel += Input.GetAxis(Vertical) * camDir * speed;

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
	
	public void setCharging(bool input){
		charging = input;
	}
	
	public bool isCharging(){
		return charging;
    }
    public float getSpeed() {
        return speed;
    }

    public float getRotationSpeed() {
        return rotationSpeed;
    }

    public Camera getPlayerCamera() {
        return PlayerCamera;
    }
	
}