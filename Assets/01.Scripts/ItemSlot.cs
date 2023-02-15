using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    PlayerController playerController;
    Item item;

    public void Init(Item item, PlayerController playerController)
    {
        this.playerController = playerController;
        this.item = item;
        this.item.group = GroupItem.INVENTORY;

        this.item.colorA = 255f;
        GetComponent<Image>().color = ItemManager.Instance.GetColor(item);
    }

    public Item GetItem()
    {
        return item;
    }

    public void OnClickItem()
    {
        playerController.UseItem(this);

        Destroy(gameObject);
    }
}
