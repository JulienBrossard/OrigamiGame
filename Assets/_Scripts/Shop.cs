using System;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Skin[] skins;

    private void Start()
    {
        skins[0].index = PlayerPrefs.GetInt("IndexShopBoat");
        NextSkinBoat(0);
        skins[1].index = PlayerPrefs.GetInt("IndexShopPlane");
        NextSkinPlane(0);
    }

    public void NextSkinBoat(int next)
    {
        skins[0].index -= next;
        if (skins[0].index<0)
        {
            skins[0].index = skins[0].materials.Length - 1;
        }
        else if(skins[0].index>=skins[0].materials.Length)
        {
            skins[0].index = 0;
        }
        for (int i = -1; i < 2; i++)
        {
            if (skins[0].index+i<0)
            {
                skins[0].images[i+1].sprite = skins[0].sprites[skins[0].sprites.Length-1];
            }
            else if(skins[0].index+i>=skins[0].sprites.Length)
            {
                skins[0].images[i+1].sprite = skins[0].sprites[0];
            }
            else
            {
                skins[0].images[i+1].sprite = skins[0].sprites[skins[0].index+i];
            }
        }
        skins[0].origami.GetComponent<MeshRenderer>().material = skins[0].materials[skins[0].index];
        PlayerPrefs.SetInt("IndexShopBoat",skins[0].index);
    }
    
    public void NextSkinPlane(int next)
    {
        skins[1].index -= next;
        if (skins[1].index<0)
        {
            skins[1].index = skins[1].materials.Length - 1;
        }
        else if(skins[1].index>=skins[1].materials.Length)
        {
            skins[1].index = 0;
        }
        for (int i = -1; i < 2; i++)
        {
            if (skins[1].index+i<0)
            {
                skins[1].images[i+1].sprite = skins[1].sprites[skins[1].sprites.Length-1];
            }
            else if(skins[1].index+i>=skins[1].sprites.Length)
            {
                skins[1].images[i+1].sprite = skins[1].sprites[0];
            }
            else
            {
                skins[1].images[i+1].sprite = skins[1].sprites[skins[1].index+i];
            }
        }
        skins[1].origami.GetComponent<MeshRenderer>().material = skins[1].materials[skins[1].index];
        PlayerPrefs.SetInt("IndexShopPlane",skins[1].index);
    }
}

[Serializable]
public class Skin
{
    [SerializeField] private string name;
    public Sprite[] sprites;
    public Material[] materials;
    public Image[] images;
    public GameObject origami;
    public int index;
}
