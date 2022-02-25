using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskManager : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject _spriteMaskPrefab;
    
    // Private GameObjects
    private GameObject _spriteMask;
    private MouseOverManager _mouseOverManager;

    private void Start()
    {
        _mouseOverManager = transform.GetChild(0).transform.GetComponent<MouseOverManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the right mouse button is pressed for the first time and the left one isn't pressed
        if (Input.GetMouseButtonDown(1) && _mouseOverManager.isMouseOver && !Input.GetMouseButton(0))
        {
            var _currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            _spriteMask = Instantiate(_spriteMaskPrefab, _currentMousePosition, Quaternion.identity);
        }

        // While the right mouse button is pressed, and the left one isn't
        if (Input.GetMouseButton(1) && _spriteMask != null)
        {
            Vector2 _currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            _spriteMask.transform.position = _currentMousePosition;
        }

        // If the User stops pressing the right mouse button
        if (Input.GetMouseButtonUp(1))
        {
            if (_spriteMask == null) return;
            Destroy(_spriteMask);
        }
    }
}
