using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelMaker
{
    private static GameObject brickPrefab;
    private static float separation = 0f;

    static LevelMaker ()
    {
        LoadBrickPrefab();
        
    }

    public static void CreateMap ()
    {
        int rows, cols;
        float padding;
        Vector3 origin = new Vector3(-Camera.Width/2, Camera.Height/2);
        rows = Random.Range(1, 5);
        cols = Random.Range(7, 10);
        padding = CalcPadding(cols);

        for (int x = 0; x < cols; x++) 
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject brick = MonoBehaviour.Instantiate(brickPrefab);
                brick.transform.position = new Vector3(
                    (x * Brick.Width + Brick.Width / 2 + x * separation),
                    -(y * Brick.Height + 1 + y * separation));
                brick.transform.position += origin;
                brick.transform.position += Vector3.right * padding;
            }
        }
    }

    private static void LoadBrickPrefab ()
    {
        brickPrefab = Resources.Load<GameObject>("Brick");
    }

    private static float CalcPadding (int cols)
    {
        return (Camera.Width - Brick.Width * cols - (cols-1) * separation) / 2;
    }
}
