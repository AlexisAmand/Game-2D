using UnityEngine;

public class arrow : MonoBehaviour
{

    private float lifetime = 5.0f; // temps de vie de la flèche

    private void Start()
    {
        // Détruire la flèche après lifetime secondes
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
                Debug.Log("Message test n°1");
                Destroy(collision.gameObject);
                Destroy(gameObject);    
        }

        Debug.Log("Message test n°2");

    }

}
