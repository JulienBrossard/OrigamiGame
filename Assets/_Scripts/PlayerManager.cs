using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public enum Shapes {
        PLANE,
        BOAT 
    }

    public static Shapes state = Shapes.PLANE;

    public static Dictionary<Shapes, Origami> origami = new Dictionary<Shapes, Origami>();
    
    [HideInInspector] public BoxCollider cloudBoxCollider;
    
    public static PlayerManager instance;
    
    public delegate void ChangeOrigami();
    public ChangeOrigami changeOrigami;

    [Header("Origami Data")]
    [SerializeField] private Origami[] origamis;

    [Header("Text Button Origami")] 
    [SerializeField] private TextMeshProUGUI origamiText;
    
    [Header("Layer")]
    [SerializeField] private LayerMask layer;
    private RaycastHit hit;
    
    [Header("Shadow")]
    private GameObject shadow;
    

    private void Awake()
    {
        InitDictionary();
        instance = this;
    }

    private void Start()
    {
        changeOrigami += UpdateText;
        changeOrigami += TriggerCloud;
        changeOrigami += ChangeState;
        changeOrigami += ChangeShadow;
        shadow = origami[state].shadow;
    }
    
    private void FixedUpdate()
    {
        Shadow();
    }

    //Initialise le dictionnaire
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

    //Change l'état du joueur
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
    
    public void ChangeShadow()
    {
        shadow = origami[state].shadow;
    }
    
    //Update le texte afficher sur le bouton de changement d'origami
    void UpdateText()
    {
        origamiText.text = origami[state].name;
    }
    
    //Change l'état Trigger du dernier nuage touché
    void TriggerCloud()
    {
        if (cloudBoxCollider != null)
        {
            cloudBoxCollider.isTrigger = !cloudBoxCollider.isTrigger;
        }
    }


    //Update de la position de l'ombre
    void Shadow()
    {
        if (Physics.Raycast(transform.position,Vector3.down,out hit,Mathf.Infinity,layer))
        {
            shadow.transform.position = hit.point;
        }
    }
}

[Serializable]
public class Origami
{
    [Header("Name")]
    public string name;
    [Header("Speeds")]
    public float speed;
    public float fallSpeed;
    [Header("Collider")]
    public Vector3 collider;
    public float colliderCenter;
    [Header("Animation")]
    public int transitionAnimationIndex;
    [Header("SHadow")] 
    public GameObject shadow;
}

