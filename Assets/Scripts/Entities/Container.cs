using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : ItemBase
{
    protected float maxWeight;
    protected float maxVolume;

    protected List<ItemBase> items;

    public Container()
    {
        items = new List<ItemBase>();
    }

    public void AddItem(ItemBase item)
    {
        items.Add(item);
    }
}
