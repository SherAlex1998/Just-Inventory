using Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRepositiry 
{
    private SaveLoadManager saveLoadManager;
    public InventoryRepositiry()
    {
        saveLoadManager = new SaveLoadManager();
    }

    public List<SpaceEntity> GetSpaces()
    {
        return new List<SpaceEntity>();
    }

    public void SaveSpace()
    {

    }

    public void LoadSpace()
    {

    }
}
