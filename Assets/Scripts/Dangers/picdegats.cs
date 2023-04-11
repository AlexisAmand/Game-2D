using UnityEngine;

/* Ce script g�re les d�g�ts des pics sur le  player */

public class picdegats : MonoBehaviour

{
    public int picDamage = 15; /* Quantit� de d�gats */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(picDamage);
            Debug.Log("Ouuuh �a pique les fesses !");
        }
    }
}
