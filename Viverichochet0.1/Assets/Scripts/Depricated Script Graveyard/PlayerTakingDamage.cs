using UnityEngine;
using System.Collections;

public class PlayerTakingDamage : MonoBehaviour {

    // Fly myself backwards and twirl and shit
    public void FlyBackwards(Transform influencingTransform) {

        // Get his and my rotation. Compare them and save the difference
        Vector3 myRotation = this.transform.rotation.eulerAngles;
        Vector3 hisRotation = influencingTransform.transform.rotation.eulerAngles;
        Vector3 diffRotation = myRotation - hisRotation;
        if (diffRotation.y > 0)
            diffRotation.y = -359 + diffRotation.y;

        // Hit me from behind
        if (diffRotation.y > -45 || diffRotation.y <= -315) {
            //print("Behind?\n");
            this.transform.eulerAngles = hisRotation;

        // Hit me from the left
        }else if(diffRotation.y <= -45 && diffRotation.y > -135) {
            //print("Left?\n");
            this.transform.eulerAngles = hisRotation + new Vector3(0f, -90f, 0f);
            
        // Hit me from the front
        }else if (diffRotation.y <= -135 && diffRotation.y > -225) {
            //print("Front?\n");
            this.transform.eulerAngles = hisRotation + new Vector3(0f, -180f, 0f);

        // Hit me from the right
        } else if (diffRotation.y <= -225 && diffRotation.y > -315) {
            //print("Right?\n");
            this.transform.eulerAngles = hisRotation + new Vector3(0f, +90f, 0f);

        }

    }
}
