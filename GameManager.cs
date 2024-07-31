using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject seciliObje;
    GameObject seciliPlatform;
    Cember cember;
    public bool hareketVarMi;
    public int hedefStandSayisi;
    [SerializeField] int tamamlananStandSayisi;
    [SerializeField] AudioSource oyunMusic;
    public AudioSource CembereGirmeMusic;
    public AudioSource CembereGeriDonmeMusic;
    public TextMeshProUGUI hedefStandSayisiText;
    public GameObject kazanmaPanel;
    void Update()
    {
        hedefStandSayisiText.text = hedefStandSayisi.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit,100))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if(seciliObje != null && seciliPlatform != hit.collider.gameObject)
                    {
                        Stand stand = hit.collider.GetComponent<Stand>();
                        if(stand.cemberler.Count != 4 && stand.cemberler.Count != 0)
                        {
                            if(cember.renk == stand.cemberler[^1].GetComponent<Cember>().renk)
                            {
                                seciliPlatform.GetComponent<Stand>().SoketDegistirmeIslemleri(seciliObje);
                                cember.HareketEt("PozisyonDegistir", hit.collider.gameObject, stand.BosSoketiVer(), stand.hareketNoktasi);
                                stand.bosOlanSoket++;
                                stand.cemberler.Add(seciliObje);
                                stand.CemberleriKontrolEt();
                                seciliObje = null;
                                seciliPlatform = null;
                            }
                            else
                            {
                                cember.HareketEt("SoketeGeriGit");
                                seciliObje = null;
                                seciliPlatform = null;
                            }

                            
                        }
                        else if(stand.cemberler.Count == 0)
                        {
                            seciliPlatform.GetComponent<Stand>().SoketDegistirmeIslemleri(seciliObje);
                            cember.HareketEt("PozisyonDegistir", hit.collider.gameObject, stand.BosSoketiVer(), stand.hareketNoktasi);
                            stand.bosOlanSoket++;
                            stand.cemberler.Add(seciliObje);
                            stand.CemberleriKontrolEt();

                            seciliObje = null;
                            seciliPlatform = null;
                        }
                        else
                        {
                            cember.HareketEt("SoketeGeriGit");
                            seciliObje = null;
                            seciliPlatform = null;
                        }


                        
                    }
                    else if (seciliPlatform == hit.collider.gameObject)
                    {
                        
                        cember.HareketEt("SoketeGeriGit");
                        seciliObje = null;
                        seciliPlatform = null;
                    }
                    else
                    {
                        Stand stand = hit.collider.GetComponent<Stand>();
                        seciliObje = stand.EnUsttekiCemberiVer(); // Tiktaldigimiz stand objesinden en ustteki cemberi aliyoruz.
                        cember = seciliObje.GetComponent<Cember>(); // Sectigimiz cemberin cember script'ini aliyoruz.
                        hareketVarMi = true;

                        if (cember.hareketEdebilirMi)
                        {
                            cember.HareketEt("Secim", gidilecekObje: cember.aitOlduguStand.GetComponent<Stand>().hareketNoktasi);
                            seciliPlatform = cember.aitOlduguStand;
                        }
                    }
                }
            }
        }
    }

    public void StandTamamlandi()
    {
        tamamlananStandSayisi++;
        if(tamamlananStandSayisi == hedefStandSayisi)
        {
            kazanmaPanel.SetActive(true);
        }
    }

    public void TekrarButonu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SonrakiLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void Cik()
    {
        Application.Quit();
    }
}
