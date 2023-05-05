using Interactor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presenter
{
    public class TopBarManager : MonoBehaviour
    {
        [SerializeField]
        private Button backButton, saveButton;
        [SerializeField]
        private TextMeshProUGUI viewTitle;
        [SerializeField]
        private TextMeshProUGUI parentTitle;

        private ChangeViewInteractor changeViewInteractor;
        private TopBarInteractor topBarInteractor;
        private InventoryInteractor inventoryInteractor;

        [Inject]
        private void Constructor(ChangeViewInteractor changeViewInteractor, TopBarInteractor topBarInteractor, InventoryInteractor inventoryInteractor)
        {
            this.changeViewInteractor = changeViewInteractor;
            this.topBarInteractor = topBarInteractor;
            this.inventoryInteractor = inventoryInteractor;
        }

        private void Awake()
        {
            backButton.onClick.AddListener(BackToPreviosView);
            saveButton.onClick.AddListener(SaveSpaces);
            topBarInteractor.onSetTitle += SetTopBarTitle;
            topBarInteractor.onDisableParentTitle += DisableParentTitle;
            topBarInteractor.onSetParentTitle += SetParentTitle;
        }

        private void SaveSpaces()
        {
            inventoryInteractor.SaveSpaces();
        }

        private void BackToPreviosView()
        {
            changeViewInteractor.ChangeViewToPrevios();
        }

        private void SetTopBarTitle(string topBarTitle)
        {
            viewTitle.text = topBarTitle;
        }

        private void SetParentTitle(string parentTitle)
        {
            this.parentTitle.gameObject.SetActive(true);
            this.parentTitle.text = parentTitle;
        }

        private void DisableParentTitle()
        {
            parentTitle.gameObject.SetActive(false);
        }
    }
}

