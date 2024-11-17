using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GridObject
{
    public Action OnGridObjectUpdated;
    
    private GridSystem<GridObject> _parentGrid;
    private GridPosition _gridPosition;
    public PipeData PipeData;
    
    public bool IsActivated => PipeData.energy > 0;

    public GridObject(GridSystem<GridObject> parentGrid, GridPosition gridPosition)
    {
        _parentGrid = parentGrid;
        _gridPosition = gridPosition;
        PipeData.Rotation = 0;
        PipeData.PipeType = PipeType.Empty;
        PipeData.energy = 0;
        PipeData.isInteractable = false;
    }

    public override string ToString()
    {
        return _gridPosition.ToString();
    }

    public void Interact()
    {
        if(!PipeData.isInteractable)
        {
            return;
        }
        
        DeactivateNeighbours();
        Rotate();
        ActivateNeighbours();
    }

    private void Rotate()
    {
        PipeData.Rotation = (PipeData.Rotation + 1) % 4;
    }
    
    private void ActivateNeighbours()
    {
        List<GridPosition> neighbours = GetNeighbours();
        neighbours.Add(_gridPosition);
        foreach (GridPosition neighbour in neighbours)
        {
            GridObject gridObject = _parentGrid.GetGridObject(neighbour);
            gridObject.PipeData.energy++;
            gridObject.OnGridObjectUpdated?.Invoke();
            if(gridObject.PipeData.PipeType == PipeType.Bomb && gridObject.PipeData.energy > 0)
            {
                Debug.Log("EXPLOSION");
            }
        }
    }
    
    private void DeactivateNeighbours()
    {
        List<GridPosition> neighbours = GetNeighbours();
        neighbours.Add(_gridPosition);
        foreach (GridPosition neighbour in neighbours)
        {
            GridObject gridObject = _parentGrid.GetGridObject(neighbour);
            gridObject.PipeData.energy--;
            gridObject.OnGridObjectUpdated?.Invoke();
        }
    }
    
    private List<GridPosition> GetNeighbours()
    {
        List<GridPosition> neighbours = GetNeighboursByShape(PipeData.PipeType);
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

        switch (PipeData.PipeType)
        {
            case PipeType.Straight:
                if(PipeData.Rotation == 0 || PipeData.Rotation == 2)
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
                if(PipeData.Rotation == 0)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                }
                else if(PipeData.Rotation == 1)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x - 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                }
                else if(PipeData.Rotation == 2)
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
                if (PipeData.Rotation == 0)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z + 1));
                }
                else if (PipeData.Rotation == 1)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x - 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                }
                else if(PipeData.Rotation == 2)
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

    public void SetPipeData(PipeData initialPipeValue)
    {
        PipeData = initialPipeValue;
        ActivateNeighbours();
    }
}
