using UnityEngine;
using System.Collections;

public class PickupNode : MonoBehaviour {

    // private fields
    private ArrayList myItems = new ArrayList();

    public Transform getTransform() {
        return this.transform;
    }

    public void setPosition(Transform t) {
        this.transform.position = t.position;
    }
}