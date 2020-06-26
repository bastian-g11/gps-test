using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneControlController : MonoBehaviour
{
    [Header("ClampUI")]
    public Transform uiElement;
    public Vector3 UIoffset;
    Camera mainCamera;

    [Header("ZoneControl")]
    public Zone currentZone;
    public List<int> teamsPopulation;
    public List<float> populationPercents;

    [Header("PopulationPopUp")]
    public List<Slider> teamsSliders;
    public List<Text> teamsAmounts;
    public Slider addSlider;
    public Text amountToAdd;

    #region Singleton
    public static ZoneControlController instance;
    private void Awake() 
    {
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

    void Start()
    {
        uiElement.GetChild(3).GetComponent<Button>().onClick.AddListener(OpenAddWindow);
        mainCamera = Camera.main;
        UpdatePopulation();
    }

    private void Update() 
    {
        if (currentZone != null)
        {
            uiElement.position = mainCamera.WorldToScreenPoint(currentZone.transform.position + UIoffset);
        }
    }

    public void SetControlBar(Zone current)
    {
        currentZone = current;
        teamsPopulation = currentZone.teamsPopulation;
        uiElement.gameObject.SetActive(true);
        UpdatePopulation();
    }

    public void OpenAddWindow()
    {
        for (int i = 0; i < populationPercents.Count; i++)
        {
            teamsSliders[i].value = populationPercents[i];
            teamsAmounts[i].text = "" + teamsPopulation[i];
        }
        addSlider.maxValue = ResourcesController.instance.currentResourcesAmount[1];
    }

    public void OnAddSliderChange()
    {
        amountToAdd.text = "+ " + addSlider.value;
    }

    public void OnClickAdd()
    {
        int amount = (int)addSlider.value;
        teamsPopulation[ResourcesController.instance.teamID] += amount;
        ResourcesController.instance.ChangeResource(ResourceType.Villagers, -amount);
        UpdatePopulation();
    }

    public void SetPopulation(int amount)
    {
        teamsPopulation[ResourcesController.instance.teamID] += amount;
        ResourcesController.instance.ChangeResource(ResourceType.Villagers, -amount);
        UpdatePopulation();
    }

    public void UpdatePopulation()
    {
        int totalPopulation = 0;
        foreach (int i in teamsPopulation)
        {
            totalPopulation += i;
        }
        Debug.Log("getting in");
        for (int i = 0; i < populationPercents.Count; i++)
        {
            populationPercents[i] = (float)teamsPopulation[i] / totalPopulation;
        }

        float currentPercent = 1;
        for (int i = populationPercents.Count - 1; i >= 0; i--)
        {
            uiElement.GetChild(populationPercents.Count - 1 - i ).GetComponent<Image>().fillAmount = currentPercent;
            currentPercent -= populationPercents[i];
        }
    }

}
