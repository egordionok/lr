using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public static float dropTime = 0.9f;
    static public float quickDropTime = 0.05f;
    public static int width = 15;
    public static int height = 30;
    public GameObject[] blocks;
    public Transform[,] grid = new Transform[width, height];
    public Text ScoreBar;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBlock();
    }
    public void ClearLines()
    {
        for (int y = 0; y < height; y++)
        {
            if(IsLineComplete(y))
            {
                print("CLEEEAR");
                ScoreBar.text = (int.Parse(ScoreBar.text) + 1).ToString();
                DestroyLine(y);
                MoveLines(y);
            }
        }
    }
    void MoveLines(int y)
    {
        for (int i = y; i < height - 1; i++)
        {   
            for (int x = 0; x < width; x++)
            {
                print(x.ToString() + ' ' + i.ToString());   
                
                if (grid[x, y+1] != null)
                {
                    grid[x, y] = grid[x, y+1];
                    grid[x, y+1].gameObject.transform.position -= new Vector3(0, 1, 0);
                    grid[x, y+1] = null;
                }
            }
        }
        
    }
    void DestroyLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
        }
    }

    bool IsLineComplete(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }   
        }
        return true;
    }
    public void SpawnBlock()
    {
        float guess = Random.Range(0, 1f);
        guess *= blocks.Length;
        Instantiate(blocks[Mathf.FloorToInt(guess)]);
    }
}
