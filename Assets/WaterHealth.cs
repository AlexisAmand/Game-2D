using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHealth : MonoBehaviour
{
    public int maxOxygen = 100; /* par defaut, on a 100 point de vie */
    public int currentOxygen;  /* via actuelle */

    public float invincibilityTimeAfterHit = 3f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvincible = false; /* par defaut, le perso n'est pas invinsible */

    public SpriteRenderer graphics; /* fait r�f�rence au dessin du player */
    public WaterBar waterBar;

    public static WaterHealth Instance;

    /* son pour les d�gats */
    public AudioClip hitSound;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de WaterHealth dans la sc�ne");
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentOxygen = maxOxygen;
        waterBar.SetOxygen(currentOxygen);
    }

    public void TakeDamage(int damage)
    {
        /* Si le perso n'est pas invinsible, il subit des d�gats */

      
            AudioManager.Instance.PlayClipAt(hitSound, transform.position);

            currentOxygen -= damage; /* nouvelle valeur du nbre de pts de vie */
            waterBar.SetOxygen(currentOxygen); /* mise � jour du visuel */

            // v�rifier si le joueur est toujours vivant
            if (currentOxygen <= 0)
            {
                Die();
                return;
            }

    }

    public void HealPlayer(int amount)
    {
        if (currentOxygen + amount > maxOxygen)
        {
            currentOxygen = maxOxygen;
        }
        else
        {
            currentOxygen += amount;
        }

        waterBar.SetOxygen(currentOxygen);
    }

    public void Die()
    {
        Debug.Log("Le joueur est �limin�");

        /* on bloque les mouvements du perso en bloquant le script PlayerMovement.cs */
        PlayerMovement.Instance.enabled = false;

        /* jouer l'animation d'�limination */
        PlayerMovement.Instance.animator.SetTrigger("Die");

        // emp�cher les interactions avec les �l�ments de la sc�ne
        PlayerMovement.Instance.rb.bodyType = RigidbodyType2D.Kinematic;

        // on met la velocity � 0 sinon la cam�ra bouge quand le joueur est �limin�.
        PlayerMovement.Instance.rb.velocity = Vector3.zero;

        // 
        PlayerMovement.Instance.playerCollider.enabled = false;

        // on appelle la m�thode qui affiche le menu
        GameOverManager.Instance.OnPlayerDeath();

    }

    public void Respawn()
    {

        /* on restaure les mouvements du perso en bloquant le script PlayerMovement.cs */
        PlayerMovement.Instance.enabled = true;


        PlayerMovement.Instance.animator.SetTrigger("Respawn");

        // on restaure les interactions avec les �l�ments de la sc�ne
        PlayerMovement.Instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.Instance.playerCollider.enabled = true;
        currentOxygen = maxOxygen;
        waterBar.SetOxygen(currentOxygen);

    }


}
