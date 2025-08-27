using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public enum ObejctType
{
    book,
    tool
}
[CreateAssetMenu(fileName = "New Item", menuName = "INvent/New Item")]
public class Item : ScriptableObject
{
    public ObejctType t = ObejctType.book;
    public string ItemName;
    public Sprite ItemImage;
    [TextArea]
    public string ItemDescription;
    //public string type=t;
}
