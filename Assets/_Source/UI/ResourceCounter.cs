using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ResourceCounter : MonoBehaviour
{
    private TMP_Text textLabel;
    private ResourceManager resourceManagerReference;
    void Start()
    {
        resourceManagerReference = ResourceManager.instance;
        textLabel = GetComponent<TMP_Text>();
    }
    void LateUpdate()
    {
        if (resourceManagerReference == null)
        {
            resourceManagerReference = ResourceManager.instance;
            return;
        }
        textLabel.text = resourceManagerReference.Resource.ToString() + " Resource";
    }
}
