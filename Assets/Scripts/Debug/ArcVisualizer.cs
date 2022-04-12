using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;


public class Spot
{
    public Vector3 tipPosition;
    public Quaternion swingDirection;
    //this is really ugly but it's just for testing
    public GameObject indicator = null;
    public Color color;
    public Spot(Vector3 pos, Quaternion dir, Color col) => (tipPosition,swingDirection,  color) = (pos,dir, col);
}

public class ArcVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject stepPrefab;
    [SerializeField]
    private int tracingInterval = 5;
    [SerializeField]
    private float angleTolerance = 30;
    [SerializeField]
    private float velocityTolerance = 0.002f;
    [SerializeField]
    private int arcsToTrack;
    SaberController saberController;
    int timer = 0;
    SortedList<int,Spot> currentArc = new SortedList<int, Spot>();
    Queue<SortedList<int, Spot>> arcs;
    Color currentColor;
    private void Start()
    {
        saberController = gameObject.GetComponentInParent<SaberController>();
        Assert.IsNotNull(saberController);
        currentColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
        arcs = new Queue<SortedList<int, Spot>>(arcsToTrack);
    }

    private void FixedUpdate()
    {  
        if (timer == tracingInterval)
        {
            DrawArc();
            timer = 0;
        }
        else
            timer++; 
    }
    /*
     * A simple array with a shift to make it FIFO would work
     */
    private void NewArc()
    {
        //if I'm going to delete an arc, destroy all old indicators
        if(arcs.Count == arcsToTrack)
        {
            var arcToDelete = arcs.Peek();
            foreach (var spot in arcToDelete){
                GameObject.Destroy(spot.Value.indicator);
            }
        }
        currentArc = new SortedList<int, Spot>();
        currentColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
        //if the last arc doesn't have just one element (so it's empty)
        if(!(arcs.ElementAt(arcs.Count - 1).Count == 1))
        arcs.Enqueue(currentArc);

    }
    private void AddSpot(Spot spot)
    {
        //adds a new spot only if the blade is moving or it's in a different position
        if (currentArc.Count ==0 || currentArc[0].tipPosition != spot.tipPosition)
        {
            currentArc.Add(currentArc.Count, spot);
            GameObject indicator = GameObject.Instantiate(stepPrefab, spot.tipPosition, Quaternion.identity);
            indicator.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", spot.color);
            spot.indicator = indicator;
        }

    }


    private void DrawArc()
    {
        Quaternion swingDirection = Quaternion.identity;
        bool changedDirection = false;
        if (currentArc.Count > 0)
        {
            //if the blades stop, stop the arc
            if ((currentArc[currentArc.Count - 1].tipPosition - transform.position).sqrMagnitude<velocityTolerance)
            {
                swingDirection = Quaternion.identity;
                changedDirection = true;
            }
              
            //else
            //{
            //    swingDirection = Quaternion.FromToRotation(currentArc[currentArc.Count - 1].tipPosition, transform.position);
            //    swingDirection = swingDirection.normalized;
            //    if (currentArc.Count > 1)
            //    {
            //        /*
            //        //right now it's detecting changes in acceleration, not changes in direction
            //        float angleDifference = Quaternion.Angle(currentArc[currentArc.Count - 1].swingDirection, swingDirection);
            //        if (angleDifference > angleTolerance)
            //        {
            //            currentColor = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);
            //            Debug.Log(angleDifference);
            //        }
            //        */
            //        float dotProduct = Quaternion.Dot(currentArc[currentArc.Count - 1].swingDirection, swingDirection);
            //        Debug.Log(dotProduct);
            //        if (dotProduct > 1000000)
            //        {
            //            changedDirection = true;
            //        }

            //    }
            //}
            if (changedDirection)
            {
                NewArc();
            }
        }
        Spot spot = new Spot(transform.position, swingDirection, currentColor);
        AddSpot(spot);


    }
}
