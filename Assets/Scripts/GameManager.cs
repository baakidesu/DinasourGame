using UnityEngine;
using Random = UnityEngine.Random;
using VContainer;
using VContainer.Unity;

public class GameManager : MonoBehaviour, IGameManager
{
    #region Public
    
        public float minSpeed;
        public float maxSpeed;
        public float timeMultiplier;
        public float currentSpeed;
        public GameObject[] spikes;
    
    #endregion

    #region Private

    private int randomNum = 0;

    #endregion
    private IObjectResolver _objectResolver;
    private Dino dino;

    [Inject]
    private void Construct(IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;
        Debug.Log("prefab factory");
    }
    private void Con(Dino d)
    {
        dino = d;
    }
    void Awake()
    {
        currentSpeed = minSpeed;
        GenerateWithGap();
    }

    public void GenerateWithGap()
    {
        float randomWait = Random.Range(0.1f, 1.2f);
        Invoke("GenerateSpike", randomWait);
    }
    
    void GenerateSpike()
    {
        randomNum = Random.Range(0, spikes.Length);
        var spawn = Lean.Pool.LeanPool.Spawn(spikes[randomNum], transform.position, transform.rotation);
        Debug.Log(spawn.name);
        Debug.Log(_objectResolver);
        _objectResolver.InjectGameObject(spawn);
        
    }

    private void Update()
    {
        if (currentSpeed < maxSpeed) 
        {
            currentSpeed += timeMultiplier;
        }
    }
    
    public float ReturnCurrentTime()
    {
        return currentSpeed;
    }
}
