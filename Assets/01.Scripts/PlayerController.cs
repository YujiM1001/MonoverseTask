using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Transform map;

    [SerializeField]
    Transform inventoryObject;

    [SerializeField]
    GameObject itemSlot;

    float moveSpeed     = 0.01f;
    Vector3 mapSize     = Vector3.zero;
    Vector3 playerSize  = Vector3.zero;

    // 소유한(인벤토리 내) 아이템 슬롯 리스트
    List<ItemSlot> itemSlots = new List<ItemSlot>();

    string strVertical      = "Vertical";
    string strHorizontal    = "Horizontal";
    string strItem          = "Item";

    void Start()
    {
        mapSize     = map.GetComponent<MeshCollider>().bounds.size;
        playerSize  = GetComponent<Collider>().bounds.size;
    }

    void Update()
    {   
        MovePosition();
    }

    // 키보드 입력에 따라 플레이어 객체의 포지션이 이동한다.
    // 맵크기를 넘어가지 않도록 이동거리 제한 : 맵 크기 + 플레이어 객체 크기로 계산  
    void MovePosition()
    {
        float moveZ     = Input.GetAxis(strVertical);
        float moveX     = Input.GetAxis(strHorizontal);    
        Vector3 movPos  = new Vector3(moveX, 0f, moveZ).normalized * moveSpeed;

        // 맵 크기 넘어가지 않게 이동하도록 체크 
        float movposX = Mathf.Abs(transform.position.x + movPos.x);
        float movposZ = Mathf.Abs(transform.position.z + movPos.z);

        if( mapSize.x/2 < playerSize.x/2 + movposX ||
            mapSize.z/2 < playerSize.z/2 + movposZ )
        {
            return;
        }
        transform.Translate(movPos);
    }                           

    // 인벤토리에 충돌한 아이템을 추가하고, 맵상의 아이템 오브젝트 제거.
    // 플레이어와 맵상의 아이템 충돌시 호출.
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == strItem)
        {
            Item item = other.gameObject.GetComponent<ItemObject>().GetItem();
            CreateItemSlotInventory(item);

            Destroy(other.gameObject);
        }
    }

    void CreateItemSlotInventory(Item item)
    {
        GameObject obj = UtilManager.Instance.CreateResource(itemSlot, inventoryObject);
        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Init(item, this);
        itemSlots.Add(slot);
    }

    /// <summary>
    /// 아이템 사용시 호출 : 플레이어 관련 정보 변경, 사용된 아이템 오브젝트/관련 데이터 제거. 
    /// </summary>
    /// <param name="useItemSlot">사용된 아이템 슬롯</param>
    public void UseItem(ItemSlot useItemSlot)
    {
        Item usedItem       = useItemSlot.GetItem();
        usedItem.location   = LocationItem.NONE;

        gameObject.GetComponent<MeshRenderer>().material.color = ItemManager.Instance.GetColor(usedItem);

        itemSlots.Remove(useItemSlot);

        ItemManager.Instance.UseItem(usedItem);
    }
}
