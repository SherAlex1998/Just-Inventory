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
        [SerializeField] Button addNewSpaceButton;
        [SerializeField] Transform content;
        [SerializeField] SpaceItemPrefab spaceItemPrefab;

        private InventoryInteractor inventoryInteractor;
        private TopBarInteractor topBarInteractor;

        private List<SpaceEntity> allSpaces;

        private void Start()
        {
            allSpaces = new List<SpaceEntity>();
            inventoryInteractor.LoadSpaces((spaces) =>
            {
                allSpaces = spaces;
                UpdateSpaces(spaces);
            });
            addNewSpaceButton.onClick.AddListener(
                () => changeViewInteractor.ChangeView(ChangeViewInteractor.ViewNames.SPACE_ADDER_VIEW));
            inventoryInteractor.onSpaceCreated += UpdateSpaces;
            topBarInteractor.SetTopBarTitle("Ваши пространства");
        }

        private void OnEnable()
        {
            topBarInteractor.SetTopBarTitle("Ваши пространства");
        }

        [Inject]
        private void Constructor(InventoryInteractor inventoryInteractor, TopBarInteractor topBarInteractor)
        {
            this.inventoryInteractor = inventoryInteractor;
            this.topBarInteractor = topBarInteractor;
        }

        public void UpdateSpaces(SpaceEntity spaceEntity)
        {
            SpaceItemPrefab newSpaceItem = Instantiate(spaceItemPrefab, content);
            allSpaces.Add(spaceEntity);
            newSpaceItem.SetUp(spaceEntity);
        }

        public void UpdateSpaces(List<SpaceEntity> spaceEntitys)
        {
            foreach (SpaceEntity spaceEntity in spaceEntitys)
            {
                SpaceItemPrefab newSpaceItem = Instantiate(spaceItemPrefab, content);
                newSpaceItem.SetUp(spaceEntity);
            }
        }

        public override string GetViewName()
        {
            return ChangeViewInteractor.ViewNames.SPACES_VIEW;
        }
    }
}

