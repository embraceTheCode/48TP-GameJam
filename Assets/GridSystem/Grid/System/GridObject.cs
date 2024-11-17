using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GridObject
{
    public Action OnGridObjectUpdated;
    
    private GridSystem<GridObject> _parentGrid;
    private GridPosition _gridPosition;
    private bool _isInteractable;
    public PipeType PipeType { get; private set; }
    public int Rotation { get; private set; }
    private int _energy;
    
    public bool IsActivated => _energy > 0;

    public GridObject(GridSystem<GridObject> parentGrid, GridPosition gridPosition, bool isInteractable = false, PipeType pipeType = PipeType.Empty)
    {
        _parentGrid = parentGrid;
        _gridPosition = gridPosition;
        _isInteractable = isInteractable;
        PipeType = pipeType;
        Rotation = Random.Range(0, 4);
        _energy = 0;
    }

    public override string ToString()
    {
        return _gridPosition.ToString();
    }

    public void Interact()
    {
        DeactivateNeighbours();
        Rotate();
        ActivateNeighbours();
    }

    private void Rotate()
    {
        Rotation = (Rotation + 1) % 4;
    }
    
    private void ActivateNeighbours()
    {
        List<GridPosition> neighbours = GetNeighbours();
        neighbours.Add(_gridPosition);
        foreach (GridPosition neighbour in neighbours)
        {
            GridObject gridObject = _parentGrid.GetGridObject(neighbour);
            gridObject._energy++;
            gridObject.OnGridObjectUpdated?.Invoke();
        }
    }
    
    private void DeactivateNeighbours()
    {
        List<GridPosition> neighbours = GetNeighbours();
        neighbours.Add(_gridPosition);
        foreach (GridPosition neighbour in neighbours)
        {
            GridObject gridObject = _parentGrid.GetGridObject(neighbour);
            gridObject._energy--;
            gridObject.OnGridObjectUpdated?.Invoke();
        }
    }
    
    private List<GridPosition> GetNeighbours()
    {
        List<GridPosition> neighbours = GetNeighboursByShape(PipeType);
        List<GridPosition> filteredNeighbours = new(neighbours);
        
        foreach (GridPosition neighbour in neighbours)
        {
            if (!_parentGrid.IsValidGridPosition(neighbour))
            {
                filteredNeighbours.Remove(neighbour);
                GridPosition? position = _parentGrid.GetWrapAroundPosition(neighbour);
                if(position != null)
                {
                    filteredNeighbours.Add((GridPosition) position);
                }
            }
        }
        
        return filteredNeighbours;
    }

    private List<GridPosition> GetNeighboursByShape(PipeType pipeType)
    {
        List<GridPosition> neighbours = new List<GridPosition>();

        switch (PipeType)
        {
            case PipeType.Straight:
                if(Rotation == 0 || Rotation == 2)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z + 1));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                }
                else
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x - 1, _gridPosition.z));
                }
                break;
            
            case PipeType.Elbow:
                if(Rotation == 0)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                }
                else if(Rotation == 1)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x - 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                }
                else if(Rotation == 2)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x - 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z + 1));
                }
                else
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z + 1));
                }
                break;
            
            case PipeType.T:
                if (Rotation == 0)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z + 1));
                }
                else if (Rotation == 1)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x - 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                }
                else if(Rotation == 2)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x - 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z + 1));
                }
                else
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x - 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z + 1));
                }
                break;
            
            default:
                break;
        }
        
        return neighbours;
    }

    public void SetPipeType(PipeType pipeType)
    {
        PipeType = pipeType;
        ActivateNeighbours();
    }
}
