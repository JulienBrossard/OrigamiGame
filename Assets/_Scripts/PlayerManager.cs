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

    public static PlayerManager instance;
    
    public delegate void ChangeOrigami();
    public ChangeOrigami changeOrigami;

    [Header("Origami Data")]
    [Tooltip("Les données spécifiques à chaque origami")]
    [SerializeField] private Origami[] origamis;

    [Header("Text Button Origami")] 
    [Tooltip("Le texte affichant le prochain origami lorqu'on appuiera sur le bouton ChangeOrigami, à récupérer dans le texte du bouton Change Origami")]
    [HideInInspector] private GameObject origamiButton;
    
    [Header("Layer")]
    [Tooltip("Les layers que le raycast pour l'ombre peut toucher, de base Everything sans Shadow et Player")]
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
        changeOrigami += UpdateButton;
        changeOrigami += ChangeState;
        changeOrigami += ChangeShadow;
        changeOrigami += ChangeLayer;
        shadow = origami[state].shadow;
    }
    
    private void FixedUpdate()
    {
        if (Time.frameCount % 3 == 0)
        {
            Shadow();
        }
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
        shadow.SetActive(false);
        shadow = origami[state].shadow;
        shadow.SetActive(true);
    }

    public void ChangeLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(origami[state].layer);
    }
    
    //Update le texte afficher sur le bouton de changement d'origami
    void UpdateButton()
    {
        AnimationManager.instance.ButtonOrigami();
    }

    //Update de la position de l'ombre
    void Shadow()
    {
        if (Physics.Raycast(transform.position + Vector3.forward*2,Vector3.down,out hit,30,layer))
        {
            shadow.transform.position = hit.point + Vector3.forward*2 + Vector3.up*0.51f;
        }
    }
}

[Serializable]
public class Origami
{
    [Header("Name")]
    [Tooltip("Le nom de l'origami, mais sans faute d'othographe et en anglais")]
    public string name;
    [Header("Speeds")]
    [Tooltip("La vitesse de cet origami")]
    public float speed;
    [Tooltip("La vitesse de la gravité sur cet origami")]
    public float fallSpeed;
    [Header("Collider")]
    [Tooltip("La taille du box collider de cet origami")]
    public Vector3 collider;
    [Tooltip("Le centre du collider de cet origami")]
    public float colliderCenter;
    [Header("Animation")]
    [Tooltip("L'index de l'animation transition dans l'animator pour cet origami, de base 2 pour l'avion et 1 pour le bateau")]
    public int transitionAnimationIndex;
    [Header("SHadow")] 
    [Tooltip("L'ombre de cet origami")]
    public GameObject shadow;
    [Header("Layer")] 
    public string layer;
}

