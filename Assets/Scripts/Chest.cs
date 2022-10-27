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
            Instantiate(key);
            Debug.Log(this + "Spawned");
            
        }
    }





}
