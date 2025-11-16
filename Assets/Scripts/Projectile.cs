using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 4) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.Enemy_dies();
        }
    }

    public void SetDirection(float dir)
    {
        lifetime = 0;
        direction = dir;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        if (Mathf.Sign(transform.localScale.x) != dir)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
