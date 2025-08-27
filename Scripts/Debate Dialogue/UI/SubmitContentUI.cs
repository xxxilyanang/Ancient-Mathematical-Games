using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SubmitContentUI : SingletonMono<SubmitContentUI>
{
    private GameObject item;

    private int currentBookCount;

    /// <summary>
    ///ÿ�α�����ʱ����
    /// </summary>
    void OnEnable()
    {
        //���ύ�����ݿɼ�ʱ������Ԥ����
        item = Resources.Load<GameObject>("UI/Submit Canvas UI/BookItemHolder");
        currentBookCount = BookInventoryMgr.GetInstance().bookInventory.Count;
        //����屻����ʱ
        UpdateBookItems();
    }

    public void UpdateBookItems()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            //ÿ�δ������ɾ�������壨����BookItem��������ʾ��������û�и��¹��ģ�
            Destroy(transform.GetChild(i).gameObject);
        }

/*        for (int i = 0; i < currentBookCount; i++)
        {
            BookItemHolderUI tempScript = item.GetComponent<BookItemHolderUI>();
            //��ʼ����Ӧ��������ѧ�鼮
            tempScript.InitBookItem(BookInventoryMgr.GetInstance().bookInventory[i]);
            Instantiate(item, this.transform);
        }*/

        for (int i = 0; i < currentBookCount; i++)
        {
            // ʹ��Instantiate���ص���GameObjectʵ��
            GameObject newItem = Instantiate(item, this.transform);
            BookItemHolderUI tempScript = newItem.GetComponent<BookItemHolderUI>();

            // ��ʼ����Ӧ��������ѧ�鼮
            tempScript.InitBookItem(BookInventoryMgr.GetInstance().bookInventory[i]);
        }
    }
}
