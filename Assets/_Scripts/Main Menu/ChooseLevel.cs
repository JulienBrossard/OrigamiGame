using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    [SerializeField] private string[] scenes;
    [SerializeField] private Animator planetAnimatior;
    [SerializeField] private int index;

    public static ChooseLevel instance;

    private void Awake()
    {
        instance = this;
    }

    public void Level(float sign)
    {
        index += (int) Mathf.Sign(sign);
        if (index>=scenes.Length)
        {
            index = 0;
        }
        else if(index<0)
        {
            index = scenes.Length - 1;
        }
        Menu.instance.resetSceneName = scenes[index];
        planetAnimatior.SetInteger("Index",index);
        //StartCoroutine(StopAnim());
    }

    IEnumerator StopAnim()
    {
        yield return new WaitForSeconds(1);
        planetAnimatior.SetInteger("Index",-1);
    }
}
