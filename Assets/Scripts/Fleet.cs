using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Fleet : MonoBehaviour
{
    private List<Ship> ships;

    private MonoPool<Ship> shipsPool;

    [SerializeField] private Ship prefab; 
    [SerializeField] private float spacing = 1f;
    [SerializeField] private float movementDistance = 1f;
    [SerializeField] private float timeBeetwenMovement = 0.33f;

    [SerializeField] private int fleetLength;
    [SerializeField] private int fleetHeight;

    [Inject] private DiContainer container;
    [Inject] private MainManager mainManager;

    private float direction = 1f;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
        CreateFleet();
    }

    public void CreateFleet()
    {
        transform.position = startPos;
        StopAllCoroutines();
        Initialize(prefab, fleetLength, fleetHeight);
        StartCoroutine(MoveRoutine());
        StartCoroutine(ShootRoutine());
    }

    private void Initialize(Ship prefab, int sizeX = 5, int sizeY = 1)
    {
        if (shipsPool == null) shipsPool = new MonoPool<Ship>(prefab, sizeX * sizeY, true, container);
        else
        {
            var clearShips = shipsPool.GetBusyElements();
            foreach (var sh in clearShips) sh.gameObject.SetActive(false);
        }
        ships = shipsPool.GetFreeElements();
        SetShips(sizeX, sizeY);
    }

    private void SetShips(int sizeX, int sizeY)
    {
        float startX = transform.position.x - ((sizeX - 1) * spacing * 0.5f);
        float startY = transform.position.y;

        int shipIndex = 0;

        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Ship ship = ships[shipIndex];
                ship.gameObject.SetActive(true);
                ship.transform.SetParent(transform, false);
                ship.transform.position = new Vector3(startX + x * spacing, startY + y * spacing, transform.position.z);
                shipIndex++;
            }
        }
    }

    private IEnumerator MoveRoutine()
    {
        yield return new WaitForSeconds(timeBeetwenMovement);
        MoveFleet();
        StartCoroutine(MoveRoutine());
    }

    private void MoveFleet()
    {
        // Перемещаем флот вправо или влево
        transform.position += Vector3.right * direction * movementDistance;

        // Проверяем, достиг ли флот края экрана
        if (ShouldChangeDirection())
        {
            // Изменяем направление движения
            direction *= -1;

            // Спускаем флот ниже
            transform.position += Vector3.down * movementDistance;
        }
    }

    private bool ShouldChangeDirection()
    {
        foreach (var ship in ships)
        {
            if (ship.OutSideOfBorders(out bool y) && ship.gameObject.activeSelf) { if (y) { mainManager.Lose(); } return true; }
        }
        return false;
    }

    private IEnumerator ShootRoutine()
    {
        bool anyActive = false;
        foreach (var ship in ships)
        {
            if (ship.gameObject.activeSelf)
            {
                yield return new WaitForSeconds(0.1f);
                if (Random.Range(0, 1f) > .8f) ship.Shoot();
                anyActive = true;
            }
        }
        if (anyActive) StartCoroutine(ShootRoutine());
        else mainManager.ContinueLevel();
    }

}