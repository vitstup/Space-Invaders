using UnityEngine;

public class Blaster : Weapon
{
    public Blaster(BulletsManager bulletsManager, AudioManager audioManager) : base(bulletsManager, audioManager)
    {

    }

    public override void Shoot(Vector3 position, float direction, string tag)
    {
        var bullet = bulletsManager.playerBulletsPool.GetElement();
        bullet.Shoot(position, direction, tag);

        audioManager.PlayBlasterSound();
    }
}