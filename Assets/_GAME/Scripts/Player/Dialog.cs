using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    //
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public bool waitTyping;
    public bool firtStart;

    public GameObject DialogText;

    // Start is called before the first frame update
    void Start()
    {
        firtStart = true;
        waitTyping = true;
        
    }

    //
    IEnumerator Type(){

        //
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    //
    public void NextSentence(){

        waitTyping = true;

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }else{
            textDisplay.text = "";
            waitTyping = true;
            DialogText.SetActive(false);
            firtStart = true;
            index = 0;
        }
    }

    



    // Update is called once per frame
    void Update()
    {
        if (DialogText.activeSelf && firtStart)
        {
            firtStart = false;
            waitTyping = true;
            StartCoroutine(Type());
        }

        if (DialogText.activeSelf)
        {
            if (textDisplay.text == sentences[index])
            {
                waitTyping = false;
            }
        
            if (Input.GetKeyUp(KeyCode.Return) && !waitTyping)
            {
                NextSentence();
            } 
        }

           
        

        
    }

    
}
