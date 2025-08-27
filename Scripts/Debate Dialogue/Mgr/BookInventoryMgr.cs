using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于管理当前收集的数学知识:
public class BookInventoryMgr : SingletonMono<BookInventoryMgr>
{
    public List<Book_SO> bookInventory = new List<Book_SO>();

    public void AddMathBook(Book_SO book)
    {
        bookInventory.Add(book);
    }
}
