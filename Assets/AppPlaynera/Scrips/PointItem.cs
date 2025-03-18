using UnityEngine;

public class PointItem : MonoBehaviour {
    [SerializeField] private Transform point;
    private readonly Vector3 _zeroPosition = new Vector3(0, 0, -0.5f);

    public void SetItem(Item item) {
        item.transform.parent = point;
        item.transform.localPosition = _zeroPosition;
    }
}
