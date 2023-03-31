using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinguKill : MonoBehaviour
{

    public int damageOnCollision = 20; /* Quantit� de d�gats de la part d'un ennemi */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }
}
