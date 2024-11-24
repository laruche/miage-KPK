using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyHealth : MonoBehaviour
{
    public int maxLife = 50;
    public Animator animator;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(maxLife <= 0)
        {
            if(isDead)
            {
                return;
            } else {
                isDead = true;
                animator.SetTrigger("dead");
                Destroy(gameObject, 1.5f);
            }
            
        }
        
    }

    public void TakeDamage(int damage)
    {
        
        // on récupere l'info si l'ennemi est caché
        bool isHided = GetComponent<EnnemyPatrol>().isHided;
        // on log l'info
        Debug.Log($"isHided: {isHided}");
        // si l'ennemi est caché, on ne prend pas de dégats
        if(isHided)
        {
            return;
        } else {
            maxLife -= damage;
            // sinon on prend des dégats
            Debug.Log($"Ennemy take {damage} damage");
        }
        // on appelle la fonction Hide() de EnnemyPatrol
        if(maxLife > 0)
        {
            GetComponent<EnnemyPatrol>().Hide();
        } else {
            // on set hided à false
            GetComponent<EnnemyPatrol>().isHided = true;
        }
    }
}
