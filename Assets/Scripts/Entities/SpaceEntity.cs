using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class SpaceEntity
    {
        private string name;

        private List<ItemBase> items;

        public string Name { get => name; set => name = value; }

        public SpaceEntity()
        {
            items = new List<ItemBase>();
        }

        public void AddItem(ItemBase item)
        {
            items.Add(item);
        }
    }
}

