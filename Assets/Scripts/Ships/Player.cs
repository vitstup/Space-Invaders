using System.Collections;
using UnityEngine;
using Zenject;

public class Player : Ship
{
    [Inject] BulletsManager bulletsManager;
    [Inject] UIManager uiManager;
    [Inject] MainManager mainManager;
    [Inject] AudioManager audioManager;

    [field: SerializeField] public int livePoints { get; private set; }

    [SerializeField] private int invinsibleTime;

    private bool invinsible;

    private void Awake()
    {
        weapon = new EnemyBlaster(bulletsManager, audioManager);
    }

    public override void TakeDamage()
    {
        if (!invinsible)
        {
            livePoints--;
            uiManager.ChangeLives();
            StartCoroutine(Invincibility());
            if (livePoints <= 0) Die();
        }
    }

    private IEnumerator Invincibility()
    {
        invinsible = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.33f);
        yield return new WaitForSeconds(invinsibleTime);
        ResetInvincibility();
    }

    public void ResetHealthPoints()
    {
        livePoints = 3;
        uiManager.ChangeLives(); 
    }

    public void AddHealthPoints(int livePoints)
    {
        this.livePoints += livePoints;
        uiManager.ChangeLives();
    }

    public override void Die()
    {
        base.Die();
        mainManager.Lose();
    }

    public void SetStartPosition()
    {
        transform.position = new Vector2(0, -1);
    }

    protected override float GetShootDirection()
    {
        return 1f;
    }

    public void ResetInvincibility()
    {
        spriteRenderer.color = Color.white;
        invinsible = false;
    }
}