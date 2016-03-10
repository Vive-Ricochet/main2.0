using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    // private fields editable by inspector
    [SerializeField]
    private Transform target;              // the target object for this camera
    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;
    [SerializeField]
    private float minDistance = 15;
    [SerializeField]
    private float distanceBias = 0.75f;    //How much the distance affects the zoom affect

    private float old_dis;
    private float distance;

    // private fields
    private float xDeg = 0.0f;
    private float yDeg = 0.0f;

    void Start()
    {
        old_dis = Vector3.Distance(player1.position, player2.position);
    }

    void LateUpdate()
    {

        if (!target)
            return;

        distance = Mathf.Lerp(old_dis, Vector3.Distance(player1.position, player2.position), Time.deltaTime * 2.0f);
        Camera.main.fieldOfView = distance * distanceBias + minDistance;
        old_dis = distance;

        // camera rotation
        Quaternion rotation = Quaternion.Euler(yDeg, xDeg, 0);


        // apply rotation
        transform.LookAt(target);
    }

}
