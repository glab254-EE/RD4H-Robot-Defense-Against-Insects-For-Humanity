using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [field: SerializeField]
    private bool paused = false;
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
}
