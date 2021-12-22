using UnityEngine;

namespace AStarPathfinding
{
    public class Node
    {
        public bool Walkable { get; set; }
        public Vector3 WorldPosition { get; set; }

        public Node(bool walkable, Vector3 worldPosition)
        {
            Walkable = walkable;
            WorldPosition = worldPosition;
        }
    }
}
