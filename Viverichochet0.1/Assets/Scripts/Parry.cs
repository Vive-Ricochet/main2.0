using UnityEngine;
using System.Collections;

public class Parry : MonoBehaviour {

    [SerializeField] private string parryButton = "Leftarm_P1";

    private int startFrame;
    private int parryProgress;
    private int parryWindow = 20;
    private Collider parryBox;

    private bool parrying = false;
    public bool canParry = false;

    private Vector3 playerScreenPos;
    private Camera playerCamera;

    [SerializeField] private Texture parrySprite;

	// Use this for initialization
	void Start () {
        
        foreach(BoxCollider box in GetComponents<BoxCollider>()) {
            if (box.isTrigger)
                parryBox = box;
        }

        playerCamera = GetComponent<PlayerMovement>().PlayerCamera;
    }
	
	// Update is called once per engine update (not frame refresh)
	void Update () {

        if (Input.GetButton(parryButton) && !parrying) {
            parrying = true;
            startFrame = Time.frameCount;
            GetComponent<PlayerMovement>().canMove = false;
        }

        if (parrying && Time.frameCount - startFrame < parryWindow) {
            canParry = true;
            playerScreenPos = playerCamera.WorldToScreenPoint(transform.position);
            
        } else {
            canParry = false;
            parrying = false;
            GetComponent<PlayerMovement>().canMove = true;
        }
    }

    void OnGUI() {
        if (parrySprite && canParry) {
            GUI.DrawTexture(new Rect(playerScreenPos.x - 38, Screen.height - playerScreenPos.y - 38, 80, 80), parrySprite);
        }
    }


    void OnTriggerEnter(Collider other) {
        ProjectileProperties projectile = other.gameObject.GetComponent<ProjectileProperties>();
        if (projectile != null) {
            if (projectile.inMotion && canParry) {
                parry(other);
            }
        }
    }

    private void parry(Collider other) {

        GetComponent<ProjectileMaker>().appendItem(other);
    }
}
