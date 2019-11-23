using UnityEngine;

public class DamagePlayerOnTouch : MonoBehaviour
{
    private GameObject playerObject;
    public float damage;

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            playerObject.GetComponent<PlayerHealth>().decreaseHealth(damage);
        }
    }
}