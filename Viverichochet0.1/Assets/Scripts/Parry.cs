using UnityEngine;
using System.Collections;

public class Parry : MonoBehaviour {

    [SerializeField]
    private string parryButton = "Rightarm_P1";

    private int startFrame;
    private int parryProgress;
    [SerializeField] private int parryWindow = 20;

    private Collider parryBox;

    private bool parrying = false;
    private bool canParry = false;

	// Use this for initialization
	void Start () {

        foreach(BoxCollider box in GetComponents<BoxCollider>()) {
            if (box.isTrigger)
                parryBox = box;
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton(parryButton) && !parrying) {
            
            parrying = true;
            parry();
        }

    }


    void OnTriggerEnter(Collider other) {
        if (canParry)
            print("I'm hit: "+other);
    }

    private void parry() {

        startFrame = Time.frameCount;
        if (Time.frameCount - startFrame < parryWindow) {

            canParry = true;
        } else {

            canParry = false;
            parrying = false;
        }
    }
}
