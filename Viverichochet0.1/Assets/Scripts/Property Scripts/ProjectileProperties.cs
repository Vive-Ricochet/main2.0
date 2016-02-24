using UnityEngine;
using System.Collections;

public class ProjectileProperties : MonoBehaviour {


    private float initialRadius = 1;
    private bool canPickUp;
    private SphereCollider collider;
    private Rigidbody rigidbody;
    private int itemCount = 0;

    // On scene load, do this
    void Start() {
        collider  = gameObject.AddComponent<SphereCollider>();
        rigidbody = gameObject.AddComponent<Rigidbody>();

        collider.isTrigger = true;
        collider.radius = initialRadius;
        rigidbody.isKinematic = true;
    }

    // On every chance to update, do this
    void Update() {

    }

    // Set this object's transformations
    public void setPosition(Vector3 t) {
        this.transform.position = t;
    }

	// Set the collision radius of this projectile
    public void setRadius(float f) {
        if (collider != null) {
            collider.radius = f;
        }
    }

    public void appendItem(GameObject otherObject) {

        otherObject.GetComponent<Rigidbody>().detectCollisions = false;
        otherObject.GetComponent<Rigidbody>().isKinematic = true;
        otherObject.GetComponent<PickupProperties>().makePickupable(false);

        otherObject.transform.parent = this.transform;
        otherObject.transform.position = this.transform.position;
        otherObject.transform.rotation = Random.rotation;        

        if (itemCount > 0) {
            
            // randomize about radius
            otherObject.transform.position += new Vector3(0, 0.75f * collider.radius, 0);
            otherObject.transform.RotateAround(this.transform.position, new Vector3 (Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359)), Random.Range(0, 359));

        }

        setRadius(collider.radius + 0.1f);
        itemCount++;
    }
}
