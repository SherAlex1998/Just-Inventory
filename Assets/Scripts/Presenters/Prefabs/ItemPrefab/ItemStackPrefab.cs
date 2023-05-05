using Presenter.Prefab.Element;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Entity;
using UnityEngine.UI;
using Interactor;
using System.Linq;
using Zenject;
using System;
using JetBrains.Annotations;

namespace Presenter.Prefab
{
    public class ItemStackPrefab : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] FloatValueBlock weightBlock;
        [SerializeField] FloatValueBlock volumeBlock;
        [SerializeField] IntValueBlock countBlock;
        [SerializeField] Button buttonPlus, buttonMinus;
        [SerializeField] Button showDetailDescription, deleteButton;
        [SerializeField] ItemPrefab detailBlock;

        private InventoryInteractor inventoryInteractor;

        private VerticalLayoutGroup[] verticalLayoutGroups;

        private List<ItemEntity> holdingItems;
        private ContainerEntity parent;

        [Inject]
        private void Construct(InventoryInteractor inventoryInteractor)
        {
            this.inventoryInteractor = inventoryInteractor;
            holdingItems = new List<ItemEntity>();
        }

        public void SetUp(ItemEntity[] items)
        {
            titleText.text = items[0].ItemName;
            detailBlock.SetUp(items[0]);
            float totalWeight = 0;
            float totalVolume = 0;
            foreach (ItemEntity item in items)
            {
                totalVolume += item.Volume;
                totalWeight += item.Weight;
            }
            weightBlock.SetValue(totalWeight);
            volumeBlock.SetValue(totalVolume);
            countBlock.SetValue(items.Length);
            holdingItems = items.ToList<ItemEntity>();
            parent = items[0].Parent;
        }

        public void AddItemToStack(ItemEntity item)
        {
            countBlock.AddtoValue(1);
            weightBlock.AddtoValue(item.Weight);
            volumeBlock.AddtoValue(item.Volume);
            holdingItems.Add(item);
        }

        public void RemoveItemFromStack(ItemEntity item)
        {
            countBlock.SubtractFromTheValue(1);
            weightBlock.SubtractFromTheValue(item.Weight);
            volumeBlock.SubtractFromTheValue(item.Volume);
            holdingItems.Remove(item);
        }

        private void Awake()
        {
            buttonPlus.onClick.AddListener(OnPlusItem);
            buttonMinus.onClick.AddListener(OnMinusItem);
            deleteButton.onClick.AddListener(DeleteThisStack);
            showDetailDescription.onClick.AddListener(ToggleDetailedDescription);
        }

        private void Start()
        {
            verticalLayoutGroups = GetComponentsInChildren<VerticalLayoutGroup>();
        }

        private void OnPlusItem()
        {
            var newItem = inventoryInteractor.CloneItem(holdingItems[0]);
            AddItemToStack(newItem);
        }

        private void OnMinusItem()
        {
            inventoryInteractor.RemoveItem(holdingItems[^1]);
            RemoveItemFromStack(holdingItems[^1]);
            if (holdingItems.Count == 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void DeleteThisStack()
        {
            inventoryInteractor.RemoveItemStack(holdingItems);
            Destroy(this.gameObject);
        }

        private void ToggleDetailedDescription()
        {
            if (!detailBlock.gameObject.activeSelf)
                detailBlock.gameObject.SetActive(true);
            else
                detailBlock.gameObject.SetActive(false);
            UpdateGrids();
        }

        private void UpdateGrids()
        {
            foreach (var grid in verticalLayoutGroups)
            {
                grid.CalculateLayoutInputHorizontal();
                grid.CalculateLayoutInputVertical();
                grid.SetLayoutHorizontal();
                grid.SetLayoutVertical();
                LayoutRebuilder.ForceRebuildLayoutImmediate(grid.GetComponent<RectTransform>());
            }
        }
    }
}

