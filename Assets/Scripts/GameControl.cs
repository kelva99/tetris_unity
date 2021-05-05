using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameControl : MonoBehaviour
{
    public static readonly int screenHeight = 20;
    public static readonly int screenWidth = 10;

    private readonly int baseScore = 9;

    public TetrisControl[] blocks;
    public Transform[,] grid = new Transform[screenHeight, screenWidth];

    private bool GameOver = false;
    private int score = 0;

    Random rand = new Random();

    private int down = 0;
    private int left = 0;
    private int right = 0;
    private int rotate = 0;
    public int Down => down;
    public int Left => left;
    public int Right => right;
    public int Rotate => rotate;

    public string ScoreBase = "Score: ";

    public Text txtScore;



    // Start is called before the first frame update

    void UpdateScore()
    {
        txtScore.text = ScoreBase + score;
    }
    void Start()
    {
        score = 0;
        GenerateNew();
        UpdateScore();

    }

    public void GenerateNew()
    {
        if (GameOver)
        {
            return;
        }
        int randint = rand.Next(0, blocks.Length);
        Instantiate(blocks[randint]);
    }

    public bool isGameOver()
    {
        return GameOver;
    }

    public int GetScore()
    {
        return score;
    }

    public void NotifyGameOver()
    {
        GameOver = true;
    }

    public void RestartGame()
    {
        // Debug.Log("Restart!");
        GameOver = false;
        for (int r = 0; r < screenHeight; r++)
        {
            for (int c = 0; c < screenWidth; c++)
            {
                if (grid[r, c] != null)
                {
                    Destroy(grid[r, c].gameObject);
                }
            }
        }

        foreach(TetrisControl tc in FindObjectsOfType<TetrisControl>())
        {
            Destroy(tc.gameObject);
        }
        
        Start();
    }

    public void AddToGrid(TetrisControl tc)
    {
        foreach (Transform child in tc.transform)
        {
            int col = Convert.ToInt32(child.transform.position.x);
            int row = Convert.ToInt32(child.transform.position.y);
            if (col >= 0 && col < screenWidth && row >= 0 && row < screenHeight)
            {
                grid[row, col] = child;
            }
        }
    }

    public void CheckGameStatus()
    {
        int rowsCancelled = 0;
        for(int r = 0; r < screenHeight; r++)
        {
            bool canCancel = true;
            for(int c = 0; c  < screenWidth; c++)
            {
                if (grid[r, c] == null)
                {
                    canCancel = false;
                    break;
                }
            }
            if (canCancel)
            {
                rowsCancelled++;
                for(int c =  0; c < screenWidth; c++)
                {
                    Destroy(grid[r, c].gameObject);
                }
                for(int r2 = r; r2 < screenHeight - 1; r2++)
                {
                    for (int c = 0; c < screenWidth; c++)
                    {
                        grid[r2, c] = grid[r2 + 1, c];
                        grid[r2 + 1, c] = null;
                        if (grid[r2, c] != null)
                        {
                            grid[r2, c].transform.position += new Vector3(0, -1, 0);
                        }
                        
                    }
                }
                r--;
            }
        }

        // calculate score
        score += (baseScore + rowsCancelled) * rowsCancelled;
        UpdateScore();

    }

    public void PressDown()
    {
        down++;
    }

    public void PressLeft()
    {
        left++;
    }

    public void PressRight()
    {
        right++;
    }
    public void PressRotate()
    {
        rotate++;
    }

    public void ReleaseRotate()
    {
        if (rotate > 0)
        rotate--;
    }

    public void ReleaseDown()
    {
        if (down > 0)
            down--;
    }

    public void ReleaseLeft()
    {
        if (left > 0)
            left--;
    }

    public void ReleaseRight()
    {
        if (right > 0)
            right--;
    }

   

}
