using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{
#if UNITY_EDITOR
    
    private LevelManager levelManager;
    private Pooler pooler;
    private string path;

    [MenuItem("Tools/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelEditor));
    }

    void OnGUI()
    {
        GUILayout.Label("Update Level",EditorStyles.boldLabel);
        levelManager = EditorGUILayout.ObjectField("Level Manager", levelManager,typeof(LevelManager),true) as LevelManager;
        pooler = EditorGUILayout.ObjectField("Pooler", pooler,typeof(Pooler),true) as Pooler;
        if (GUILayout.Button("Update Level"))
        {
            UpdateLevel();
        }
    }

    void UpdateLevel()
    {
        List<string> filesPath = new List<string>();
        var files = 
        Directory.GetFiles("Assets/_Prefabs/LEVEL DESIGN/Level_Prefab");
        foreach (var file in files)
        {
            if (file.Contains(".meta"))
            {
                continue;
            }
            filesPath.Add(file);
        }

        List<GameObject> prefabs = new List<GameObject>();
        foreach (var file in filesPath)
        {
            prefabs.Add(AssetDatabase.LoadAssetAtPath<GameObject>(file));
        }

        levelManager.levels = new GameObject[prefabs.Count];
        pooler.poolKeys = new List<Pooler.PoolKey>();
        pooler.pools = new Dictionary<string, Pooler.Pool>();

        for (int i = 0; i < prefabs.Count; i++)
        {
            levelManager.levels[i] = prefabs[i];
            pooler.poolKeys.Add(new Pooler.PoolKey());
            pooler.poolKeys[i].pool = new Pooler.Pool();
            pooler.poolKeys[i].key = prefabs[i].name;
            pooler.poolKeys[i].pool.prefab = prefabs[i];
            pooler.poolKeys[i].pool.baseCount = 8;
            pooler.poolKeys[i].pool.baseRefreshSpeed = 5;
        }

        filesPath = new List<string>();
        files = Directory.GetFiles("Assets/_Prefabs/ART/High Score");
        foreach (var file in files)
        {
            if (file.Contains(".meta"))
            {
                continue;
            }
            filesPath.Add(file);
        }

        prefabs = new List<GameObject>();
        foreach (var file in filesPath)
        {
            prefabs.Add(AssetDatabase.LoadAssetAtPath<GameObject>(file));
        }
        
        for (int i = 0; i < prefabs.Count; i++)
        {
            pooler.poolKeys.Add(new Pooler.PoolKey());
            pooler.poolKeys[pooler.poolKeys.Count-1].pool = new Pooler.Pool();
            pooler.poolKeys[pooler.poolKeys.Count-1].key = prefabs[i].name;
            pooler.poolKeys[pooler.poolKeys.Count-1].pool.prefab = prefabs[i];
            pooler.poolKeys[pooler.poolKeys.Count-1].pool.baseCount = 8;
            pooler.poolKeys[pooler.poolKeys.Count-1].pool.baseRefreshSpeed = 5;
        }
    }
    
#endif
}
