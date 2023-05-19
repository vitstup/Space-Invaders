using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private float direction;

    private float minY;
    private float maxY;

    private void Start()
    {
        GetBorders();
    }

    public void Shoot(Vector2 position, float direction, string tag)
    {
        transform.position = new Vector2(position.x, position.y + direction / 7.5f);
        this.direction = direction;
        this.tag = tag;
    }

    private void Update()
    {
        transform.Translate(Vector2.up * direction * speed * Time.deltaTime);

        if (IsOutOfScreen()) gameObject.SetActive(false);
    }

    private void GetBorders()
    {
        // Получаем размеры экрана в пикселях
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Получаем размеры экрана в мировых координатах
        Camera mainCamera = Camera.main;
        Vector3 screenSize = mainCamera.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, 0));

        float height = GetComponentInChildren<Renderer>().bounds.size.y;

        minY = -1f * screenSize.y - height / 2f;
        maxY = screenSize.y + height / 2f;
    }

    private bool IsOutOfScreen()
    {
        return transform.position.y < minY || transform.position.y > maxY;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}