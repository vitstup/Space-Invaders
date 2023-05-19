using Zenject;

public class Enemy : Ship
{
    [Inject] BulletsManager bulletsManager;
    [Inject] MainManager mainManager;

    public override void Shoot()
    {
        var bullet = bulletsManager.enemyBulletsPool.GetElement();
        bullet.Shoot(transform.position, -1f, tag);
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
}