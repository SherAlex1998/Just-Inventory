using Interactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Presenter.View
{
    public class ViewBase : MonoBehaviour
    {
        protected ChangeViewInteractor changeViewInteractor;

        [Inject]
        private void Construct(ChangeViewInteractor changeViewInteractor)
        {
            this.changeViewInteractor = changeViewInteractor;
        }    

        public virtual string GetViewName()
        {
            return "BaseView";
        }
    }
}

