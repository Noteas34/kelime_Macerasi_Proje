using UnityEngine;
using System.Collections.Generic;
using TMPro;

using UnityEngine.SceneManagement;

public class SahneGecisi : MonoBehaviour
{
    public TMP_Dropdown karakter, zorluk;

    void Start()
    {
        int saklananKarakterSecenegi = PlayerPrefs.GetInt("karakterSecenegi", 0);
        int saklananZorlukSecenegi = PlayerPrefs.GetInt("zorlukSecenegi", 0);


        karakter.value = saklananKarakterSecenegi;
        zorluk.value = saklananZorlukSecenegi;

    }


    void Update()
    {
        
    }


    public void SahneDegistir(string sahneAdi)
    {
        PlayerPrefs.SetInt("karakterSecenegi", karakter.value);
        PlayerPrefs.SetInt("zorlukSecenegi",zorluk.value);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sahneAdi);
    }

    public void OyunuKapat()
    {
        Application.Quit();
    }
}
