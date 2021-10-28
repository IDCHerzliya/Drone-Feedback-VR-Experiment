using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{

    public Transform droneRoot;
    public Transform targetPosition;
    [SerializeField]
    private float approachTime = 10f;


    public List<GameObject> propellers;

    public void Start()
    {
        StartCoroutine(Movement());
    }

    private void Update()
    {
        if (propellers.Count != 0)
        {
            foreach (GameObject propeller in propellers) { propeller.transform.Rotate(0, 15, 0); }
        }   
    }

    private IEnumerator Movement()
    {
        
        var initPos = droneRoot.position;
        var endPos = targetPosition.position;

        yield return new WaitForSecondsRealtime(1f);

        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        //Debug.Log("Staring Movement");

        float deltat = 0;
        while(deltat < approachTime)
        {
            yield return new WaitForEndOfFrame();
            deltat += Time.unscaledDeltaTime / 2;
            //descale time
            //Debug.Log(deltat);
            //Debug.Log(watch.Elapsed);
            droneRoot.position = Vector3.Lerp(initPos, endPos, deltat / approachTime);
        }
        watch.Stop();
        Debug.Log(watch.Elapsed);
        yield return new WaitForEndOfFrame();

        yield return new WaitForSecondsRealtime(.6f);
        yield return new WaitForSecondsRealtime(10f);
        //End of experiment stuff goes here
        yield return null;
    }

}
