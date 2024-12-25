using UnityEngine;
using System.Collections.Generic;

public class Node
{
    public Vector2Int gridPosition;
    public bool isWalkable;
    public Node parent;
    public int gCost; // Cost from start node
    public int hCost; // Heuristic cost to target
    public int fCost => gCost + hCost;
}

public class GridManager : MonoBehaviour
{
    public Vector2 worldTopLeft;
    public Vector2 worldBottomRight;
    public float nodeSize;
    public LayerMask unwalkableMask;

    private Node[,] grid;
    private int gridSizeX, gridSizeY;

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        // Calculate the grid size dynamically
        gridSizeX = Mathf.RoundToInt((worldBottomRight.x - worldTopLeft.x) / nodeSize);
        gridSizeY = Mathf.RoundToInt((worldBottomRight.y - worldTopLeft.y) / nodeSize);
        grid = new Node[gridSizeX, gridSizeY];

        // Start from the top-left corner
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Calculate the world position of each node
                Vector2 worldPoint = new Vector2(
                    worldTopLeft.x + (x * nodeSize) + nodeSize / 2,
                    worldTopLeft.y - (y * nodeSize) - nodeSize / 2 // Subtract to go downward
                );

                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeSize / 2, unwalkableMask);
                grid[x, y] = new Node { gridPosition = new Vector2Int(x, y), isWalkable = walkable };
            }
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        // Convert world position to grid position
        float percentX = Mathf.Clamp01((worldPosition.x - worldTopLeft.x) / (worldBottomRight.x - worldTopLeft.x));
        float percentY = Mathf.Clamp01((worldTopLeft.y - worldPosition.y) / (worldTopLeft.y - worldBottomRight.y));
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node, int workerRow)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; // Skip the current node

                int checkX = node.gridPosition.x + x;
                int checkY = node.gridPosition.y + y;

                // Ensure the neighbor is within bounds
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    // Allow movement in the same row or one row above/below if directly horizontal
                    if (checkY == workerRow || (y == 0 && (x == -1 || x == 1)))
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }
        }
        return neighbours;
    }

    public List<Vector2> FindPath(Vector2 startPos, Vector2 targetPos, int workerRow)
    {
        Node startNode = NodeFromWorldPoint(startPos);
        Node targetNode = NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node> { startNode };
        HashSet<Node> closedSet = new HashSet<Node>();

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbour in GetNeighbours(currentNode, workerRow))
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour)) continue;

                int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                }
            }
        }
        return null;
    }

    private List<Vector2> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2> path = new List<Vector2>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(WorldPositionFromNode(currentNode));
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridPosition.x - nodeB.gridPosition.x);
        int distY = Mathf.Abs(nodeA.gridPosition.y - nodeB.gridPosition.y);
        return distX > distY ? 14 * distY + 10 * (distX - distY) : 14 * distX + 10 * (distY - distX);
    }

    private Vector2 WorldPositionFromNode(Node node)
    {
        return new Vector2(
            worldTopLeft.x + (node.gridPosition.x * nodeSize) + nodeSize / 2,
            worldTopLeft.y - (node.gridPosition.y * nodeSize) - nodeSize / 2
        );
    }
}
