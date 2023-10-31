using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveController : MonoBehaviour
{
    private bool _isMouseDragging;
    private Vector3 _screenPosition;
    private Vector3 _offset;

    private IMoveable _moveable;
    private GameObject _target;

    private Slot _lastSlot;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction * 10, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<IMoveable>(out IMoveable moveable))
                {
                    _moveable = moveable;
                    _lastSlot = moveable.StandOnSlot;

                    _target = hit.collider.gameObject;
                    hit.collider.enabled = false;
                }
            }

            if (_target != null)
            {
                _isMouseDragging = true;
                _screenPosition = Camera.main.WorldToScreenPoint(_target.transform.position);
                _offset = _target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPosition.z));
            }
        }

        if (Input.GetMouseButtonUp(0) && _target != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction * 10, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Slot>(out Slot slot))
                {
                    if (!slot.IsFull)
                    {
                        slot.FillSlot(_moveable);
                        _lastSlot.SetEmptySlot();
                        _lastSlot = slot;
                    }
                }
            }

            _target.transform.position = _lastSlot.StandV3;
            _target.GetComponent<Collider>().enabled = true;
            _isMouseDragging = false;
        }

        if (_isMouseDragging)
        {
            var currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPosition.z);
            var currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + _offset;
            _target.transform.position = new Vector3(currentPosition.x, _target.transform.position.y, currentPosition.z);
        }
    }
}
