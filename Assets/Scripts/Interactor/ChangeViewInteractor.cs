using Presenter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        ChangeViewInteractor()
        {
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
            if (string.IsNullOrEmpty(previousView)) return;
            onViewChanged?.Invoke(previousView, currentView);
            string temp = currentView;
            currentView = previousView;
            previousView = temp;
        }

        public struct ViewNames
        {
            public static string SPACES_VIEW = "SpacesView";
            public static string SPACE_ADDER_VIEW = "SpaceAdderView";
            public static string SPACE_SCREEN = "SpaceScreenView";
            public static string CONTAINER_ADD_SCREEN = "ContainerAdderView";
        }
    }
}
