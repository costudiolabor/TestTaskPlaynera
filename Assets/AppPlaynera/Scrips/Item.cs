using UnityEngine;

public class Item : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb2d;
    private Transform _thisTransform;
    private PointItem _pointItem;
    private Transform _startParent;
    private bool _isPointItem = false;

    private void Awake() {
        _thisTransform = transform;
        _startParent = _thisTransform.parent;
    }

    public void Capture() {
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        _pointItem = null;
        _isPointItem = false;
    }

    public void Drop() {
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        if (_isPointItem == true) {
            _pointItem.SetItem(this);
            rb2d.bodyType = RigidbodyType2D.Kinematic;
            _thisTransform.localScale = Vector3.one;
        }
    }
    public void Move(Vector2 delta) { _thisTransform.Translate(delta);}
  
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.TryGetComponent(out _pointItem)) {
                _isPointItem = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        _pointItem = null;
        _isPointItem = false;
        if (_thisTransform.parent.gameObject.activeInHierarchy) 
            _thisTransform.parent = _startParent;
    }
    
}
