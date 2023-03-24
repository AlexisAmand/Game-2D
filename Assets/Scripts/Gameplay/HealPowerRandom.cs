// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class HealPowerRandom : MonoBehaviour
{

    private int healthpoint;

    /* son pour la vie en + */
    public AudioClip pickupSoundGood;
    /* son pour la vie en - */
    public AudioClip pickupSoundBad;

    private void Start()
    {
        /* g�n�ration d'un nombre entre -33 et 33 pour une quantit� de points de vie al�atoire */
        healthpoint = Random.Range(-33, 33);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (healthpoint < 0)
            {
                PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(Mathf.Abs(healthpoint));
                // mettre � jour de dessin
                Destroy(gameObject);
                Debug.LogWarning("Le joueur a perdu " + healthpoint + " points de vie");
                // on joue le son
                AudioManager.Instance.PlayClipAt(pickupSoundBad, transform.position);
            }
            else
            {
                if (PlayerHealth.Instance.currentHealth != PlayerHealth.Instance.maxHealth)
                {
                    // rendre de la vie au joueur
                    PlayerHealth.Instance.HealPlayer(healthpoint);
                    // mettre � jour de dessin
                    Destroy(gameObject);
                    Debug.LogWarning("Le joueur a r�pup�r� " + healthpoint + " points de vie");
                    // on joue le son
                    AudioManager.Instance.PlayClipAt(pickupSoundGood, transform.position);
                }
            }
        }

    }
}
