using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    private Shop shop;
    private Item item;

    private void Start()
    {
        shop = transform.parent.GetComponent<Shop>();
    }

    public void OnClick()
    {
        shop.selectedItem = transform.GetChild(0).gameObject;
        item = shop.selectedItem.GetComponent<Item>();
        shop.price.text = "" + item.price;
        shop.shopItemName.text = item.getName();
    }
}
