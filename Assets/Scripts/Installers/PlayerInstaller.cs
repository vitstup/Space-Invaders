using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player player;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromComponentInNewPrefab(player).AsSingle().NonLazy();
        player.SetStartPosition();
    }
}