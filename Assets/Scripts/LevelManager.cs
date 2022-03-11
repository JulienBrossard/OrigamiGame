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

    [SerializeField] private List<Vector3> cloudPositions;
    private Vector3[] obstaclesPositions;

    private GameObject currentObstacle;
    private int currentObstacleIndex;

    [SerializeField] private CloudHeightData[] cloudHeightData;
    [SerializeField] private CloudData cloudData;
    [SerializeField] private LevelData leveldata;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MakeLevel();
    }

    /*private void Update()
    {
        if (currentObstacle!=null)
        {
            hits = Physics.BoxCastAll(currentObstacle.transform.position, boxSize/2, transform.forward,
                Quaternion.identity, boxLenght, layer);
            for (int i = 0; i < hits.Length; i++)
            {
                Debug.Log(hits[i].transform.name);
            }
        }
    }*/


    void MakeLevel()
    {
        
        //cloud
        cloudPositions = new List<Vector3>();
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
                            Random.Range(0,leveldata.areaSize)
                            );
                        
                        //Tools.instance.AddVariableInArray(cloudPositions,currentCloud.transform.position);
                        cloudPositions.Add(currentCloud.transform.position);

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
                            Random.Range(0,leveldata.areaSize)
                        );
                        
                        //Tools.instance.AddVariableInArray(cloudPositions,currentCloud.transform.position);
                        cloudPositions.Add(currentCloud.transform.position);

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
                            Random.Range(0,leveldata.areaSize)
                        );
                        
                        //Tools.instance.AddVariableInArray(cloudPositions,currentCloud.transform.position);
                        cloudPositions.Add(currentCloud.transform.position);

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
            MakeObstacle();
        }
    }

    void MakeObstacle()
    {
        currentObstacleIndex = Random.Range(0, poolers.Length);
        currentObstacle = Pooler.instance.Pop(poolers[currentObstacleIndex]);
        currentObstacle.transform.position = new Vector3(Random.Range(-leveldata.maxHorizontalPosition, leveldata.maxHorizontalPosition),
            0,
            Random.Range(0,leveldata.areaSize));
        Debug.Log(cloudPositions.Count);
        for (int i = 0; i < cloudPositions.Count; i++)
        {
            Debug.Log((currentObstacle.transform.position.x>=(cloudPositions[i].x-2) && currentObstacle.transform.position.x<=(cloudPositions[i].x+2)) && (currentObstacle.transform.position.z>=(cloudPositions[i].z-6) && currentObstacle.transform.position.z<=(cloudPositions[i].z+6)));
            if ((currentObstacle.transform.position.x>=(cloudPositions[i].x-2) && currentObstacle.transform.position.x<=(cloudPositions[i].x+2)) && (currentObstacle.transform.position.z>=(cloudPositions[i].z-6) && currentObstacle.transform.position.z<=(cloudPositions[i].z+6)))
            {
                Pooler.instance.DePop(poolers[currentObstacleIndex],currentObstacle);
                MakeObstacle();
                /*if (currentObstacle.transform.position.y>cloudPositions[i].y)
                {
                    Pooler.instance.DePop(poolers[currentObstacleIndex],currentObstacle);
                    MakeObstacle();
                }*/
            }
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
