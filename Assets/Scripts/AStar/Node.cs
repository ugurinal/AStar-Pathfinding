using UnityEngine;

namespace AStarPathfinding
{
    public class Node : IHeapItem<Node>
    {
        public Node Parent { get; set; }
        public int GridX { get; }
        public int GridY { get; }
        public Vector3 WorldPosition { get; }
        public bool Walkable { get; }

        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost => GCost + HCost;
        public int HeapIndex { get; set; }

        public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
        {
            Walkable = walkable;
            WorldPosition = worldPosition;
            GridX = gridX;
            GridY = gridY;
        }

        public int CompareTo(Node nodeToCompare)
        {
            int compare = FCost.CompareTo(nodeToCompare.FCost);

            if (compare == 0)
            {
                compare = HCost.CompareTo(nodeToCompare.HCost);
            }

            return -compare;
        }
    } // node
} // namespace
