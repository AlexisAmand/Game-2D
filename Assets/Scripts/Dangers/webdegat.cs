using UnityEngine;

public class webdegat : MonoBehaviour
{
    public int damage = 15; /* Quantit� de d�gats */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
    }
}
