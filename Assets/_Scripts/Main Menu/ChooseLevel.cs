using UnityEngine;


public class ChooseLevel : MonoBehaviour
{
    [SerializeField] private string[] scenes;
    [SerializeField] private Animator planetAnimatior;
    [SerializeField] private int index;
    [SerializeField] private Transform planet;

    public static ChooseLevel instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        index = PlayerPrefs.GetInt("IndexAnimPlanet");
        Menu.instance.resetSceneName = scenes[index];
        planetAnimatior.SetInteger("Index",index);
    }
    
    public void Level(float sign)
    {
        index += (int) Mathf.Sign(sign);
        if (index>=scenes.Length)
        {
            index = 0;
        }
        else if(index<0)
        {
            index = scenes.Length - 1;
        }
        Menu.instance.resetSceneName = scenes[index];
        planetAnimatior.SetInteger("Index",index);
    }

    public void SaveChooseLevel()
    {
        PlayerPrefs.SetInt("IndexAnimPlanet",index);
    }
}
