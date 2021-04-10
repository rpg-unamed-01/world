using UnityEngine;

public class SlotButton : MonoBehaviour
{
    private ItemHolder itemHolder;

    private void Start()
    {
        itemHolder = transform.parent.GetComponent<ItemHolder>();
    }

    public void OnClick() {
        itemHolder.index = transform.GetSiblingIndex();
        itemHolder.currentItem = itemHolder.items[itemHolder.index];
        itemHolder.DisplayCurrentItem();
    }
}
