using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemon : MonoBehaviour
{
    public Item item;
    public MYbag mYbag;
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.CompareTag("Player"))
    //    {

    //    }
    //}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddnewItem();
            Destroy(gameObject);
        }
    }
    void AddnewItem()
    {
        if(!mYbag.items.Contains(item))
        {
            mYbag.items.Add(item);
            
        }
        InvManager.ReshItem();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    
}
