using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisControl : MonoBehaviour
{
    private bool canmove = true;

    private float timer = 0.0f;
    private float waittime = 0.7f;

    public GameControl control;

    // Start is called before the first frame update
    void Start()
    {
        control = FindObjectOfType<GameControl>();
        // check if block can move at top..
        if (!InScreen())
        {
            canmove = false;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            control.NotifyGameOver();
        }

        // hide object above the gamepane
        foreach(Transform child in transform)
        {
            int row = Convert.ToInt32(child.transform.position.y);
            if (row >= GameControl.screenHeight)
            {
                child.GetComponent<Renderer>().enabled = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!canmove)
        {
            return;
        }
        timer += Time.deltaTime;

        Vector3 v = new Vector3(0, 0, 0);
        // down
        if (Input.GetKeyDown(KeyCode.DownArrow) || timer > waittime || control.Down > 0)
        {
            // Debug.Log("down");
            v += new Vector3(0, -1, 0);
            transform.position += v;
            if (!InScreen())
            {
                transform.position -= v;
                canmove = false;
                control.AddToGrid(this);
                control.CheckGameStatus();
                control.GenerateNew();
            }
            timer = 0.0f;

            control.ReleaseDown();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || control.Left > 0)
        {
           Debug.Log("left");
           v += new Vector3(-1, 0, 0);
           transform.position += v;
           if (!InScreen())
           {
                transform.position -= v;
           }
            control.ReleaseLeft();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || control.Right > 0)
        {
            Debug.Log("right");
            v += new Vector3(1, 0, 0);
            transform.position += v;
            if (!InScreen())
            {
                transform.position -= v;
            }
            control.ReleaseRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || control.Rotate > 0)
        {
            Debug.Log("rotate");

            transform.RotateAround(transform.position, new Vector3(0, 0, 1), -90);
            if (!InScreen())
            {
                transform.RotateAround(transform.position,new Vector3(0, 0, 1), 90);
            }
            control.ReleaseRotate();
        }

        // Debug.Log(control.Left + " " +  control.Right + " " + control.Down + " " + control.Rotate);

        // unhide object if neccessary
        foreach (Transform child in transform)
        {
            int row = Convert.ToInt32(child.transform.position.y);
            if (row < GameControl.screenHeight)
            {
                child.GetComponent<Renderer>().enabled = true;
            }
        }


    }


    public bool InScreen(){
       //  Debug.Log("---");
        foreach(Transform child in transform)
        {
            int col = Convert.ToInt32(child.transform.position.x);
            int row = Convert.ToInt32(child.transform.position.y);
            if (row >= GameControl.screenHeight)
            {
                continue;
            }
            // Debug.Log("Row" + row + " col" + col);
            if ( col < 0 || (col + 1) > GameControl.screenWidth || row < 0 || control.grid[row, col] != null)
            {
                return false;
            }
        }
        return true;
    }


}
