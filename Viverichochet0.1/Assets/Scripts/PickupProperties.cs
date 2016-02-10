using UnityEngine;
using System.Collections;

public abstract class PickupProperties : MonoBehaviour {

    // this object's various values
    private float durrability_value;
    private float attack_mod;
    private float defense_mod;
    private float weight_value;

	// create a private list of properties for this object...
    private ArrayList properties = new ArrayList();

    // on load scene
    void Start() {
        // ...
    }

    /////// GETTING VALUES /////////
    public float attackModifier() {
        return attack_mod;
    }

    public float defenseModifier() {
        return defense_mod;
    }

    public float weight() {
        return weight_value;
    }

    public float durrability() {
        return durrability_value;
    }
    /////////////////////////////

    // properties' accessor
    public ArrayList GetProperties() {
        return properties;
    }

    // add a property too this object
    public void AddProperty(string name) {
        properties.Add(name);
    }

    // print properties to console
    public void PrintProperties() {
        foreach (string element in properties) {
            print(element);
        }
    }
	
}