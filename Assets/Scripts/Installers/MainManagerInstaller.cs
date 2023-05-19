using UnityEngine;
using Zenject;

public class MainManagerInstaller : MonoInstaller
{
    [SerializeField] private MainManager mainManager;

    public override void InstallBindings()
    {
        Container.Bind<MainManager>().FromComponentInHierarchy(mainManager).AsSingle().NonLazy();
    }
}