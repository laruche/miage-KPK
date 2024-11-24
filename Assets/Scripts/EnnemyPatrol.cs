using UnityEngine;
using System.Collections;


public class EnnemyPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    public SpriteRenderer spriteRenderer;
    private Transform target;
    public Animator animator;
    private int destPoint = 0;
    public int damageOnCollision = 10;

    public bool isHideSystem = false;
    public bool isHided = false;
    

    // Start is called before the first frame update
    void Start()
    {
        if (waypoints.Length > 0)
        {
            target = waypoints[0];
        }
        else
        {
            Debug.LogError("Waypoints array is empty. Please assign waypoints in the inspector.");
            Debug.Log($"object name: {this.gameObject.transform.name}",this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isHided)
        {
            return;
        }
        // Move the ennemy towards the target
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // If the ennemy is close enough to the target, move to the next one
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

    }

    public void Hide()
    {
        if (isHideSystem)
        {
            isHided = true;
            // gameObject.SetActive(false);
            animator.SetTrigger("isAttacked");
            StartCoroutine(HideTimer());
        }
    }



    //Give damage to the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the ennemy is hided, don't give damage
        if (isHided)
        {
            return;
        }
        if (collision.transform.CompareTag("Player"))
        {
            PlayerLife playerLife = collision.transform.GetComponent<PlayerLife>();
            playerLife.TakeDamage(damageOnCollision);
        }
    }

    // Wait for 2 second and not hiding anymore
    public void Unhide()
    {
        animator.SetTrigger("UnHide");
        StartCoroutine(NOTHideAnymore());
    }

    public void NotHide()
    {
        animator.SetTrigger("Patrol");
        isHided = false;

        // gameObject.SetActive(true);

    }

    public IEnumerator HideTimer()
    {
        yield return new WaitForSeconds(2f);
        Unhide();
    }

    public IEnumerator NOTHideAnymore()
    {
        yield return new WaitForSeconds(1f);
        NotHide();
    }
    
}
