using Presenter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interactor;
using Entity;
using Zenject;
using Presenter.Prefab;

namespace Presenter.View
{
    public class SpaceScreenView : ViewBase
    {
        [SerializeField] protected Button AddItemButton;
        [SerializeField] protected GameObject itemStackPrefab;
        [SerializeField] protected GameObject itemContainerPrefab;
        [SerializeField] protected Transform content;

        protected InventoryInteractor inventoryInteractor;
        protected TopBarInteractor topBarInteractor;


        public override string GetViewName()
        {
            return ChangeViewInteractor.ViewNames.SPACE_SCREEN;
        }

        [Inject]
        private void Construct(InventoryInteractor inventoryInteractor, TopBarInteractor topBarInteractor)
        {
            this.inventoryInteractor = inventoryInteractor;
            this.topBarInteractor = topBarInteractor;
        }

        private void Start()
        {
            AddItemButton.onClick.AddListener(delegate {
                changeViewInteractor.ChangeView(
                ChangeViewInteractor.ViewNames.ITEM_ADDER_VIEW); });
        }

        private void OnEnable()
        {
            ClearAllItems();
            SpawnRootContainers();
            topBarInteractor.SetTopBarTitle($"Пространство {inventoryInteractor.CurrentSpace.Name}");
        }

        private void SpawnRootContainers()
        {
            ContainerEntity[] containers = inventoryInteractor.GetRootContainers();
            foreach (var container in containers)
            {
                GameObject newContainer = Instantiate(itemContainerPrefab, content);
                ContainerPrefab containerPrefab = newContainer.GetComponent<ContainerPrefab>();
                containerPrefab.SetUp(container);
            }
        }

        protected void ClearAllItems()
        {
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

