using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeController : MonoBehaviour {
    public MazeBuild Build;

    public bool IsMazeBuilt;
    public bool isPlayerWinner;
    public MazeGrid maze;
    private int Dimension;
    private int Sparsity;
    private Camera MazeCamera;
    private MenuController MenuCtrler;
    void Start()
    {
        IsMazeBuilt = false;
        Dimension = 20;
        Sparsity = 1;
        MenuCtrler = GameObject.Find("MenuController").GetComponent<MenuController>();
    }

    void Update()
    {
        if ( IsMazeBuilt )
        {
            foreach (GameObject key in Build.KeyPieces)
            {
                if (!key.GetComponent<KeyPieceController>().collected)
                {
                    break;
                }
                DeleteMaze();
                MenuCtrler.SetEndScreen(true);
            }

        }
    }

    public void SliderValueChanged()
    {
        Dimension = (int)GameObject.Find("SizeSlider").GetComponent<Slider>().value;
        Sparsity = (int)GameObject.Find("SparsitySlider").GetComponent<Slider>().value;
    }

    public void GenerateMaze()
    {
        if (IsMazeBuilt)
        {
            DeleteMaze();
        }

        maze = RandomMaze.Apply(Dimension, Sparsity);
        Build = new MazeBuild(maze);
        IsMazeBuilt = true;
        GameObject.Find("PlayerCamera").GetComponent<Camera>().enabled = true;
    }

    public void DeleteMaze()
    {
        GameObject.Find("PlayerCamera").GetComponent<Camera>().enabled = false;
        Build.DeleteMaze();

        IsMazeBuilt = false;
    }

}
