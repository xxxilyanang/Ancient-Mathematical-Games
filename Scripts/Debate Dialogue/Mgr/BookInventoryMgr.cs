using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ڹ���ǰ�ռ�����ѧ֪ʶ:
public class BookInventoryMgr : SingletonMono<BookInventoryMgr>
{
    public List<Book_SO> bookInventory = new List<Book_SO>();

    public void AddMathBook(Book_SO book)
    {
        bookInventory.Add(book);
    }
}
