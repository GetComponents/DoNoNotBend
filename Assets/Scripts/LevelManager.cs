using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField]
    GameObject[] Levels;
    int levelIndex;
    GameObject currentLevel;
    [SerializeField]
    GameObject nextLevelButton;
    [SerializeField]
    AudioClip ADeny, ASuccess, ARotate, APickup, ADrop;
    [SerializeField]
    AudioSource RotateSound;
    [SerializeField]
    float glowTime;
    [SerializeField]
    Color ErrorColor;
    public List<PackageSegment> ErrorPackages;
    [SerializeField]
    int[] Restrictions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        currentLevel = Instantiate(Levels[levelIndex]);
    }

    public void NextLevel()
    {
        if (levelIndex < Levels.Length - 1)
        {
            nextLevelButton.SetActive(false);
            Destroy(currentLevel);
            levelIndex++;
            ErrorPackages = new List<PackageSegment>();
            currentLevel = Instantiate(Levels[levelIndex]);
        }
        else
        {
            SceneManager.LoadScene("Credits");
        }
    }

    public void AddError(PackageSegment error)
    {
        if (!ErrorPackages.Contains(error))
        {
            ErrorPackages.Add(error);
        }
        //StartCoroutine(Glow(error));
        //int random = Random.Range(0, 2);
        //switch (random)
        //{
        //    case 0:
        //        error.myRenderer.material.color = ErrorColor;
        //        //error.myRenderer.color = ErrorColor;
        //        break;
        //    case 1:
        //        error.myRenderer.material.color = Color.green;
        //        //error.myRenderer.color = Color.green;
        //        break;
        //    default:
        //        break;
        //}
        //StartCoroutine(Glow(error));
    }

    public void ShowErrors()
    {
        foreach (PackageSegment segment in ErrorPackages)
        {
            StartCoroutine(Glow(segment));
        }
    }

    public void RemoveError(PackageSegment error)
    {
        if (ErrorPackages.Contains(error))
        {
            ErrorPackages.Remove(error);
        }
    }

    IEnumerator Glow(PackageSegment error)
    {
        error.myRenderer.color = ErrorColor;
        yield return new WaitForSeconds(glowTime);
        error.myRenderer.color = Color.white;
    }

    public void ReloadLevel()
    {
        ErrorPackages = new List<PackageSegment>();
        Destroy(currentLevel);
        currentLevel = Instantiate(Levels[levelIndex]);
    }

    public void PlaySound(ESound sound)
    {
        switch (sound)
        {
            case ESound.NONE:
                break;
            case ESound.DENY:
                AudioSource.PlayClipAtPoint(ADeny, transform.position);
                break;
            case ESound.SUCCESS:
                AudioSource.PlayClipAtPoint(ASuccess, transform.position);
                break;
            case ESound.PICKUP:
                AudioSource.PlayClipAtPoint(APickup, transform.position);
                break;
            case ESound.DROP:
                AudioSource.PlayClipAtPoint(ADrop, transform.position);
                break;
            case ESound.ROTATE:
                //AudioSource.PlayClipAtPoint(ARotate, transform.position);
                RotateSound.Play();
                break;
            case ESound.STOPROTATE:
                RotateSound.Stop();
                break;
            default:
                break;
        }
    }

    public void EnableButton()
    {
        nextLevelButton.SetActive(true);
    }
}

public enum ESound
{
    NONE,
    DENY,
    SUCCESS,
    PICKUP,
    DROP,
    ROTATE,
    STOPROTATE

}
