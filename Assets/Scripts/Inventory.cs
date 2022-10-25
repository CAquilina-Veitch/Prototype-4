using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum item { Apple, Medicine, Key }

public class Inventory : MonoBehaviour
{
    public int appleCount;
    public int medicineCount;
    public int keyCount;

    public int itemNum(item id)
    {
        if(id == item.Apple)
        {
            return appleCount;
        }
        else
        {
            return id == item.Medicine ? medicineCount : keyCount;
        }
    }

    public void changeItem(item id, int change)
    {
        switch (id)
        {
            case item.Apple:
                appleCount += change;
                break;
            case item.Medicine:
                medicineCount = change > 0 ? 1 : 0;
                break;
            case item.Key:
                keyCount = change > 0 ? 1 : 0;
                break;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
