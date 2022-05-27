using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private Skin[] skins;

    private void Start()
    {
        #region Initialisation
        
        PlayerPrefs.SetInt("Purchased Skin Boat" + 0, 1); // Skin de base gratuit
        PlayerPrefs.SetInt("Purchased Skin Plane" + 0, 1); // Skin de base gratuit
        skins[0].index = PlayerPrefs.GetInt("IndexShopBoat");
        NextSkinBoat(0);
        skins[1].index = PlayerPrefs.GetInt("IndexShopPlane");
        NextSkinPlane(0);
        DisplayMoney();

        #endregion
    }

    //Skins du bateau
    public void NextSkinBoat(int next)
    {
        // Index du shop
        skins[0].index -= next;
        if (skins[0].index<0)
        {
            skins[0].index = skins[0].materials.Length - 1;
        }
        else if(skins[0].index>=skins[0].materials.Length)
        {
            skins[0].index = 0;
        }
        
        // Sprites affichés dans le shop
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

        // Si déjà acheté
        if (PlayerPrefs.GetInt("Purchased Skin Boat"+skins[0].index,0)==1)
        {
            skins[0].origami.GetComponent<MeshRenderer>().material = skins[0].materials[skins[0].index];
            PlayerPrefs.SetInt("IndexShopBoat",skins[0].index);
            skins[0].purshasedButton.SetActive(false);
            skins[0].padlock.SetActive(false);
        }
        //Si pas acheté
        else
        {
            skins[0].purshasedButton.gameObject.SetActive(true);
            skins[0].purshasedText.text = skins[0].prices[skins[0].index].ToString();
            skins[0].padlock.SetActive(true);
        }
    }
    
    //Skins de l'avion
    public void NextSkinPlane(int next)
    {
        // Index du shop
        skins[1].index -= next;
        if (skins[1].index<0)
        {
            skins[1].index = skins[1].materials.Length - 1;
        }
        else if(skins[1].index>=skins[1].materials.Length)
        {
            skins[1].index = 0;
        }
        
        // Sprites affichés dans le shop
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
        
        // Si déjà acheté
        if (PlayerPrefs.GetInt("Purchased Skin Plane"+skins[1].index,0)==1)
        {
            skins[1].origami.GetComponent<SkinnedMeshRenderer>().material = skins[1].materials[skins[1].index];
            PlayerPrefs.SetInt("IndexShopPlane",skins[1].index);
            skins[1].purshasedButton.SetActive(false);
            skins[1].padlock.SetActive(false);
        }
        //Si pas acheté
        else
        {
            skins[1].purshasedButton.gameObject.SetActive(true);
            skins[1].purshasedText.text = skins[1].prices[skins[1].index].ToString();
            skins[1].padlock.SetActive(true);
        }
    }

    //Acheter un skin
    public void Purchased(string origami)
    {
        //Si avion et assez d'argent
        if (origami == "Plane" && PlayerPrefs.GetInt("PaperPieces",0)>=skins[1].prices[skins[1].index])
        {
            PlayerPrefs.SetInt("PaperPieces",PlayerPrefs.GetInt("PaperPieces",0)-skins[1].prices[skins[1].index]);
            PlayerPrefs.SetInt("Purchased Skin Plane"+skins[1].index,1);
            PlayerPrefs.SetInt("IndexShopPlane",skins[1].index);
            skins[1].origami.GetComponent<SkinnedMeshRenderer>().material = skins[1].materials[skins[1].index];
            skins[1].purshasedButton.SetActive(false);
            skins[1].padlock.SetActive(false);
        }
        //Si bateau et assez d'argent
        else if(origami == "Boat" && PlayerPrefs.GetInt("PaperPieces",0)>=skins[0].prices[skins[0].index])
        {
            PlayerPrefs.SetInt("PaperPieces",PlayerPrefs.GetInt("PaperPieces",0)-skins[0].prices[skins[0].index]);
            PlayerPrefs.SetInt("Purchased Skin Boat"+skins[0].index,1);
            PlayerPrefs.SetInt("IndexShopPlane",skins[0].index);
            skins[0].origami.GetComponent<MeshRenderer>().material = skins[0].materials[skins[0].index];
            skins[0].purshasedButton.SetActive(false);
            skins[0].padlock.SetActive(false);
        }
        //Afficher l'argent restant
        DisplayMoney();
    }

    //Afficher l'argent du joueur
    public void DisplayMoney()
    {
        money.text = PlayerPrefs.GetInt("PaperPieces", 0).ToString();
    }
}

[Serializable]
public class Skin
{
    [SerializeField] private string name;
    public Sprite[] sprites;
    public Material[] materials;
    public int[] prices;
    public Image[] images;
    public GameObject origami;
    public int index;
    public GameObject purshasedButton;
    public TextMeshProUGUI purshasedText;
    public GameObject padlock;
}
