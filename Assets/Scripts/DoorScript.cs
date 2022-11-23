using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DoorScript : MonoBehaviour
{
    [SerializeField] bool locked = true;
    Inventory inv;

    [SerializeField] Vector3 tpPos;
    [SerializeField] int treasureSceneNum;


    [SerializeField] GameObject indicator;

    private void Awake()
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log(other.gameObject);
        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                //Debug.Log("1");
                
                if (other.GetComponent<Inventory>().keyQuestDone&&!locked)
                {
                    //Debug.Log("2");
                    
                    other.GetComponent<PlayerController>().LoadScene(treasureSceneNum);
                    other.transform.position = tpPos;
                    other.GetComponent<PlayerController>().spawnpoint = tpPos;
                    //Debug.Log("3");
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (locked)
        {
            if (inv.keyQuestDone)
            {
                locked = false;
                gameObject.transform.GetChild(0).gameObject.active = true;
                indicator.active = true;
            }
        }
    }
}


