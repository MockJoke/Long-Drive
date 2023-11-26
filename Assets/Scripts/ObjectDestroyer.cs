using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Fuel"))
        {
            Destroy(collision.gameObject);
        }
    }
}
