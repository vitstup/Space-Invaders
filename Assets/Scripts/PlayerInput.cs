using UnityEngine;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    [Inject] private Player player;

    private void Update()
    {
        // Получаем значение горизонтальной оси ввода
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (player.gameObject.activeSelf)
        {
            // Передаем значение ввода в логику передвижения игрока
            player.MoveX(moveX);
            player.MoveY(moveY);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.Shoot();
            }
        }
    }
}