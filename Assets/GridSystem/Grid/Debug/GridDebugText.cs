using UnityEngine;
using TMPro;
using TurnBasedStrategy.Grid;

public class GridDebugText : MonoBehaviour
{
    [SerializeField] protected TextMeshPro _text;
    private object _gridObject;

    public virtual void SetGridObject(object gridObject)
    {
        _gridObject = gridObject;
    }

    protected virtual void Update()
    {
        GridObject g = (GridObject)_gridObject;
        if (g.IsActivated)
        {
            _text.color = Color.green;
        }
        else
        {
            _text.color = Color.red;
        }
        _text.text = _gridObject.ToString();
    }
}
