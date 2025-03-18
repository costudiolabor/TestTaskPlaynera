using System;
using UnityEngine;

[Serializable]
public class TouchHandler {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float sensitive = 0.01f;
    public event Action ClickEvent, DropEvent, DragEvent;
    public event Action<Item> ClickItemEvent;
    public event Action<Vector2> ChangeDragEvent;

    private Vector2 _currentPosition;
    private Vector2 _lastPosition;
    private bool _firstClick;
    private bool _isDrag;
    
    public void OnUpdate() {
        DetectObjectWithRaycast();
        DragEvent?.Invoke();
    }
    
    private void DetectObjectWithRaycast() {
        if (Input.GetMouseButtonDown(0)) {
            Click();
            DragEvent += Drag;
            _lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) {
            _firstClick = false;
            _isDrag = false;
            DragEvent -= Drag;
            DropEvent?.Invoke();
        }
    }

    private void Click() {
        if (_isDrag) return;
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit) {
            if (hit.collider.TryGetComponent(out Item slot)) {
                ClickItemEvent?.Invoke(slot);
                return;
            }
        }
        ClickEvent?.Invoke();
    }

    private void Drag() {
        _currentPosition = Input.mousePosition;
        if (_currentPosition != _lastPosition) _isDrag = true;
        if (_firstClick == false) {
            _lastPosition = _currentPosition;
            _firstClick = true;
        }
        Vector2 delta = _currentPosition - _lastPosition;
        ChangeDragEvent?.Invoke(delta * sensitive);
        _lastPosition = _currentPosition;
    }
}
