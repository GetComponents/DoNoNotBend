using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSegment : MonoBehaviour
{
    public SpriteRenderer myRenderer;
    [SerializeField]
    Color defaultColor, selectedColor;
    [SerializeField]
    Vector3 rotationSpeed;
    [SerializeField]
    public GameObject pivot, pivot2;
    [SerializeField]
    LineRenderer lr;
    public bool AmBendable;
    [SerializeField]
    int myLRPos;
    [HideInInspector]
    public Rigidbody2D RB;
    [HideInInspector]
    public Package PackageParent;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        defaultColor = myRenderer.color;
    }

    private void Update()
    {
        //if (myLRPos != -1)
        //{
        //    lr.SetPosition(myLRPos, new Vector3(pivot2.transform.position.x,
        //        pivot2.transform.position.y, lr.GetPosition(myLRPos).z));
        //}
    }

    public void ChangeColor(EPackageColor color)
    {
        switch (color)
        {
            case EPackageColor.NONE:
                break;
            case EPackageColor.DEFAULT:
                Debug.Log("Default");
                myRenderer.color = defaultColor;
                break;
            case EPackageColor.SELECTED:
                Debug.Log("Selected");
                myRenderer.color = selectedColor;
                break;
            default:
                break;
        }
    }

    public void ChangeColor(Color color)
    {
        myRenderer.color = color;
    }

    public void RotatePackage(bool left)
    {
        float rotationMul;
        if (left)
        {
            rotationMul = -1;
        }
        else
        {
            rotationMul = 1;
        }
        if (pivot == null)
        {
            Debug.Log("PIVOT NOT ATTACHED");
            transform.eulerAngles -= rotationSpeed * rotationMul * Time.deltaTime;
        }
        else
        {
            pivot.transform.eulerAngles -= rotationSpeed * rotationMul * Time.deltaTime;
            //if (myLRPos != -1)
            //{
            //    lr.SetPosition(myLRPos, new Vector3(pivot2.transform.position.x - GameManager.Instance.CurrentPackage.transform.position.x,
            //        pivot2.transform.position.y - GameManager.Instance.CurrentPackage.transform.position.y, lr.GetPosition(myLRPos).z));
            //}
            //lr.SetPosition(myLRPos, new Vector3(lr.GetPosition(myLRPos).x, lr.GetPosition(myLRPos).y + (rotationMul * Time.deltaTime), lr.GetPosition(myLRPos).z));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Package" && !GameManager.Instance.CurrentPackage.AllSegments.Contains(collision.transform.GetComponent<PackageSegment>()))
        {
            LevelManager.Instance.AddError(collision.transform.GetComponent<PackageSegment>());
            //DO STUFF
            //Debug.Log($"I am {name} and I collide with {collision.transform.name}");
            //Debug.Log($"Collision with {collision.transform.name}");
            GameManager.Instance.Submitable = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Package" && !GameManager.Instance.CurrentPackage.AllSegments.Contains(collision.transform.GetComponent<PackageSegment>()))
        {
            LevelManager.Instance.RemoveError(collision.transform.GetComponent<PackageSegment>());
        }
    }
}

public enum EPackageColor
{
    NONE,
    DEFAULT,
    SELECTED
}
