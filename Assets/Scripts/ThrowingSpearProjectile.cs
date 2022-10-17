using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSpearProjectile : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float speed;
    public int damage = 5;

    [SerializeField] float hitSize = .7f;

    float ttl = 6f;
    bool hitDetected = false;

    public void SetDirection(float dir_x, float dir_y)
    {
        direction = new Vector3(dir_x, dir_y);

        if(dir_x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if(Time.frameCount % 6 == 0 ) // to increse performance only check evey 6 frames
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, hitSize);
            foreach (Collider2D collider in hit)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    hitDetected = true;
                    break;
                }
            }

            if (hitDetected == true)
            {
                Destroy(gameObject);
            }
        }

        ttl -= Time.deltaTime;
        if(ttl < 0)
        {
            Destroy(gameObject);
        }
        
    }
}
