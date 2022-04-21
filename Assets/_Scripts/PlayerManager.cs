using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public enum Shapes {
        PLANE,
        BOAT 
    }
    
    public static Shapes state = Shapes.PLANE;

    public static Dictionary<Shapes, Origami> origami = new Dictionary<Shapes, Origami>();

    [SerializeField] private Origami[] origamis;

    public static PlayerManager instance;
    
    private void Awake()
    {
        InitDictionary();
        instance = this;
    }

    [ContextMenu("InitDictionary")]
    void InitDictionary()
    {
        state = 0;
        origami = new Dictionary<Shapes, Origami>();
        
        for (int i = 0; i < origamis.Length; i++)
        {
            for (int j = 0; j < origamis.Length; j++)
            {
                if (origamis[j].name.ToUpper() == state.ToString())
                {
                    origami.Add(state, origamis[j]);
                    state++;
                    Debug.Log($"{origamis[i].name} add");
                    goto CONTINUE;
                }
            }
            Debug.LogWarning($"{origamis[i].name} isn't add");
            CONTINUE : continue;
        }
        state = 0;
    }

    [ContextMenu("AddState")]
    public void ChangeState()
    {
        if (state == Shapes.PLANE)
        {
            state = Shapes.BOAT;
        }
        else
        {
            state = Shapes.PLANE;
        }
    }
}

[Serializable]
public class Origami
{
    public string name;
    public float speed;
    public float fallSpeed;
    public Vector3 collider;
    public float colliderCenter;
    public int transitionAnimationIndex;
}

