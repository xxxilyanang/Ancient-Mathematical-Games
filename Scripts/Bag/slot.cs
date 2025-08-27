using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour
{
    public Item item;
    public Image slotimage;

    public void ItemOnClicked()
    {
        InvManager.UpdateINformation(item.ItemDescription);
        InvManager.UpdateInimage(item.ItemImage);
        //slotimage.sprite = item.ItemImage;
        InvManager.instance.inforPanel.SetActive(true);
        Debug.Log("我点击了一个东西");
    }
}
