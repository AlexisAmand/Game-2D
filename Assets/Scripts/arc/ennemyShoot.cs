using UnityEngine;

public class ennemyShoot : MonoBehaviour
{
    // public AudioClip sound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Arrow"))
        {
            // on joue le son
            // AudioManager.Instance.PlayClipAt(sound, transform.position);

            // on d�truit de gameobject de l'ennemi
            Destroy(collision.gameObject);
            Destroy(gameObject);

            // on ajoute des pieces � l'inventaire
            //Inventory.Instance.AddCoins(5);
            //CurrentSceneManager.Instance.coinsPickedUpInThisSceneCount++;

        }
    }
}
