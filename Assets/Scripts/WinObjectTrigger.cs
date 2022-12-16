using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinObjectTrigger : MonoBehaviour
{
    bool notHolding = true;
    bool restarting = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            if (Input.GetKey(KeyCode.Mouse1)&& notHolding)
            {

                other.GetComponent<PlayerController>().Win(other.GetComponent<Inventory>().questsCompleted);
                notHolding = false;
                StartCoroutine(RestartDelay(15f));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject.FindGameObjectWithTag("DialoguePanel").GetComponent<dialogueManager>().End();
        GameObject.FindGameObjectWithTag("DialoguePanel").GetComponent<dialogueManager>().End();
        if (!restarting)
        {
            restarting = true;
            StartCoroutine(RestartDelay(4));
        }
        
    }
    private void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            notHolding = true;
        }
    }
    IEnumerator RestartDelay(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Restart();
    }
}
