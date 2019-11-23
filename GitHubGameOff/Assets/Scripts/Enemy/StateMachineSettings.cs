using UnityEngine;
using System.Collections;

/** Class simulates a singleton pattern without actually declaring it**/
public class StateMachineSettings : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2f;
    [SerializeField] private float aggroRadius = 4f;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float distanceWanderRange = 5f;
    [SerializeField] private float extraChaseDistance = .5f;
    [SerializeField] private GameObject enemyProjectilePrefab;
    public static float AggroRadius => Instance.aggroRadius;
    public static float EnemySpeed => Instance.enemySpeed;
    public static float AttackRange => Instance.attackRange;
    public static float DistanceWanderRange => Instance.distanceWanderRange;
    public static float ExtraChaseDistance => Instance.extraChaseDistance;
    public static GameObject EnemyProjectilePrefab => Instance.enemyProjectilePrefab;

    public static StateMachineSettings Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
}
