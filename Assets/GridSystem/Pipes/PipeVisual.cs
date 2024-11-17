using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PipeVisual : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private List<Sprite> regularPipeSprites;
    [SerializeField] private List<Sprite> activatedPipeSprites;
    
    private GridPosition _gridPosition;
    private GridObject GridObject => LevelGrid.Instance.GetGridObject(_gridPosition);
    private float[] _rotations = { 0, 90, 180, 270 };

    private void Start()
    {
        transform.localEulerAngles = new Vector3(0, 0, _rotations[GridObject.Rotation]);
        GridObject.OnGridObjectUpdated += UpdatePipeVisual;
        UpdatePipeVisual();
    }

    private void UpdatePipeVisual()
    {
        RotatePipe();
        if (GridObject.IsActivated)
        {
            image.sprite = activatedPipeSprites[(int)GridObject.PipeType];
        }
        else
        {
            image.sprite = regularPipeSprites[(int)GridObject.PipeType];
        }
    }
    
    private void RotatePipe()
    {
        transform.localEulerAngles = new Vector3(0, 0, _rotations[GridObject.Rotation]);
    }

    public void SetGridPosition(GridPosition gridPosition)
    {
        this._gridPosition = gridPosition;
    }

    private void OnMouseDown()
    {
        Debug.Log("VAR");
        GridObject.Interact();
    }
}
