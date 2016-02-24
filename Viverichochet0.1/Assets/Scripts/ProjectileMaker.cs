using UnityEngine;
using System.Collections;

public class  ProjectileMaker : MonoBehaviour {

    [SerializeField] private string pickupTrigger = "Rightarm_P1";
    private GameObject currentProjectile;
    private bool canPickUp = false;
    private string projectileName = "Projectile Object";

	// Use this for initialization
	void Start () {
        buildNewProjectile();
	}
	
	// Update is called once per frame
	void Update () {
        canPickUp = Input.GetButton(pickupTrigger);
	}

    // How to build a new projectile
    void buildNewProjectile() {

        // Create a game object with the necessary values and scripts
        GameObject projectile = new GameObject(projectileName);
        projectile.AddComponent<ProjectileProperties>();
        //projectile.name = "Projectile Ball";

        // Set initial values (transforms, radius, etc.) of the ProjectileProperties
        projectile.transform.parent = this.transform;
        projectile.GetComponent<ProjectileProperties>().setPosition(this.transform.position + new Vector3(0, 2, 0));

        currentProjectile = projectile;
    }

    /****** APPENDING ITEMS TO PLAYER'S NODES *******/
    // On colliding with pickup item
    /*** must be trigger enabled ***/
    void OnTriggerEnter(Collider other) {

        if (canPickUp && currentProjectile != null) {
            if (other.gameObject.CompareTag("Pickup") && other.gameObject.GetComponent<PickupProperties>().isPickupable()) {
                GameObject otherObject = other.gameObject;
                currentProjectile.GetComponent<ProjectileProperties>().appendItem(otherObject);
            }
        }
    }
}
