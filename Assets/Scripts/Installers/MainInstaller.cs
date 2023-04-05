using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Interactor;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.BindInterfacesAndSelfTo<AccountInteractor>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TopBarInteractor>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ChangeViewInteractor>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InventoryInteractor>().AsSingle().NonLazy();  
    }
}
