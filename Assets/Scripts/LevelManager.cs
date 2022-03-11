using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string[] poolers;
    [SerializeField] private int[] heights;
    public float maxHorizontalPosition;
    public static LevelManager instance;
    
    private GameObject currentCloud;
    private int zPositionIndex;
    private int zScale;

    private List<GameObject> clouds;
    private List<GameObject> obstacles;
    private List<GameObject> lastClouds;
    private List<GameObject> lastObstacles;

    private GameObject currentObstacle;
    private int currentObstacleIndex;

    [SerializeField] private Transform player;
    private float lastPlanePosition;

    [SerializeField] private CloudHeightData[] cloudHeightData;
    [SerializeField] private CloudData cloudData;
    [SerializeField] private LevelData leveldata;
    

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
            DePopLevel();
            MakeLevel(lastPlanePosition+leveldata.areaSize);
        }
    }


    void MakeLevel(float planePosition)
    {
        
        //cloud
        if (clouds!=null && obstacles!=null)
        {
            lastClouds = clouds;
            lastObstacles = obstacles;
        }
        clouds= new List<GameObject>();
        obstacles= new List<GameObject>();
        for (int j = 0; j<heights.Length; j++)
        {
            switch (j)
            {
                case 0 :
                    //Tools.instance.AddVariablesInArray(poolers,"Cloud",Random.Range(cloudANdObstaclesData[j].minNbCloud,cloudANdObstaclesData[j].maxNbCloud));
                    //cloudHeightData[j].positions = Tools.instance.FillArray(0,leveldata.areaSize,cloudHeightData[j].positions);
                    for (int i = 0; i < Random.Range(cloudHeightData[j].minNbCloud,cloudHeightData[j].maxNbCloud); i++)
                    {
                        currentCloud = Pooler.instance.Pop("Cloud");
                        

                        currentCloud.transform.position = new Vector3(
                            Random.Range(-cloudHeightData[j].maxCloudHorizontalPosition,
                                cloudHeightData[j].maxCloudHorizontalPosition + 1),
                            Random.Range(cloudHeightData[j].minCloudHeight,
                                cloudHeightData[j].maxCloudHeight + 1),
                            Random.Range(planePosition+10,planePosition + leveldata.areaSize+10)
                            );
                        
                        //Tools.instance.AddVariableInArray(cloudPositions,currentCloud.transform.position);
                        clouds.Add(currentCloud);

                        zScale = Random.Range(cloudData.minCloudScale, cloudData.maxCloudScale);

                        currentCloud.transform.localScale = new Vector3(
                            currentCloud.transform.localScale.x,
                            currentCloud.transform.localScale.y,
                            zScale);
                    }
                    break;
                case 1 :
                    //Tools.instance.AddVariablesInArray(poolers,"Cloud",Random.Range(cloudANdObstaclesData[j].minNbCloud,cloudANdObstaclesData[j].maxNbCloud));
                    //cloudHeightData[j].positions = Tools.instance.FillArray(0,leveldata.areaSize,cloudHeightData[j].positions);
                    for (int i = 0; i < Random.Range(cloudHeightData[j].minNbCloud,cloudHeightData[j].maxNbCloud); i++)
                    {
                        currentCloud = Pooler.instance.Pop("Cloud");

                        currentCloud.transform.position = new Vector3(
                            Random.Range(-cloudHeightData[j].maxCloudHorizontalPosition,
                                cloudHeightData[j].maxCloudHorizontalPosition + 1),
                            Random.Range(cloudHeightData[j].minCloudHeight,
                                cloudHeightData[j].maxCloudHeight + 1),
                            Random.Range(planePosition+10,planePosition + leveldata.areaSize+10)
                        );
                        
                        //Tools.instance.AddVariableInArray(cloudPositions,currentCloud.transform.position);
                        clouds.Add(currentCloud);

                        zScale = Random.Range(cloudData.minCloudScale, cloudData.maxCloudScale);

                        currentCloud.transform.localScale = new Vector3(
                            currentCloud.transform.localScale.x,
                            currentCloud.transform.localScale.y,
                            zScale);
                    }
                    break;
                case 2 :
                    //Tools.instance.AddVariablesInArray(poolers,"Cloud",Random.Range(cloudANdObstaclesData[j].minNbCloud,cloudANdObstaclesData[j].maxNbCloud));
                    //cloudHeightData[j].positions = Tools.instance.FillArray(0,leveldata.areaSize,cloudHeightData[j].positions);
                    for (int i = 0; i < Random.Range(cloudHeightData[j].minNbCloud,cloudHeightData[j].maxNbCloud); i++)
                    {
                        currentCloud = Pooler.instance.Pop("Cloud");

                        currentCloud.transform.position = new Vector3(
                            Random.Range(-cloudHeightData[j].maxCloudHorizontalPosition,
                                cloudHeightData[j].maxCloudHorizontalPosition + 1),
                            Random.Range(cloudHeightData[j].minCloudHeight,
                                cloudHeightData[j].maxCloudHeight + 1),
                            Random.Range(planePosition+10,planePosition + leveldata.areaSize+10)
                        );
                        
                        //Tools.instance.AddVariableInArray(cloudPositions,currentCloud.transform.position);
                        clouds.Add(currentCloud);

                        zScale = Random.Range(cloudData.minCloudScale, cloudData.maxCloudScale);

                        currentCloud.transform.localScale = new Vector3(
                            currentCloud.transform.localScale.x,
                            currentCloud.transform.localScale.y,
                            zScale);
                    }
                    break;
            }
        }

        for (int i = 0; i < leveldata.maxObstacles; i++)
        {
            MakeObstacle(planePosition);
        }
    }

    void MakeObstacle(float planePosition)
    {
        currentObstacleIndex = Random.Range(0, poolers.Length);
        currentObstacle = Pooler.instance.Pop(poolers[currentObstacleIndex]);
        currentObstacle.transform.position = new Vector3(Random.Range(-leveldata.maxHorizontalPosition, leveldata.maxHorizontalPosition),
            0,
            Random.Range(planePosition+10,planePosition + leveldata.areaSize+10));
        for (int i = 0; i < clouds.Count; i++)
        {
            if ((currentObstacle.transform.position.x>=(clouds[i].transform.position.x-2) && currentObstacle.transform.position.x<=(clouds[i].transform.position.x+2)) && (currentObstacle.transform.position.z>=(clouds[i].transform.position.z-6) && currentObstacle.transform.position.z<=(clouds[i].transform.position.z+6)))
            {
                /*Pooler.instance.DePop(poolers[currentObstacleIndex],currentObstacle);
                MakeObstacle();*/
                if (currentObstacle.transform.position.y+currentObstacle.transform.localScale.y/2>=clouds[i].transform.position.y-0.5f)
                {
                    Pooler.instance.DePop(poolers[currentObstacleIndex],currentObstacle);
                    MakeObstacle(planePosition);
                    return;
                }
            }
        }
        
        for (int i = 0; i < obstacles.Count; i++)
        {
            if ((currentObstacle.transform.position.x>=obstacles[i].transform.position.x-1 && currentObstacle.transform.position.x<=obstacles[i].transform.position.x+1) && (currentObstacle.transform.position.z>=obstacles[i].transform.position.z-1 && currentObstacle.transform.position.z<=obstacles[i].transform.position.z+1))
            {
                Pooler.instance.DePop(poolers[currentObstacleIndex],currentObstacle);
                MakeObstacle(planePosition);
                return;
            }
        }
        obstacles.Add(currentObstacle);
    }

    void DePopLevel()
    {
        for (int i = 0; i < lastClouds.Count; i++)
        {
            Pooler.instance.DePop("Cloud",lastClouds[i]);
        }

        for (int i = 0; i < lastObstacles.Count; i++)
        {
            Pooler.instance.DePop(lastObstacles[i].name.Replace("(Clone)",String.Empty),lastObstacles[i]);
        }
    }
}

[Serializable]
public class CloudData
{
    public int minCloudScale;
    public int maxCloudScale;
}

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
