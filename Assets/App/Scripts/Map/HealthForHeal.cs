using UnityEngine;

public class HealthForHeal : MonoBehaviour
{
    [SerializeField] private int _healValue = 2;
    private bool _lockerOpen = true;
    private PlayerCharacteristic _playerCharacteristic;

    private void Start()
    {
        _playerCharacteristic = StaticClass.playerCharacteristic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_lockerOpen)
        {
            if (collision.CompareTag("Player"))
            {
                _lockerOpen = false;
                _playerCharacteristic.HealHp(_healValue);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        if (Vector3.Distance(_playerCharacteristic.transform.position, transform.position) < 100)
        {
            transform.position = Vector3.Lerp(transform.position, _playerCharacteristic.transform.position, 6 * Time.deltaTime);
        }
    }
}