using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int price;
    public virtual string Description() {
        return "No item selected";
    }

    public virtual void Use(PlayerController player) {}

    public Texture GetImage() {
        return transform.GetComponent<RawImage>().texture;
    }

    public virtual string getName() {
        return "No Item Selected";
    }
}
