using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowledgeScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<NPCConversation>().ItemGiven = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().checkQuest(GetComponent<NPCConversation>().requestedItem);
    }

}
