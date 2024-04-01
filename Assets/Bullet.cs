using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 forward;
    public float speed = 4f;
    public int damage = 10;
    public void Init(Vector2 f,int d)
    {
        forward = f;
        damage = d;
        Destroy(gameObject, 5f);
    }

    public void Update()
    {
        transform.position += (Vector3)forward * Time.deltaTime*speed;
        
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
