using UnityEngine;
using Zenject;

public class FleetInstaller : MonoInstaller
{
    [SerializeField] private Fleet fleet;

    public override void InstallBindings()
    {
        Container.Bind<Fleet>().FromComponentInHierarchy(fleet).AsSingle().NonLazy();
    }
}