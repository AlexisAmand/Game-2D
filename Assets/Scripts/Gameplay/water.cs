using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// cr�� par mes soins, mais modifi� par ChatGPT 3.5
// je ne suis pas s�r de l'int�r�t de isPlayerInTrapZone

public class water : MonoBehaviour
{

    private bool isPlayerInTrapZone = false;
    public int healthpoint = 10;

    private WaitForSeconds loseInterval = new WaitForSeconds(3f);
    private WaitForSeconds winInterval = new WaitForSeconds(2f);

    public AudioClip ploufSound;
    public AudioClip bubbleSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayerInTrapZone)
        {
            isPlayerInTrapZone = true;
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0.05f;

            // le joueur perd des points de vie � chaque entr�e dans l'eau

            Debug.LogWarning("Le joueur a perdu " + healthpoint + "Points de vie");
            WaterHealth waterHealth = collision.transform.GetComponent<WaterHealth>();
            waterHealth.TakeDamage(healthpoint);

            // on joue le son du plouf dans l'eau
            AudioManager.Instance.PlayClipAt(ploufSound, transform.position);

            // Lancer la coroutine qui fait perdre des points de vie au joueur
            StartCoroutine("TakeDamageOverTime", collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrapZone = false;
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1f;
        }

        // Arr�ter la coroutine qui fait perdre des points de vie au joueur
        StopCoroutine("TakeDamageOverTime");

        // Commence la coroutine pour restaurer l'oxyg�ne du joueur
        StartCoroutine(RestoreOxygen());
    }

    /* cette coroutine a �t� �crite avec l'aide de ChatGPT 3.5 */
    /* elle enl�ve de l'oxyg�ne � intervalle r�gulier */
    private IEnumerator TakeDamageOverTime(GameObject player)
    {
        while (isPlayerInTrapZone)
        {
            // Faire perdre des points de vie au joueur
            WaterHealth waterHealth = player.GetComponent<WaterHealth>();
            waterHealth.TakeDamage(healthpoint);
            Debug.LogWarning("Le joueur a perdu " + WaterHealth.Instance.currentOxygen + " points d'oxyg�ne");

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
            WaterHealth.Instance.HealPlayer(healthpoint);
            Debug.LogWarning("Le joueur a gagn� " + WaterHealth.Instance.currentOxygen + " points d'oxyg�ne");

            if (WaterHealth.Instance.currentOxygen > WaterHealth.Instance.maxOxygen)
            {
                WaterHealth.Instance.currentOxygen = WaterHealth.Instance.maxOxygen;
            }
            yield return winInterval;
        }
    }
}