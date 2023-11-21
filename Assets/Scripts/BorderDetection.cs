using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Package")
        {
            GameManager.Instance.Submitable = false;
            LevelManager.Instance.AddError(collision.transform.GetComponent<PackageSegment>());
        }
    }
}
