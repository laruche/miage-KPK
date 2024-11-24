using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMoove : MonoBehaviour
{

    public float speed;
    public Transform startPoint; 
    public Transform endPoint;
    public Transform platform;
    
    int direction = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = currentMovementTarget();

        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)platform.position).magnitude;

        if(distance < 0.1f) {
           direction = direction * -1;
        }
    }

    // move the player when the platform is moving
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("collision");
        if(other.gameObject.tag == "Player") {
            other.transform.SetParent(platform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            other.transform.SetParent(null);
        }
    }

    Vector2 currentMovementTarget() {
        if(direction == 1) {
            return endPoint.position;
        } else {
            return startPoint.position;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPoint.position, 0.5f);
        Gizmos.DrawWireSphere(endPoint.position, 0.5f);
        Gizmos.DrawLine(platform.transform.position, endPoint.position);
        Gizmos.DrawLine(platform.transform.position, startPoint.position);
    }
}
