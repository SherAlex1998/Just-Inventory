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
        [SerializeField]
        private TMP_InputField titleInputField;
        [SerializeField]
        private Button addSpaceButton;

        private InventoryInteractor inventoryInteractor;
        private TopBarInteractor topBarInteractor;


        public override string GetViewName()
        {
            return ChangeViewInteractor.ViewNames.SPACE_ADDER_VIEW;
        }

        [Inject]
        private void Constructor(InventoryInteractor inventoryInteractor, TopBarInteractor topBarInteractor)
        {
            this.topBarInteractor = topBarInteractor;
            this.inventoryInteractor = inventoryInteractor;
        }

        private void Start()
        {   
            addSpaceButton.onClick.AddListener(AddNewSpace);
        }

        private void OnEnable()
        {
            titleInputField.text = string.Empty;
            topBarInteractor.SetTopBarTitle("Новое пространство");
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

