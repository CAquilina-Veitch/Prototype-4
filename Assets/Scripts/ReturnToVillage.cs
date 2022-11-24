using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReturnToVillage : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().maxScene >= 4)
        {
            GetComponent<nextScene>().sceneNum = 4;
        }
    }
}
