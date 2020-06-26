using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesController : MonoBehaviour
{
    public int teamID;
    public List<int> maxResourcesAmount;
    public List<int> currentResourcesAmount;

    #region Singleton
    public static ResourcesController instance;
    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    #endregion

    public event Action OnChangeResource;
    
    private void Start() 
    {
        OnChangeResource();
    }
    
    public void ChangeResource(ResourceType rt, int amount)
    {
        currentResourcesAmount[(int)rt] += amount;
        OnChangeResource();
    }
}

public enum ResourceType { Credits, Villagers, Wizards }
