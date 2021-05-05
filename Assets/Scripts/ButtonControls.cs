using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    // Start is called before the first frame update
    public GameControl gamecontrol;

    public void Start()
    {
        // gamecontrol = FindObjectOfType<GameControl>();
    }
    public void Restart()
    {
        gamecontrol.RestartGame();
    }
    public void Rotate()
    {
        gamecontrol.PressRotate();
    }

    public void Left()
    {
        gamecontrol.PressLeft();
    }

    public void Right()
    {
        gamecontrol.PressRight();
    }

    public void Down()
    {
        gamecontrol.PressDown();
    }
}
