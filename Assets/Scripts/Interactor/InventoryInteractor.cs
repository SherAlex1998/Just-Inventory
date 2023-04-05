using Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactor
{
    public class InventoryInteractor
    {
        private List<SpaceEntity> spaces;

        private InventoryRepositiry inventoryRepositiry;

        public Action<SpaceEntity> onSpaceCreated;

        public InventoryInteractor()
        {
            inventoryRepositiry = new InventoryRepositiry();
            spaces = inventoryRepositiry.GetSpaces();
        }

        public List<SpaceEntity> GetSpaces()
        {
            return inventoryRepositiry.GetSpaces();
        }

        public void AddSpace(SpaceEntity space)
        {
            spaces.Add(space);
            onSpaceCreated?.Invoke(space);
        }

        public void RemoveSpace(SpaceEntity space)
        {
            spaces.Remove(space);
        }
    }
}

