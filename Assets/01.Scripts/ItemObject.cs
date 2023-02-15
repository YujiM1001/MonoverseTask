using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
    Coroutine runningCoroutineTimer = null;

    // TODO : 테스트 후 PUBLIC 제거 
    public Item item;

    public void Init(Item item)
    {
        this.item = item;
        GetComponent<MeshRenderer>().material.color = ItemManager.Instance.GetColor(this.item);
        gameObject.transform.position = ItemManager.Instance.GetPosition(this.item);

        StartTimer();
    }

    public void StartTimer()
    {
        runningCoroutineTimer = StartCoroutine(ResetItemTimer());
    }

    IEnumerator ResetItemTimer()
    {
        float maxTime = ItemManager.Instance.GetTimeSecChangeItem();

        while(maxTime > 0)
        {
            maxTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        StopCoroutine(runningCoroutineTimer);
        ResetSlot();
    }

    void ResetSlot()
    {
        ItemManager.Instance.SetItemColor(item);
        ItemManager.Instance.SetItemPosition(item);

        Init(item);
    }

    public Item GetItem()
    {
        return item;
    }
}
