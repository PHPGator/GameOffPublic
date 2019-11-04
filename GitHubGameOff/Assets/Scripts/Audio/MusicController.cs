using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController _instance;

    public static MusicController Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
