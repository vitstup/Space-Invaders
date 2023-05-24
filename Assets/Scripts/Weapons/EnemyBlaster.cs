using UnityEngine;

public class EnemyBlaster : Weapon
{
    public EnemyBlaster(BulletsManager bulletsManager, AudioManager audioManager) : base(bulletsManager, audioManager)
    {

    }

    public override void Shoot(Vector3 position, float direction, string tag)
    {
        var bullet = bulletsManager.enemyBulletsPool.GetElement();
        bullet.Shoot(position, direction, tag);

        audioManager.PlayBlasterSound();
    }
}