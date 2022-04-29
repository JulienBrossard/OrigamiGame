using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Subsection Data")] 
    [Tooltip("Le nombre de sous-section d'une section, de base à 2")]
    [SerializeField] private int nbSubsection;
    [Tooltip("La longueur d'une sous-section, de base à 100")]
    [SerializeField] private float subSectionLenght;
    [Tooltip("La position initiale du spawn de la première sous-section du jeu, de base à (0, 0, 0)")]
    [SerializeField] private Vector3 initialPosition;

    [Header("Section Data")] 
    [Tooltip("Le nombre de section d'un level, de base à 2")]
    [SerializeField] private int nbSection;
    private float sectionLenght;

    [Header("Level Data")]
    [Tooltip("La largeur du level, de base à 5")]
    [SerializeField] public float levelWidth;
    private GameObject[] currentLevels;
    private float lastZPosition;

    [Header("Prefabs")]
    [TextArea] [SerializeField] private string prefabsInformation;
    [Tooltip("Les prefabs des LD du jeu, à récupérer dans le dossier Level Design")]
    [SerializeField] public GameObject[] levels;
    private GameObject currentSubSection;

    [Header("Player Data")] 
    [Tooltip("Le transform du joueur, à récupérer sur le joueur")]
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentLevels = new GameObject[nbSection * nbSubsection];
        lastZPosition = playerTransform.position.z;
        sectionLenght = nbSubsection * subSectionLenght;
        InitLevel();

#if UNITY_EDITOR
        
        ldName.gameObject.SetActive(true);
        
#endif
    }

    private void Update()
    {
        if (lastZPosition + sectionLenght < playerTransform.position.z)
        {
            StartCoroutine(NewLevel());
        }

#if UNITY_EDITOR
        
        UpdateName();
        
#endif
        
    }

    [ContextMenu("Init Level")]
    void InitLevel()
    {
        for (int i = 0; i < nbSection; i++)
        {
            for (int j = 0; j < nbSubsection; j++)
            {
                MakeLevel(subSectionLenght*(i*nbSubsection+j+1) - subSectionLenght/2,i*nbSubsection+j );
            }
        }
    }

    private void MakeLevels()
    {
        for (int i = 0; i < nbSubsection; i++)
        {
            MakeLevel(subSectionLenght*((nbSubsection+i))+sectionLenght-subSectionLenght - subSectionLenght/2, i);
        }
    }
    
    private void MakeLevel(float zPosition, int index)
    {
        currentSubSection = Pooler.instance.Pop(levels[UnityEngine.Random.Range(0,levels.Length)].name);
        currentSubSection.transform.position = initialPosition + new Vector3(0, 0, lastZPosition+zPosition);
        currentLevels[index] = currentSubSection;
    }

    private void DePopLevels()
    {
        for (int i = 0; i < 2; i++)
        {
            Pooler.instance.DePop(currentLevels[i].name.Replace("(Clone)",String.Empty),currentLevels[i]);
        }
    }

    IEnumerator NewLevel()
    {
        lastZPosition = (int) playerTransform.position.z;
        yield return new WaitForSeconds(5);
        Tools.instance.Sort(currentLevels);
        DePopLevels();
        MakeLevels();
        Debug.Log("New Level");
    }


#if UNITY_EDITOR

    private RaycastHit hit;
    private int currentID;
    private int temp;
    private Transform nextTransform;
    private Transform currentTransform;
    [Header("Level Design Name")]
    [SerializeField] private TextMeshProUGUI ldName;
    [Header("Level Design Layer")] 
    [SerializeField] private LayerMask layer;

    void UpdateName()
    {
        if (Physics.Raycast(playerTransform.position, Vector3.down, out hit, 100, layer) && hit.collider.GetInstanceID() != currentID)
        {
            currentID = hit.collider.GetInstanceID();
            currentTransform = hit.collider.transform;
            nextTransform = currentTransform.parent;
            while (nextTransform.name != "Pooler" && temp<6)
            {
                temp++;
                currentTransform = nextTransform;
                nextTransform = currentTransform.parent;
            }

            temp = 0;
            ldName.text = currentTransform.name;
        }
        
    }

#endif
}
