using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {

	// Check if player has been hit by a moving projectile
    void OnCollisionEnter(Collision other) {

        // The agent is a projectile
        if (other.gameObject.GetComponent<ProjectileProperties>() != null) {
            if (other.gameObject.GetComponent<ProjectileProperties>().inMotion && !GetComponent<Parry>().canParry) {
                
                // Affect the player
                hitPlayer();

                // Split up the incoming object and scatter
                /*for (int i = 0; i < other.transform.childCount; i++) {

                    Transform child = other.transform.GetChild(i);
                    other.transform.GetChild(i).parent = null;
                    StillProjectile child_properties = child.gameObject.GetComponent<StillProjectile>();
                    if (child_properties != null)
                        child.gameObject.GetComponent<StillProjectile>().makePickupable(true);
                }*/

                other.rigidbody.velocity = new Vector3(0, 50f, 0);
            }
        }
    }

    // The player has been it
    void hitPlayer() {
        print("Player hit!");
    }
}
