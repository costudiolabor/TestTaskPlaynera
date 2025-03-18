using UnityEngine;

public class Item : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb2d;
    private Transform _thisTransform;
    private PointItem _pointItem;
    private Transform _startParent;
    private bool _isPointItem;
    private bool _isGround;

    private void Awake() {
        _thisTransform = transform;
        _startParent = _thisTransform.parent;
    }
    public void Capture() {
       _thisTransform.parent = _startParent;
       _thisTransform.localScale = Vector3.one;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void Drop() {
        if (_isPointItem == true) {
            _pointItem.SetItem(this);
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            _thisTransform.localScale = Vector3.one;
        }
        else { rb2d.constraints = RigidbodyConstraints2D.FreezeRotation; }
    }
    public void Move(Vector2 delta) { _thisTransform.Translate(delta); }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.TryGetComponent(out _pointItem)) { _isPointItem = true; }
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if (other.transform.TryGetComponent(out Ground ground)) { rb2d.constraints = RigidbodyConstraints2D.FreezeAll; }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.transform.TryGetComponent(out _pointItem)) {
            _isPointItem = false;
            if (_thisTransform.parent.gameObject.activeInHierarchy) 
                _thisTransform.parent = _startParent;
        }
    }
}
