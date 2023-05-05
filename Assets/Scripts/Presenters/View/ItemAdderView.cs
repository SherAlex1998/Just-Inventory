using Entity;
using Interactor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presenter.View
{
    public class ItemAdderView: ViewBase
    {
        [SerializeField] TextMeshProUGUI parentNameText;
        [SerializeField] Toggle isContainerToggle;
        [SerializeField] GameObject containerAttributeBlock;
        [SerializeField] TMP_InputField maxWeightInputField, maxVolumeInputField;
        [SerializeField] Toggle isContainerableToggle;
        [SerializeField] TMP_InputField nameInputField, weightInputField, volumeInputField, descriptionInputField;
        [SerializeField] Button addItemButton;

        private InventoryInteractor inventoryInteractor;
        private TopBarInteractor topBarInteractor;

        private VerticalLayoutGroup[] verticalLayoutGroups;

        public struct ItemAdderViewData
        {
            public string ParentName;
        }

        public override string GetViewName()
        {
            return "ItemAdderView";
        }

        [Inject]
        private void Construct(InventoryInteractor inventoryInteractor, TopBarInteractor topBarInteractor)
        {
            this.inventoryInteractor = inventoryInteractor;
            this.topBarInteractor = topBarInteractor;
        }

        private void Start()
        {
            containerAttributeBlock.SetActive(false);
            isContainerToggle.isOn = false;

            addItemButton.onClick.AddListener(AddItem);
            isContainerToggle.onValueChanged.AddListener(OpenContainerAttributeForm);

            verticalLayoutGroups = GetComponentsInChildren<VerticalLayoutGroup>();
        }

        private void OnEnable()
        {
            if (inventoryInteractor.CurrentContainer != null)
            {
                parentNameText.text = inventoryInteractor.CurrentContainer.ItemName;
                topBarInteractor.SetTopBarTitle(parentNameText.text);
                topBarInteractor.SetTopBarTitle($"Новый предмет в {inventoryInteractor.CurrentContainer.ItemName}");
            }
            else
            {
                topBarInteractor.SetTopBarTitle("Новый контейнер");
                parentNameText.text = "Нет родительского контейнера. Это корневой элемент.";
            }
            containerAttributeBlock.SetActive(false);
            isContainerToggle.isOn = false;
        }

        private void OpenContainerAttributeForm(bool value)
        {
            containerAttributeBlock.SetActive(value);
            UpdateGrids();
        }

        private void AddItem()
        {
            string name = nameInputField.text;
            string description = descriptionInputField.text;
            float weight = 5;
            float volume = 5;

            try
            {
                weight = float.Parse(weightInputField.text);
                volume = float.Parse(volumeInputField.text);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return;
            }

            if (name.Length <= 0) Debug.Log("Не дано имя");

            if (isContainerToggle.isOn)
            {
                float maxWeight = 30;
                float maxVolume = 30;
                bool isContainerabale = isContainerableToggle.isOn;

                try
                {
                    maxWeight = float.Parse(maxWeightInputField.text);
                    maxVolume = float.Parse(maxVolumeInputField.text);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    return;
                }

                ContainerData containerData = new ContainerData()
                {
                    ContainerName = name,
                    ContainerDescription = description,
                    Weight = weight,
                    Volume = volume,
                    MaxWeight = maxWeight,
                    MaxVolume = maxVolume,
                    NetWeight = 0,
                    NetVolume = 0,
                    IsContainerable = isContainerabale,
                    Items = new List<ItemBaseEntity>()
                };

                inventoryInteractor.CreateContainer(containerData);
            }
            else
            {
                ItemData data = new ItemData()
                {
                    ItemName = name,
                    ItemDescription = description,
                    Weight = weight,
                    Volume = volume
                };

                inventoryInteractor.CreateItem(data);
            }
            changeViewInteractor.ChangeViewToPrevios();
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