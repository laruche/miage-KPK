using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject ennemy;
    public GameObject graphicsEnnemy;
    public int damage = 20;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // on log le message
            Debug.Log("Player hit the weak spot");
            // on récupere le script EnnemyHealth de l'ennemi 
            // on appelle la fonction TakeDamage() de EnnemyHealth
            graphicsEnnemy.GetComponent<EnnemyHealth>().TakeDamage(damage);
        }
    }
}
