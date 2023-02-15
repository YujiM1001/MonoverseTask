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
    Text textTimer;

    [SerializeField]
    List<Color> colorItems = new List<Color>();

    // 현재 등장 중인 아이템 리스트
    // TODO : 테스트 후 PUBLIC 지우기
    public List<Item> shownItems = new List<Item>();

    const int MAX_COUNT_ITEM_IN_WORLD   = 5;
    const int MAX_COUNT_ITEM_TOTAL      = 20;
    const float TIME_SEC_CHANGE_ITEM    = 60f;

    Vector3 mapSize = Vector3.zero;
    int countCreateItem = 0;

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

    void CreateItem(int countItem)
    {
        for(int i = 0; i < countItem ; i++)
        {
            if(countCreateItem >= MAX_COUNT_ITEM_TOTAL)
                return;

            AddCountCreateItem();

            Item item = new Item();
            SetItemColor(item);
            SetItemPosition(item);
            item.group = GroupItem.MAP;

            shownItems.Add(item);

            GameObject obj = GameObject.Instantiate(itemObject, map) as GameObject;
            obj.GetComponent<MeshRenderer>().material.color = GetColor(item);
            obj.transform.position = GetPosition(item);
            obj.SetActive(true);
            obj.GetComponent<ItemObject>().Init(item);
        }
    }

    void AddCountCreateItem()
    {
        countCreateItem++;
        textCountCreatedItem.text = countCreateItem.ToString();
    }

    public void SetItemColor(Item item)
    {
        int indexColor = Random.Range(0, colorItems.Count);
        SetColor(item, colorItems[indexColor]);
    }

    public void SetItemPosition(Item item)
    {
        float posX = Random.Range(-mapSize.x/2 , mapSize.x/2);
        float posZ = Random.Range(-mapSize.z/2 , mapSize.z/2);
        SetPosition(item, new Vector3(posX, 0.5f, posZ));
    }

    public void RemoveItem(Item item)
    {
        shownItems.Remove(item);

        CreateItem(1);
    }

    public float GetTimeSecChangeItem()
    {
        return TIME_SEC_CHANGE_ITEM;
    }

    #region ItemUtil
    public Color GetColor(Item item)
    {
        return new Color(item.colorR, item.colorG, item.colorB, item.colorA);
    }

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
