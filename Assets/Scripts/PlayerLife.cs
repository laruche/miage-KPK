using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;


public class PlayerLife : MonoBehaviour
{
    public int maxLife = 100;
    public int currentLife;
    public Animator animator;

    public LifeBar lifeBar;

    public bool isInvincible = false;
    public SpriteRenderer graphics;
    public float timeFlash = 0.1f;
    public float timeInvincible = 2f;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
        lifeBar.SetMaxLife(maxLife);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // if(Input.GetKeyDown(KeyCode.H))
        // {
        //     TakeDamage(20);
        // }

        if(currentLife <= 0)
        {
            Debug.Log("Player is dead");
            animator.SetBool("isDead", true);
            
            // on appel la fonction Die() de PlayerMovement dans 2f 
            Invoke("Die", 2f);

        }
        
        
    }

    public void Die()
    {
        SceneManager.LoadScene(1);
    }

    public void TakeDamage(int damage)
    {
        if(!isInvincible)
        {
            currentLife -= damage;
            lifeBar.SetLife(currentLife);
            isInvincible = true;
            StartCoroutine(InvincibleFlash());
            StartCoroutine(InvincibleTimer());
        }
    }

    public IEnumerator InvincibleFlash()
    {
        while(isInvincible)
        {
            graphics.color = new Color(1f,1f,1f,0f);
            yield return new WaitForSeconds(timeFlash);
            graphics.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(timeFlash);
        }
    }

    public IEnumerator InvincibleTimer()
    {
        yield return new WaitForSeconds(timeInvincible);
        isInvincible = false;
    }
}
