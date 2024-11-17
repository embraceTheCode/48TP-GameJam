using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPipeGrid : MonoBehaviour
{
    private const int Sections = 4;
    private int _index = 0;
    [SerializeField] private List<GameObject> grids;
    [SerializeField] private GameObject pipePrefab;
    private Transform _currentGrid;
    private void Start()
    {
        foreach (var grid in grids)
        {
            _currentGrid = grid.transform;   
            
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int y = 0; y < LevelGrid.Instance.GetHeight()/Sections; y++)
                {
                    GameObject pipe = Instantiate(pipePrefab, _currentGrid);
                    pipe.GetComponent<PipeVisual>().SetGridPosition(new GridPosition(x, y + (_index * LevelGrid.Instance.GetHeight()/Sections)));
                }
            }
            
            _index++;
        }
    }
}
