using Entity;
using Presenter.Prefab;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Presenter.View
{
    public class ContainerScreenView : SpaceScreenView
    {
        private Dictionary<string, List<ItemEntity>> itemStacks;

        public override string GetViewName()
        {
            return "ContainerScreenView";
        }

        private void OnEnable()
        {
            ClearAllItems();
            itemStacks = new Dictionary<string, List<ItemEntity>>();
            SpawnAllItems();
            topBarInteractor.SetTopBarTitle($"Контейнер {inventoryInteractor.CurrentContainer.ItemName}");
            if (inventoryInteractor.CurrentContainer.Parent != null)
                topBarInteractor.SetParentTitle($"в {inventoryInteractor.CurrentContainer.Parent.ItemName}");
        }

        private void OnDisable()
        {
            topBarInteractor.DisableParentTitle();
        }

        private void SpawnAllItems()
        {
            ItemBaseEntity[] items = inventoryInteractor.GetCurrentContainerItems();
            foreach (ItemBaseEntity item in items)
            {
                if (item is ContainerEntity)
                {
                    GameObject newContainer = Instantiate(itemContainerPrefab, content);
                    ContainerPrefab containerPrefab = newContainer.GetComponent<ContainerPrefab>();
                    containerPrefab.SetUp(item as ContainerEntity);
                }
                if (item is ItemEntity) 
                {
                    if (itemStacks.ContainsKey(item.ItemName))
                    {
                        itemStacks[item.ItemName].Add(item as ItemEntity);
                    }
                    else
                    {
                        itemStacks[item.ItemName] = new List<ItemEntity>
                        {
                            item as ItemEntity
                        };
                    }
                }
            }
            foreach (KeyValuePair<string, List<ItemEntity>> itemStack in itemStacks)
            {
                GameObject newItem = Instantiate(itemStackPrefab, content);
                ItemStackPrefab newItemStack = newItem.GetComponent<ItemStackPrefab>();
                newItemStack.SetUp(itemStack.Value.ToArray());
            }
        }
    }
}