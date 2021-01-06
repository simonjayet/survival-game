using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    //
    public int maxHealth = 100;
    public int currentHealth;
    private bool touched = false;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
 

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Player" && !touched)
        {
            touched = true;
            StartCoroutine(DrainHealth());     

        }        
    }

    IEnumerator DrainHealth(){

            //
			TakeDamage(10);

            yield return new WaitForSeconds(5f);

            touched = false;
		}

}
