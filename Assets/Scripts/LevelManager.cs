using System;
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

    private GameObject currentObstacle;

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


    void MakeLevel()
    {
        
        //cloud
        for (int j = 0; j<heights.Length; j++)
        {
            switch (j)
            {
                case 0 :
                    //Tools.instance.AddVariablesInArray(poolers,"Cloud",Random.Range(cloudANdObstaclesData[j].minNbCloud,cloudANdObstaclesData[j].maxNbCloud));
                    cloudHeightData[j].positions = Tools.instance.FillArray(0,leveldata.areaSize,cloudHeightData[j].positions);
                    for (int i = 0; i < Random.Range(cloudHeightData[j].minNbCloud,cloudHeightData[j].maxNbCloud); i++)
                    {
                        currentCloud = Pooler.instance.Pop("Cloud");

                        zPositionIndex = Random.Range(0,
                            cloudHeightData[j].positions.Length);

                        currentCloud.transform.position = new Vector3(
                            Random.Range(-cloudHeightData[j].maxCloudHorizontalPosition,
                                cloudHeightData[j].maxCloudHorizontalPosition + 1),
                            Random.Range(cloudHeightData[j].minCloudHeight,
                                cloudHeightData[j].maxCloudHeight + 1),
                            cloudHeightData[j].positions[zPositionIndex]
                            );

                        zScale = Random.Range(cloudData.minCloudScale, cloudData.maxCloudScale);

                        currentCloud.transform.localScale = new Vector3(
                            currentCloud.transform.localScale.x,
                            currentCloud.transform.localScale.y,
                            zScale);
                    }
                    break;
                case 1 :
                    //Tools.instance.AddVariablesInArray(poolers,"Cloud",Random.Range(cloudANdObstaclesData[j].minNbCloud,cloudANdObstaclesData[j].maxNbCloud));
                    cloudHeightData[j].positions = Tools.instance.FillArray(0,leveldata.areaSize,cloudHeightData[j].positions);
                    for (int i = 0; i < Random.Range(cloudHeightData[j].minNbCloud,cloudHeightData[j].maxNbCloud); i++)
                    {
                        currentCloud = Pooler.instance.Pop("Cloud");

                        zPositionIndex = Random.Range(0,
                            cloudHeightData[j].positions.Length);

                        currentCloud.transform.position = new Vector3(
                            Random.Range(-cloudHeightData[j].maxCloudHorizontalPosition,
                                cloudHeightData[j].maxCloudHorizontalPosition + 1),
                            Random.Range(cloudHeightData[j].minCloudHeight,
                                cloudHeightData[j].maxCloudHeight + 1),
                            cloudHeightData[j].positions[zPositionIndex]
                        );

                        zScale = Random.Range(cloudData.minCloudScale, cloudData.maxCloudScale);

                        currentCloud.transform.localScale = new Vector3(
                            currentCloud.transform.localScale.x,
                            currentCloud.transform.localScale.y,
                            zScale);
                    }
                    break;
                case 2 :
                    //Tools.instance.AddVariablesInArray(poolers,"Cloud",Random.Range(cloudANdObstaclesData[j].minNbCloud,cloudANdObstaclesData[j].maxNbCloud));
                    cloudHeightData[j].positions = Tools.instance.FillArray(0,leveldata.areaSize,cloudHeightData[j].positions);
                    for (int i = 0; i < Random.Range(cloudHeightData[j].minNbCloud,cloudHeightData[j].maxNbCloud); i++)
                    {
                        currentCloud = Pooler.instance.Pop("Cloud");

                        zPositionIndex = Random.Range(0,
                            cloudHeightData[j].positions.Length);

                        currentCloud.transform.position = new Vector3(
                            Random.Range(-cloudHeightData[j].maxCloudHorizontalPosition,
                                cloudHeightData[j].maxCloudHorizontalPosition + 1),
                            Random.Range(cloudHeightData[j].minCloudHeight,
                                cloudHeightData[j].maxCloudHeight + 1),
                            cloudHeightData[j].positions[zPositionIndex]
                        );

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
            currentObstacle = Pooler.instance.Pop(poolers[Random.Range(0, poolers.Length)]);
            currentObstacle.transform.position = new Vector3(Random.Range(-leveldata.maxHorizontalPosition, leveldata.maxHorizontalPosition),
                0,
                cloudHeightData[0].positions[Random.Range(0,cloudHeightData[0].positions.Length)]);
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
    public int[] positions;
}

[Serializable]
public class LevelData
{
    public int maxHorizontalPosition;
    public int areaSize;
    public int maxObstacles;
}
