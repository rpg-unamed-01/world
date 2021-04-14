using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
    public GameObject[] items;
    public GameObject currentItem;
    public GameObject holster;
    public PlayerController player;
    public int index;

    public Text itemName;

    public void Start()
    {
        items = new GameObject[10];
    }

    public void AddItem(GameObject item, int i) {
        items[i] = item;
    }

    public void PopulateItems() {
        int count = 0;
        Transform go;
        foreach (var item in items) {
            go = transform.GetChild(count);
            DisplayImage(item, go.gameObject);
            count++;
        }
    }

    public void SetShopText() {
        if (GetEmptyIndex() < 10)
        {
            itemName.text = "Bag has room";
        }
        else
        {
            itemName.text = "Choose Item to Discard";
        }
    }

    public int GetEmptyIndex() {
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == null) {
                return i;
            }
        }
        return 10;
    }

    public void DisplayCurrentItem() {
        if (!player.displayShop)
        {
            DisplayImage(currentItem, holster);
            itemName.text = "No Item Selected";
        }
        else if (GetEmptyIndex() < items.Length) return;
        if (currentItem != null)
        {
            itemName.text = currentItem.GetComponent<Item>().getName();
        }
    }

    public void DisplayImage(GameObject item, GameObject parent) {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        player.selectedItem = null;

        if (item != null)
        {
            RawImage displayItem;
            GameObject itemDisplay = new GameObject();
            itemDisplay.name = "YO";
            displayItem = itemDisplay.AddComponent<RawImage>();
            displayItem.texture = item.GetComponent<Item>().GetImage();
            itemDisplay.transform.SetParent(parent.transform);
            itemDisplay.transform.localPosition = Vector3.zero;
            itemDisplay.transform.localScale = Vector3.one * 0.2f;
        }
    }
}
