using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    [SerializeField]
    public List<PackageSegment> AllSegments;
    [SerializeField]
    public Transform[] LinePoints;
    [SerializeField]
    LineRenderer lr;

    private void Awake()
    {
        //Debug.Log(gameObject.name + " " + AllSegments.Count);
        foreach (PackageSegment segment in AllSegments)
        {
            segment.PackageParent = this;
        }
        //lr.positionCount = AllSegments.Length -1;
    }

    private void Update()
    {
        for (int i = 0; i < LinePoints.Length; i++)
        {
            lr.SetPosition(i, new Vector3(LinePoints[i].transform.position.x,
                LinePoints[i].transform.position.y, lr.GetPosition(i).z));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string text = "Segment Angles: ";
            for (int i = 0; i < AllSegments.Count - 2; i++)
            {
                text += $"{i}-{i+1}: {Mathf.Abs(Mathf.Abs(AllSegments[i].pivot.transform.eulerAngles.z - AllSegments[i + 2].pivot.transform.eulerAngles.z - 180) - 180)};";
            }

            Debug.Log(text);
        }
        //for (int i = 0; i < AllSegments.Length - 2; i++)
        //{
        //    if (Mathf.Abs(Mathf.Abs(AllSegments[i].pivot.transform.eulerAngles.z - AllSegments[i + 2].pivot.transform.eulerAngles.z - 180) - 180) > 90)
        //    {

        //    }
        //    text += $"{i}-{i + 1}: {Mathf.Abs(Mathf.Abs(AllSegments[i].pivot.transform.eulerAngles.z - AllSegments[i + 2].pivot.transform.eulerAngles.z - 180) - 180)};";
        //}
    }
    public void EnableSegments(bool enable)
    {
        //Debug.Log($"I have {AllSegments.Count} Segments");
        foreach (PackageSegment segment in AllSegments)
        {
            segment.RB.simulated = enable;
            if (!enable)
            {
                LevelManager.Instance.RemoveError(segment);
            }
        }
        //GetComponent<Rigidbody2D>().simulated = enable;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.transform.tag == "Package" /*&& !AllSegments.Contains(collision.transform.GetComponent<PackageSegment>())*/)
        //{
        //    //DO STUFF
        //    Debug.Log($"Collision with {collision.transform.name}");
        //    GameManager.Instance.Submitable = false;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"I am {name} and I collide with {collision.transform.name}");
        if (collision.transform.tag == "Package")
        {
            Debug.Log($"Collision with {collision.transform.name}");
        }
    }
}
