using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Play playState;
    private float dx, dy;
    private const float speed = 6;
    private Vector3 Velocity => new Vector3(dx,dy).normalized * speed;
    private float diameter;
    private bool CanHit = true;

    private float x
    {
        get => transform.position.x;
        set => transform.position = new Vector3(value, y);
    }
    private float y
    {
        get => transform.position.y;
        set => transform.position = new Vector3(x, value);
    }

    public enum Color { blue, green, red, purple, yellow, grey, orange }
    
    public void Reset ()
    {
        dx = dy = 0;
        transform.position = Vector3.zero;
    }

    public void Serve (float dx, float dy)
    {
        this.dx = dx;
        this.dy = dy;
    }

    private void Update()
    {
        CanHit = true;
        transform.position += Velocity * Time.deltaTime;
        KeepBallInsideCameraBounds();

    }

    public void SetSkin (Color color)
    {
        GetComponent<SpriteRenderer>().sprite = Quads.Balls[(int)color];
        diameter = GetComponent<SpriteRenderer>().bounds.size.x;
        GetComponent<BoxCollider2D>().size = Vector2.one * diameter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Paddle"))
        {
            PaddleHit(obj.GetComponent<Paddle>());
            
        }
        else if (obj.CompareTag("Brick") && CanHit)
        {
            CanHit = false;
            Brick brick = obj.GetComponent<Brick>();
            BrickHit(brick);
            brick.Break(); 
            StartCoroutine(HitFreeze());
        }
    }

    IEnumerator HitFreeze ()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.05f);
        Time.timeScale = 1;
    }

    private void PaddleHit (Paddle paddle)
    {
        dy = -dy;
        dx = Mathf.Abs(x - paddle.transform.position.x) * dx/Mathf.Abs(dx) * 4;
        playState.PlaySound("paddle_hit");
    }

    private void BrickHit (Brick brick)
    {
        Vector3 brickPos = brick.transform.position;
        if (y > brickPos.y+Brick.Height/2 && dy < 0)
        {
            FlipVerticalSpeed();
        }
        else if (y < brickPos.y - Brick.Height / 2 && dy > 0)
        {
            FlipVerticalSpeed();
        }
        else if (x > brickPos.x+Brick.Width/2 && dx < 0)
        {
            FlipHorizontalSpeed();
        }
        else if (x < brickPos.x - Brick.Width / 2 && dx > 0)
        {
            FlipHorizontalSpeed();
        }
    }

    private void PlayWallHitSound()
    {
        playState.PlaySound("wall_hit");
    }

    private void FlipHorizontalSpeed()
    {
        dx = -dx;
    }

    private void FlipVerticalSpeed()
    {
        dy = -dy;
    }

    private void KeepBallInsideCameraBounds ()
    {
        if (Mathf.Abs(x) > Camera.Width/2 - diameter/2)
        {
            x = (x>0?1:-1)*(Camera.Width / 2 - diameter / 2);
            FlipHorizontalSpeed();
            PlayWallHitSound();
        }

        if (y > Camera.Height / 2 - diameter / 2)
        {
            y = Camera.Height / 2 - diameter / 2;
            FlipVerticalSpeed();
            PlayWallHitSound();
        }
    }
}
