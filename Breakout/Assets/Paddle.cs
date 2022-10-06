using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private Play playState;

    private Vector2 dimensions;
    private float Width => dimensions.x;
    private float dx;
    private float speed = 10;

    public enum Color { blue=0, green=1, red=2, purple=3 }
    public enum Size { small=0, medium=1, big=2, huge=3 }

    private void Update()
    {
        InputCheck();
        Move();
    }

    public void SetSkin (Color color, Size size)
    {
        int index = (int)color * 4 + (int)size;
        Sprite skin = Quads.Paddles[index];
        if (skin == null) Debug.LogError($"Failed to load paddle skin of code {index}");
        GetComponent<SpriteRenderer>().sprite = skin;
        UpdateDimentions();
    }

    private void UpdateDimentions()
    {
        dimensions = GetComponent<SpriteRenderer>().bounds.size;
        GetComponent<BoxCollider2D>().size = dimensions;
    }

    private void InputCheck ()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            dx = -speed;
        else if (Input.GetKey(KeyCode.RightArrow))
            dx = speed;
        else
            dx = 0;
    }

    private void Move ()
    {
        transform.position += Vector3.right * dx * Time.deltaTime;
        if (Mathf.Abs(transform.position.x)+Width/2 > Camera.Width / 2)
        {
            if (transform.position.x > 0)
                transform.position = new Vector3(Camera.Width / 2 - Width / 2, transform.position.y);
            if (transform.position.x < 0)
                transform.position = new Vector3(-Camera.Width / 2 + Width / 2, transform.position.y);
        }
    }
}
