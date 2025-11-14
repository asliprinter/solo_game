using UnityEngine;

public class Enemies_Horizontal : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        Flip();  //becauuse they be moving left
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else
            {
                movingLeft = false;
                Flip();
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                movingLeft = true;
                Flip();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
