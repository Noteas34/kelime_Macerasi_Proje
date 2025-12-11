using UnityEngine;

public class script3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int sayi = Random.Range(0, 101);
        Debug.Log("Sayi: " +sayi);

        if (sayi>50)
 {
            Debug.Log("Sayý 50  ile 100 arasýnda!");
        }

        if (sayi >= 50)
        {
            Debug.Log("Sayýn 50 ile 100 arasýnda");
        }
        else
        {
            Debug.Log("Sayýn 0 ile 50 arasýnda");
        }

        if (sayi >= 75)
        {
            Debug.Log("Sayýn 75 ile 100 arasýnda");
        }
        else if (sayi >= 50)
        {
            Debug.Log("Sayýn 50 ile 75 arasýnda");
        }
        else
        {
            Debug.Log("Sayýn 0 ile 50 arasýnda");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
