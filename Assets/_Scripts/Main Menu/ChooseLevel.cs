using UnityEngine;


public class ChooseLevel : MonoBehaviour
{
    [Header("Scenes name")]
    [SerializeField] private string[] scenes;
    [Header("Planet Animator")]
    [SerializeField] private Animator planetAnimatior;
    private int index;
    [Header("Animation")] 
    [SerializeField] private GameObject handAnimation;

    public static ChooseLevel instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        #region Initialisation
        
        index = PlayerPrefs.GetInt("IndexAnimPlanet"); // Récupère l'index sauvegardé
        Menu.instance.resetSceneName = scenes[index]; // Reset du nom de la scène
        planetAnimatior.SetInteger("Index",index); // On sauvegarde la valeur de notre index
        
        #endregion
        
        if (PlayerPrefs.GetInt("FirstTime",0)==1 && PlayerPrefs.GetInt("isChooseLevel",0)==0)
        {
            handAnimation.SetActive(true);
        }
    }
    
    public void Level(float sign)
    {
        if (PlayerPrefs.GetInt("FirstTime",0)==1)
        {
            index += (int) Mathf.Sign(sign);
            if (index>=scenes.Length) // On revient à zéro
            {
                index = 0;
            }
            else if(index<0) // On revient au dernier index de la liste
            {
                index = scenes.Length - 1;
            }
            Menu.instance.resetSceneName = scenes[index]; // On reset le nom de la scène
            planetAnimatior.SetInteger("Index",index); // On sauvegarde la valeur de notre index
            PlayerPrefs.SetInt("isChooseLevel",1);
            PlayerPrefs.Save();
            handAnimation.SetActive(false);
        }
    }

    public void SaveChooseLevel()
    {
        PlayerPrefs.SetInt("IndexAnimPlanet",index);
    }
}
