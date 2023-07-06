using System.Collections;
using UnityEngine;

public class arc : MonoBehaviour
{

    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;

    private float arrowSpeed = 10f; /* Vitesse de la flèche */
    private float fireRate = 0.5f; /* Durée de vie de la flèche */

    private bool isShooting = false;

    public AudioClip sound; 

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.T) && !isShooting && (Inventory.Instance.arrowsCount > 0 ))
        {
            Debug.Log("Tir d'une fleche");

            // on joue le son
            AudioManager.Instance.PlayClipAt(sound, transform.position);

            Inventory.Instance.RemoveArrows(1);
            isShooting = true;
            StartCoroutine(ShootArrow());
        }

    }

    private IEnumerator ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        /* le modele accroché est masqué */
        arrow.SetActive(false);

        // Disable arrow collider while shooting
        Collider2D arrowCollider = arrow.GetComponent<Collider2D>();
        arrowCollider.enabled = false;

        if (GetComponent<SpriteRenderer>().flipX == false)
        {

            arrow.SetActive(true);
            arrow.GetComponent<SpriteRenderer>().flipX = false;
            Debug.Log("je vais tirer vers la droite");

            rb.velocity = arrowSpawnPoint.right * arrowSpeed;

            yield return new WaitForSeconds(fireRate);

            // Re-enable arrow collider
            arrowCollider.enabled = true;

            isShooting = false;
        
        }
        else
        {

            arrow.SetActive(true);
            arrow.GetComponent<SpriteRenderer>().flipX = true;
            Debug.Log("je vais tirer vers la gauche");

            rb.velocity = -arrowSpawnPoint.right * arrowSpeed;

            yield return new WaitForSeconds(fireRate);

            // Re-enable arrow collider
            arrowCollider.enabled = true;

            isShooting = false;

        }
    }

}
