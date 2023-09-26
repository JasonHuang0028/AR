using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class ShowTextOnRecognize : MonoBehaviour
{
    //public TextMesh textMesh;
    public TextMeshPro textMeshPro;


    private CloudRecoBehaviour cloudRecoBehaviour;
    private Dictionary<string, string> imageToText = new Dictionary<string, string>()
    {
        {"Pan", "Step 1: Firstly add water to the pan, then and place the pan on the hob and heat it until the water comes to a boil."},
        {"Pasta", "Step 2: Add the pasta to the water and cook for 10 minutes, then drain."},
        {"Oil", "Step 3: Turn on the heat on the hob and add oil to the pan on the hob."},
        {"Tomato", "Step 4: Dice the tomatoes and add them to the pan for a stir fry"},
        {"SeaSalt", "Step 5: Add sea salt to the pan and continue to simmer the tomato sauce for 2 minutes, then add the cooked pasta to the pan and stir well, then transfer to a serving dish."},
        {"BlackPepper", "Step 6: A sprinkle of black pepper over the pasta on the plate completes the classic tomato pasta."},
        // You can continue to add more image names and corresponding text.
    };

    void Start()
    {
        cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        if (cloudRecoBehaviour)
        {
            cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
            cloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
            //cloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
            cloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
            cloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
            cloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
        }

        if (textMeshPro)
        {
            textMeshPro.gameObject.SetActive(false); // Default hidden text
        }
    }

    public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        // This method is called at initialisation time, but we don't need to do anything here
        Debug.Log("Cloud Reco initialized");
    }

    public void OnStateChanged(bool scanning)
    {
        if (scanning)
        {
            // If scanning starts, hide the text
            textMeshPro.gameObject.SetActive(false);
            Debug.Log("Scanning started");
        }
    }

    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {
        Debug.Log("Recognized Target: " + cloudRecoSearchResult.TargetName);
        if (imageToText.ContainsKey(cloudRecoSearchResult.TargetName))
        {
            textMeshPro.text = imageToText[cloudRecoSearchResult.TargetName];
            textMeshPro.gameObject.SetActive(true); // Show text
            Debug.Log("Setting text to: " + textMeshPro.text);
        }
        else
        {
            Debug.LogWarning(cloudRecoSearchResult.TargetName + " not found in the dictionary");
        }
    }

    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        // This is where any errors related to cloud recognition updates can be handled
        Debug.LogError("Cloud Reco update error: " + updateError);
    }
}
