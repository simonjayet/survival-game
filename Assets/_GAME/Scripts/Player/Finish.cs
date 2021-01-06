using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject finishText;
    private bool touched;

     void Start()
    {
        touched = false;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Finish" && !touched)
        {

            // 
            finishText.SetActive(true);

        }       
    }
    
}
