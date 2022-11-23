using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    [SerializeField] item id;
    BoxCollider2D box;
    Vector2 size;
    float i;
    [SerializeField] Sprite apple;
    [SerializeField] Sprite medicine;
    [SerializeField] Sprite key;

    private void OnEnable()
    {
        init();
    }
    void init()
    {

        if (id == item.Apple)
        {
            GetComponent<SpriteRenderer>().sprite = apple;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = id == item.Medicine ? medicine : key;
        }


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
        Debug.Log($"TRiggered by {other.gameObject.name} which has component? {other.GetComponent<Inventory>()}");
        if(other.TryGetComponent(out Inventory inv))
        {
            inv.changeItem(id, 1);
            Destroy(gameObject);
        }
        
    }
    public void Typechange(item to)
    {
        id = to;
        init();
    }
}
