using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum item { Apple, Medicine, Key }

public class Inventory : MonoBehaviour
{
    public int appleCount;
    public int medicineCount;
    public int keyCount;

    [SerializeField] Text Apple;
    [SerializeField] Text Medicine;
    [SerializeField] Text Key;

    public bool appleQuestDone;
    public bool medicineQuestDone;
    public bool keyQuestDone;

    public int questsCompleted;

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
                medicineCount += change;
                break;
            case item.Key:
                keyCount = change > 0 ? 1 : 0;
                break;
        }
        refreshUI();
    }

    void refreshUI()
    {
        Apple.text = appleCount.ToString();
        Medicine.text = medicineCount.ToString();
        Key.text = keyCount.ToString();

    }


    public void completeQuest(item id)
    {
        switch (id)
        {
            case item.Apple:
                appleQuestDone = true;
                break;
            case item.Medicine:
                medicineQuestDone = true;
                break;
            case item.Key:
                keyQuestDone = true;
                break;
        }
    }
    public bool checkQuest(item id)
    {
        switch (id)
        {
            case item.Apple:
                return appleQuestDone;
            case item.Medicine:
                return medicineQuestDone;
            case item.Key:
                return keyQuestDone;
            default:
                return false;
        }
    }

    public int questTally()
    {
        int temp1 = appleQuestDone ? 1 : 0;
        int temp2 = medicineQuestDone ? 1 : 0;
        int temp3 = keyQuestDone ? 1 : 0;
        questsCompleted = temp1 + temp2 + temp3;
        return questsCompleted;
    }

}
