using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextScene : MonoBehaviour
{
    [SerializeField] public Vector3 tpPos;
    [SerializeField] public int sceneNum;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().LoadScene(sceneNum);
            other.transform.position = tpPos;
            other.GetComponent<PlayerController>().spawnpoint = tpPos;
        }
    }
}
