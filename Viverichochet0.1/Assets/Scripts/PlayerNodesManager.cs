using UnityEngine;
using System.Collections;

public class PlayerNodesManager : MonoBehaviour {

    // private fields editable by inspector
    /***** BUTTONS AND SHIT ******/
    [SerializeField] private string leftArmTrigger = "Leftarm_P1";
    [SerializeField] private string rightArmTrigger = "Rightarm_P1";
    [SerializeField] private string buttonLeftArmN = "LeftArmNorth_P1";
    [SerializeField] private string buttonLeftArmW = "LeftArmWest_P1";
    [SerializeField] private string buttonRightArmN = "RightArmNorth_P1";
    [SerializeField] private string buttonRightArmE = "RightArmEast_P1";


	// private fields
    /***** NODE MANAGEMENT ******/
    // the nodes
    PickupNode leftArmNode;
    PickupNode rightArmNode;

    // node properties
    private string rightArmPosition;
    private string leftArmPosition;
    private bool canPickUpL = false;
    private bool canPickUpR = false;

    bool LF = true;
    bool RF = true;
    bool RS = false;
    bool LS = false;


    Animator animate;
	PlayerItemManager itemManager;


    // On scene load, do this...
    void Start() {

        leftArmNode  = transform.Find("Left Arm Node").GetComponent<PickupNode>();
        rightArmNode = transform.Find("Right Arm Node").GetComponent<PickupNode>();

        leftArmNode.setPosition(transform.Find("Left Arm Node"));
        rightArmNode.setPosition(transform.Find("Right Arm Node"));

        leftArmPosition  = "North";
        rightArmPosition = "North";


        animate = GetComponent<Animator>();
		itemManager = GetComponent<PlayerItemManager> ();

        // NOTE: 
        //      "Node Position" is really only the coordinates of game objects called
        // "Left Arm Node", "Right Arm Node" and such. The Nodes themselves are a part
        // of this own manager's abstract class.
        //
        //      In other words, no properties are ever allocated to those node objects in the
        // game space. They are only the points at which
    }

    void Update() {

        if(Input.anyKeyDown) {

            // on pickup on pickUpTrigger activating button
            if (Input.GetButton(leftArmTrigger)) canPickUpL = true;
                else canPickUpL = false;

            if (Input.GetButton(rightArmTrigger)) canPickUpR = true;
                else canPickUpR = false;
             
            // check for moving arms to positions
            if (Input.GetButton(buttonLeftArmN))
                moveLeftArmN();
            if (Input.GetButton(buttonLeftArmW))
                moveLeftArmW();
            if (Input.GetButton(buttonRightArmN))
                moveRightArmN();
            if (Input.GetButton(buttonRightArmE))
                moveRightArmE();
        }
    }

    /****** APPENDING ITEMS TO PLAYER'S NODES *******/
    // On colliding with pickup item
    /*** must be trigger enabled ***/
    void OnTriggerEnter(Collider other) {

        if (canPickUpL || canPickUpR) {

            // if other object is a "Pickup":
            if (other.gameObject.CompareTag("Pickup")) {

                GameObject otherObject = other.gameObject;
                // append item to a node representing 
				if (canPickUpL) {
					AppendItem (otherObject, "Left Arm Node");
				}
				if (canPickUpR) {
					AppendItem (otherObject, "Right Arm Node");
				}

            }
        }
    }

    void AppendItem(GameObject otherObject, string nodeToAppendTo) {

        // disable pickup's rigid body to give it that ephemeral feel
        // also make it kinematic to disable effective forces
        otherObject.GetComponent<Rigidbody>().detectCollisions = false;
        otherObject.GetComponent<Rigidbody>().isKinematic = true;

        // append object to player node, make it a child of that node
        otherObject.transform.parent = this.transform.Find(nodeToAppendTo);
        otherObject.transform.position = this.transform.Find(nodeToAppendTo).position;
       
        // obtain pickup's properties
		var pickupProperties = otherObject.GetComponent<PickupProperties>().GetProperties();
		otherObject.transform.GetComponentInParent<PickupNode>().updateProps (pickupProperties);
		otherObject.transform.GetComponentInParent<PickupNode>().updateItems (otherObject);
		if (otherObject.transform.GetComponentInParent<PickupNode>() != null) {
			print(otherObject.transform.GetComponentInParent<PickupNode>().getNodeAtk());
			print (otherObject.transform.GetComponentInParent<PickupNode> ().getNodeProps ());

        }
    }


