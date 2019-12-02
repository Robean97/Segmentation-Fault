using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbackStrength;
    public float knockbackTime;
    public float damage;
    public AudioSource PotBreakSound;
    public AudioSource GettingHitSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<pot>().PotDestroy();
            PotBreakSound.Play();
        }

        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D target = collision.GetComponent<Rigidbody2D>();
            // if enemy has a rigidbody
            if(target != null)
            {
                Vector2 distance = target.transform.position - transform.position;
                distance = distance.normalized * knockbackStrength;
                target.AddForce(distance, ForceMode2D.Impulse);
                if (collision.gameObject.CompareTag("enemy") && collision.isTrigger)
                {
                    target.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(target, knockbackTime,damage);
                }

                if (collision.gameObject.CompareTag("Player"))
                {
                    if (collision.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        target.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        collision.GetComponent<PlayerMovement>().Knock(knockbackTime, damage);
                        GettingHitSound.Play();

                    }
                }
            }
        }
    }
}
