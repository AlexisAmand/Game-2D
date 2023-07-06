using UnityEngine;

/* gére l'oxygène du player */

public class WaterHealth : MonoBehaviour
{
    public int maxOxygen = 100; /* par defaut, on a 100 point d'oxygène */
    public int currentOxygen;  /* quantité d'oxygène actuelle */

    // public float invincibilityTimeAfterHit = 3f;
    // public float invincibilityFlashDelay = 0.2f;
    // public bool isInvincible = false; /* par defaut, le perso n'est pas invinsible */

    public SpriteRenderer graphics; /* fait référence au dessin du player */
    public WaterBar waterBar;

    public static WaterHealth Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de WaterHealth dans la scène");
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
        /* Si le perso n'est pas invinsible, il subit des dégats */
 
            // AudioManager.Instance.PlayClipAt(hitSound, transform.position);

            currentOxygen -= damage; /* nouvelle valeur du nbre de pts de vie */
            waterBar.SetOxygen(currentOxygen); /* mise à jour du visuel */

            // vérifier si le joueur est toujours vivant
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
        Debug.Log("Le joueur est éliminé");

        /* on bloque les mouvements du perso en bloquant le script PlayerMovement.cs */
        PlayerMovement.Instance.enabled = false;

        /* jouer l'animation d'élimination */
        PlayerMovement.Instance.animator.SetTrigger("Die");

        // empêcher les interactions avec les éléments de la scène
        PlayerMovement.Instance.rb.bodyType = RigidbodyType2D.Kinematic;

        // on met la velocity à 0 sinon la caméra bouge quand le joueur est éliminé.
        PlayerMovement.Instance.rb.velocity = Vector3.zero;

        // 
        PlayerMovement.Instance.playerCollider.enabled = false;

        // on appelle la méthode qui affiche le menu
        GameOverManager.Instance.OnPlayerDeath();

    }

    public void Respawn()
    {

        /* on restaure les mouvements du perso en bloquant le script PlayerMovement.cs */
        PlayerMovement.Instance.enabled = true;

        PlayerMovement.Instance.animator.SetTrigger("Respawn");

        // on restaure les interactions avec les éléments de la scène
        PlayerMovement.Instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.Instance.playerCollider.enabled = true;
        currentOxygen = maxOxygen;
        waterBar.SetOxygen(currentOxygen);

    }

}