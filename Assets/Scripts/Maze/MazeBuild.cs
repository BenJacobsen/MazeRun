using System.Collections.Generic;
using UnityEngine;
using System;

public class MazeBuild{
    public bool IsBuilt = false;
    public GameObject Player;
    public GameObject Minotaur;

    public static Material Blue = Resources.Load("Materials/BlueMat", typeof(Material)) as Material;
    public static Material Green = Resources.Load("Materials/GreenMat", typeof(Material)) as Material;
    public static Material Red = Resources.Load("Materials/RedMat", typeof(Material)) as Material;
    public static Material Orange = Resources.Load("Materials/OrangeMat", typeof(Material)) as Material;
    public static Material LBlue = Resources.Load("Materials/LBlueMat", typeof(Material)) as Material;
    public static Material LGreen = Resources.Load("Materials/LGreenMat", typeof(Material)) as Material;
    public static Material Pink = Resources.Load("Materials/PinkMat", typeof(Material)) as Material;
    public static Material Yellow = Resources.Load("Materials/YellowMat", typeof(Material)) as Material;
    public static Material Gray = Resources.Load("Materials/GrayMat", typeof(Material)) as Material;
    public static GameObject WallModel;
    public static System.Random rnd;

    public List<GameObject> WallList = new List<GameObject>();
    public List<GameObject> KeyPieces = new List<GameObject>();

    public int dim;

    public MazeBuild()
    {
        IsBuilt = false;
    }

    public void NewBuild(MazeGrid grid)
    {
        if (IsBuilt)
        {
            DeleteMaze();
        }
        rnd = new System.Random();
        Player = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/PlayerPrefab") as GameObject);
        Minotaur = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/MinotaurPrefab") as GameObject);
        dim = grid.Dimension;
        Player.name = "Player";
        Minotaur.name = "Minotaur";
        for (int y = 0; y < dim; y++)
        {
            for (int x = 0; x < dim; x++)
            {
                Cell cell = grid.GridForm[x, y];

                if (cell.N && y < dim-1)
                {
                    MakeNWall(x, y);
                }

                if (cell.E && x < dim-1)
                {
                    MakeEWall(x, y);
                }
            }
        }

        // make north wall
        GameObject nWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        nWall.GetComponent<Renderer>().material = Gray;
        nWall.transform.localScale = new Vector3(dim * 3, 3, 0.25F);
        nWall.transform.position = new Vector3(dim * 1.5F, 1.5F, dim * 3);
        // duplicate for south wall
        GameObject sWall = GameObject.Instantiate(nWall);
        sWall.transform.position = new Vector3(dim * 1.5F, 1.5F, 0);
        // make east wall
        GameObject eWall = GameObject.Instantiate(nWall);
        eWall.transform.position = new Vector3(dim * 3, 1.5F, dim * 1.5F);
        eWall.transform.Rotate(new Vector3(0, 90, 0));
        // make west wall
        GameObject wWall = GameObject.Instantiate(eWall);
        wWall.transform.position = new Vector3(0, 1.5F, dim * 1.5F);

        nWall.name = "NWall";
        sWall.name = "SWall";
        eWall.name = "EWall";
        wWall.name = "WWall";

        WallList.Add(nWall);
        WallList.Add(sWall);
        WallList.Add(eWall);
        WallList.Add(wWall);

        //make maze floor
        float upper = ((float)dim * 9) / 4;
        float lower = ((float)dim * 3) / 4;

        GameObject blueFloor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        blueFloor.transform.localScale = new Vector3(((float)dim * 3) / 20, 1, ((float)dim * 3) / 20);
        blueFloor.transform.position = new Vector3(lower, 0, upper);
        blueFloor.GetComponent<Renderer>().material = Blue;

        GameObject redFloor = GameObject.Instantiate(blueFloor);
        redFloor.transform.position = new Vector3(lower, 0, lower);
        redFloor.GetComponent<Renderer>().material = Red;

        GameObject greenFloor = GameObject.Instantiate(blueFloor);
        greenFloor.transform.position = new Vector3(upper, 0, upper);
        greenFloor.GetComponent<Renderer>().material = Green;

        GameObject orangeFloor = GameObject.Instantiate(blueFloor);
        orangeFloor.transform.position = new Vector3(upper, 0, lower);
        orangeFloor.GetComponent<Renderer>().material = Orange;

        blueFloor.name = "BlueFloor";
        redFloor.name = "RedFloor";
        greenFloor.name = "GreenFloor";
        orangeFloor.name = "OrangeFloor";

        WallList.Add(blueFloor);
        WallList.Add(redFloor);
        WallList.Add(greenFloor);
        WallList.Add(orangeFloor);

        //set player positions
        Player.transform.position = new Vector3((3 * rnd.Next(dim)) + 1.5F, 1, (3 * rnd.Next(dim)) + 1.5F);

        Minotaur.transform.position = new Vector3(1.5F, 1, 1.5F);

        //create key pieces
        GameObject kpRed = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KeyPiecePrefab") as GameObject, new Vector3(rndLowerPos(), 1, rndLowerPos()), Quaternion.identity);
        kpRed.GetComponent<Renderer>().material = Pink;
        kpRed.name = "RedKP";
        KeyPieces.Add(kpRed);

        GameObject kpOrange = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KeyPiecePrefab") as GameObject, new Vector3(rndUpperPos(), 1, rndLowerPos()), Quaternion.identity);
        kpOrange.GetComponent<Renderer>().material = Yellow;
        kpOrange.name = "OrangeKP";
        KeyPieces.Add(kpOrange);

        GameObject kpBlue = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KeyPiecePrefab") as GameObject, new Vector3(rndLowerPos(), 1, rndUpperPos()), Quaternion.identity);
        kpBlue.GetComponent<Renderer>().material = LBlue;
        kpBlue.name = "BlueKP";
        KeyPieces.Add(kpBlue);

        GameObject kpGreen = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KeyPiecePrefab") as GameObject, new Vector3(rndUpperPos(), 1, rndUpperPos()), Quaternion.identity);
        kpGreen.GetComponent<Renderer>().material = LGreen;
        kpGreen.name = "GreenKP";
        KeyPieces.Add(kpGreen);

        IsBuilt = true;
    }

