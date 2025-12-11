using UnityEngine;
using UnityEngine.SceneManagement;


public class KarakterHareketi : MonoBehaviour
{
    public float hareketHizi = 5f;
    int flag = 0;
    public float donmeHizi = 250f;
    public float ziplamaGucu = 7f;
    public GameObject panel;
    Rigidbody rb;
    private bool ziplamaYapabilir = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float sagaSolaDegeri = Input.GetAxis("Horizontal");
        float ileriGeriDegeri = Input.GetAxis("Vertical");

        float xHaraket = sagaSolaDegeri * hareketHizi * Time.deltaTime;
        float zHaraket = ileriGeriDegeri * hareketHizi * Time.deltaTime;
        transform.Translate(new Vector3(xHaraket, 0, zHaraket));

        if (Input.GetMouseButton(0))
        {
            float yataydakiFareHareketi = Input.GetAxis("Mouse X");
            float donmeMiktari = yataydakiFareHareketi * donmeHizi * Time.deltaTime;
            transform.Rotate(Vector3.up, donmeMiktari);
        }

        if (ziplamaYapabilir && Input.GetButtonDown("Jump"))
        {
            Ziplama();
        }
    }

    void Ziplama()
    {
        rb.AddForce(Vector3.up * ziplamaGucu, ForceMode.Impulse);
        ziplamaYapabilir = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zemin"))
        {
            ziplamaYapabilir = true;
        }
    }
    public void PanelAc()
    {
        if (flag==0) 
        {
            panel.SetActive(true);
            flag = 1;
        }
        else
        {
            panel.SetActive(false);
            flag = 0;
        }
    }

}
