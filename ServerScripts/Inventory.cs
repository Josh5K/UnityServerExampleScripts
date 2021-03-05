using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] items;
    public int itemSlots = 12;
    // Start is called before the first frame update
    void Start()
    {
        items = new Item[itemSlots];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwapItemsSlots(int from, int to)
    {
        Item itemToSwap = items[from];
        items[from] = items[to];
        items[to] = itemToSwap;
    }
}
