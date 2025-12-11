using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KelimeOyunKontrolu : MonoBehaviour
{
    public TextMeshPro soruEkranYazisi;
    public GameObject karakterGovdesi1, karakterGovdesi2;
    public TextMeshProUGUI puanYazisi, sureYazisi;
    public GameObject bitisEkrani;
    public TextMeshProUGUI bitisMesaji;


    public int TOPLAM_SURE_GENEL = 150;
    private int TOPLAM_SURE_AZALAN;
    private bool oyunDevamEdiyor = true;

    private int puanDegiskeni = 0;
    private int soruSayisi = 0;
    private int saklananZorlukSecenegi = 0;
    private int saklananKarakterSecenegi = 0;
    private int sorulacakSoruSayisi;

    private List<Soru> sorularListesi;

    private bool oyuncuCevabi;
    private bool aktifSoruCevabi;
    private bool soruCevaplandiMi = true;


    [System.Serializable]
    public class Soru
    {
        public string Kelime;
        public bool DogruMu;
    }


    void Start()
    {
   
        saklananZorlukSecenegi = PlayerPrefs.GetInt("zorlukSecenegi", 0);
        sorulacakSoruSayisi = (saklananZorlukSecenegi == 0) ? 5 : 10;

        saklananKarakterSecenegi = PlayerPrefs.GetInt("karakterSecenegi", 0);
        karakterGovdesi1.SetActive(saklananKarakterSecenegi == 0);
        karakterGovdesi2.SetActive(saklananKarakterSecenegi == 1);

    
        SorulariJSONdanOku();

        if (soruCevaplandiMi)
            SoruUret();


        TOPLAM_SURE_AZALAN = TOPLAM_SURE_GENEL;
        sureYazisi.text = "Süre: " + TOPLAM_SURE_AZALAN;
        InvokeRepeating(nameof(SureKontrol), 1f, 1f);
    }
    private void SureKontrol()
    {
        if (!oyunDevamEdiyor) return;

        TOPLAM_SURE_AZALAN--;
        sureYazisi.text = "Süre: " + TOPLAM_SURE_AZALAN;

        if (TOPLAM_SURE_AZALAN <= 0)
        {
            OyunuBitir("Süre Bitti!");
        }
    }

    public void OyunuBitir(string mesaj)
    {
        if (!oyunDevamEdiyor) return;
        oyunDevamEdiyor = false;

        CancelInvoke(nameof(SureKontrol));

        bitisEkrani.SetActive(true);
        bitisMesaji.text = mesaj + "\nPUAN: " + puanDegiskeni;
    }


    private void KulCvbnaGoreIslem(int puan)
    {
        if (!oyunDevamEdiyor) return;

       
        if (puan > 0)
            soruEkranYazisi.text = "+ " + puan + " puan";
        else
            soruEkranYazisi.text = puan + " puan";

       
        puanDegiskeni += puan;
        if (puanDegiskeni < 0) puanDegiskeni = 0;

        puanYazisi.text = "Puan = " + puanDegiskeni;

        soruSayisi++;

      
        if (soruSayisi == sorulacakSoruSayisi)
        {
            OyunuBitir("Tüm Sorularý Tamamladýnýz!");
        }
        else
        {
            StartCoroutine(BekleVeSoruUret(1f));
        }
    }



    IEnumerator BekleVeSoruUret(float saniye)
    {
        yield return new WaitForSeconds(saniye);
        soruEkranYazisi.text = "Soru Hazýrlanýyor";
        yield return new WaitForSeconds(0.8f);
        SoruUret();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!oyunDevamEdiyor) return;

        if (collision.gameObject.CompareTag("Dogru") && !soruCevaplandiMi)
        {
            oyuncuCevabi = true;
            soruCevaplandiMi = true;

            KulCvbnaGoreIslem(oyuncuCevabi == aktifSoruCevabi ? 10 : -2);
        }
        else if (collision.gameObject.CompareTag("Yanlis") && !soruCevaplandiMi)
        {
            oyuncuCevabi = false;
            soruCevaplandiMi = true;

            KulCvbnaGoreIslem(oyuncuCevabi == aktifSoruCevabi ? 10 : -2);
        }
    }


    public void TekrarOyna()
    {
       
        oyunDevamEdiyor = true;

        bitisEkrani.SetActive(false);

        puanDegiskeni = 0;
        soruSayisi = 0;
        puanYazisi.text = "Puan = 0";

      
        CancelInvoke(nameof(SureKontrol));
        TOPLAM_SURE_AZALAN = TOPLAM_SURE_GENEL;
        sureYazisi.text = "Süre: " + TOPLAM_SURE_AZALAN;
        InvokeRepeating(nameof(SureKontrol), 1f, 1f);

     
        soruCevaplandiMi = true;
        SoruUret();
    }
    public void SorulariJSONdanOku()
    {
        string jsonText = Resources.Load<TextAsset>("kelimeler").text;
        if (!string.IsNullOrEmpty(jsonText))
        {
            sorularListesi = JsonConvert.DeserializeObject<List<Soru>>(jsonText);
        }
    }


    public Soru RastgeleSoruOlustur()
    {
        int index = Random.Range(0, sorularListesi.Count);
        Soru secilenSoru = sorularListesi[index];
        sorularListesi.RemoveAt(index);
        return secilenSoru;
    }


    public void SoruUret()
    {
        Soru aktifSoru = RastgeleSoruOlustur();

        aktifSoruCevabi = aktifSoru.DogruMu;
        soruEkranYazisi.text = (soruSayisi + 1) + " - " + aktifSoru.Kelime;

        soruCevaplandiMi = false;
    }

    public void SahneDegistir(string sahneAdi)
    { 
        SceneManager.LoadScene(sahneAdi);
    }
}
