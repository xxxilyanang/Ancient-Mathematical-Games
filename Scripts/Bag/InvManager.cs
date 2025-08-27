using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class InvManager : MonoBehaviour
{
    public static InvManager instance;
    public MYbag mybag;
    public GameObject slotGrid;
    public GameObject toolGird;
    public slot slotitem;
    public slot iteminfor;
    public Text itemINformation;
    public Image image;
    public GameObject inforPanel;
    private Item it;

    private void Awake()
    {
        if (instance! == null)
        {
            instance = this;
        }


    }
    private void OnEnable()
    {

        ReshItem();
        instance.itemINformation.text = "";
    }

    public static void UpdateINformation(string itemDescription)
    {
        instance.itemINformation.text = itemDescription;
    }
    public static void UpdateInimage(Sprite sprite)
    {
        instance.image.sprite = sprite;
    }

    public static void CreatNewbookitem(Item item)
    {
        //instance.image.sprite = item.ItemImage;
        slot newitem = Instantiate(instance.slotitem, instance.slotGrid.transform.position, Quaternion.identity);
        newitem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newitem.item = item;

        Image imageComponent = newitem.GetComponent<Image>();
        if (imageComponent == null)
        {
            imageComponent = newitem.gameObject.AddComponent<Image>();
        }

        // 获取或添加 Button 组件
        Button buttonComponent = newitem.GetComponent<Button>();
        if (buttonComponent == null)
        {
            buttonComponent = newitem.gameObject.AddComponent<Button>();
        }

        // 激活 Image 和 Button 组件
        imageComponent.enabled = true;
        buttonComponent.enabled = true;

        newitem.slotimage.sprite = item.ItemImage;
    }
    public static void CreatNewtoolitem(Item item)
    {
        //instance.image.sprite = item.ItemImage;
        slot newitem = Instantiate(instance.slotitem, instance.toolGird.transform.position, Quaternion.identity);
        newitem.gameObject.transform.SetParent(instance.toolGird.transform);
        newitem.item = item;

        Image imageComponent = newitem.GetComponent<Image>();
        if (imageComponent == null)
        {
            imageComponent = newitem.gameObject.AddComponent<Image>();
        }

        // 获取或添加 Button 组件
        Button buttonComponent = newitem.GetComponent<Button>();
        if (buttonComponent == null)
        {
            buttonComponent = newitem.gameObject.AddComponent<Button>();
        }

        // 激活 Image 和 Button 组件
        imageComponent.enabled = true;
        buttonComponent.enabled = true;

        newitem.slotimage.sprite = item.ItemImage;
    }
    public static void CreatNewbookinfor(Item item)
    {
        slot newinfor = Instantiate(instance.iteminfor, instance.image.transform.position, Quaternion.identity);
        newinfor.gameObject.transform.SetParent(instance.image.transform);
        newinfor.item = item;
        newinfor.slotimage.sprite = item.ItemImage;

    }
    public static void CreatNewtoolinfor(Item item)
    {
        slot newinfor = Instantiate(instance.iteminfor, instance.image.transform.position, Quaternion.identity);
        newinfor.gameObject.transform.SetParent(instance.image.transform);
        newinfor.item = item;
        newinfor.slotimage.sprite = item.ItemImage;

    }
    public static void ReshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < instance.toolGird.transform.childCount; i++)
        {
            if (instance.toolGird.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.toolGird.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < instance.mybag.items.Count; i++)
        {
            if (instance.mybag.items[i].t == ObejctType.book)
            {
                CreatNewbookitem(instance.mybag.items[i]);
                CreatNewbookinfor(instance.mybag.items[i]);
            }
            else if (instance.mybag.items[i].t == ObejctType.tool)
            {
                CreatNewtoolitem(instance.mybag.items[i]);
                CreatNewtoolinfor(instance.mybag.items[i]);
            }


        }
    }
}
