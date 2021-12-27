using System.Collections.Generic;
using UnityEngine;

namespace AStarPathfinding
{
    public class Pathfinding : MonoBehaviour
    {
        [SerializeField] private Transform seeker;
        [SerializeField] private Transform target;
        private Grid grid;

        private void Awake()
        {
            grid = GetComponent<Grid>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                FindPath(seeker.position, target.position);
        }

        public void FindPath(Vector3 startPos, Vector3 targetPos)
        {
            Node startNode = grid.GetNodeFromWorldPoint(startPos);
            Node targetNode = grid.GetNodeFromWorldPoint(targetPos);

            Heap<Node> openSet = new Heap<Node>(grid.GridSize);
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    return;
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newMovementCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.Parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }

        private void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            path.Reverse();

            grid.Path = path;
        }

        private int GetDistance(Node startNode, Node targetNode)
        {
            int distX = Mathf.Abs(startNode.GridX - targetNode.GridX);
            int distY = Mathf.Abs(startNode.GridY - targetNode.GridY);

            if (distX > distY)
            {
                return 14 * distY + 10 * (distX - distY);
            }

            return 14 * distX + 10 * (distY - distX);
        }
    } // pathfinding
} // namespace
