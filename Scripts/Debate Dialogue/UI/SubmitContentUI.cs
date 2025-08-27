using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SubmitContentUI : SingletonMono<SubmitContentUI>
{
    private GameObject item;

    private int currentBookCount;

    /// <summary>
    ///每次被激活时调用
    /// </summary>
    void OnEnable()
    {
        //当提交的内容可见时，加载预设体
        item = Resources.Load<GameObject>("UI/Submit Canvas UI/BookItemHolder");
        currentBookCount = BookInventoryMgr.GetInstance().bookInventory.Count;
        //当面板被激活时
        UpdateBookItems();
    }

    public void UpdateBookItems()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            //每次打开面板先删除子物体（所有BookItem，放置显示的内容是没有更新过的）
            Destroy(transform.GetChild(i).gameObject);
        }

/*        for (int i = 0; i < currentBookCount; i++)
        {
            BookItemHolderUI tempScript = item.GetComponent<BookItemHolderUI>();
            //初始化对应容器的数学书籍
            tempScript.InitBookItem(BookInventoryMgr.GetInstance().bookInventory[i]);
            Instantiate(item, this.transform);
        }*/

        for (int i = 0; i < currentBookCount; i++)
        {
            // 使用Instantiate返回的新GameObject实例
            GameObject newItem = Instantiate(item, this.transform);
            BookItemHolderUI tempScript = newItem.GetComponent<BookItemHolderUI>();

            // 初始化对应容器的数学书籍
            tempScript.InitBookItem(BookInventoryMgr.GetInstance().bookInventory[i]);
        }
    }
}
