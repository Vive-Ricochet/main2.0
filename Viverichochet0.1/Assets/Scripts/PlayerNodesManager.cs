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

    // node game objects' coordinates
    Transform leftArmNodePosition;
    Transform rightArmNodePosition;

    // On scene load, do this...
    void Start() {

        leftArmPosition  = "North";
        rightArmPosition = "North";

        leftArmNodePosition = transform.FindChild("Left Arm Node");
        rightArmNodePosition = transform.FindChild("Right Arm Node");

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
                if (canPickUpL)
                    AppendItem(otherObject, "Left Arm Node");
                if (canPickUpR)
                    AppendItem(otherObject, "Right Arm Node");
            }
        }
    }

    void AppendItem(GameObject otherObject, string nodeToAppendTo) {

        // disable pickup's rigid body to give it that ephemeral feel
        // also make it kinematic to disable effective forces
        otherObject.GetComponent<Rigidbody>().detectCollisions = false;
        otherObject.GetComponent<Rigidbody>().isKinematic = true;

        // append object to player node, make it a child of that node
        otherObject.transform.parent = this.transform.FindChild(nodeToAppendTo);
        otherObject.transform.position = this.transform.FindChild(nodeToAppendTo).position;
       
        // obtain pickup's properties
        PickupProperties pickupProperties = otherObject.GetComponent<PickupProperties>();
        if (pickupProperties != null) {
            print(pickupProperties.GetProperties());
        }
    }


    /****** MOVING PLAYER'S ARMS AND SHIT *******/
    // To move arms around the player
    void moveLeftArmN() {
        float old_y = leftArmNodePosition.localPosition.y;
        leftArmPosition = leftArmPosition.Replace("East", "North")
                                         .Replace("South", "North")
                                         .Replace("West", "North");
        leftArmNodePosition.localPosition = new Vector3(-0.3f, old_y, 1.0f);
    }

    void moveLeftArmE() {
        float old_y = leftArmNodePosition.localPosition.y;
        leftArmPosition = leftArmPosition.Replace("North", "East")
                                         .Replace("South", "East")
                                         .Replace("West", "East");
        leftArmNodePosition.localPosition = new Vector3(1.0f, old_y, 0.3f);
    }

    void moveLeftArmS() {
        float old_y = leftArmNodePosition.localPosition.y;
        leftArmPosition = leftArmPosition.Replace("East", "South")
                                         .Replace("North", "South")
                                         .Replace("West", "South");
        leftArmNodePosition.localPosition = new Vector3(-0.3f, old_y, -1.0f);
    }

    void moveLeftArmW() {
        float old_y = leftArmNodePosition.localPosition.y;
        leftArmPosition = leftArmPosition.Replace("East", "West")
                                         .Replace("South", "West")
                                         .Replace("North", "West");
        leftArmNodePosition.localPosition = new Vector3(-1.0f, old_y, -0.3f);
    }

    void moveRightArmN() {
        float old_y = rightArmNodePosition.localPosition.y;
        rightArmPosition = rightArmPosition.Replace("East", "North")
                                           .Replace("South", "North")
                                           .Replace("West", "North");
        rightArmNodePosition.localPosition = new Vector3(0.3f, old_y, 1.0f);
    }

    void moveRightArmE() {
        float old_y = rightArmNodePosition.localPosition.y;
        rightArmPosition = rightArmPosition.Replace("North", "East")
                                           .Replace("South", "East")
                                           .Replace("West", "East");
        rightArmNodePosition.localPosition = new Vector3(1.0f, old_y, -0.3f);
    }

    void moveRightArmS() {
        float old_y = rightArmNodePosition.localPosition.y;
        rightArmPosition = rightArmPosition.Replace("East", "South")
                                           .Replace("North", "South")
                                           .Replace("West", "South");
        rightArmNodePosition.localPosition = new Vector3(0.3f, old_y, -1.0f);
    }

    void moveRightArmW() {
        float old_y = rightArmNodePosition.localPosition.y;
        rightArmPosition = rightArmPosition.Replace("East", "West")
                                           .Replace("South", "West")
                                           .Replace("North", "West");
        rightArmNodePosition.localPosition = new Vector3(-1.0f, old_y, 0.3f);
    }

    // To move arms "up" and "down"
    /*void TogglePlayerLeftArm() {
                
        float old_x = leftArmNodePosition.localPosition.x;
        float old_z = leftArmNodePosition.localPosition.z;

        // first, update the player state
        if (leftArmPosition.Contains("Up")) {
            leftArmPosition = leftArmPosition.Replace("Up", "Down");
            leftArmNodePosition.localPosition = new Vector3(old_x, -0.33f, old_z);
        } else {
            leftArmPosition = leftArmPosition.Replace("Down", "Up");
            leftArmNodePosition.localPosition = new Vector3(old_x, 1.0f, old_z);

        }

    }*/

    /*void TogglePlayerRightArm() {

        float old_x = rightArmNodePosition.localPosition.x;
        float old_z = rightArmNodePosition.localPosition.z;

        // first, update the player state
        if (rightArmPosition.Contains("Up")) {
            rightArmPosition = rightArmPosition.Replace("Up", "Down");
            rightArmNodePosition.localPosition = new Vector3(old_x, -0.33f, old_z);
        } else {
            rightArmPosition = rightArmPosition.Replace("Down", "Up");
            rightArmNodePosition.localPosition = new Vector3(old_x, 1.0f, old_z);

        }
    }*/
}

public class PickupNode : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}