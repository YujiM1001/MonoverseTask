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

    float moveSpeed = 0.01f;
    Vector3 mapSize = Vector3.zero;
    Vector3 playerSize = Vector3.zero;

    List<ItemSlot> itemSlots = new List<ItemSlot>();

    void Start()
    {
        // itemSlot.gameObject.SetActive(false);
        mapSize = map.GetComponent<MeshCollider>().bounds.size;
        playerSize = GetComponent<Collider>().bounds.size;
    }

    void Update()
    {   
        Move();
    }

    void Move()
    {
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");    
        Vector3 movPos = new Vector3(moveX, 0f, moveZ).normalized * moveSpeed;

        // 맵 크기만큼만 이동하도록 체크 
        float movposX = Mathf.Abs(transform.position.x + movPos.x);
        float movposZ = Mathf.Abs(transform.position.z + movPos.z);

        if( mapSize.x/2 < playerSize.x/2 + movposX ||
            mapSize.z/2 < playerSize.z/2 + movposZ )
        {
            return;
        }
        transform.Translate(movPos);
    }                           

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Item")
        {
            Item item = other.gameObject.GetComponent<ItemObject>().GetItem();
            CreateItemSlot(item);

            Destroy(other.gameObject);
        }
    }

    void CreateItemSlot(Item item)
    {
        GameObject obj = GameObject.Instantiate(itemSlot, inventoryObject) as GameObject;
        obj.SetActive(true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Init(item, this);
        itemSlots.Add(slot);
    }

    public void UseItem(ItemSlot useItemSlot)
    {
        Item usedItem = useItemSlot.GetItem();
        usedItem.group = GroupItem.NONE;

        // change color
        gameObject.GetComponent<MeshRenderer>().material.color = ItemManager.Instance.GetColor(usedItem);

        // remove slot at list
        itemSlots.Remove(useItemSlot);

        // call itemmanager
        ItemManager.Instance.RemoveItem(usedItem);
    }
}
