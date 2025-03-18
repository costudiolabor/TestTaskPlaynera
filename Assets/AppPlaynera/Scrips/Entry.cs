using UnityEngine;

public class Entry : MonoBehaviour {
    [SerializeField] private TouchHandler touchHandler;
    [SerializeField] private Transform cameraTransform;
    
    private Item _item;
    private bool _IsItem;
    void Awake() { Initialize();}
    private void Initialize() { Subscription(); }
    void Update() { touchHandler.OnUpdate(); }
    private void OnClick() { touchHandler.ChangeDragEvent += OnScroll; } 
    private void OnClickItem(Item item) {
        touchHandler.ChangeDragEvent += OnMoveItem;
        _item = item;
        _IsItem = true;
        _item.Capture();
    }
    
    private void OnScroll(Vector2 delta) {
        delta.y = 0;
        cameraTransform.Translate(-delta);
    }
    private void OnMoveItem(Vector2 delta) { _item.Move(delta); }
    private void OnDrop() {
        touchHandler.ChangeDragEvent -= OnScroll;
        touchHandler.ChangeDragEvent -= OnMoveItem;
        if(_IsItem) _item.Drop();
    }
    
    private void Subscription() {
        touchHandler.ClickEvent += OnClick;
        touchHandler.ClickItemEvent += OnClickItem;
        touchHandler.DropEvent += OnDrop;
    }

    private void UnSubscription() {
        touchHandler.ClickEvent -= OnClick;
        touchHandler.ClickItemEvent -= OnClickItem;
        touchHandler.DropEvent -= OnDrop;
    }

    private void OnDestroy() { UnSubscription(); }
}
