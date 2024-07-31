using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject hareketNoktasi;
    public GameObject[] soketler;
    public int bosOlanSoket;
    public List<GameObject> cemberler = new();
    [SerializeField] GameManager manager;
    int aynýRenkSayisi;

    public GameObject EnUsttekiCemberiVer()
    {
        if (cemberler.Count != 0)
        {
            return cemberler[^1];
        }
        return null; 
        
    }
    public GameObject BosSoketiVer()
    {
        return soketler[bosOlanSoket];
    }

    public void SoketDegistirmeIslemleri(GameObject silinecekCember)
    {
        cemberler.Remove(silinecekCember);
        if(cemberler.Count != 0)
        {
            bosOlanSoket--;
            cemberler[^1].GetComponent<Cember>().hareketEdebilirMi = true;
        }
        else
        {
            bosOlanSoket = 0;
        }
    }

    public void CemberleriKontrolEt()
    {
        if(cemberler.Count == 4)
        {
            string renk = cemberler[0].GetComponent<Cember>().renk;
            foreach (var item in cemberler)
            {
                if(item.GetComponent<Cember>().renk == renk)
                {
                    aynýRenkSayisi++;
                }
            }
            if(aynýRenkSayisi == 4)
            {
                manager.StandTamamlandi();
                TamamlananStandIslemleri();
            }
            else
            {
                
                aynýRenkSayisi = 0;
            }
        }
    }

    public void TamamlananStandIslemleri()
    {
        foreach (var item in cemberler)
        {
            item.GetComponent<Cember>().hareketEdebilirMi = false;
            item.GetComponent<MeshRenderer>().material.color = Color.magenta + Color.cyan;
            gameObject.tag = "Tamamlanmis";
        }
        
    }
}
