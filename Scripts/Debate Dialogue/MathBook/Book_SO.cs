using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//书籍信息
[CreateAssetMenu(fileName = "New MathBook", menuName = "MathBook/New Book")]
public class Book_SO : ScriptableObject
{
    public string BookName;

    public Sprite BookImage;

    [TextArea]
    public string BookDescription;

    [TextArea]
    public string BookBrief;//选项简介
}