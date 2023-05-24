using UnityEngine;
using Zenject;

public class MainManager : MonoBehaviour
{
    [Inject] UIManager uiManager;
    [Inject] Player player;
    [Inject] Fleet fleet;

    public int score { get; private set; }
    public bool lose { get; private set; }

    public void AddScore(int score)
    {
        this.score += score;
        uiManager.ChangeScoreText();
    }

    public void Lose()
    {
        lose = true;
        uiManager.OpenRestart();
    }

    public void ResetLevel()
    {
        lose = false;
        score = 0;
        player.gameObject.SetActive(true); 
        player.ResetHealthPoints();
        fleet.CreateFleet();
        player.SetStartPosition();
        player.ResetInvincibility();

        uiManager.Restarted();
    }

    public void ContinueLevel()
    {
        player.AddHealthPoints(1);
        fleet.CreateFleet();
    }
}