using UnityEngine;

public class MobileInput : PlayerInput
{
    private bool isMoving = false;
    private bool isShooting = false;
    private Vector2 moveStartPosition;
    private bool hasShot = false;

    protected override void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x < Screen.width / 2f)
                {
                    // Движение
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            isMoving = true;
                            moveStartPosition = touch.position;
                            break;
                        case TouchPhase.Moved:
                            Vector2 swipeDirection = touch.position - moveStartPosition;
                            moveX = swipeDirection.normalized.x;
                            moveY = swipeDirection.normalized.y;
                            break;
                        case TouchPhase.Ended:
                            isMoving = false;
                            break;
                    }
                }
                else
                {
                    // Стрельба
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            if (!hasShot)
                            {
                                isShooting = true;
                                player.Shoot();
                                hasShot = true;
                            }
                            break;
                        case TouchPhase.Ended:
                            isShooting = false;
                            hasShot = false;
                            break;
                    }
                }
            }
        }
        else
        {
            isMoving = false;
            isShooting = false;
        }

        if (player.gameObject.activeSelf)
        {
            if (isMoving)
            {
                player.MoveX(moveX);
                player.MoveY(moveY);
            }
            else
            {
                player.MoveX(0f);
                player.MoveY(0f);
            }
        }
    }
}