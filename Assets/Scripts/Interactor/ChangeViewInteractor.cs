using Presenter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static Interactor.ChangeViewInteractor;

namespace Interactor
{
    public class ChangeViewInteractor
    {
        private string currentView;

        private string previousView;

        public Action<string, string> onViewChanged;

        public string CurrentView { get => currentView; private set => currentView = value; }
        public string PreviousView { get => previousView; private set => previousView = value; }

        private InventoryInteractor inventoryInteractor;

        [Inject]
        ChangeViewInteractor(InventoryInteractor inventoryInteractor)
        {
            this.inventoryInteractor = inventoryInteractor;
            currentView = ChangeViewInteractor.ViewNames.SPACES_VIEW;
            previousView = "";
        }

        public void ChangeView(string viewName)
        {
            previousView = currentView;
            currentView = viewName;
            onViewChanged?.Invoke(viewName, previousView);
        }

        public void ChangeViewToPrevios()
        {
            if (currentView == ViewNames.SPACE_SCREEN)
            {
                inventoryInteractor.CurrentSpace = null;
                ChangeView(ViewNames.SPACES_VIEW);
                return;
            }
            if (currentView == ViewNames.CONTAINER_SCREEN_VIEW)
            {
                if (inventoryInteractor.CurrentContainer.Parent != null)
                {
                    inventoryInteractor.CurrentContainer = inventoryInteractor.CurrentContainer.Parent;
                    ChangeView(ViewNames.CONTAINER_SCREEN_VIEW);
                }
                else
                {
                    inventoryInteractor.CurrentContainer = null;
                    ChangeView(ViewNames.SPACE_SCREEN);
                }
                return;
            }
            if (string.IsNullOrEmpty(previousView)) return;
            onViewChanged?.Invoke(previousView, currentView);
            string temp = currentView;
            currentView = previousView;
            previousView = temp;
        }

        public struct ViewNames
        {
            public readonly static string SPACES_VIEW = "SpacesView";
            public readonly static string SPACE_ADDER_VIEW = "SpaceAdderView";
            public readonly static string SPACE_SCREEN = "SpaceScreenView";
            public readonly static string ITEM_ADDER_VIEW = "ItemAdderView";
            public readonly static string CONTAINER_SCREEN_VIEW = "ContainerScreenView";
        }
    }
}
