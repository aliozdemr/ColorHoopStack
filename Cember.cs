using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cember : MonoBehaviour
{
    public GameObject aitOlduguStand;
    public GameObject aitOlduguSoket;
    public bool hareketEdebilirMi;
    public string renk;
    public GameManager manager;

    GameObject hareketNoktasi, aitOlduguStandTemp;
    bool secildi, posDegis, soketOtur, soketeGeriGit;

    public void HareketEt(string islem, GameObject stand = null, GameObject soket = null, GameObject gidilecekObje = null)
    {
        switch (islem)
        {
            case "Secim":
                hareketNoktasi = gidilecekObje;
                secildi = true;
                break;

            case "PozisyonDegistir":
                aitOlduguStandTemp = stand;
                aitOlduguSoket = soket;
                hareketNoktasi = gidilecekObje;
                posDegis = true;
                break;

            case "SoketeGeriGit":
                soketeGeriGit = true;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (secildi)
        {
            transform.position = Vector3.Lerp(transform.position, hareketNoktasi.transform.position, .2f);
            if (Vector3.Distance(transform.position, hareketNoktasi.transform.position) < .10f)
            {
                secildi = false; 
            }
        }
        if (posDegis)
        {
            transform.position = Vector3.Lerp(transform.position, hareketNoktasi.transform.position, .2f);
            if (Vector3.Distance(transform.position, hareketNoktasi.transform.position) < .10f)
            {
                posDegis = false;
                soketOtur = true ;
            }
        }
        if (soketOtur)
        {
            manager.CembereGirmeMusic.Play();
            transform.position = Vector3.Lerp(transform.position, aitOlduguSoket.transform.position, .2f);
            if (Vector3.Distance(transform.position, aitOlduguSoket.transform.position) < .10f)
            {
                transform.position = aitOlduguSoket.transform.position;
                soketOtur = false;

                aitOlduguStand = aitOlduguStandTemp;

                if(aitOlduguStand.GetComponent<Stand>().cemberler.Count > 1)
                {
                    aitOlduguStand.GetComponent<Stand>().cemberler[^2].GetComponent<Cember>().hareketEdebilirMi = false;

                }
                manager.hareketVarMi = false;
            }
        }

        if (soketeGeriGit)
        {
            manager.CembereGeriDonmeMusic.Play();
            transform.position = Vector3.Lerp(transform.position, aitOlduguSoket.transform.position, .2f);
            if (Vector3.Distance(transform.position, aitOlduguSoket.transform.position) < .10f)
            {
                transform.position = aitOlduguSoket.transform.position;
                soketeGeriGit = false;
                manager.hareketVarMi = false;
            }
        }
    }
}
