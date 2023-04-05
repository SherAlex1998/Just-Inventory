using Entity;
using Interactor;
using Presenter.Prefab;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presenter.View
{
    public class SpacesView : ViewBase
    {
        private InventoryInteractor inventoryInteractor;

        [SerializeField] Button addNewViewButton;
        [SerializeField] Transform content;
        [SerializeField] SpaceItemPrefab spaceItemPrefab;

        private void Start()
        {
            addNewViewButton.onClick.AddListener(
                () => changeViewInteractor.ChangeView(ChangeViewInteractor.ViewNames.SPACE_ADDER_VIEW));
            inventoryInteractor.onSpaceCreated += UpdateSpaces;
        }

        [Inject]
        private void Constructor(InventoryInteractor inventoryInteractor)
        {
            this.inventoryInteractor = inventoryInteractor;
        }

        public void UpdateSpaces(SpaceEntity spaceEntity)
        {
            SpaceItemPrefab newSpaceItem = Instantiate(spaceItemPrefab, content);
            newSpaceItem.SetUp(spaceEntity);
        }

        public override string GetViewName()
        {
            return ChangeViewInteractor.ViewNames.SPACES_VIEW;
        }
    }
}

