using Zenject;

public class Enemy : Ship
{
    [Inject] BulletsManager bulletsManager;
    [Inject] MainManager mainManager;
    [Inject] AudioManager audioManager;

    private void Awake()
    {
        weapon = new EnemyBlaster(bulletsManager, audioManager);
    }

    public override void TakeDamage()
    {
        Die();
    }

    public override void Die()
    {
        base.Die();
        mainManager.AddScore(1);
    }

    protected override float GetShootDirection()
    {
        return -1f;
    }
}