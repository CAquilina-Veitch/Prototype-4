using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinObjectTrigger : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {

                other.GetComponent<PlayerController>().Win(other.GetComponent<Inventory>().questsCompleted);
            }
        }
    }
}
