using UnityEngine;
using System.Collections;

public class PlayerAccesor : MonoBehaviour {


    /***** MISC. PLAYER VALUE GETTERS ******/
    // Game object Transform
    public Transform getTransform() {
        return this.transform;
    }

    // Player status Dashing
    public bool isDashing() {
        return GetComponent<PlayerStatus>().isDashing();
    }


    /***** PUBLIC METHODS *****/
    // Apply this objects "damage-self" function
    public void DamageThis(Transform influencingTransform) {
        GetComponent<PlayerTakingDamage>().FlyBackwards(influencingTransform);
    }
}
