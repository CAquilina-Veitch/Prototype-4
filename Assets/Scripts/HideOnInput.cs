using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnInput : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }
}
