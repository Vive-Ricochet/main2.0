using UnityEngine;
using System.Collections;

public class  ProjectileMaker : MonoBehaviour {

    [SerializeField] private string pickupTrigger = "Rightarm_P1";
    [SerializeField] private string throwTrigger = "Dash_P1";
    [SerializeField] private GameObject otherPlayer;


    private GameObject currentProjectile = null;

    private float gravity = 50;
    private bool canPickUp = false;
    private bool isThrowing = false;
    private float throwSpeed = 60;

    private string projectileName = "Projectile Object";

	// Use this for initialization
	void Start () {
        //buildNewProjectile();
        Physics.gravity = Vector3.down * gravity;
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
    GameObject buildNewProjectile() {

        // Create a game object with the necessary values and scripts
        GameObject projectile = new GameObject(projectileName);
        projectile.AddComponent<ProjectileProperties>();
        projectile.GetComponent<ProjectileProperties>().Init();
    
        projectile.AddComponent<PoleProperties>();
        projectile.tag = "Pickup";

        // Set initial values (transforms, radius, etc.) of the ProjectileProperties
        projectile.transform.parent = this.transform;
        projectile.transform.rotation = this.transform.rotation;
        projectile.GetComponent<ProjectileProperties>().setPosition(this.transform.position + new Vector3(0, 2, 0));

        //currentProjectile = projectile;

        return projectile;
    }

    /****** APPENDING ITEMS TO PLAYER'S NODES *******/
    // On colliding with pickup item
    /*** must be trigger enabled ***/
    void OnTriggerEnter(Collider other) {

        if (canPickUp) {

                if (other.gameObject.CompareTag("Pickup") && other.gameObject.GetComponent<PickupProperties>().isPickupable()) {

                    if (currentProjectile == null) {
                        currentProjectile = buildNewProjectile();
                    }
      

                    GameObject otherObject = other.gameObject;
                    Transform thisTransform = currentProjectile.transform;
                    ProjectileProperties thisProjectile = currentProjectile.GetComponent<ProjectileProperties>();

                    currentProjectile.GetComponent<ProjectileProperties>().appendItem(otherObject);

                    float amount = currentProjectile.GetComponent<ProjectileProperties>().getRadius() + GetComponent<BoxCollider>().size.y / 2;
                    print(amount);
                    currentProjectile.transform.localPosition = new Vector3(0f, amount, 0f); 
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

            // getting initial projectile references
            Vector3 projectilePosition = currentProjectile.transform.position;
            Vector3 heading = otherPlayer.transform.position - currentProjectile.transform.position; // the vector between this player and target
            ///print(heading);


            // jump the projectile through potential clipping objects before making active
            Collider[] checkResult = Physics.OverlapSphere(projectilePosition, currentProjectile.GetComponent<ProjectileProperties>().getRadius());
            while (checkResult.Length != 0) {

                print("Current position: " + currentProjectile.transform.position);
                print("Move this way: " + heading);

                currentProjectile.transform.position = currentProjectile.transform.position + heading.normalized + new Vector3(0, 0.1f, 0);

                print("New position: " + currentProjectile.transform.position);

                projectilePosition = currentProjectile.transform.position;
                checkResult = Physics.OverlapSphere(projectilePosition, currentProjectile.GetComponent<ProjectileProperties>().getRadius());

                print("Any collisions?: " + checkResult);
            }




            // projectile property calculations
            float distance = new Vector2(heading.x, heading.z).magnitude; // the horizontal distance between this player and target
            float deltaHeight = currentProjectile.transform.position.y - otherPlayer.transform.position.y - 7.5f; // projectile's relative transform height
            float upwardsMagnitude = ((-deltaHeight * throwSpeed) / distance) - ((gravity * distance) / (2 * throwSpeed)); // projectile "y" velocity component
            //upwardsMagnitude *= 0.75f;

            print(upwardsMagnitude);

            print("Height difference: " + deltaHeight);
            print("Horizontal difference: " + distance);


            currentProjectile.transform.parent = null;
            currentProjectile.GetComponent<Rigidbody>().isKinematic = false;
            currentProjectile.GetComponent<Rigidbody>().detectCollisions = true;
            currentProjectile.GetComponent<SphereCollider>().isTrigger = false;

            Vector3 newVelocity = new Vector3(heading.x, 0f, heading.z).normalized * throwSpeed + new Vector3(0, -upwardsMagnitude, 0);
            currentProjectile.GetComponent<Rigidbody>().velocity = newVelocity;
            currentProjectile = null;
        }
    }
}
