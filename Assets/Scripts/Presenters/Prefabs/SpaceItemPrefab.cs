using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Entity;
using UnityEngine.UI;
using System;
using Interactor;
using Zenject;

namespace Presenter.Prefab
{
    [RequireComponent(typeof(Button))]
    public class SpaceItemPrefab : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText;

        private SpaceEntity holdingSpaceEntity;

        private Button openSpaceButton;

        private ChangeViewInteractor changeViewInteractor;
        private InventoryInteractor inventoryInteractor;

        [Inject]
        private void Construct(ChangeViewInteractor changeViewInteractor, InventoryInteractor inventoryInteractor)
        {
            this.inventoryInteractor = inventoryInteractor;
            this.changeViewInteractor = changeViewInteractor;
        }

        public void SetUp(SpaceEntity spaceIntity)
        {
            holdingSpaceEntity = spaceIntity;
            titleText.text = holdingSpaceEntity.Name.ToString();
        }
        
        public void Awake()
        {
            openSpaceButton = GetComponent<Button>();
            openSpaceButton.onClick.AddListener(OpenSpace);
        }
        
        private void OpenSpace()
        {
            inventoryInteractor.CurrentSpace = holdingSpaceEntity;
            changeViewInteractor.ChangeView(ChangeViewInteractor.ViewNames.SPACE_SCREEN);
        }
    }
}

