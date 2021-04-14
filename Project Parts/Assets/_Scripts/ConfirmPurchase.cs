using UnityEngine;

public class ConfirmPurchase : MonoBehaviour
{
    public Shop shop;
    public ItemHolder itemHolder;
    public GameObject holster;
    public PlayerController player;

    public void OnClick() {
        if (shop.selectedItem == null) return;
        int index = itemHolder.index;
        itemHolder.index = itemHolder.GetEmptyIndex();
        if (itemHolder.index == shop.itemHolder.items.Length)
        {
            if (itemHolder.currentItem == null) return;
            itemHolder.index = index;
        }

        if (player.money < shop.selectedItem.GetComponent<Item>().price) return;
        Purchase(shop.selectedItem, itemHolder.index); 
    }

    private void Purchase(GameObject item, int bagSlot) {
        itemHolder.DisplayImage(null, holster);
        itemHolder.AddItem(item, bagSlot);
        player.money -= item.GetComponent<Item>().price;
        shop.money.text = "" + player.money;
        itemHolder.PopulateItems();
        itemHolder.SetShopText();
        shop.shopItemName.text = "No Item Selected";
        shop.selectedItem = null;
        itemHolder.currentItem = null;
    }
}
