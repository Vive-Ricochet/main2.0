using UnityEngine;
using System.Collections;

public abstract class PickupProperties : MonoBehaviour {

	// create a private list of properties for this object...
    private ArrayList properties = new ArrayList();

    // on load scene
    void Start() {
        // ...
    }

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