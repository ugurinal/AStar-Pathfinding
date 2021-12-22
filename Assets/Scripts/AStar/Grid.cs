using UnityEngine;

namespace AStarPathfinding
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private GridSettings settings;

        private Node[,] grid;

        private float nodeDiameter;
        private int gridSizeX;
        private int gridSizeY;

        private void Start()
        {
            nodeDiameter = settings.nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(settings.gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(settings.gridWorldSize.y / nodeDiameter);

            CreateGrid();
        }

        private void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];

            Vector3 worldBottomLeft = transform.position - Vector3.right * settings.gridWorldSize.x / 2 -
                                      Vector3.forward * settings.gridWorldSize.y / 2;

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + settings.nodeRadius) +
                                         Vector3.forward * (y * nodeDiameter + settings.nodeRadius);
                    bool walkable = !Physics.CheckSphere(worldPoint, settings.nodeRadius, settings.unwalkableLayerMask);
                    grid[x, y] = new Node(walkable, worldPoint);
                }
            }
        }

        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            float percentX = (worldPosition.x + settings.gridWorldSize.x / 2) / settings.gridWorldSize.x;
            float percentY = (worldPosition.z + settings.gridWorldSize.y / 2) / settings.gridWorldSize.y;

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.FloorToInt(Mathf.Min(gridSizeX * percentX, gridSizeX - 1));
            int y = Mathf.FloorToInt(Mathf.Min(gridSizeY * percentY, gridSizeY - 1));

            return grid[x, y];
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(settings.gridWorldSize.x, 1, settings.gridWorldSize.y));

            if (grid != null)
            {
                foreach (var node in grid)
                {
                    Color green = new Color(0, 255, 0, .25f);
                    Color red = new Color(255, 0, 0, .25f);
                    Gizmos.color = node.Walkable ? green : red;
                    Gizmos.DrawCube(node.WorldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        }
    }
}
