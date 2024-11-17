using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PipeVisual : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private List<Sprite> regularPipeSprites;
    [SerializeField] private List<Sprite> activatedPipeSprites;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    
    private GridPosition _gridPosition;
    private GridObject GridObject => LevelGrid.Instance.GetGridObject(_gridPosition);
    private float[] _rotations = { 0, 90, 180, 270 };

    private void Start()
    {
        transform.localEulerAngles = new Vector3(0, 0, _rotations[GridObject.PipeData.Rotation]);
        GridObject.OnGridObjectUpdated += UpdatePipeVisual;
        UpdatePipeVisual();
    }

    private void UpdatePipeVisual()
    {
        RotatePipe();
        if (GridObject.IsActivated)
        {
            image.sprite = activatedPipeSprites[(int)GridObject.PipeData.PipeType];
        }
        else
        {
            image.sprite = regularPipeSprites[(int)GridObject.PipeData.PipeType];
        }
        SetGridPosition(_gridPosition);
    }
    
    private void RotatePipe()
    {
        transform.localEulerAngles = new Vector3(0, 0, _rotations[GridObject.PipeData.Rotation]);
    }

    public void SetGridPosition(GridPosition gridPosition)
    {
        _gridPosition = gridPosition;
        _textMeshProUGUI.text = gridPosition.ToString() + "\n" + GridObject.PipeData.energy;
    }

    private void OnMouseDown()
    {
        GridObject.Interact();
    }
}
