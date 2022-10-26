using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardscript : MonoBehaviour
{
    [SerializeField] Transform teleportPosition;
    dialogueManager dM;

    // Start is called before the first frame update
    void Start()
    {
        dM = GameObject.FindGameObjectWithTag("DialoguePanel").GetComponent<dialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!other.GetComponent<PlayerController>().isInvisible)
            {
                StartCoroutine(Caught(other));
                
            }
        }
    }
    IEnumerator Caught(Collider2D player)
    {
        dM.Next("You cant be here!!");
        yield return new WaitForSeconds(1);
        dM.End();
        dM.End();
        // send to pos
        player.transform.position = teleportPosition.position;
    }
}
