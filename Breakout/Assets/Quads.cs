using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Quads
{
    private static int pixelsPerUnit = 25;
    private static Texture2D atlas;
    public static Sprite[] Paddles;
    public static Sprite[] Balls;
    public static Sprite[] Bricks;
    public static Sprite[] Hearts;

    static Quads ()
    {
        atlas = LoadAtlas();
        Paddles = GeneratePaddles();
        Balls = GenerateBalls();
        Bricks = GenerateBricks();
        Hearts = GenerateHearts();
    }

    private static Sprite[] GeneratePaddles ()
    {
        Sprite[] paddles = new Sprite[16];
        Rect rect = new Rect(0, 176, 32, 16);

        for (int i = 0; i < 4; i++)
        {
            // Small
            rect.width = 32;
            paddles[i * 4] = CreateQuad();

            // Medium
            rect.x += rect.width;
            rect.width += 32;
            paddles[i * 4 + 1] = CreateQuad();

            // Big
            rect.x += rect.width;
            rect.width += 32;
            paddles[i * 4 + 2] = CreateQuad();

            // Huge
            rect.y -= 16;
            rect.x = 0;
            rect.width += 32;
            paddles[i * 4 + 3] = CreateQuad();
            rect.y -= 16;
        }

        return paddles;

        Sprite CreateQuad ()
        {
            if (atlas == null) Debug.LogError("Failed to load Atlas");
            return Sprite.Create(atlas, rect, new Vector2(.5f, .5f), pixelsPerUnit);
        }
    }
    private static Sprite[] GenerateBalls()
    {
        Sprite[] balls = new Sprite[7];
        Rect rect = new Rect(96, 200, 8, 8);

        for (int i = 0; i < balls.Length; i++)
        {
            balls[i] = CreateQuad();
            if (i == 3) rect.position = new Vector2(96, 192);
            else rect.x += 8;
        }

        return balls;

        Sprite CreateQuad ()
        {
            if (atlas == null) Debug.LogError("Failed to load Atlas");
            return Sprite.Create(atlas, rect, Vector2.one * .5f, pixelsPerUnit);
        }
    }

    private static Sprite[] GenerateBricks ()
    {
        int max = 20;
        Sprite[] bricks = new Sprite[max];
        Rect rect = new Rect(0, 240, 32, 16);
        float BrickWidth = (float)32 / pixelsPerUnit;
        float BrickHeight = (float)16 / pixelsPerUnit;

        for (int i = 0; i < max; i++)
        {
            bricks[i] = GenerateQuad();
            if ((i + 1) % 6 == 0)
            {
                rect.x = 0;
                rect.y -= 16;
            }
            else
                rect.x += 32;
        }

        return bricks;

        Sprite GenerateQuad ()
        {
            return Sprite.Create(atlas, rect, Vector2.one * .5f, pixelsPerUnit);
        }
    }

    private static Sprite[] GenerateHearts ()
    {
        Sprite[] hearts = new Sprite[2];
        Rect rect = new Rect(128, 199, 10, 9);

        hearts[0] = GenerateQuad();
        rect.x += 10;
        hearts[1] = GenerateQuad();

        return hearts;

        Sprite GenerateQuad ()
        {
            return Sprite.Create(atlas, rect, Vector2.one * .5f, pixelsPerUnit);
        }
    }

    private static Texture2D LoadAtlas ()
    {
        return Resources.Load<Texture2D>("Graphics/breakout");
    }
}