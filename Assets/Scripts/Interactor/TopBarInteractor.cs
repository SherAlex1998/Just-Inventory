using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactor
{
    public class TopBarInteractor
    {
        public Action<string> onSetTitle;
        public Action<string> onSetParentTitle;
        public Action onDisableParentTitle;

        public void SetTopBarTitle(string title)
        {
            onSetTitle?.Invoke(title);
        }

        public void SetParentTitle(string parentTitle)
        {
            onSetParentTitle?.Invoke(parentTitle);
        }

        public void DisableParentTitle()
        {
            onDisableParentTitle?.Invoke();
        }
    }
}

