using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{

    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text textbox;

    [SerializeField] bool typing = false;
    [SerializeField] float timebetweenletters = 0.1f;
    [SerializeField] int letter;

    string currentText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool End()
    {
        if (!typing)
        {
            dialogueBox.SetActive(false);
            letter = 0;
            return true;
        }
        else
        {
            typing = false;
            textbox.text = currentText;
            letter = 0;
            return false;
        }
    }
    public bool Next(string words)
    {
        Debug.Log("WROds" + words);

        
        if (!typing)
        {
            currentText = words;
            dialogueBox.SetActive(true);
            letter = 0;
            StartCoroutine(nextletter());
            return true;
        }
        else
        {
            typing = false;
            textbox.text = currentText;
            return false;
        }
        

    }
    IEnumerator nextletter()
    {
        typing = true;
        letter++;
        if (typing)
        {
            textbox.text = currentText.Substring(0, letter);
        }
        
        yield return new WaitForSeconds(timebetweenletters);
        if (letter < currentText.Length && typing)
        {
            StartCoroutine(nextletter());
        }
        else
        {
            typing = false;
        }
        


    }
}
