using UnityEngine;

public class chrono : MonoBehaviour
{

    private float temps;
    
    private int minutes;
    private int seconds;

    private string minutesToShow;
    private string secondesToShow;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.Instance.currentHealth > 0)
        {
            AfficheChrono();
        }    
    }

    /* cette fonction fait les calculs pour le chrono qui compte le temps passé dans le niveau */
    void AfficheChrono()
    {
        Debug.Log(PlayerHealth.Instance.currentHealth);

        temps += Time.deltaTime;

        minutes = (int)(temps / 60);
        seconds = (int)(temps % 60);

        secondesToShow = seconds.ToString();
        minutesToShow = minutes.ToString();

        if (minutes < 10)
        {
            minutesToShow = "0" + minutes.ToString();
        }

        if (seconds < 10)
        {
            secondesToShow = "0" + seconds.ToString();
        }

        Debug.Log(temps);
        Debug.Log(minutesToShow + ":" + secondesToShow);
    }
}
