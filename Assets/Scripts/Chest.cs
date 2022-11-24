using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject key;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.Mouse1))
        {
            Destroy(gameObject);
            GameObject keyObj = Instantiate(key,transform.position,Quaternion.identity);
            keyObj.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 3);
            //Debug.Log(this + "Spawned");
            
        }
    }
    private void OnEnable()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().hasGottenChest)
        {
            Destroy(gameObject);
        }
    }




}