    public void DeleteMaze ()
    {
        IsBuilt = false;
        DeleteObjectList(WallList);
        DeleteObjectList(KeyPieces);
        UnityEngine.Object.Destroy(Player);
        UnityEngine.Object.Destroy(Minotaur);
    }

    private void MakeNWall(int x, int y)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.GetComponent<Renderer>().material = Gray;
        wall.transform.localScale = new Vector3(3, 3, 0.25F);
        wall.transform.position = new Vector3((3 * x) + 1.5F, 1.5F, 3 * (y + 1));
        wall.name = "NWall (" + x.ToString() + ", " + y.ToString() + ")";
        WallList.Add(wall);
    }

    private void MakeEWall(int x, int y)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.GetComponent<Renderer>().material = Gray;
        wall.transform.localScale = new Vector3(3, 3, 0.25F);
        wall.transform.Rotate(new Vector3(0, 90, 0));
        wall.transform.position = new Vector3(3 * (x + 1), 1.5F, (3 * y) + 1.5F);
        wall.name = "EWall (" + x.ToString() + ", " + y.ToString() + ")";
        WallList.Add(wall);
    }

    private void DeleteObjectList (List<GameObject> list)
    {
        while (list.Count != 0)
        {
            GameObject thing = list[list.Count - 1];
            list.Remove(thing);
            UnityEngine.Object.Destroy(thing);
        }
    }

    private float rndUpperPos ()
    {
        return (3 * (rnd.Next(dim / 2) + (dim % 2) + (dim / 2))) + 1.5F;
    }

    private float rndLowerPos()
    {
        return (3 * rnd.Next(dim / 2)) + 1.5F;
    }
}
