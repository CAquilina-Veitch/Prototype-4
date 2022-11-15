using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] NPCConversation dog;
    [SerializeField] NPCConversation beggar;
    [SerializeField] NPCConversation door;
    [SerializeField] GameObject indicator;

    public int questsCompleted;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1)){
            int temp1 = dog.ItemGiven ? 1 : 0;
            int temp2 = beggar.ItemGiven ? 1 : 0;
            int temp3 = door.ItemGiven ? 1 : 0;
            questsCompleted = temp1 + temp2 + temp3;
            if (door.ItemGiven)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                indicator.active = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                int temp1 = dog.ItemGiven ? 1 : 0;
                int temp2 = beggar.ItemGiven ? 1 : 0;
                int temp3 = beggar.ItemGiven ? 1 : 0;
                questsCompleted = temp1 + temp2 + temp3;
                GetComponent<BoxCollider2D>().enabled = false;
                other.GetComponent<PlayerController>().Win(questsCompleted);
            }
        }
    }
}
