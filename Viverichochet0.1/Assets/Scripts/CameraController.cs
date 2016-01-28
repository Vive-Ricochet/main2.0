using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    // private fields editable by inspector
    [SerializeField] private Transform target;              // the target object for this camera
    [SerializeField] private float verticalOffset = 1.7f;   // how high above the target the camera sits
    [SerializeField] private float distance = 6.0f;         // distance from target
    [SerializeField] private float xSpeed = 200.0f;         // camera X/Y sensitivity
    [SerializeField] private float ySpeed = 200.0f;

    // private fields
    private float yMinLimit = -18.0f;   // lower and upper bounds of up/down rotation
    private float yMaxLimit = 80.0f;
    private float xDeg = 0.0f;
    private float yDeg = 0.0f;

    void LateUpdate() {

        if (!target)
            return;

        // read axis inputs from something (currently the mouse)
        xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit); // Keep up-down rotation within bounds

        // camera rotation
        Quaternion rotation = Quaternion.Euler(yDeg, xDeg, 0);

        // camera position
        Vector3 vTargetOffset = new Vector3(0, -verticalOffset, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * distance + vTargetOffset);

        // apply transformations
        transform.rotation = rotation;
        transform.position = position;
    }

    // Mathf.Clamp functionality floor'd within 360 degree angles
    static float ClampAngle(float angle, float min, float max) {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
