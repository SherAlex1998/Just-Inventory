using Entity;
using Interactor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presenter.View
{
    public class SpaceAdderView : ViewBase
    {
        private InventoryInteractor inventoryInteractor;

        [SerializeField]
        private TMP_InputField titleInputField;
        [SerializeField]
        private Button addSpaceButton;


        public override string GetViewName()
        {
            return ChangeViewInteractor.ViewNames.SPACE_ADDER_VIEW;
        }

        [Inject]
        private void Constructor(InventoryInteractor inventoryInteractor)
        {
            this.inventoryInteractor = inventoryInteractor;
        }

        private void Start()
        {   
            addSpaceButton.onClick.AddListener(AddNewSpace);
        }
        /*
        private override void SetUp()
        {
            titleInputField.text = string.Empty;
        }
        */

        private void OnEnable()
        {
            titleInputField.text = string.Empty;
        }

        public void AddNewSpace()
        {
            if (titleInputField.text.Length > 0)
            {
                SpaceEntity spaceEntity = new SpaceEntity();
                spaceEntity.Name = titleInputField.text;
                inventoryInteractor.AddSpace(spaceEntity);
                changeViewInteractor.ChangeView(ChangeViewInteractor.ViewNames.SPACES_VIEW);
            }
            else
            {
                Debug.Log("Empty name");
            }
        }
    }
}

