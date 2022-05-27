using UnityEngine;
using UnityEditor;

public class ShopEditor : MonoBehaviour
{
    private static string localPath;
    private static GameObject origami;

    // Creates a new menu item 'Examples > Create Prefab' in the main menu.
    static void ChangeMaterial(string prefab, Material material)
    {
        // Loop through every GameObject in the array above
        localPath = "Assets/Prefabs/" + prefab + ".prefab";
            
        origami = AssetDatabase.LoadAssetAtPath<GameObject>(localPath);

        if ( prefab == "Plane")
        {
            origami.GetComponent<SkinnedMeshRenderer>().material = material;
        }
        else 
        { 
            origami.GetComponent<MeshRenderer>().material = material;
        }
            
        // Create the new Prefab and log whether Prefab was saved successfully.
        PrefabUtility.SaveAsPrefabAssetAndConnect(origami, localPath, InteractionMode.UserAction);
    }
}
