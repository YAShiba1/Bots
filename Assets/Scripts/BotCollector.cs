using UnityEngine;

public class BotCollector : MonoBehaviour
{
    [SerializeField] private float _speed = 15;
    [SerializeField] private float _pickupRadius = 1.5f;

    private Transform _currentTarget;

    public bool IsPickedGold { get; private set; } = false;

    public bool IsFree { get; private set; } = true;

    private void Update()
    {
        if (_currentTarget != null)
        {
            MoveTo();
            CheckForNearbyGoldResources();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Base newBase))
        {
            IsFree = true;
            IsPickedGold = false;
        }
    }

    public void SetTarget(Transform target)
    {
        _currentTarget = target;
    }

    private void PickUpGoldResource()
    {
        _currentTarget.SetParent(transform);
    }

    private void MoveTo()
    {
        transform.LookAt(new Vector3(_currentTarget.position.x, 0, _currentTarget.position.z));

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_currentTarget.position.x, transform.position.y, _currentTarget.position.z), _speed * Time.deltaTime);

        IsFree = false;
    }

    private void CheckForNearbyGoldResources()
    {
        if(_currentTarget.TryGetComponent(out Gold gold) == true)
        {
            float distance = Vector3.Distance(transform.position, _currentTarget.position);

            if (distance < _pickupRadius)
            {
                PickUpGoldResource();
                IsPickedGold = true;
            }
        }
    }
}
