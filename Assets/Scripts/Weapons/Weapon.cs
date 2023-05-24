using UnityEngine;

public abstract class Weapon
{
    protected BulletsManager bulletsManager;
    protected AudioManager audioManager;

    public Weapon(BulletsManager bulletsManager, AudioManager audioManager)
    {
        this.bulletsManager = bulletsManager;
        this.audioManager = audioManager;
    }

    public abstract void Shoot(Vector3 position, float direction, string tag);
}