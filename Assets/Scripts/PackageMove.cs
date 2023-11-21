using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageMove : MonoBehaviour
{
    private PackageSegment cSeg => GameManager.Instance.CurrentSegment;
    Vector2 grabOffset;
    bool grabbingObj, checkValid;


    void Start()
    {

    }

    void Update()
    {
        RotateInput();
        MoveInput();
        SubmitInput();
    }

    private void MoveInput()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            LevelManager.Instance.PlaySound(ESound.DROP);
            grabbingObj = false;
            EnableCollision(true);
            //CheckValidation();
        }
        if (grabbingObj)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos = new Vector3(mousePos.x + grabOffset.x, mousePos.y + grabOffset.y, GameManager.Instance.CurrentPackage.transform.position.z);
            GameManager.Instance.CurrentPackage.transform.position = mousePos;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward);
            if (hit.transform != null && hit.transform.tag == "Package")
            {
                LevelManager.Instance.PlaySound(ESound.PICKUP);
                SelectPackage(hit.transform.GetComponent<PackageSegment>());
                EnableCollision(false);
                grabbingObj = true;
                grabOffset = new Vector2(GameManager.Instance.CurrentPackage.transform.position.x - hit.point.x, GameManager.Instance.CurrentPackage.transform.position.y - hit.point.y);
            }
        }
    }

    private void SelectPackage(PackageSegment newPackage)
    {
        GameManager.Instance.CurrentSegment = newPackage;
        GameManager.Instance.CurrentPackage = newPackage.PackageParent;

    }

    public void RotateInput()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            EnableCollision(false);
            LevelManager.Instance.PlaySound(ESound.ROTATE);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (cSeg.AmBendable && cSeg.pivot.transform.localEulerAngles.z > 90 && cSeg.pivot.transform.localEulerAngles.z < 260)
            {
                return;
            }
            cSeg.RotatePackage(true);
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (cSeg.AmBendable && cSeg.pivot.transform.localEulerAngles.z > 100 && cSeg.pivot.transform.localEulerAngles.z < 270)
            {
                return;
            }
            cSeg.RotatePackage(false);
        }
        if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Q))
        {
            //CheckValidation();
            LevelManager.Instance.PlaySound(ESound.STOPROTATE);
            EnableCollision(true);
        }
    }

    private IEnumerator CheckValidation()
    {
        GameManager.Instance.CurrentPackage.EnableSegments(false);
        GameManager.Instance.Submitable = true;
        checkValid = false;
        yield return new WaitForFixedUpdate();
        GameManager.Instance.CurrentPackage.EnableSegments(true);
        yield return new WaitForFixedUpdate();
        checkValid = true;
    }

    private void EnableCollision(bool enable)
    {
        GameManager.Instance.CurrentPackage.EnableSegments(false);
        if (enable)
        {
            StartCoroutine(CheckValidation());
        }
    }

    private void SubmitInput()
    {
        //   LevelManager.Instance.EnableButton();

        if (Input.GetKeyDown(KeyCode.Return))
        {

            if (LevelManager.Instance.ErrorPackages.Count >0)
            {
                LevelManager.Instance.PlaySound(ESound.DENY);
                LevelManager.Instance.ShowErrors();
            }
            else
            {
                LevelManager.Instance.PlaySound(ESound.SUCCESS);
                LevelManager.Instance.EnableButton();
            }
            //if (!checkValid)
            //{
            //    LevelManager.Instance.PlaySound(ESound.DENY);
            //    LevelManager.Instance.ShowErrors();
            //    Debug.Log("Wait for verification");
            //    return;
            //}
            //else if (!GameManager.Instance.Submitable)
            //{
            //    LevelManager.Instance.PlaySound(ESound.DENY);
            //    LevelManager.Instance.ShowErrors();
            //    Debug.Log("Package does not fit");
            //    return;
            //}
            //else
            //{
            //    if (LevelManager.Instance != null)
            //    {
            //        LevelManager.Instance.PlaySound(ESound.SUCCESS);
            //        LevelManager.Instance.EnableButton();
            //    }
            //    else
            //    {
            //        Debug.Log("YOU WIN!!");
            //    }
            //    //TODO: Load next Level
            //}
        }
    }
}
