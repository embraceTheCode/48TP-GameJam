using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridObject
{
    private GridSystem<GridObject> _parentGrid;
    private GridPosition _gridPosition;
    private bool _isInteractable;
    private PipeType _pipeType;
    private int _rotation;
    private int _energy;
    
    public bool IsActivated => _energy > 0;

    public GridObject(GridSystem<GridObject> parentGrid, GridPosition gridPosition, bool isInteractable = false, PipeType pipeType = PipeType.Empty)
    {
        _parentGrid = parentGrid;
        _gridPosition = gridPosition;
        _isInteractable = isInteractable;
        _pipeType = pipeType;
        _rotation = Random.Range(0, 4);
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
        _rotation = (_rotation + 1) % 4;
    }
    
    private void ActivateNeighbours()
    {
        List<GridPosition> neighbours = GetNeighbours();
        neighbours.Add(_gridPosition);
        foreach (GridPosition neighbour in neighbours)
        {
            GridObject gridObject = _parentGrid.GetGridObject(neighbour);
            gridObject._energy++;
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
        }
    }
    
    private List<GridPosition> GetNeighbours()
    {
        List<GridPosition> neighbours = GetNeighboursByShape(_pipeType);
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

        switch (_pipeType)
        {
            case PipeType.Straight:
                if(_rotation == 0 || _rotation == 2)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x - 1, _gridPosition.z));
                }
                else
                {
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z + 1));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                }
                break;
            
            case PipeType.Elbow:
                if(_rotation == 0)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                }
                else if(_rotation == 1)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x - 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                }
                else if(_rotation == 2)
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
                if (_rotation == 0)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z + 1));
                }
                else if (_rotation == 1)
                {
                    neighbours.Add(new GridPosition(_gridPosition.x + 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x - 1, _gridPosition.z));
                    neighbours.Add(new GridPosition(_gridPosition.x, _gridPosition.z - 1));
                }
                else if(_rotation == 2)
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
        _pipeType = pipeType;
        ActivateNeighbours();
    }
}
