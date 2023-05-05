using Entity;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactor
{
    public class InventoryInteractor
    {
        private List<SpaceEntity> spaces;

        private InventoryRepository inventoryRepository;

        private SpaceEntity currentSpace;
        private ContainerEntity currentContainer;

        public Action<SpaceEntity> onSpaceCreated;

        public ContainerEntity CurrentContainer { get => currentContainer; set => currentContainer = value; }
        public SpaceEntity CurrentSpace { get => currentSpace; set => currentSpace = value; }

        public InventoryInteractor()
        {
            inventoryRepository = new InventoryRepository();
            spaces = new List<SpaceEntity>();
        }

        public List<SpaceEntity> GetSpaces()
        {
            return inventoryRepository.GetSpaces();
        }

        public void AddSpace(SpaceEntity space)
        {
            foreach (var existingSpace in spaces)
            {
                if (existingSpace.Name == space.Name)
                {
                    Debug.Log("Уже существует пространство с таким именем");
                    return;
                }
            }
            spaces.Add(space);
            onSpaceCreated?.Invoke(space);
        }

        public void RemoveSpace(SpaceEntity space)
        {
            spaces.Remove(space);
        }

        public void LoadSpaces(Action<List<SpaceEntity>> onSucces = null, Action onError = null)
        {
            inventoryRepository.LoadSpaces((spaces) =>
            {
                this.spaces = spaces;
                onSucces?.Invoke(spaces);
            },
            (error) =>
            {
                Debug.Log(error);
            });
        }

        public void SaveSpaces()
        {
            inventoryRepository.SaveSpaces(spaces);
        }

        public void CreateItem(ItemData data)
        {
            ItemEntity item = new ItemEntity();
            item.SetItemData(data);
            item.Id = GetAvailableId();
            if (currentContainer != null)
            {
                currentContainer.AddItem(item);
                item.Parent = currentContainer;
            }
                
            currentSpace.AddItem(item);
            Debug.Log("Added item " + item.ToString() + " to space " + currentSpace.Name);
        }

        public ItemEntity CloneItem(ItemEntity item)
        {
            ItemEntity newItem = (ItemEntity)item.Clone();
            newItem.Id = GetAvailableId();
            if (currentContainer != null)
            {
                currentContainer.AddItem(newItem);
                newItem.Parent = currentContainer;
            }

            currentSpace.AddItem(newItem);
            Debug.Log("Added item " + newItem.ToString() + " to space " + currentSpace.Name);
            return newItem;
        }

        public void CreateContainer(ContainerData data)
        {
            ContainerEntity container = new ContainerEntity();
            container.SetContainerData(data);
            container.Id = GetAvailableId();
            if (currentContainer != null && container.IsContainerable)
            {
                try
                {

                    currentContainer.AddItem(container);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
                container.Parent = currentContainer;
            }   
            currentSpace.AddItem(container);
            Debug.Log("Added container " + container.ToString() + " to space " + currentSpace.Name);
        }

        public void RemoveContainer(ContainerEntity container)
        {
            if (container.Parent != null)
                container.Parent.RemoveItem(container);
            var items = container.GetItems();
            foreach (var item in items)
            {
                if (item is ContainerEntity)
                {
                    RemoveContainer(item as ContainerEntity);
                }
                else
                {
                    RemoveItem(item as ItemEntity);
                }
            }
            currentSpace.RemoveItem(container);
        }

        public void RemoveItem(ItemEntity item)
        {
            item.Parent.RemoveItem(item);
            currentSpace.RemoveItem(item);
        }

        public void RemoveItemStack(List<ItemEntity> itemStack)
        {
            foreach (var item in itemStack)
            {
                RemoveItem(item);
            }
        }
        
        public ContainerEntity[] GetRootContainers()
        {
            return currentSpace.GetRootContainers();
            /*
            ItemBaseEntity[] itemBaseEntities = currentSpace.GetItems();
            List<ContainerEntity> rootContainers = new List<ContainerEntity>();
            foreach (ItemBaseEntity item in itemBaseEntities)
            {
                if (item is ContainerEntity)
                {
                    if ((item as ContainerEntity).Parent is null)
                    {
                        rootContainers.Add(item as ContainerEntity);
                    }
                }
            }
            return rootContainers.ToArray();
            */
        }

        public ItemBaseEntity[] GetCurrentContainerItems()
        {
            return currentContainer.GetItems();
        }
        
        private int GetAvailableId()
        {
            ItemBaseEntity[] items = currentSpace.GetItems();
            int maxId = -1;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Id > maxId)
                    maxId = items[i].Id;
            }
            return maxId + 1;
        }
    }
}

