using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : SingletonGameObject<ItemManager>
{
    [SerializeField]
    Transform map;

    [SerializeField]
    GameObject itemObject;

    [SerializeField]
    Text textCountCreatedItem;

    [SerializeField]
    List<Color> colorsItem = new List<Color>();

    // 현재 등장 중인 모든 아이템 리스트(맵, 인벤 포함)
    List<Item> liveItems = new List<Item>();

    // 맵+인벤토리에 동시에 등장 가능한 아이템 갯수.
    const int MAX_COUNT_ITEM_IN_WORLD   = 5;
    // 생성될 수 있는 아이템의 총 갯수.
    const int MAX_COUNT_ITEM_TOTAL      = 20;
    // 맵 위 아이템이 리셋되는 시간(초 단위)
    const float TIME_SEC_CHANGE_ITEM    = 60f;

    Vector3 mapSize = Vector3.zero;
    int countCreatedItem = 0;
    float posYItemObject = 0.5f;

    void Awake()
    {
        CreateInstance(gameObject);
        Init();
    }

    void Init()
    {
        mapSize = map.GetComponent<MeshCollider>().bounds.size;
        CreateItem(MAX_COUNT_ITEM_IN_WORLD);
        itemObject.SetActive(false);
    }

    // 파라미터 수 만큼 아이템 오브젝트 생성
    void CreateItem(int quantity)
    {
        for(int i = 0; i < quantity ; i++)
        {
            if(countCreatedItem >= MAX_COUNT_ITEM_TOTAL)
                return;

            AddCountCreatedItem();

            Item item = new Item();
            SetItem(item);

            liveItems.Add(item);

            CreateItemObject(item);
        }
    }

    void AddCountCreatedItem()
    {
        countCreatedItem++;
        textCountCreatedItem.text = countCreatedItem.ToString();
    }

    void CreateItemObject(Item item)
    {
        GameObject obj = UtilManager.Instance.CreateResource(itemObject, map);
        obj.GetComponent<ItemObject>().Init(item);
    }

    void SetItem(Item item, LocationItem location = LocationItem.MAP)
    {
        SetItemColor(item);
        SetItemPosition(item);
        item.location = location;
    }

    /// <summary>
    /// item.Color(colorR, colorG, colorB, colorA)를 랜덤으로 셋팅 
    /// </summary>
    /// <param name="item">세팅하고자 하는 아이템 데이터</param>
    public void SetItemColor(Item item)
    {
        SetColor(item, GetItemColor());
    }

    // 아이템 컬러를 colorsItem내 컬러로 랜덤+겹치지 않게 리턴.  
    Color GetItemColor()
    {
        int indexColor = Random.Range(0, colorsItem.Count);

        while(!IsOnlyColor(colorsItem[indexColor]))
        {
            indexColor = Random.Range(0, colorsItem.Count);
        }

        return colorsItem[indexColor];
    }

    bool IsOnlyColor(Color color)
    {
        for(int i = 0 ; i < liveItems.Count ; i++)
        {
            if(color == GetColor(liveItems[i]))
                return false;
        }
        return true;
    }

    /// <summary>
    /// item.position(positionX, positionY, positionZ)를 랜덤으로 셋팅 
    /// </summary>
    /// <param name="item">세팅하고자 하는 아이템 데이터</param>
    public void SetItemPosition(Item item)
    {
        float posX = Random.Range(-mapSize.x/2 , mapSize.x/2);
        float posZ = Random.Range(-mapSize.z/2 , mapSize.z/2);
        SetPosition(item, new Vector3(posX, posYItemObject, posZ));
    }

    bool IsOverlapPositionItemToPlayer(Vector3 vector)
    {
        return false;
    }

    /// <summary>
    /// 사용된 아이템 처리.
    /// </summary>
    /// <param name="item">사용된 아이템 데이터</param>
    public void UseItem(Item usedItem)
    {
        liveItems.Remove(usedItem);

        CreateItem(1);
    }

    /// <summary>
    /// 맵 위 아이템이 리셋되는 시간(초 단위) 리턴
    /// </summary>
    /// <returns>맵 위 아이템이 리셋되는 시간(초 단위) 리턴</returns>
    public float GetTimeSecChangeItem()
    {
        return TIME_SEC_CHANGE_ITEM;
    }

    #region ItemUtil
    /// <summary>
    /// item color값들(colorA, colorG ..)을 Color형식으로 리턴
    /// </summary>
    /// <param name="item">Color값을 얻고자 하는 item 데이터</param>
    /// <returns>입력된 item의 Color</returns>
    public Color GetColor(Item item)
    {
        return new Color(item.colorR, item.colorG, item.colorB, item.colorA);
    }

    /// <summary>
    /// item position값들(positionX, ..)을 Vector3형태로 리턴
    /// </summary>
    /// <param name="item">Vector3값을 얻고자 하는 item 데이터</param>
    /// <returns>입력된 item의 Vector3</returns>
    public Vector3 GetPosition(Item item)
    {
        return new Vector3(item.positionX, item.positionY, item.positionZ);
    }

    public void SetColor(Item item, Color color)
    {
        item.colorR = color.r;
        item.colorG = color.g;
        item.colorB = color.b;
        item.colorA = color.a;
    }

    public void SetPosition(Item item, Vector3 vector)
    {
        item.positionX = vector.x;
        item.positionY = vector.y;
        item.positionZ = vector.z;
    }
    #endregion ItemUtil
}
