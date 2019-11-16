using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    public GameObject brokenParts;

    public bool onPlayerColission;

    public bool onHookColission;

    private GameObject playerObject;

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (onPlayerColission && col.collider.CompareTag("Player"))
        {
            Break();
        }
    }

    private void Break()
    {
        Destroy(gameObject);
        GameObject brokenObject = Instantiate(brokenParts, transform.position, Quaternion.identity);

        foreach (Transform child in brokenObject.transform)
        {
            child.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(5f, 10f));
        }
    }
}
