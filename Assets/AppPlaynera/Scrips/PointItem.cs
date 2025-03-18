using UnityEngine;

public class PointItem : MonoBehaviour {
    [SerializeField] private Transform point;

    public void SetItem(Item item) {
        item.transform.parent = point;
        item.transform.localPosition = Vector3.zero;
    }
    
}
