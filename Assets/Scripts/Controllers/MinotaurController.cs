using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Maze;
using System;

public class MinotaurController : MonoBehaviour {
    public enum AIState { Startup, GoToWaypoint, GoToPlayer }
    public float speed = 5F;
    public AIState CurrentState;

    private GameObject minotaur;
    private GameObject m_player;
    private MazeGrid m_maze;
    private Cell m_waypoint;
    private List<Cell> m_pathToWayPoint;
    private int m_playerLayerMask = 1 << 9;

    void Start () {
        minotaur = this.gameObject;
        m_maze = GameObject.Find("MazeController").GetComponent<MazeController>().maze;
        m_player = GameObject.Find("MazeController").GetComponent<MazeController>().Build.Player;
        setNewWayPointPath(m_maze.getRandomCell());
        CurrentState = AIState.GoToWaypoint;
        
	}
	
	void Update () {
        if (CurrentState != AIState.Startup)
        {
            updateLocation();
            if (m_pathToWayPoint.Count == 0)
            {
                setNewWayPointPath(m_maze.getRandomCell());
                CurrentState = AIState.GoToWaypoint;
            }
            minotaur.transform.LookAt(centerCellPosition(m_pathToWayPoint[0]));
            float step = speed * Time.deltaTime;
            minotaur.transform.position = Vector3.MoveTowards(minotaur.transform.position, centerCellPosition(m_pathToWayPoint[0]), step);
            decimal angle = Math.Abs((decimal)Vector3.Angle(m_player.transform.position - minotaur.transform.position, minotaur.transform.forward));
            bool nothingBetween = !Physics.Linecast(minotaur.transform.position, m_player.transform.position, m_playerLayerMask);
            if (angle < 30 && nothingBetween && (m_pathToWayPoint.Last() != getClosestCell(m_player.transform.position)))
            {
                setNewWayPointPath(getClosestCell(m_player.transform.position));
                CurrentState = AIState.GoToPlayer;
            }
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
        return new Vector3((3 * cell.X) + 1.5F, minotaur.transform.position.y, (3 * cell.Y) + 1.5F);
    }

    private void setNewWayPointPath(Cell newWaypoint)
    {
        m_waypoint = newWaypoint;
        List<Cell> untrimmedPath = ShortestPathFinder.Find(m_maze, getClosestCell(minotaur.transform.position), m_waypoint);
        m_pathToWayPoint = ShortestPathFinder.CreateTrimmedPath(m_maze, untrimmedPath);
    }

    private Cell getClosestCell (Vector3 position)
    {
        return m_maze.GridForm[(int)(position.x / 3), (int)(position.z / 3)];
    }
}
