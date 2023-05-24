using UnityEngine;

public abstract class Ship : MonoBehaviour, IShootable, IMovableHorizontal, IMovableVertical, IDamagable
{
    protected Vector2 minPos;
    protected Vector2 maxPos;

    [SerializeField] protected float horizontalSpeed;
    [SerializeField] protected float verticalSpeed;

    protected SpriteRenderer spriteRenderer;

    protected Weapon weapon;

    protected void Start()
    {
        GetBorders();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void GetBorders()
    {
        // Получаем размеры экрана в пикселях
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Получаем размеры экрана в мировых координатах
        Camera mainCamera = Camera.main;
        Vector3 screenSize = mainCamera.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, 0));

        float width = GetComponentInChildren<Renderer>().bounds.size.x;
        float height = GetComponentInChildren<Renderer>().bounds.size.y;

        minPos.x = -1f * screenSize.x + width / 2f;
        maxPos.x = screenSize.x - width / 2f;

        minPos.y = -1f * screenSize.y + height / 2f;
        maxPos.y = screenSize.y - height / 2f;
    }

    public bool OutSideOfBorders(out bool y)
    {
        y = false;
        if (transform.position.x > maxPos.x || transform.position.x < minPos.x) return true;
        else if (transform.position.y > maxPos.y) return true;
        else if (transform.position.y < minPos.y) { y = true; return true; }
        else return false;
    }

    public void MoveX(float distance)
    {
        // Вычисляем новую позицию корабля с учетом скорости и времени
        Vector3 newPosition = transform.position + new Vector3(distance * horizontalSpeed * Time.deltaTime, 0, 0);

        // Ограничиваем позицию корабля в пределах экрана
        newPosition.x = Mathf.Clamp(newPosition.x, minPos.x, maxPos.x);

        // Применяем новую позицию к кораблю
        transform.position = newPosition;
    }

    public void MoveY(float distance)
    {
        // Вычисляем новую позицию корабля с учетом скорости и времени
        Vector3 newPosition = transform.position + new Vector3(0, distance * verticalSpeed * Time.deltaTime, 0);

        // Ограничиваем позицию корабля в пределах экрана
        newPosition.y = Mathf.Clamp(newPosition.y, minPos.y, maxPos.y);

        // Применяем новую позицию к кораблю
        transform.position = newPosition;

    }

    public void Shoot()
    {
        weapon.Shoot(transform.position, GetShootDirection(), tag);
    }

    protected abstract float GetShootDirection();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<Bullet>();

        if (bullet != null && bullet.tag != tag)
        {
            bullet.Hide();
            TakeDamage();
        }
    }

    public abstract void TakeDamage();

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}