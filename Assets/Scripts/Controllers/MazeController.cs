using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeController : MonoBehaviour {
    public MazeBuild Build;

    public bool isPlayerWinner;
    public MazeGrid maze;
    private int Dimension;
    private int Sparsity;
    private Camera MazeCamera;
    private MenuController MenuCtrler;
    void Start()
    {
        Build = new MazeBuild();
        Dimension = 20;
        Sparsity = 1;
        MenuCtrler = GameObject.Find("MenuController").GetComponent<MenuController>();
    }

    void Update()
    {
        int collectedKeyCount = 0;
        if (Build.IsBuilt)
        {
            foreach (GameObject key in Build.KeyPieces)
            {
                if (key.GetComponent<KeyPieceController>().collected)
                {
                    collectedKeyCount += 1;
                }
            }
            if (collectedKeyCount == 4)
            {
                DeleteMaze();
                MenuCtrler.SetEndScreen(true);
            }
            if (Vector3.Distance(Build.Player.transform.position, Build.Minotaur.transform.position) < 1.4F)
            {
                DeleteMaze();
                MenuCtrler.SetEndScreen(false);
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

        maze = RandomMaze.Apply(Dimension, Sparsity);
        Build.NewBuild(maze);
        GameObject.Find("PlayerCamera").GetComponent<Camera>().enabled = true;
    }

    public void DeleteMaze()
    {
        GameObject.Find("PlayerCamera").GetComponent<Camera>().enabled = false;
        Build.DeleteMaze();
    }

}
