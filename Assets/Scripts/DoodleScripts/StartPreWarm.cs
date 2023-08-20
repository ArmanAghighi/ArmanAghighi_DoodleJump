using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPreWarm : MonoBehaviour
{
    private int distanceBetweenEachPoint = 1;
    public GameObject platformPrefab;
    int screenWidth = Screen.width;
    int screenHeight = Screen.height;
    const float pi = Mathf.PI;
    private void Start()
    {
        preWarm();
    }
    void preWarm()
    {
        GameObject NormalPlatformParent = GameObject.Find("NormalPlatformParent");
        for (int i = 0; i < findTheBestNumberForInstantiation(); i++)
        {
            float newXForPreWarm = UnityEngine.Random.Range(-converScreenToWorld().x + 1, converScreenToWorld().x - 1);
            float newYForPreWarm = UnityEngine.Random.Range(-converScreenToWorld().y + 1, converScreenToWorld().y - 1);
            if (!isPrefabInCircle(distanceBetweenEachPoint, newXForPreWarm, newYForPreWarm))
            {
                GameObject NormalPlatform = Instantiate(platformPrefab, new Vector3(newXForPreWarm, newYForPreWarm, 0), gameObject.transform.rotation);
                NormalPlatform.transform.SetParent(GameObject.Find("NormalPlatformParent").transform);
            }
            else
            {
                i--;
                continue;
            }
        }
    }
    Vector2 converScreenToWorld()
    {
        Camera mainCamera = Camera.main;
        Vector2 screenPosition = new Vector2(Screen.width, Screen.height);
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    bool isPrefabInCircle(int radius , float x , float y)
    {
        float area = radius * radius * pi;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(x,y), new Vector2(radius,radius), 0f);
        string tagToCheck = "normalPlatform";

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(tagToCheck))
            {
                return true;
            }
        }
        return false;
    }
    int findTheBestNumberForInstantiation()
    {  
        Camera mainCamera = Camera.main;
        Vector2 screenPosition = new Vector2(Screen.width, Screen.height);
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        int numberOfPlatformToBeInstantiate = ((int)worldPosition.x + (int)worldPosition.y);
        return numberOfPlatformToBeInstantiate + 3;
    }
}