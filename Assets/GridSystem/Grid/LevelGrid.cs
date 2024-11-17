using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

//? This class depends on changing the script execution order to run before the Default Time
public class LevelGrid : SerializedMonoBehaviour
{

    public static LevelGrid Instance {get; private set;}

    [SerializeField] private GameObject _prefab;
    
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;
    
    [SerializeField] private Dictionary<Vector2, PipeType> _initialPipes;


    private GridSystem<GridObject> _gridSystem;

    private void Awake()
    {
        if(Instance != null)
        {
            UnityEngine.Debug.LogError("More than one Level Grid singleton detected");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _gridSystem = new GridSystem<GridObject>(_width, _height, _cellSize, (GridSystem<GridObject> grid, GridPosition gridPosition) => new GridObject(grid, gridPosition));
        _gridSystem.CreateDebugObjects(_prefab);
        
        foreach (KeyValuePair<Vector2, PipeType> initialPipe in _initialPipes)
        {
            GridPosition gridPosition = new GridPosition((int)initialPipe.Key.x, (int)initialPipe.Key.y);
            GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
            gridObject.SetPipeType(initialPipe.Value);
        }
    }
    
    public GridObject GetGridObject(GridPosition gridPosition) => _gridSystem.GetGridObject(gridPosition);

    public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => _gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => _gridSystem.IsValidGridPosition(gridPosition);

    public int GetWidth() => _gridSystem.GetWidth();

    public int GetHeight() => _gridSystem.GetHeight();
}