    /****** MOVING PLAYER'S ARMS AND SHIT *******/
    // To move arms around the player
    void moveLeftArmN() {
        float old_y = leftArmNode.getTransform().localPosition.y;
        leftArmPosition = leftArmPosition.Replace("East", "North")
                                         .Replace("South", "North")
                                         .Replace("West", "North");
        leftArmNode.getTransform().localPosition = new Vector3(-0.3f, old_y, 1.0f);
        LF = true;
        LS = false;
        animate.SetBool("LF", LF);
        animate.SetBool("LS", LS);
    }

    void moveLeftArmE() {
        float old_y = leftArmNode.getTransform().localPosition.y;
        leftArmPosition = leftArmPosition.Replace("North", "East")
                                         .Replace("South", "East")
                                         .Replace("West", "East");
        leftArmNode.getTransform().localPosition = new Vector3(1.0f, old_y, 0.3f);
    }

    void moveLeftArmS() {
        float old_y = leftArmNode.getTransform().localPosition.y;
        leftArmPosition = leftArmPosition.Replace("East", "South")
                                         .Replace("North", "South")
                                         .Replace("West", "South");
        leftArmNode.getTransform().localPosition = new Vector3(-0.3f, old_y, -1.0f);
    }

    void moveLeftArmW() {
        float old_y = leftArmNode.getTransform().localPosition.y;
        leftArmPosition = leftArmPosition.Replace("East", "West")
                                         .Replace("South", "West")
                                         .Replace("North", "West");
        leftArmNode.getTransform().localPosition = new Vector3(-1.0f, old_y, -0.3f);
        LS = true;
        LF = false;
        animate.SetBool("LF", LF);
        animate.SetBool("LS", LS);
    }

    void moveRightArmN() {
        float old_y = rightArmNode.getTransform().localPosition.y;
        rightArmPosition = rightArmPosition.Replace("East", "North")
                                           .Replace("South", "North")
                                           .Replace("West", "North");
        rightArmNode.getTransform().localPosition = new Vector3(0.3f, old_y, 1.0f);
        RF = true;
        RS = false;
        animate.SetBool("RF", RF);
        animate.SetBool("RS", RS);
    }

    void moveRightArmE() {
        float old_y = rightArmNode.getTransform().localPosition.y;
        rightArmPosition = rightArmPosition.Replace("North", "East")
                                           .Replace("South", "East")
                                           .Replace("West", "East");
        rightArmNode.getTransform().localPosition = new Vector3(1.0f, old_y, -0.3f);
        RS = true;
        RF = false;
        animate.SetBool("RF", RF);
        animate.SetBool("RS", RS);
    }

    void moveRightArmS() {
        float old_y = rightArmNode.getTransform().localPosition.y;
        rightArmPosition = rightArmPosition.Replace("East", "South")
                                           .Replace("North", "South")
                                           .Replace("West", "South");
        rightArmNode.getTransform().localPosition = new Vector3(0.3f, old_y, -1.0f);
    }

    void moveRightArmW() {
        float old_y = rightArmNode.getTransform().localPosition.y;
        rightArmPosition = rightArmPosition.Replace("East", "West")
                                           .Replace("South", "West")
                                           .Replace("North", "West");
        rightArmNode.getTransform().localPosition = new Vector3(-1.0f, old_y, 0.3f);
    }

    // To move arms "up" and "down"
    /*void TogglePlayerLeftArm() {
                
        float old_x = leftArmNode.getTransform().localPosition.x;
        float old_z = leftArmNode.getTransform().localPosition.z;

        // first, update the player state
        if (leftArmPosition.Contains("Up")) {
            leftArmPosition = leftArmPosition.Replace("Up", "Down");
            leftArmNode.getTransform().localPosition = new Vector3(old_x, -0.33f, old_z);
        } else {
            leftArmPosition = leftArmPosition.Replace("Down", "Up");
            leftArmNode.getTransform().localPosition = new Vector3(old_x, 1.0f, old_z);

        }

    }*/

    /*void TogglePlayerRightArm() {

        float old_x = rightArmNode.getTransform().localPosition.x;
        float old_z = rightArmNode.getTransform().localPosition.z;

        // first, update the player state
        if (rightArmPosition.Contains("Up")) {
            rightArmPosition = rightArmPosition.Replace("Up", "Down");
            rightArmNode.getTransform().localPosition = new Vector3(old_x, -0.33f, old_z);
        } else {
            rightArmPosition = rightArmPosition.Replace("Down", "Up");
            rightArmNode.getTransform().localPosition = new Vector3(old_x, 1.0f, old_z);

        }
    }*/
}

