using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClickInteraction : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 pos = Physics.Raycast(ray, out RaycastHit hit, float.MaxValue) ? hit.point : Vector3.one * -1;
            GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(pos);
            
            if (LevelGrid.Instance.IsValidGridPosition(gridPosition))
            {
                GridObject gridObject = LevelGrid.Instance.GetGridObject(gridPosition);
                if (gridObject != null)
                {
                    gridObject.Interact();
                }
            }
        }
    }
}
