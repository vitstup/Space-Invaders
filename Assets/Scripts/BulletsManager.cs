using UnityEngine;
using Zenject;

public class BulletsManager : MonoBehaviour
{
    [SerializeField] private Bullet playerBullet;
    [SerializeField] private Bullet enemyBullet;

    public MonoPool<Bullet> playerBulletsPool { get; private set; }
    public MonoPool<Bullet> enemyBulletsPool { get; private set; }

    [Inject] private DiContainer container;

    private void Awake()
    {
        playerBulletsPool = new MonoPool<Bullet>(playerBullet, 3, true, container);
        enemyBulletsPool = new MonoPool<Bullet>(enemyBullet, 10, true, container);
    }

}