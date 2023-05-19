using TMPro;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour
{
    [Inject] private Player player;
    [Inject] private MainManager mainManager;
    private ObjectPool heartPool;

    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private RectTransform heartsPanel;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Canvas restartCanvas;

    private void Awake()
    {
        heartPool = new ObjectPool(heartPrefab, 3, true);
        Restarted();
    }

    public void ChangeLives()
    {
        foreach(var obj in heartPool.GetBusyElements()) obj.gameObject.SetActive(false);

        for (int i = 0; i < player.livePoints; i++)
        {
            var heart = heartPool.GetElement();
            heart.transform.SetParent(heartsPanel, false);
        }
    }

    public void ChangeScoreText()
    {
        scoreText.text = "Score: " + mainManager.score.ToString();
    }

    public void OpenRestart()
    {
        restartCanvas.gameObject.SetActive(true);
    }

    public void Restarted()
    {
        ChangeLives();
        ChangeScoreText();
        restartCanvas.gameObject.SetActive(false);
    }
}