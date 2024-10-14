using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;
    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Define the min and max values for X and Y
        float minX = -71f;
        float maxX = 108f;
        float minY = 4f;
        float maxY = 12f;

        // Calculate the target position
        Vector3 targetPosition = new Vector3(player.transform.position.x + posOffset.x, player.transform.position.y + posOffset.y, posOffset.z);

        // Clamp the target position to the min and max values
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // Smoothly move the camera to the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, timeOffset);
    }
}
