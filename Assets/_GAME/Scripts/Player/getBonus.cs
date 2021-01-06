using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getBonus : MonoBehaviour
{
    	private int points = 0;
        private bool touched = false;
		public static string myLog = "Score : 0";

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Banana" && !touched)
        {
            //
            touched = true;

            //
            points += 10;
            
            //
            myLog = "Score : " + points;

            //
            Destroy(hit.gameObject);            

        }else{

            //
            touched = false;

        }        
    }

    void OnGUI()
    {
        GUI.TextArea(new Rect(10, 10, 90, 30), myLog);
    }
    
}
