using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 forward;
    private Vector3 worldPosition;

    [SerializeField]
    GameObject drawpoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);


        //var mousePos = Input.mousePosition;
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, forward);
        //RaycastHit2D hit = Physics2D.Raycast(new Vector3(mousePos.x, mousePos.y, gameObject.transform.position.z), Camera.main.ScreenToWorldPoint(mousePos));
        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.gameObject.transform.position, Camera.main.ScreenToWorldPoint(mousePos));

        Debug.Log($"Position: {worldPosition}, Direction: {forward}");
        //Debug.Log($"Position: {new Vector3(mousePos.x, mousePos.y, gameObject.transform.position.z)}, Direction: {Camera.main.ScreenToWorldPoint(mousePos)}");
        if (hit.collider != null)
        {
            Instantiate(drawpoint, hit.point, Quaternion.identity);
            Debug.Log("I hit " + hit.collider.gameObject.name);
            //Debug.Log($"Position: {Camera.main.gameObject.transform.position}, Direction: {Camera.main.ScreenToWorldPoint(mousePos)}");
        }
        else
        {
            Debug.Log("I hit nothing :(");
        }


        if (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            return;
        }
        //mousePos.z = 1000;
        //Vector3 pointPos = Camera.main.ScreenToWorldPoint(mousePos);
        //RaycastHit hit;
        //if (!Physics.Raycast(transform.position, pointPos, out hit, Mathf.Infinity))
        //{
        //    Debug.Log("Ray didnt hit");
        //    return;
        //}
        //Debug.Log("Raycast hit!!");
        //if (hit.transform.tag == "Paintable")
        //{
        //    Debug.Log("I am painting");
        //    return;
        //}
    }
}
