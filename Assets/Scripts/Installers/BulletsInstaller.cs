using UnityEngine;
using Zenject;

public class BulletsInstaller : MonoInstaller
{
    [SerializeField] private BulletsManager bulletsManager;

    public override void InstallBindings()
    {
        Container.Bind<BulletsManager>().FromComponentInHierarchy(bulletsManager).AsSingle().NonLazy();
    }
}