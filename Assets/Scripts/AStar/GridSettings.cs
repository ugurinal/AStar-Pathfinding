using UnityEngine;

namespace AStarPathfinding
{
    [CreateAssetMenu(fileName = "GridSettings", menuName = "AStarPathfinding/GridSettings")]
    public class GridSettings : ScriptableObject
    {
        public LayerMask unwalkableLayerMask;
        public Vector2 gridWorldSize;
        public float nodeRadius;

        public bool drawPath;
        public bool drawGrid;
        public Color pathColor;
        public Color walkableCellColor;
        public Color unwalkableCellColor;
    }
}
