using System;
using System.Collections;
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
    [SerializeField] private GameObject[] levels;
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
    }

    private void Update()
    {
        if (lastZPosition + sectionLenght < playerTransform.position.z)
        {
            StartCoroutine(NewLevel());
        }
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


    /*
    [SerializeField] private string[] obstaclesPooler;
    [SerializeField] private string[] cloudsPooler;
    [SerializeField] private string[] cloudPaperPieces;
    [SerializeField] private int[] heights;
    public float maxHorizontalPosition;
    public static LevelManager instance;
    
    private GameObject currentCloud;
    private int zPositionIndex;
    private int zScale;

    private GameObject paperPiece;

    private List<GameObject> clouds;
    private List<GameObject> obstacles;
    private List<GameObject> lastClouds;
    private List<GameObject> lastObstacles;
    private List<GameObject> paperPieces;
    private List<GameObject> lastpaperPieces;

    private GameObject currentObstacle;
    private int currentObstacleIndex;

    [SerializeField] private Transform player;
    private float lastPlanePosition;

    private GameObject lastLevelStruct;
    private GameObject levelStruct;
    

    [SerializeField] private CloudHeightData[] cloudHeightData;
    [SerializeField] private LevelData leveldata;
    [SerializeField] private PaperPieceData paperPieceData;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lastPlanePosition = player.position.z;
        MakeLevel(lastPlanePosition);
        MakeLevel(lastPlanePosition+leveldata.areaSize);
    }

    private void Update()
    {
        if (player.position.z>=lastPlanePosition+leveldata.areaSize)
        {
            lastPlanePosition = player.position.z;
            StartCoroutine(NewLevel());
        }
    }

    void MakeLevel(float planePosition)
    {
        
        //cloud
        if (clouds!=null && obstacles!=null)
        {
            lastClouds = clouds;
            lastObstacles = obstacles;
            lastLevelStruct = levelStruct;
            lastpaperPieces = paperPieces;
        }
        clouds= new List<GameObject>();
        obstacles= new List<GameObject>();
        paperPieces = new List<GameObject>();
        levelStruct = Pooler.instance.Pop("Level Struct");
        levelStruct.transform.position = new Vector3(0, 0, planePosition + 10 + leveldata.areaSize / 2);
        for (int j = 0; j<heights.Length; j++)
        {
            for (int k = 0; k < leveldata.areaSize/10; k++)
            {
                switch (j)
                {
                    case 0 :
                        for (int i = 0; i < Random.Range(cloudHeightData[j].minNbCloud,cloudHeightData[j].maxNbCloud); i++)
                        {
                            if (Random.Range(1,9)==1)
                            {
                                currentCloud = Pooler.instance.Pop(cloudsPooler[Random.Range(0,cloudsPooler.Length)]);
                            

                                currentCloud.transform.position = new Vector3(
                                    Random.Range(-cloudHeightData[j].maxCloudHorizontalPosition,
                                        cloudHeightData[j].maxCloudHorizontalPosition + 1),
                                    Random.Range(cloudHeightData[j].minCloudHeight,
                                        cloudHeightData[j].maxCloudHeight + 1),
                                    Random.Range(planePosition+10+leveldata.areaSize/10*k + 10,planePosition +leveldata.areaSize/10*k + leveldata.areaSize/10 + 10)
                                );

                                if (Random.Range(0, 10)>=(1-paperPieceData.cloudPaperPiecesProbability)*10)
                                {
                                    paperPiece = Pooler.instance.Pop(cloudPaperPieces[Random.Range(0, cloudPaperPieces.Length)]);

                                    paperPiece.transform.position = currentCloud.transform.position + Vector3.up*1.5f;
                                    paperPieces.Add(paperPiece);
                                }
                                
                                clouds.Add(currentCloud);
                            }
                        }
                        break;
                    case 1 :
                        for (int i = 0; i < Random.Range(cloudHeightData[j].minNbCloud,cloudHeightData[j].maxNbCloud); i++)
                        {
                            if (Random.Range(1,9)==1)
                            {
                                currentCloud = Pooler.instance.Pop(cloudsPooler[Random.Range(0,cloudsPooler.Length)]);

                                currentCloud.transform.position = new Vector3(
                                    Random.Range(-cloudHeightData[j].maxCloudHorizontalPosition,
                                        cloudHeightData[j].maxCloudHorizontalPosition + 1),
                                    Random.Range(cloudHeightData[j].minCloudHeight,
                                        cloudHeightData[j].maxCloudHeight + 1),
                                    Random.Range(planePosition+10+leveldata.areaSize/10*k + 10,planePosition +leveldata.areaSize/10*k + leveldata.areaSize/10 + 10)
                                );
                                
                                if (Random.Range(0, 10)>=(1-paperPieceData.cloudPaperPiecesProbability)*10)
                                {
                                    paperPiece = Pooler.instance.Pop(cloudPaperPieces[Random.Range(0, cloudPaperPieces.Length)]);

                                    paperPiece.transform.position = currentCloud.transform.position + Vector3.up*1.5f;
                                    paperPieces.Add(paperPiece);
                                }
                                
                                clouds.Add(currentCloud);
                            }
                        }
                        break;
                    case 2 :
                        for (int i = 0; i < Random.Range(cloudHeightData[j].minNbCloud,cloudHeightData[j].maxNbCloud); i++)
                        {
                            if (Random.Range(1,9)==1)
                            {
                                currentCloud = Pooler.instance.Pop(cloudsPooler[Random.Range(0,cloudsPooler.Length)]);

                                currentCloud.transform.position = new Vector3(
                                    Random.Range(-cloudHeightData[j].maxCloudHorizontalPosition,
                                        cloudHeightData[j].maxCloudHorizontalPosition + 1),
                                    Random.Range(cloudHeightData[j].minCloudHeight,
                                        cloudHeightData[j].maxCloudHeight + 1),
                                    Random.Range(planePosition+10+leveldata.areaSize/10*k + 10,planePosition +leveldata.areaSize/10*k + leveldata.areaSize/10 + 10)
                                );
                                
                                if (Random.Range(0, 10)>=(1-paperPieceData.cloudPaperPiecesProbability)*10)
                                {
                                    paperPiece = Pooler.instance.Pop(cloudPaperPieces[Random.Range(0, cloudPaperPieces.Length)]);

                                    paperPiece.transform.position = currentCloud.transform.position + Vector3.up*1.5f;
                                    paperPieces.Add(paperPiece);
                                }
                            
                                clouds.Add(currentCloud);
                            }
                        }
                        break;
                }
            }
        }

        //Obstacles
        for (int i = 0; i < leveldata.maxObstacles; i++)
        {
            MakeObstacle(planePosition,i);
        }
    }

    void MakeObstacle(float planePosition,int section)
    {
        currentObstacleIndex = Random.Range(0, obstaclesPooler.Length);
        currentObstacle = Pooler.instance.Pop(obstaclesPooler[currentObstacleIndex]);
        currentObstacle.transform.position = new Vector3(Random.Range(-leveldata.maxHorizontalPosition, leveldata.maxHorizontalPosition),
            0,
            Random.Range(planePosition+10+leveldata.areaSize/10*section + 10,planePosition+leveldata.areaSize/10*section + leveldata.areaSize/10 + 10));
        for (int i = 0; i < clouds.Count; i++)
        {
            if ((currentObstacle.transform.position.x>=(clouds[i].transform.position.x-2) && currentObstacle.transform.position.x<=(clouds[i].transform.position.x+2)) && (currentObstacle.transform.position.z>=(clouds[i].transform.position.z-6) && currentObstacle.transform.position.z<=(clouds[i].transform.position.z+6)))
            {
                if (currentObstacle.transform.position.y+currentObstacle.transform.localScale.y/2>=clouds[i].transform.position.y-0.5f)
                {
                    Pooler.instance.DePop(obstaclesPooler[currentObstacleIndex],currentObstacle);
                    MakeObstacle(planePosition,section);
                    return;
                }
            }
        }
        
        for (int i = 0; i < obstacles.Count; i++)
        {
            if ((currentObstacle.transform.position.x>=obstacles[i].transform.position.x-1 && currentObstacle.transform.position.x<=obstacles[i].transform.position.x+1) && (currentObstacle.transform.position.z>=obstacles[i].transform.position.z-1 && currentObstacle.transform.position.z<=obstacles[i].transform.position.z+1))
            {
                Pooler.instance.DePop(obstaclesPooler[currentObstacleIndex],currentObstacle);
                MakeObstacle(planePosition,section);
                return;
            }
        }
        obstacles.Add(currentObstacle);
    }

    void DePopLevel()
    {
        Pooler.instance.DePop("Level Struct",lastLevelStruct);
        for (int i = 0; i < lastClouds.Count; i++)
        {
            Pooler.instance.DePop(lastClouds[i].name.Replace("(Clone)",String.Empty),lastClouds[i]);
        }

        for (int i = 0; i < lastObstacles.Count; i++)
        {
            Pooler.instance.DePop(lastObstacles[i].name.Replace("(Clone)",String.Empty),lastObstacles[i]);
        }

        for (int i = 0; i < lastpaperPieces.Count; i++)
        {
            Pooler.instance.DePop(lastpaperPieces[i].name.Replace("(Clone)",String.Empty),lastpaperPieces[i]);
        }
    }

    IEnumerator NewLevel()
    {
        yield return new WaitForSeconds(5f);
        DePopLevel();
        MakeLevel(lastPlanePosition+leveldata.areaSize);
    }*/
}

/*
[Serializable]
public class CloudHeightData
{
    public int minNbCloud;
    public int maxNbCloud;
    public int minCloudHeight;
    public int maxCloudHeight;
    public int maxCloudHorizontalPosition;
}

[Serializable]
public class LevelData
{
    public int maxHorizontalPosition;
    public int areaSize;
    public int maxObstacles;
}

[Serializable]
public class PaperPieceData
{
    public float cloudPaperPiecesProbability;
}*/