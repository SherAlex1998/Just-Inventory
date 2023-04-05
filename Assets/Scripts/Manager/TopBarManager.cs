using Interactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Presenter
{
    public class TopBarManager : MonoBehaviour
    {
        private ChangeViewInteractor changeViewInteractor;

        [SerializeField]
        Button backButton;

        [Inject]
        private void Constructor(ChangeViewInteractor changeViewInteractor)
        {
            this.changeViewInteractor = changeViewInteractor;
        }

        private void Start()
        {
            backButton.onClick.AddListener(BackToPreviosView);
        }

        private void BackToPreviosView()
        {
            changeViewInteractor.ChangeViewToPrevios();
        }
    }
}

