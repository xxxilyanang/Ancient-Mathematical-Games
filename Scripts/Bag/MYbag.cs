using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Bag", menuName = "INvent/New Bag")]

public class MYbag : ScriptableObject
{
   public  List<Item> items = new List<Item>();
}
