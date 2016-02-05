using UnityEngine;
using System.Collections;

public class PlayerAccesor : MonoBehaviour {

    public void DamageThis(Transform influencingTransform) {

        GetComponent<PlayerTakingDamage>().FlyBackwards(influencingTransform);
    
    }
}
