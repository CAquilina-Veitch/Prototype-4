using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinObjectTrigger : MonoBehaviour
{
    bool notHolding = true;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            if (Input.GetKey(KeyCode.Mouse1)&& notHolding)
            {

                other.GetComponent<PlayerController>().Win(other.GetComponent<Inventory>().questsCompleted);
                notHolding = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject.FindGameObjectWithTag("DialoguePanel").GetComponent<dialogueManager>().End();
        GameObject.FindGameObjectWithTag("DialoguePanel").GetComponent<dialogueManager>().End();
    }
    private void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            notHolding = true;
        }
    }
}
