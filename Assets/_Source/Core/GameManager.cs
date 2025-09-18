using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [field: SerializeField]
    internal bool paused = false;
    [field: SerializeField]
    private float TickDuration = 0.2f;
    private UnityEvent beforeOnTick;
    private UnityEvent onTick;
    private UnityEvent afterOnTick;
    private float currentTick;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        beforeOnTick = new();
        onTick = new();
        afterOnTick = new();
    }
    void Update()
    {
        if (!paused && ResourceManager.instance.currentLives.GetValue() <= 0)
        {
            paused = true;
            return;
        }
        if (paused) return;
        currentTick += Time.deltaTime;
        if (currentTick >= TickDuration)
        {
            currentTick = 0;
            InvokeTick();
        }
    }
    void InvokeTick()
    {
        beforeOnTick.Invoke();
        onTick.Invoke();
        afterOnTick.Invoke();
    }
    public void ConnectEvent(UnityAction unityEvent, int eventType)
    {
        if (unityEvent == null) return;
        try
        {
            switch (eventType)
            {
                case 1:
                    afterOnTick.AddListener(unityEvent);
                    break;
                case -1:
                    beforeOnTick.AddListener(unityEvent);
                    break;
                default:
                    onTick.AddListener(unityEvent);
                    break;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Failed to add listener: " + e.Message);
        }
    }
    public void DissconnectEvent(UnityAction unityEvent, int eventType)
    {
        if (unityEvent == null) return;
        try
        {
            switch (eventType)
            {
                case 1:
                    afterOnTick.RemoveListener(unityEvent);
                    break;
                case -1:
                    beforeOnTick.RemoveListener(unityEvent);
                    break;
                default:
                    onTick.RemoveListener(unityEvent);
                    break;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Failed to remove listener: " + e.Message);
        }
    }
    void OnDestroy()
    {
        afterOnTick.RemoveAllListeners();
        beforeOnTick.RemoveAllListeners();
        onTick.RemoveAllListeners();
    }
}
