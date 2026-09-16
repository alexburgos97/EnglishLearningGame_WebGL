using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RecursosAdicionales : MonoBehaviour
{
    public string resourceURL;
    public string resourceName;

    private bool playerInRange = false;

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
        ResourcePromptUI.Instance.Show($"Press E to open resource: {resourceName}");
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        ResourcePromptUI.Instance.Hide();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
            OpenResource();
    }

    public void OpenResource()
    {
        Debug.Log("Opening: " + resourceName);
        Application.OpenURL(resourceURL);
        playerInRange = false;
        ResourcePromptUI.Instance.Hide();
    }
}
