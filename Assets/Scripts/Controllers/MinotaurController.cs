using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Maze;

public class MinotaurController : MonoBehaviour {
    public enum AIState { Startup, GoToWaypoint, SeePlayer }
    public float speed = 5F;
    public AIState CurrentState;

    private GameObject minotaur;
    private MazeGrid m_maze;
    private Cell m_waypoint;
    private List<Cell> m_pathToWayPoint;

    void Start () {
        minotaur = this.gameObject;
        m_maze = GameObject.Find("MazeController").GetComponent<MazeController>().maze;
        setNewRandomWayPointPath();
        CurrentState = AIState.GoToWaypoint;
        
	}
	
	void Update () {
        switch (CurrentState) {
            case AIState.GoToWaypoint:
                updateLocation();
                if (m_pathToWayPoint.Count == 0)
                {
                    setNewRandomWayPointPath();
                }
                break;

            default:
                break;
        }
        if (CurrentState != AIState.Startup)
        {
            minotaur.transform.LookAt(centerCellPosition(m_pathToWayPoint[0]));
            float step = speed * Time.deltaTime;
            minotaur.transform.position = Vector3.MoveTowards(minotaur.transform.position, centerCellPosition(m_pathToWayPoint[0]), step);
        }
	}

    private void updateLocation() {
        if (m_pathToWayPoint != null)
        {
            float dist = Vector3.Distance(centerCellPosition(m_pathToWayPoint[0]), minotaur.transform.position);
            if (dist < 0.2F)
            {
                m_pathToWayPoint.Remove(m_pathToWayPoint[0]);

            }
        }
    }

    private Vector3 centerCellPosition(Cell cell)
    {
        return new Vector3((3 * cell.X) + 1.5F, 1.5F, (3 * cell.Y) + 1.5F);
    }

    private void setNewRandomWayPointPath()
    {
        m_waypoint = m_maze.getRandomCell();
        m_pathToWayPoint = ShortestPathFinder.Trim(m_maze, untrimmedPath);
    }

    private Cell getClosestCell ()
    {
        return m_maze.GridForm[(int)(minotaur.transform.position.x / 3), (int)(minotaur.transform.position.z / 3)];
    }
}
