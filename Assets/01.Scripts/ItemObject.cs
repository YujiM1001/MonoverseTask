using System.Collections;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    Item item;
    Coroutine runningCoroutineTimer = null;

    // 아이템 오브젝트 갱신을 위해 남은시간을 측정. 시간이 지나면 ResetColorPosition()호출.
    IEnumerator ResetTimer()
    {
        float maxTime = ItemManager.Instance.GetTimeSecChangeItem();

        while(maxTime > 0)
        {
            maxTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        StopCoroutine(runningCoroutineTimer);
        ResetColorPosition();
        Init(item);
    }

    // 아이템 컬러와 포지션 재셋팅. 
    void ResetColorPosition()
    {
        ItemManager.Instance.SetItemColor(item);
        ItemManager.Instance.SetItemPosition(item);
    }

    /// <summary>
    /// 파라미터 아이템 데이터로 아이템 오브젝트 갱신.
    /// </summary>
    /// <param name="myItem">Item 데이터</param>
    public void Init(Item myItem)
    {
        item = myItem;
        transform.position                          = ItemManager.Instance.GetPosition(item);
        GetComponent<MeshRenderer>().material.color = ItemManager.Instance.GetColor(item);
        
        runningCoroutineTimer = StartCoroutine(ResetTimer());
    }

    /// <summary>
    /// 소유한 아이템 데이터 리턴
    /// </summary>
    /// <returns>소유한 아이템(Item) 데이터</returns>
    public Item GetItem()
    {
        return item;
    }
}
