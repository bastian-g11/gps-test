using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<Text> resourcesTexts; 

    private void Start() 
    {
        ResourcesController.instance.OnChangeResource += SetResourcesText;
    }

    public void SetResourcesText()
    {
        List<int> resourcesAmount = ResourcesController.instance.currentResourcesAmount;
        List<int> maxAmount = ResourcesController.instance.maxResourcesAmount;
        for (int i = 0; i < resourcesAmount.Count; i++)
        {
            resourcesTexts[i].text = resourcesAmount[i] + "/" + maxAmount[i];
        }
    }

}
