using UnityEngine;
using UnityEngine.UI;

// Prompt de pantalla compartido por todos los RecursosAdicionales del juego.
// Se autogenera en runtime (igual que PassportManager.EnsureEventSystem) para que
// cualquier objeto con RecursosAdicionales.cs funcione sin configuracion de escena.
public class ResourcePromptUI : MonoBehaviour
{
    public static ResourcePromptUI Instance { get; private set; }

    private GameObject promptRoot;
    private Text promptText;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnsureInstance()
    {
        if (Instance != null) return;
        var go = new GameObject("ResourcePromptUI");
        DontDestroyOnLoad(go);
        go.AddComponent<ResourcePromptUI>();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        BuildUI();
    }

    private void BuildUI()
    {
        var canvasGO = new GameObject("ResourcePromptCanvas");
        canvasGO.transform.SetParent(transform, false);
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        var textGO = new GameObject("PromptText");
        textGO.transform.SetParent(canvasGO.transform, false);
        promptText = textGO.AddComponent<Text>();
        promptText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        promptText.fontSize = 32;
        promptText.alignment = TextAnchor.MiddleCenter;
        promptText.color = Color.white;
        promptText.text = "";

        var rect = textGO.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.12f);
        rect.anchorMax = new Vector2(0.5f, 0.12f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = new Vector2(900, 60);
        rect.anchoredPosition = Vector2.zero;

        promptRoot = canvasGO;
        promptRoot.SetActive(false);
    }

    public void Show(string message)
    {
        promptText.text = message;
        promptRoot.SetActive(true);
    }

    public void Hide()
    {
        promptRoot.SetActive(false);
    }
}
