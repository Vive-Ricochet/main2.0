using UnityEngine;
using System.Collections;

public class  ProjectileMaker : MonoBehaviour {

    [SerializeField] private string pickupTrigger = "Rightarm_P1";
    [SerializeField] private string throwTrigger = "Dash_P1";
    [SerializeField] private GameObject otherPlayer;


    private GameObject currentProjectile = null;

    private bool canPickUp = false;
    private bool isThrowing = false;

    private string projectileName = "Projectile Object";

	// Use this for initialization
	void Start () {
        buildNewProjectile();
	}
	
	// Update is called once per frame
	void Update () {

        canPickUp = Input.GetButton(pickupTrigger);

        if (Input.GetAxis(throwTrigger) == 1) {
            isThrowing = true;
            spinBall();
        }

        if (isThrowing && Input.GetAxis(throwTrigger) != 1) {
            isThrowing = false;
            throwBall();
        }

	}

    // How to build a new projectile
    void buildNewProjectile() {

        // Create a game object with the necessary values and scripts
        GameObject projectile = new GameObject(projectileName);
        projectile.AddComponent<ProjectileProperties>();
        projectile.AddComponent<SphereCollider>();
        projectile.AddComponent<Rigidbody>();


        // Set initial values (transforms, radius, etc.) of the ProjectileProperties
        projectile.transform.parent = this.transform;
        projectile.transform.rotation = this.transform.rotation;
        projectile.GetComponent<ProjectileProperties>().setPosition(this.transform.position + new Vector3(0, 2, 0));

        currentProjectile = projectile;
    }

    /****** APPENDING ITEMS TO PLAYER'S NODES *******/
    // On colliding with pickup item
    /*** must be trigger enabled ***/
    void OnTriggerEnter(Collider other) {

        if (canPickUp) {

                if (other.gameObject.CompareTag("Pickup") && other.gameObject.GetComponent<PickupProperties>().isPickupable()) {

                    if (currentProjectile == null) {
                        buildNewProjectile();
                    }

                    GameObject otherObject = other.gameObject;
                    Transform thisTransform = currentProjectile.transform;
                    ProjectileProperties thisProjectile = currentProjectile.GetComponent<ProjectileProperties>();

                    currentProjectile.GetComponent<ProjectileProperties>().appendItem(otherObject);
                    currentProjectile.transform.position += new Vector3(0f, 0.02f, 0f); 
                }
        }
    }

    // Spin a ball for some reason
    void spinBall() {
        if (currentProjectile != null) {
            currentProjectile.transform.Rotate(Vector3.right * 15);
        }
    }

    // Throw a ball
    void throwBall() {
        if (currentProjectile != null) {

            currentProjectile.GetComponent<Rigidbody>().isKinematic = false;
            currentProjectile.GetComponent<SphereCollider>().isTrigger = false;
            currentProjectile.transform.parent = null;

            Vector3 heading = otherPlayer.transform.position - currentProjectile.transform.position;

            currentProjectile.GetComponent<Rigidbody>().velocity = heading.normalized * 10;
            currentProjectile = null;
            buildNewProjectile();
        }
    }
}
