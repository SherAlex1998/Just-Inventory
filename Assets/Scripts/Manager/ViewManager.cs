using Presenter.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactor;
using Zenject;
using UnityEngine.UI;

namespace Manager
{
    public class ViewManager : MonoBehaviour
    {
        private ChangeViewInteractor changeViewInteractor;

        [SerializeField]
        List<ViewBase> ViewList = new List<ViewBase>();

        [Inject]
        private void Constructor(ChangeViewInteractor changeViewInteractor)
        {
            this.changeViewInteractor = changeViewInteractor;
            changeViewInteractor.onViewChanged += OnViewChanged;
        }

        private void OnViewChanged(string newViewname, string previosViewName)
        {
            foreach (ViewBase view in ViewList)
            {
                if (view.GetViewName() == newViewname)
                {
                    view.gameObject.SetActive(true);
                    view.SetUp<string>("temp");
                }
                    
                if (view.GetViewName() == previosViewName)
                    view.gameObject.SetActive(false);
            }
        }
    }
}

