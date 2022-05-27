using UnityEngine;


public class ChooseLevel : MonoBehaviour
{
    [Header("Scenes name")]
    [SerializeField] private string[] scenes;
    [Header("Planet Animator")]
    [SerializeField] private Animator planetAnimatior;
    private int index;

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
    }
    
    public void Level(float sign)
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
    }

    public void SaveChooseLevel()
    {
        PlayerPrefs.SetInt("IndexAnimPlanet",index);
    }
}
