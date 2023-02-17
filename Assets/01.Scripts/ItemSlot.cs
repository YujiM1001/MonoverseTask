using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    PlayerController playerController;
    Item item;

    public void Init(Item myitem, PlayerController playerController)
    {
        this.playerController       = playerController;
        item                        = myitem;
        item.location               = LocationItem.INVENTORY;
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
