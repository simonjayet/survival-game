using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    
    //
    public int maxStamina = 100;
    public int currentStamina;
    private bool touched = false;
    public Staminabar staminaBar;

    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DecreaseHungre(20);
        }
    }

    void DecreaseHungre(int points)
    {
        currentStamina -= points;
        staminaBar.SetStamina(currentStamina);
    }

    void IncreaseHungre(int points)
    {
        currentStamina += points;
        staminaBar.SetStamina(currentStamina);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Banana" && !touched)
        {
            //
            touched = true;

            //
            //points += 10;
            
            //
            //myLog = "Score : " + points;

            //
            Destroy(hit.gameObject);    

            IncreaseHungre(20);        

        }else{

            //
            touched = false;

        }        
    }
}
