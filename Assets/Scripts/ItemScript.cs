using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] item id;
    BoxCollider2D box;
    Vector2 size;
    float i;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-0.2f, 0.2f), 0.4f);
        box = GetComponent<BoxCollider2D>();
        size = box.size;
    }

    private void FixedUpdate()
    {
        box.size = new Vector2(size.x, size.y+0.5f*Mathf.Sin(i));
        i += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Inventory inv))
        {
            inv.changeItem(id, 1);
            Destroy(gameObject);
        }
        
    }
}
