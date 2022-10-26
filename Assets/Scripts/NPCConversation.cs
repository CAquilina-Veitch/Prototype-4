using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCConversation : MonoBehaviour
{
    [Header("DIALOGUE")]
    [SerializeField] string[] requestDialogue;
    [SerializeField] string[] GivingItemDialogue;
    [SerializeField] string[] HaveGivenItemDialogue;
    string[] currentDialogue;

     

    [Header("QuestAttributes")]
    
    [SerializeField] item requestedItem;
    [SerializeField] int requestedQuantity;
    bool ItemGiven;







    dialogueManager dM;
    bool reachable = false;
     int nextDialogue = 0;
    
    

   

    // Start is called before the first frame update
    void Start()
    {
        dM = GameObject.FindGameObjectWithTag("DialoguePanel").GetComponent<dialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reachable & Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (nextDialogue == 0)
            {
                if (isGivingItem())
                {
                    currentDialogue = GivingItemDialogue;
                }
                else
                {
                    currentDialogue = ItemGiven ? HaveGivenItemDialogue : requestDialogue;
                }

            }
            NextText();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        reachable = true; 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        reachable = false;
    }
    void NextText()
    {
        //Debug.Log($"{dialogue[nextDialogue]} words, {nextDialogue} number, {dialogue.Length} length");
        if (nextDialogue >= currentDialogue.Length)
        {
            Debug.Log(":END");
            
            if (dM.End())
            {
                nextDialogue = 0;
            }
            return;
        }
        else
        {
            if (dM.Next(currentDialogue[nextDialogue]))
            {
                Debug.Log("NEXT");
                nextDialogue++;
            }
            
        }
        Debug.LogError($"{nextDialogue},{ currentDialogue.Length}");
        
    }
    bool isGivingItem()
    {
        Inventory playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        if(playerInv.itemNum(requestedItem)>= requestedQuantity)
        {
            playerInv.changeItem(requestedItem, -requestedQuantity);
            ItemGiven = true;
            return true;
        }
        else
        {
            return false;
        }

    }

}
