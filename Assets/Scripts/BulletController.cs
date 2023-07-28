using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float _bulletSpeed;
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] GameObject _impactEffect;

    public Vector2 moveDirection;

    void Update()
    {
        _rigidbody2D.velocity = moveDirection * _bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_impactEffect != null)
        {
            Instantiate(_impactEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
