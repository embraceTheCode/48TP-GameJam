using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

//? This class depends on changing the script execution order to run before the Default Time
public class LevelGrid : SerializedMonoBehaviour
{

    public static LevelGrid Instance {get; private set;}

    [SerializeField] private GameObject _prefab;
    
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;
    
    [SerializeField] private Dictionary<Vector2, PipeData> _initialPipes = new ();


    private GridSystem<GridObject> _gridSystem;

    private void Awake()
    {
        if (Instance != null)
        {
            UnityEngine.Debug.LogError("More than one Level Grid singleton detected");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        // Initialize grid system
        _gridSystem = new GridSystem<GridObject>(_width, _height, _cellSize, 
            (GridSystem<GridObject> grid, GridPosition gridPosition) => new GridObject(grid, gridPosition));
        _gridSystem.CreateDebugObjects(_prefab);

        // Set initial pipe data
        foreach (KeyValuePair<Vector2, PipeData> initialPipe in _initialPipes)
        {
            GridPosition gridPosition = new GridPosition((int)initialPipe.Key.X, (int)initialPipe.Key.Y);
            GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
            gridObject.SetPipeData(initialPipe.Value);
        }
        
        RecalculateEnergyDistribution();
    }

    public void RecalculateEnergyDistribution()
    {
        // Reset all energy states to 0
        for (int x = 0; x < GetWidth(); x++)
        {
            for (int z = 0; z < GetHeight(); z++)
            {
                GridPosition pos = new GridPosition(x, z);
                GridObject obj = GetGridObject(pos);
                PipeData pipeData = obj.PipeData;
                pipeData.energy = 0;
                obj.SetPipeData(pipeData);
                obj.OnGridObjectUpdated?.Invoke();
            }
        }

        // Find all detonators and propagate energy
        HashSet<GridPosition> energizedPositions = new HashSet<GridPosition>();
        
        // Find detonators and propagate from each one
        for (int x = 0; x < GetWidth(); x++)
        {
            for (int z = 0; z < GetHeight(); z++)
            {
                GridPosition pos = new GridPosition(x, z);
                GridObject obj = GetGridObject(pos);
                
                if (obj.PipeData.PipeType == PipeType.Detonator)
                {
                    PropagateEnergy(pos, energizedPositions);
                }
            }
        }
    }

    private void PropagateEnergy(GridPosition startPos, HashSet<GridPosition> energizedPositions)
    {
        Queue<GridPosition> positionsToCheck = new Queue<GridPosition>();
        positionsToCheck.Enqueue(startPos);
        
        while (positionsToCheck.Count > 0)
        {
            GridPosition currentPos = positionsToCheck.Dequeue();
            
            // Skip if already energized
            if (!energizedPositions.Add(currentPos))
                continue;

            GridObject currentObject = GetGridObject(currentPos);
            PipeData pipeData = currentObject.PipeData;
            pipeData.energy++;
            currentObject.SetPipeData(pipeData);
            currentObject.OnGridObjectUpdated?.Invoke();
            
            // Get connected neighbors
            List<GridPosition> connectedNeighbors = currentObject.GetNeighbours();
            
            foreach (var neighborPos in connectedNeighbors)
            {
                if (IsValidConnection(currentPos, neighborPos))
                {
                    positionsToCheck.Enqueue(neighborPos);
                }
            }
        }
    }

    private bool IsValidConnection(GridPosition pos1, GridPosition pos2)
    {
        // Get the objects at both positions
        GridObject obj1 = GetGridObject(pos1);
        GridObject obj2 = GetGridObject(pos2);

        // Get the neighbors that each pipe connects to
        List<GridPosition> neighbors1 = obj1.GetNeighbours();
        List<GridPosition> neighbors2 = obj2.GetNeighbours();

        // Check if they are mutually connected
        return neighbors1.Contains(pos2) && neighbors2.Contains(pos1);
    }

    public GridObject GetGridObject(GridPosition gridPosition) => _gridSystem.GetGridObject(gridPosition);

    public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => _gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => _gridSystem.IsValidGridPosition(gridPosition);

    public int GetWidth() => _gridSystem.GetWidth();

    public int GetHeight() => _gridSystem.GetHeight();

    public void ExplodeBombs()
    {
        for (int x = 0; x < GetWidth(); x++)
        {
            for (int y = 0; y < GetHeight(); y++)
            {
                GridPosition gridPosition = new GridPosition(x, y);
                GridObject gridObject = GetGridObject(gridPosition);
                
                if (gridObject.PipeData.PipeType == PipeType.Bomb)
                {
                    gridObject.Explode();
                }
            }
        }
    }
}
