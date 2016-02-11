using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PickupNode : MonoBehaviour {

    // private fields
	private ArrayList myProps = new ArrayList();
	private ArrayList myItems = new ArrayList();
	private float totalAtk;
	private float totalDef;

	//Getters
    public Transform getTransform() {
        return this.transform;
    }

	public float getNodeAtk(){
		return totalAtk;
	}

	public float getNodeDef(){
		return totalDef;
	}

	public ArrayList getNodeProps(){
		return myProps;
	}

	public ArrayList getNodeItems(){
		return myItems;
	}

	//Setters
    public void setPosition(Transform t) {
        this.transform.position = t.position;
    }

	public void updateAtk(float val){
		totalAtk = val;
	}

	public void updateDef(float val){
		totalDef = val;
	}

	public void updateProps(ArrayList props){
		foreach(var element in props){
			if (!myProps.Contains (element)) {
				myProps.Add (element);
			}
		}
	}

	public void updateItems(GameObject item){
		myItems.Add (item);
	}
}