using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public enum Color { blue = 0, green = 1, red = 2, purple = 3, yellow = 4 };
    public enum Tier { none = 0, iron = 1, bronze = 2, gold = 3 }
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private int skinIndex;
    public static float Width { get; set; }
    public static float Height { get; set; }

    private void Awake()
    {
        SetSkin(skinIndex);
    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void SetSkin (int skinIndex)
    {
        Assert.Equals(skinIndex>=0 && skinIndex<20);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        renderer.sprite = Quads.Bricks[skinIndex];
        collider.size = renderer.bounds.size;
    }

    public void SetSkin (Color color, Tier tier)
    {
        SetSkin((int)color * 4 + (int)tier);
    }

    public void Break ()
    {
        audioManager.PlaySound("brick-hit-2");
        gameObject.SetActive(false);
    }
}
