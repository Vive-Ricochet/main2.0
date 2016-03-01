using UnityEngine;
using System.Collections;

public class CameraTargetManager : MonoBehaviour {
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
    }

    void FixedUpdate()
    {
        //get the average vector from both targets
        float aX = (target1.position.x + target2.position.x) * 0.5f;
        float aY = (target1.position.y + target2.position.y) * 0.5f;
        float aZ = (target1.position.z + target2.position.z) * 0.5f;

        Vector3 position = new Vector3(aX, aY, aZ);
        transform.position = position;
    }
}
