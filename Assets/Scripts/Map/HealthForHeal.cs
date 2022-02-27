using UnityEngine;

public class HealthForHeal : MonoBehaviour
{
    [SerializeField]private int _healValue = 2;
    private bool _lockerOpen = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_lockerOpen)
        {
            if (collision.CompareTag("Player"))
            {
                _lockerOpen = false;
                StaticClass.playerCharacteristic.HealHp(_healValue);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        if (DistanceBetween2dPoints(StaticClass.player.transform.position, gameObject.transform.position) < 100)
        {
            transform.position = Vector3.Lerp(transform.position, StaticClass.player.transform.position, 6 * Time.deltaTime);
        }
    }

    private float DistanceBetween2dPoints(Vector2 vec1, Vector2 vec2)
    {
        float distance;

        distance = Mathf.Pow(Mathf.Pow(vec2.x - vec1.x, 2) + Mathf.Pow(vec2.y - vec1.y, 2), 0.5f);

        return distance;
    }
}