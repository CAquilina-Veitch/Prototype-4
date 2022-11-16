using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Transform player;
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] Vector2 clampMin;
    [SerializeField] Vector2 clampMax;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position + cameraOffset;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampMin.x, clampMax.x), Mathf.Clamp(transform.position.y, clampMin.y, clampMax.y), transform.position.z);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position+cameraOffset,0.1f);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,clampMin.x,clampMax.x), Mathf.Clamp(transform.position.y, clampMin.y, clampMax.y),transform.position.z);
    }
}
