using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float _bulletSpeed;
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] Vector2 _moveDirection;

    void Update()
    {
        _rigidbody2D.velocity = _moveDirection * _bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
