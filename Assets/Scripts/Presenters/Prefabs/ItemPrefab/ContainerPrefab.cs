using Entity;
using Interactor;
using Presenter.Prefab.Element;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presenter.Prefab
{
    public class ContainerPrefab : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] Button showDetailDescription;
        [SerializeField] private FillIndicator weightFillIndicator;
        [SerializeField] private FillIndicator volumeFillIndicator;
        [SerializeField] private ContainerBlock detailBlock;
        [SerializeField] private Button openContainerButton, deleteButton;

        private VerticalLayoutGroup[] verticalLayoutGroups;

        private ChangeViewInteractor changeViewInteractor;
        private InventoryInteractor inventoryInteractor;

        private ContainerEntity holdingContainer;

        [Inject]
        private void Construct(ChangeViewInteractor changeViewInteractor, InventoryInteractor inventoryInteractor)
        {
            this.changeViewInteractor = changeViewInteractor;
            this.inventoryInteractor = inventoryInteractor;
        }

        public void SetUp(ContainerEntity item)
        {
            holdingContainer = item;
            titleText.text = item.ItemName;
            weightFillIndicator.SetMaxValue(item.MaxWeight);
            volumeFillIndicator.SetMaxValue(item.MaxVolume);
            weightFillIndicator.SetCurrentValue(item.NetWeight);
            volumeFillIndicator.SetCurrentValue(item.NetVolume);
            detailBlock.SetUp(item);
        }

        private void Awake()
        {
            showDetailDescription.onClick.AddListener(ToggleDetailedDescription);
            openContainerButton.onClick.AddListener(OpenContainerView);
            deleteButton.onClick.AddListener(DeleteContainer);
        }


        private void DeleteContainer()
        {
            inventoryInteractor.RemoveContainer(holdingContainer);
            Destroy(this.gameObject);
        }

        private void OpenContainerView()
        {
            inventoryInteractor.CurrentContainer = holdingContainer;
            changeViewInteractor.ChangeView(ChangeViewInteractor.ViewNames.CONTAINER_SCREEN_VIEW);
        }

        private void Start()
        {
            verticalLayoutGroups = GetComponentsInChildren<VerticalLayoutGroup>();
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