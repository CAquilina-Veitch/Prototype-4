using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardscript : MonoBehaviour
{
    [SerializeField] Transform teleportPosition;
    dialogueManager dM;

    CapsuleCollider2D cc;

    // Start is called before the first frame update
    void Start()
    {
        dM = GameObject.FindGameObjectWithTag("DialoguePanel").GetComponent<dialogueManager>();
        cc = GetComponent<CapsuleCollider2D>();
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
                StartCoroutine(Caught(other, other.transform.position - transform.position));
                
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            cc.enabled = other.GetComponent<PlayerController>().isInvisible ? false : true;

        }
    }

    IEnumerator Caught(Collider2D player, Vector3 diff)
    {
        dM.Next("You cant be here!!");
        yield return new WaitForSeconds(1);
        dM.End();
        dM.End();
        // send to pos

        //player.transform.position = transform.position + diff * 1.5f;

        //player.transform.position = teleportPosition.position;
    }
}
