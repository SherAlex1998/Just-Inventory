using Presenter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Interactor;

namespace Presenter.View
{
    public class SpaceScreenView : ViewBase
    {
        [SerializeField] Button AddContainerButton;

        private void Start()
        {
            AddContainerButton.onClick.AddListener(delegate {
                changeViewInteractor.ChangeView(
                ChangeViewInteractor.ViewNames.CONTAINER_ADD_SCREEN); });
        }
    }
}

