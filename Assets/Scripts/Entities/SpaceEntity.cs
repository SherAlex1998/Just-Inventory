using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Entity
{
    public class SpaceEntity
    {
        private string name;

        private List<ItemBaseEntity> items;

        [JsonProperty] private List<ContainerEntity> rootContainers;

        public string Name { get => name; set => name = value; }

        public SpaceEntity()
        {
            items = new List<ItemBaseEntity>();
            rootContainers = new List<ContainerEntity>();
        }

        public void AddItem(ItemBaseEntity item)
        {
            if (item is ContainerEntity)
            {
                var container = (ContainerEntity)item;
                if (container.Parent is null)
                {
                    rootContainers.Add(container);
                }
            }
            items.Add(item);
        }

        public void RemoveItem(ItemBaseEntity item)
        {
            if (item is ContainerEntity)
            {
                var container = (ContainerEntity)item;
                if (container.Parent is null)
                {
                    rootContainers.Remove(container);
                }
            }
            items.Remove(item);
        }

        public ItemBaseEntity[] GetItems()
        {
            return items.ToArray();
        }

        public ContainerEntity[] GetRootContainers()
        {
            return rootContainers.ToArray();
        }
    }
}

