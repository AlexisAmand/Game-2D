using System.Collections;
using UnityEngine;

/* Ce script g�re l'entr�e du player dans l'eau */

public class water : MonoBehaviour
{

    // le joueur est-il dans la zone ?
    private bool isPlayerInTrapZone = false;

    // Quantit� d'oxyg�ne gagn� ou perdu
    private int looseOxygen = 13;
    private int recoveryOxygen = 20;

    // Dur�e entre deux pertes ou deux r�cup�rations d'oxyg�ne
    private WaitForSeconds loseInterval = new WaitForSeconds(2f);
    private WaitForSeconds winInterval = new WaitForSeconds(1f);

    // Sons du joueur et l'eau
    public AudioClip ploufSound;
    public AudioClip bubbleSound;

    // les bulles
    public ParticleSystem bubblesParticleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayerInTrapZone)
        {
            isPlayerInTrapZone = true;

            Debug.LogWarning("plouf !");

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0.05f;

            // on joue le son du plouf dans l'eau
            AudioManager.Instance.PlayClipAt(ploufSound, transform.position);

            // Lancer la coroutine qui fait perdre des points de vie au joueur
            StartCoroutine("TakeDamageOverTime", collision.gameObject);

            // on active les bulles
            bubblesParticleSystem.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.LogWarning("On sort de l'eau !");

            isPlayerInTrapZone = false;
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1f;

            // Arr�ter la coroutine qui fait perdre des points de vie au joueur
            StopCoroutine("TakeDamageOverTime");

            // Commence la coroutine pour restaurer l'oxyg�ne du joueur
            StartCoroutine(RestoreOxygen());

            // on d�sactive les bulles
            bubblesParticleSystem.gameObject.SetActive(false);
        }

    }

    /* cette coroutine a �t� �crite avec l'aide de ChatGPT 3.5 */
    /* elle enl�ve de l'oxyg�ne � intervalle r�gulier */
    private IEnumerator TakeDamageOverTime(GameObject player)
    {
        while (isPlayerInTrapZone)
        {
            // Faire perdre des points de vie au joueur
            WaterHealth waterHealth = player.GetComponent<WaterHealth>();
            waterHealth.TakeDamage(looseOxygen);
            Debug.LogWarning("Le joueur a perdu " + looseOxygen + " points d'oxyg�ne");

            // on joue le son des bubbles dans l'eau
            AudioManager.Instance.PlayClipAt(bubbleSound, transform.position);

            // Attendre X secondes avant de faire perdre de nouveau des points de vie au joueur
            yield return loseInterval;
        }
    }

    /* cette coroutine rend de l'oxyg�ne � intervalle r�gulier */
    private IEnumerator RestoreOxygen()
    {
        while (WaterHealth.Instance.currentOxygen < WaterHealth.Instance.maxOxygen)
        {
            /* On rend 10 points d'oxygene */
            WaterHealth.Instance.HealPlayer(recoveryOxygen);
            Debug.LogWarning("Le joueur a gagn� " + recoveryOxygen + " points d'oxyg�ne");

            if (WaterHealth.Instance.currentOxygen > WaterHealth.Instance.maxOxygen)
            {
                WaterHealth.Instance.currentOxygen = WaterHealth.Instance.maxOxygen;
            }
            yield return winInterval;
        }
    }
}