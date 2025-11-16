using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float damage;
    protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
            Debug.LogError("Animator missing on " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }

    public void Enemy_dies()
    {
        if (anim != null)
            anim.SetTrigger("enemy_death");
        else
            Debug.LogError("Animator is null on " + gameObject.name);

        ScoreManager.Instance.AddScore(10);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Reactivate()
    {
        gameObject.SetActive(true);
    }
}

