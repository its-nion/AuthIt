using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    [Header("X-Drag Settings")]
    [Range(0f, 5.0f)] public float _xDragRange;
    [Range(0f, 5.0f)] public float _xDragSmoothRange;
    [Range(1.0f, 5.0f)] public float _xDragSmoothSensitivity;

    [Header("Y-Drag Settings")]
    [Range(0f, 5.0f)] public float _yDragRange;
    [Range(0f, 5.0f)] public float _yDragSmoothRange;
    [Range(1.0f, 5.0f)] public float _yDragSmoothSensitivity;

    [Header("Lerp Back Settings")]
    [Range(0f, 1.0f)] public float _lerpPaintingBackFinishErrorAcceptance;
    [Range(0f, 0.1f)] public float _lerpPaintingBackSpeed;

    // Private Positions
    private Vector2 _paintingPos;
    private Vector2 _paintingStartPos;
    private Vector2 _paintingAwakePos;
    private Vector2 _mouseStartPos;

    // Private Game Objects
    private GameObject _picture;
    private MouseOverManager _mouseOverManager;
    
    private void Start()
    {
        _picture = transform.GetChild(0).gameObject;
        _mouseOverManager = _picture.GetComponent<MouseOverManager>();

        _paintingAwakePos = transform.position;
    }

    private void Update()
    {
        // If the left mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            _mouseStartPos = Vector2.zero;
            StartCoroutine(LerpPaintingBackToPos());
            return;
        }
        
        // If left mouse button was pressed and not released
        if (_mouseStartPos != Vector2.zero)
        {
            Vector2 _currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2 _currentMouseDistance = (_currentMousePosition - _mouseStartPos);

            // Calculate the painting x coordinate
            if (Mathf.Abs(_currentMouseDistance.x) <= _xDragRange)
            {
                var x = _paintingStartPos.x + _currentMouseDistance.x;
                _paintingPos.x = _paintingStartPos.x + _currentMouseDistance.x;
            }
            else if (_currentMouseDistance.x > _xDragRange)
            {
                var x = _xDragRange + _xDragSmoothRange - ((_xDragSmoothRange + _xDragSmoothSensitivity) * _xDragSmoothRange) / (_currentMouseDistance.x - _xDragRange + (_xDragSmoothRange + _xDragSmoothSensitivity));
                _paintingPos.x = _paintingStartPos.x + x;
            }
            else if (_currentMouseDistance.x < -_xDragRange)
            {
                var x = -_xDragRange - _xDragSmoothRange - ((_xDragSmoothRange + _xDragSmoothSensitivity) * _xDragSmoothRange) / (_currentMouseDistance.x + _xDragRange - (_xDragSmoothRange + _xDragSmoothSensitivity));
                _paintingPos.x = _paintingStartPos.x + x;
            }
            
            // Calculate the painting y coordinate
            if (_currentMouseDistance.y >= _yDragRange)
            {
                var y = _yDragRange + _yDragSmoothRange - ((_yDragSmoothRange + _yDragSmoothSensitivity) * _yDragSmoothRange) / (_currentMouseDistance.y - _yDragRange + (_yDragSmoothRange + _yDragSmoothSensitivity));
                _paintingPos.y = _paintingStartPos.y + y;
            }
            else if (_currentMouseDistance.y < -_yDragRange)
            {
                var y = -_yDragRange - _yDragSmoothRange - ((_yDragSmoothRange + _yDragSmoothSensitivity) * _yDragSmoothRange) / (_currentMouseDistance.y + _yDragRange - (_yDragSmoothRange + _yDragSmoothSensitivity));
                _paintingPos.y = _paintingStartPos.y + y;
            }

            // apply the calculated painting coordinates to the transform position of the painting
            transform.position = _paintingPos;
        }
        
        // If the left mouse button is pressed for the first time, and the mouse is over the painting
        if (Input.GetMouseButtonDown(0) && _mouseOverManager.isMouseOver && !Input.GetMouseButton(1))
        {
            _mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _paintingStartPos = transform.position;
        }

    }
    
    IEnumerator LerpPaintingBackToPos ()
    {
        while (((Vector2)transform.position-_paintingAwakePos).magnitude >= _lerpPaintingBackFinishErrorAcceptance)
        {
            transform.position = Vector2.Lerp(transform.position, _paintingAwakePos, _lerpPaintingBackSpeed);
            yield return null;
        }
    }
}