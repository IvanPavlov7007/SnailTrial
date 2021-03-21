using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField]
    float cellSide;
    [SerializeField]
    [Range(0f, 1f)]
    float boundingBoxPercentage;
    [SerializeField]
    float diameterRandomSphere;
    [SerializeField]
    GameObject[] objectsPrefab;
    [SerializeField]
    int[] prefabsProbabilities;
    Dictionary<Vector2, GameObject> createdObjects;
    int prefabsCount, prefabsProbabilitiesSum = 0;

    [SerializeField]
    private Vector2 generatedWorldArea;

    public static EnvironmentSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    public void RemoveElement(Vector2 key)
    {
        createdObjects.Remove(key);
    }

    void Start()
    {
        createdObjects = new Dictionary<Vector2, GameObject>();
        prefabsCount = objectsPrefab.Length;
        foreach(var weight in prefabsProbabilities)
        {
            prefabsProbabilitiesSum += weight;
        }
        //Starting Planet
        createdObjects.Add(Vector2.zero,
                            Instantiate(objectsPrefab[0],
                            Vector2.zero, Quaternion.identity));
        StartCoroutine(CreationCycle());
    }

    Rect lastArea,boundingBox;
    IEnumerator CreationCycle()
    {
        Vector2 size = generatedWorldArea;
        Vector2 boundingBoxSize = size * boundingBoxPercentage;
        Vector2 lastPos = transform.position;
        lastArea = new Rect(lastPos - size / 2f, size);
        CreateObjectsInARect(lastArea);
        while (true)
        {
            boundingBox = new Rect(lastPos - boundingBoxSize / 2f, boundingBoxSize);
            yield return new WaitUntil(CenterGotOutOfTheBoundingBox);
            Rect updatedArea = new Rect(lastArea);
            Vector2 curPos = (Vector2)transform.position;
            updatedArea.position += curPos - lastPos;
            foreach (var rect in OwnRects(updatedArea, lastArea))
                CreateObjectsInARect(rect);
            foreach (var rect in OwnRects(lastArea, updatedArea))
                DestroyObjectsInARect(rect);
            lastPos = curPos;
            lastArea = updatedArea;
        }
    }

    bool CenterGotOutOfTheBoundingBox()
    {
        return !boundingBox.Contains(transform.position);
    }

    //Works when both rects have same sizes
    Rect[] OwnRects(Rect self, Rect other)
    {
        Rect[] result = new Rect[2];
        if (self.xMin < other.xMin)
        {
            result[0] = new Rect(self.xMin, self.yMin, other.xMin - self.xMin, self.height);
            if (self.yMin < other.yMin)
            {
                result[1] = new Rect(other.xMin, self.yMin, self.xMax - other.xMin, other.yMin - self.yMin);
            }
            else if (self.yMin > other.yMin)
            {
                result[1] = new Rect(other.xMin, other.yMax, self.xMax - other.xMin, self.yMax - other.yMax);
            }
            else
                result[1] = default;
        }
        else if (self.xMin > other.xMin)
        {
            result[0] = new Rect(other.xMax, self.yMin, self.xMax - other.xMax, self.height);
            if (self.yMin < other.yMin)
            {
                result[1] = new Rect(self.xMin, self.yMin, other.xMax - self.xMin, other.yMin - self.yMin);
            }
            else if (self.yMin > other.yMin)
            {
                result[1] = new Rect(self.xMin, other.yMax, other.xMax - self.xMin, self.yMax - other.yMax);
            }
            else
                result[1] = default;
        }
        else
        {
            if(self.yMin < other.yMin)
            {
                result[0] = new Rect(self.xMin, self.yMin, self.width, other.yMin - self.yMin);
            }
            else if(self.yMin > other.yMin)
            {
                result[0] = new Rect(self.xMin, other.yMax, self.width, self.yMax - other.yMax);
            }
            else
                result[0] = default;
            result[1] = default;
        }
        return result;
    }

    void CreateObjectsInARect(Rect rect)
    {
        if (rect == default)
            return;
        int xMin = Mathf.CeilToInt(rect.xMin / cellSide);
        int yMin = Mathf.CeilToInt(rect.yMin / cellSide);
        int xMax = Mathf.FloorToInt(rect.xMax / cellSide);
        int yMax = Mathf.FloorToInt(rect.yMax / cellSide);
        for(int x = xMin; x <= xMax; x++)
        {
            for (int y = yMin; y <= yMax; y++)
            {
                Vector2 pos = new Vector2(x * cellSide, y * cellSide);
                if (!createdObjects.ContainsKey(pos))
                {
                    var prefab = objectsPrefab[CommonTools.RandomIndex(prefabsProbabilities, prefabsProbabilitiesSum)];
                    GameObject envObject = Instantiate(prefab, 
                                                    pos + (Vector2)Random.insideUnitSphere * diameterRandomSphere, Quaternion.identity);
                    createdObjects.Add(pos, envObject);
                }
            }
        }
    }

    void DestroyObjectsInARect(Rect rect)
    {
        if (rect == default)
            return;
        int xMin = Mathf.CeilToInt(rect.xMin / cellSide);
        int yMin = Mathf.CeilToInt(rect.yMin / cellSide);
        int xMax = Mathf.FloorToInt(rect.xMax / cellSide);
        int yMax = Mathf.FloorToInt(rect.yMax / cellSide);
        for (int x = xMin; x <= xMax; x++)
        {
            for (int y = yMin; y <= yMax; y++)
            {
                Vector2 pos = new Vector2(x * cellSide, y * cellSide);
                GameObject envObject;
                if(createdObjects.TryGetValue(pos, out envObject))
                {
                    createdObjects.Remove(pos);
                    Destroy(envObject);
                }
            }
        }
    }
}
