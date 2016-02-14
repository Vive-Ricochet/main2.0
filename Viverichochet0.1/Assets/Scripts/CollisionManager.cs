using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {

	// Private fields editable by inspector
    [SerializeField] private GameObject Player01_obj;
    [SerializeField] private GameObject Player02_obj;

    // Public fields
    private PlayerAccesor Player01;
    private PlayerAccesor Player02;

    // On scene load, do this
    void Start() {

        // Get player respective accesors
        if (Player01_obj != null && Player02_obj != null) {
            Player01 = Player01_obj.GetComponent<PlayerAccesor>();
            Player02 = Player02_obj.GetComponent<PlayerAccesor>();
        }

    }

    // On every update, do this
    void FixedUpdate() {
        // Check for collision between the players
        if (Player01 != null && Player02 != null) {
            // Player 01 hit innocent Player 02
            if (Player01.isDashing() && ! Player02.isDashing()) {
                if (Player01.isColliding()) {
                    CollidePlayers(Player01.getTransform(), Player02.getTransform());
                }
            }

            // Player 02 hit innocent Player 01
            if (Player02.isDashing() && !Player01.isDashing()) {
                if (Player02.isColliding()) {
                    CollidePlayers(Player02.getTransform(), Player01.getTransform());
                }
            }

            // Player 01 and 02 hit eachother (equally guilty)
            /*** INSERT FUNCTION HERE ***/
        }
    }


    ///////////////// FUNCTIONS AND JUNK FOR COLLISIONS ///////////////////////


    // WHEN A COLLISION IS ONE-SIDED
    void CollidePlayers(Transform instigator, Transform victim) {

        // First reinitialize player states so collisions don't reapply over and over
        Player01.resetDash();
        Player02.resetDash();

		// sum up instigator's ATK
		float attack = instigator.GetComponent<PlayerNodesManager>().rightarmAttack() +
			instigator.GetComponent<PlayerNodesManager>().leftarmAttack();

		// apply durabillity mod to instigator's items;
		instigator.GetComponent<PlayerNodesManager>().effectRightarmDur();
		instigator.GetComponent<PlayerNodesManager>().effectLeftarmDur();

		// the victim's accessor
		PlayerAccesor victim_accessor = victim.GetComponent<PlayerAccesor>();


        // Get his (instigator) and my (victim) rotation. Compare them and save the difference
        Vector3 myRotation = victim.rotation.eulerAngles;
        Vector3 hisRotation = instigator.rotation.eulerAngles;
        Vector3 diffRotation = myRotation - hisRotation;
        if (diffRotation.y > 0)
            diffRotation.y = -359 + diffRotation.y;

        // Hit me from behind
        if (diffRotation.y > -45 || diffRotation.y <= -315) {
            //print("Behind?\n");
			victim_accessor.damagePlayer(attack);
            victim.eulerAngles = hisRotation;

            // Hit me from the left
        } else if (diffRotation.y <= -45 && diffRotation.y > -135) {
            //print("Left?\n");
            victim.eulerAngles = hisRotation + new Vector3(0f, -90f, 0f);

            // Hit me from the front
        } else if (diffRotation.y <= -135 && diffRotation.y > -225) {
            //print("Front?\n");
            victim.eulerAngles = hisRotation + new Vector3(0f, -180f, 0f);

            // Hit me from the right
        } else if (diffRotation.y <= -225 && diffRotation.y > -315) {
            //print("Right?\n");
            victim.eulerAngles = hisRotation + new Vector3(0f, +90f, 0f);
        }

        // FLY BABY
        BlowBack(victim.gameObject, instigator.forward);

		//sounds
		victim.GetComponent<AudioSource> ().Play();

    }

    // Fly a player backwards
    void BlowBack(GameObject target, Vector3 direction) {

        // how hard it hits
        float intensity = 12f;
        direction.y = 1;

        target.GetComponent<Rigidbody>().AddForce(direction * intensity, ForceMode.Impulse);
    }
}
