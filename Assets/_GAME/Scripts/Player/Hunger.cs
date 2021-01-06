using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    
    //
    public int maxHunger = 100;
    public int currentHunger;
    private bool touched = false;
    public Hungerbar hungerBar;

    void Start()
    {
        currentHunger = maxHunger/2;
        hungerBar.SetMaxHunger(maxHunger);
        hungerBar.SetHunger(currentHunger);

        InvokeRepeating("DrainHunger", 10f, 10f);
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
        currentHunger -= points;
        hungerBar.SetHunger(currentHunger);
    }

    void IncreaseHungre(int points)
    {
        currentHunger += points;
        hungerBar.SetHunger(currentHunger);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((hit.gameObject.name == "Banana" || hit.gameObject.name == "Avocado" || hit.gameObject.name == "Pizza") && !touched)
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

    void DrainHunger(){

        //Debug.Log("currentHunger : " + currentHunger);
        currentHunger -= 10;
        hungerBar.SetHunger(currentHunger);
	
		}
}
