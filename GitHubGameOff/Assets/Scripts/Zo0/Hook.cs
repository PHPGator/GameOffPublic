using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hook : MonoBehaviour
{

    public GameObject anchorPoint;
    private Rigidbody2D anchorRB;
    private SpriteRenderer anchorSprite;

    public SpringJoint2D playerJoint;
    public DistanceJoint2D pullJoint;
    private Vector2 playerPosition;
    private bool isChained;
    private bool isPulled;
    private float aimMemory;

    public Transform target;
    public SpriteRenderer targetSprite;

    public LineRenderer chainRenderer;
    public LayerMask chainLayerMask; //Which layers the chain reacts to(Must add each in unity)
    public float chainLength;
    private List<Vector2> chainPositions = new List<Vector2>();
    private bool canHook;
    private float chainDistance;
    
    //physical hook
    private Dictionary<Vector2, int> wrapPointsLookup = new Dictionary<Vector2, int>();
    
    void Awake()
    {

        chainDistance = 0f;
        playerJoint.enabled = false;
        //playerJoint.frequency = 1;
        //playerJoint.dampingRatio = 0.0f;
        playerPosition = transform.position;
        anchorRB = anchorPoint.GetComponent<Rigidbody2D>();
        anchorSprite = anchorPoint.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 aimDirection = TakeAim();
        HandleInput(aimDirection);
        UpdateHook();
        HandleRopeUnwrap();
        //playerJoint.frequency = 0f + chainDistance/chainLength;
        //playerJoint.dampingRatio = 0.1f;
        //var PosTest = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 6f));
        //var tempPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        //var TestPos = GetComponent<Transform>().position;
        //print(PosTest + "Is made from: " + tempPos + " Compare " + TestPos);
    }

    private void SetTargetPosition(float aimAngle, float distance)
    {
        if (!targetSprite.enabled)
        {
            targetSprite.enabled = true;
        }

        var x = transform.position.x + distance * Mathf.Cos(aimAngle);
        var y = transform.position.y + distance * Mathf.Sin(aimAngle);

        var targetPosition = new Vector3(x, y, 10f);
        target.transform.position = targetPosition;
    }

    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetMouseButton(0))
        {
            
            if (isChained) return;
            chainRenderer.enabled = true;

            var hit = Physics2D.Raycast(playerPosition, aimDirection, chainLength, chainLayerMask);
            

            if (hit.collider != null)
            {
                isChained = true;
                if (!chainPositions.Contains(hit.point))
                {

                    chainPositions.Add(hit.point);
                    chainDistance = Vector2.Distance(playerPosition, hit.point);

                    //grappling hook force
                    Vector2 redline = hit.point - playerPosition;
                    aimMemory = Mathf.Atan2(redline.y, redline.x);

                    if (hit.transform.gameObject.tag == "Hooked")
                    {

                        pullJoint.connectedBody = hit.rigidbody;
                        pullJoint.distance = chainDistance;
                        pullJoint.enabled = true;
                        isPulled = true;
                    }
                    else
                    {

                        playerJoint.distance = chainDistance;
                        playerJoint.enabled = true;
                        anchorSprite.enabled = true;
                    }



                }
            }

            else
            {
                isPulled = false;
                chainRenderer.enabled = false;
                isChained = false;
                playerJoint.enabled = false;
                pullJoint.enabled = false;
                pullJoint.connectedBody = null;
            }
        }else if (!Input.GetMouseButton(0))
        {
            
            ResetHook();
        }
    }

    private Vector2 TakeAim()
    {

        //Aim with mouse
        var worldMousePosition =
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        playerPosition = transform.position;

        if (!isChained)
        {
            var reddot = Physics2D.Raycast(playerPosition, aimDirection, chainLength, chainLayerMask);
            var updatedDistance = Vector2.Distance (reddot.point, playerPosition);
            SetTargetPosition(aimAngle, updatedDistance);
        }
        else
        {
            targetSprite.enabled = false;

            //hook feels a bit more realistic
            
            if (chainPositions.Count > 0)
            {
                var lastRopePoint = chainPositions.Last();
                var playerToCurrentNextHit = Physics2D.Raycast(playerPosition, (lastRopePoint - playerPosition).normalized, Vector2.Distance(playerPosition, lastRopePoint) - 0.1f, chainLayerMask);
                
                if (playerToCurrentNextHit)
                {
                    var colliderWithVertices = playerToCurrentNextHit.collider as PolygonCollider2D;
                    if (colliderWithVertices != null)
                    {
                        var closestPointToHit = GetClosestColliderPointFromRaycastHit(playerToCurrentNextHit, colliderWithVertices);
                        
                        if (wrapPointsLookup.ContainsKey(closestPointToHit))
                        {
                            ResetHook();
                        }
                       
                        chainPositions.Add(closestPointToHit);
                        wrapPointsLookup.Add(closestPointToHit, 0);
                        canHook = false;
                    }
                }
            }
            
        }

        return aimDirection;
    }

    private void UpdateHook()
    {
        //return if nothing connects
        if (!isChained)
        {
            return;
        }

        //scorpion pull
        if (isPulled && pullJoint.distance > 1)
        {

            pullJoint.distance *= 0.9f;
        }

        //hook grappling force
        //turned off for now
        if (playerJoint.distance > 1f && false)
        {

            //up
            if (aimMemory * Mathf.Rad2Deg > 25 && aimMemory * Mathf.Rad2Deg < 155)
            {
                playerJoint.distance *= 0.999f;
            }
            //down
            else if (aimMemory * Mathf.Rad2Deg > 225 && aimMemory * Mathf.Rad2Deg < 315)
            {

                playerJoint.distance *= 0.5f;
            }
            //sideways
            else
            {

                playerJoint.distance *= 0.99f;
                //anchorPoint.transform.parent.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1f), ForceMode2D.Impulse);
            }
        }

        //find the correct position
        if (!isPulled)
        {

            playerJoint.frequency = chainDistance/chainLength;
            print(playerJoint.frequency = chainDistance / chainLength);

            chainRenderer.positionCount = chainPositions.Count + 1;

            for (var i = chainRenderer.positionCount - 1; i >= 0; i--)
            {
                if (i != chainRenderer.positionCount - 1)
                {
                    chainRenderer.SetPosition(i, chainPositions[i]);

                    //set anchor
                    if (i == chainPositions.Count - 1 || chainPositions.Count == 1)
                    {
                        var tempChainPosition = chainPositions[chainPositions.Count - 1];
                        if (chainPositions.Count == 1)
                        {
                            anchorRB.transform.position = tempChainPosition;
                            if (!canHook)
                            {
                                playerJoint.distance = Vector2.Distance(transform.position, tempChainPosition);
                                canHook = true;
                            }
                        }
                        else
                        {
                            anchorRB.transform.position = tempChainPosition;
                            if (!canHook)
                            {
                                playerJoint.distance = Vector2.Distance(transform.position, tempChainPosition);
                                canHook = true;
                            }
                        }
                    }
                    // fixes some strange behaviours
                    else if (i - 1 == chainPositions.IndexOf(chainPositions.Last()))
                    {
                        var tempChainPosition = chainPositions.Last();
                        anchorRB.transform.position = tempChainPosition;
                        if (!canHook)
                        {
                            playerJoint.distance = Vector2.Distance(transform.position, tempChainPosition);
                            canHook = true;
                        }
                    }
                }
                else
                {
                    // yay
                    chainRenderer.SetPosition(i, transform.position);
                }
            }
        }
        else
        {

        }
    }

    private void HandleRopeUnwrap()
    {

        if (chainPositions.Count <= 1)
        {
            return;
        }

        var anchorIndex = chainPositions.Count - 2;
        var hingeIndex = chainPositions.Count - 1;
        var anchorPosition = chainPositions[anchorIndex];
        var hingePosition = chainPositions[hingeIndex];
        var hingeDir = hingePosition - anchorPosition;
        var hingeAngle = Vector2.Angle(anchorPosition, hingeDir);
        var playerDir = playerPosition - anchorPosition;
        var playerAngle = Vector2.Angle(anchorPosition, playerDir);
        
        if (!wrapPointsLookup.ContainsKey(hingePosition))
        {
            Debug.LogError("We were not tracking hingePosition (" + hingePosition + ") in the look up dictionary.");
            return;
        }

        if (playerAngle < hingeAngle)
        {
            if (wrapPointsLookup[hingePosition] == 1)
            {
                UnwrapRopePosition(anchorIndex, hingeIndex);
                return;
            }

            wrapPointsLookup[hingePosition] = -1;
        }
        else
        {
            if (wrapPointsLookup[hingePosition] == -1)
            {
                UnwrapRopePosition(anchorIndex, hingeIndex);
                return;
            }
            
            wrapPointsLookup[hingePosition] = 1;
        }
    }

    private void UnwrapRopePosition(int anchorIndex, int hingeIndex)
    {

        var newAnchorPosition = chainPositions[anchorIndex];
        wrapPointsLookup.Remove(chainPositions[hingeIndex]);
        chainPositions.RemoveAt(hingeIndex);
        
        anchorRB.transform.position = newAnchorPosition;
        canHook = false;

        // Set new rope distance joint distance for anchor position if not yet set.
        if (canHook)
        {
            return;
        }
        playerJoint.distance = Vector2.Distance(transform.position, newAnchorPosition);
        canHook = true;
    }

    private void ResetHook()
    {
        playerJoint.enabled = false;
        pullJoint.enabled = false;
        pullJoint.connectedBody = null;
        isPulled = false;
        isChained = false;
        chainRenderer.positionCount = 2;
        chainRenderer.SetPosition(0, transform.position);
        chainRenderer.SetPosition(1, transform.position);
        chainPositions.Clear();
        anchorSprite.enabled = false;
        wrapPointsLookup.Clear();
    }

    private Vector2 GetClosestColliderPointFromRaycastHit(RaycastHit2D hit, PolygonCollider2D polyCollider)
    {

        var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
            position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)),
            position => polyCollider.transform.TransformPoint(position));
        
        var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);
        return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
    }
}
