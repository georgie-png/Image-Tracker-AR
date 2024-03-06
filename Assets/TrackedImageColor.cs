using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation; // import the XR libraries

public class TrackedImageColor : MonoBehaviour
{

    // add a field to the unity ui where you can add the 'XR Tracked Image Manager'
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;

    // add a field to the unity ui where you can write the 1st images tag
    [SerializeField]
    private string trackTag1;

    // add a field to the unity ui where you can write the 2nd images tag
    [SerializeField]
    private string trackTag2;

    // add a field to the unity ui where you can write the 1st image's object color
    [SerializeField]
    private Color trackCol1;

    // add a field to the unity ui where you can write the 2nd image's object color
    [SerializeField]
    private Color trackCol2;

// setup callbacks
    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

// function called whenevr there is a change in the scene.
    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // iterate over event changes, getting a singke images change per step.
        foreach (var newImage in eventArgs.added)
        {
			Debug.Log(newImage.referenceImage.name);
            if (newImage.referenceImage.name== trackTag1){ // if name of image matches tag 1
                
                // Get the Renderer component from the new cube on the tracked image
                var trackedObjRenderer = newImage.gameObject.GetComponent<Renderer>();
                
                //Set color to track col 1
                trackedObjRenderer.material.SetColor("_Color", trackCol1);

            }
            else if (newImage.referenceImage.name== trackTag2){ // else if name of image matches tag 2
                
                // Get the Renderer component from the new cube on the tracked image
                var trackedObjRenderer = newImage.gameObject.GetComponent<Renderer>();
                
                //Set color to track col 2
                trackedObjRenderer.material.SetColor("_Color", trackCol2);
            }
        }

        // itterate over the tracked images leaving the scene
        foreach (var removedImage in eventArgs.removed)
        {
            // remove those gameobjects 
            Destroy(removedImage.gameObject);
        }
    }

}
