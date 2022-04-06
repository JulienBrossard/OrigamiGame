using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tools : MonoBehaviour
{
    public static Tools instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddVariableInArray <T>(T[] array, T variable)
    {
        array.ToList().Add(variable);
        array.ToArray();
    }
    
    public void AddVariablesInArray <T>(T[] array, T variable, int number)
    {
        for (int i = 0; i < number; i++)
        {
            array.ToList().Add(variable);
        }
        array.ToArray();
    }

    public int[] FillArray (int minValue, int maxValue, int[] array)
    {
        array = new int[maxValue+1];
        for (int i = minValue; i < maxValue+1; i++)
        {
            array[i] = i;
        }

        return array;
    }
    
    public bool IsPointerOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }

        var pe = new PointerEventData(EventSystem.current)
        {
            position = Input.GetTouch(0).position
        };
        var hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pe, hits);
        return hits.Count > 0;
    }

}
