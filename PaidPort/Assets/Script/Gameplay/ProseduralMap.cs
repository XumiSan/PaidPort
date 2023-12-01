using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProseduralMap : MonoBehaviour
{
    public Transform groundPrefab;

    public Transform bronzePrefab;

    public Transform silverPrefab;

    public Transform goldPrefab;

    public Transform diamondPrefab;

    public Transform groundCollider;
    [SerializeField]
    private Vector2 spawnPosition = new Vector2(0, 0);
    [SerializeField]
    private int groundWidth = 10;
    [SerializeField]
    private int groundHeight = 10;
    private float bronzeChance = 0.03f;
    private float silverChance = 0.03f;
    private float goldChance = 0.02f;
    private float diamondChance = 0.02f;
    [SerializeField]
    private int maxDepth = 50;

    private int currentDepth = 0;

    public Transform groundContainer;

    public bool isLoad;


    public GroundState state;


    public GroundState state1;

    private void Awake()
    {
    }

    void Start()
    {

        GenerateInitialGround();
    }

    void Update()
    {

        if (Camera.main.transform.position.y > spawnPosition.y - (groundHeight * groundCollider.localScale.y) && currentDepth < maxDepth)
        {
            GenerateGround();
        }

    }

    void GenerateInitialGround()
    {
        int currentLayerDepth = currentDepth;
        for (int j = 0; j < groundHeight; j++)
        {
            for (int i = 0; i < groundWidth; i++)
            {
                Vector3 spawnPos = new Vector3(spawnPosition.x + i * groundCollider.localScale.x, spawnPosition.y - j * groundCollider.localScale.y, 0);
                float randomValue = Random.value;

                if (currentLayerDepth >= -2)
                {
                    Instantiate(groundPrefab, spawnPos, Quaternion.identity, groundContainer);
                }
            }

            currentLayerDepth = -currentDepth;
        }

        currentDepth += groundHeight;
    }

    void GenerateGround()
    {

        for (int i = 0; i < groundWidth; i++)
        {
            Transform currentGround = groundContainer;
            int groundType = 0;
            Vector3 spawnPos = new Vector3(spawnPosition.x + i * groundCollider.localScale.x, spawnPosition.y - (groundHeight * groundCollider.localScale.y), 0);
            float randomValue = Random.value;
            int currentLayerDepth = -currentDepth;

            if (currentLayerDepth >= -2)
            {
                Transform ground = Instantiate(groundPrefab, spawnPos, Quaternion.identity, groundContainer);
                currentGround = ground;
            }
            else if (currentLayerDepth >= -150)
            {
                if (randomValue <= bronzeChance)
                {
                    if (isLoad)
                    {
                        Transform ground = Instantiate(groundPrefab, spawnPos, Quaternion.identity, groundContainer);
                        currentGround = ground;
                    }

                    if (!isLoad)
                    {

                        Transform ground = Instantiate(bronzePrefab, spawnPos, Quaternion.identity, groundContainer);
                        groundType = 1;
                        currentGround = ground;
                    }

                }
                else
                {
                    Transform ground = Instantiate(groundPrefab, spawnPos, Quaternion.identity, groundContainer);
                    currentGround = ground;
                }
            }
            else if (currentLayerDepth >= -300)
            {
                if (randomValue <= silverChance)
                {



                    if (isLoad)
                    {
                        Transform ground = Instantiate(groundPrefab, spawnPos, Quaternion.identity, groundContainer);
                        currentGround = ground;
                    }

                    if (!isLoad)
                    {

                        Transform ground = Instantiate(silverPrefab, spawnPos, Quaternion.identity, groundContainer);
                        groundType = 2;
                        currentGround = ground;
                    }
                }
                else
                {
                    Transform ground = Instantiate(groundPrefab, spawnPos, Quaternion.identity, groundContainer);
                    currentGround = ground;
                    GroundHealth groundHealth = ground.GetComponent<GroundHealth>();
                    if (groundHealth != null)
                    {
                        groundHealth.maxHealth = 40;
                    }
                }
            }
            else if (currentLayerDepth >= -400)
            {
                if (randomValue <= goldChance)
                {
                    if (isLoad)
                    {
                        Transform ground = Instantiate(groundPrefab, spawnPos, Quaternion.identity, groundContainer);
                        currentGround = ground;
                    }

                    if (!isLoad)
                    {

                        Transform ground = Instantiate(goldPrefab, spawnPos, Quaternion.identity, groundContainer);
                        groundType = 3;
                        currentGround = ground;
                    }


                }
                else
                {
                    Transform ground = Instantiate(groundPrefab, spawnPos, Quaternion.identity, groundContainer);
                    currentGround = ground;
                    GroundHealth groundHealth = ground.GetComponent<GroundHealth>();
                    if (groundHealth != null)
                    {
                        groundHealth.maxHealth = 60;
                    }
                }
            }
            else
            {
                if (randomValue <= diamondChance)
                {

                    if (isLoad)
                    {
                        Transform ground = Instantiate(groundPrefab, spawnPos, Quaternion.identity, groundContainer);
                        currentGround = ground;
                    }

                    if (!isLoad)
                    {

                        Transform ground = Instantiate(diamondPrefab, spawnPos, Quaternion.identity, groundContainer);
                        groundType = 4;
                        currentGround = ground;
                    }
                }

                else
                {
                    Transform ground = Instantiate(groundPrefab, spawnPos, Quaternion.identity, groundContainer);
                    currentGround = ground;
                    GroundHealth groundHealth = ground.GetComponent<GroundHealth>();
                    if (groundHealth != null)
                    {
                        groundHealth.maxHealth = 80;
                    }
                }
            }



            if (groundType != 0)
            {

                int index = currentGround.GetSiblingIndex();
                GameManager.Instance.groundListType.Add(groundType);
                GameManager.Instance.groundListTypeIndex.Add(index);

            }

            //GameManager.Instance.groundState
        }
        spawnPosition.y -= groundCollider.localScale.y;
        currentDepth += groundHeight;

        int childCount = groundContainer.childCount;
        Debug.Log(childCount);
        if (childCount == 8096)
        {
            if (isLoad)
            {
                LoadMap();

            }

            else
            {
                SaveMap();
            }
        }
    }

    private void SaveMap()
    {
        state.data = GameManager.Instance.groundListType;
        state1.data = GameManager.Instance.groundListTypeIndex;
        string json = JsonUtility.ToJson(state);
        string json1 = JsonUtility.ToJson(state1);

        Debug.Log(json);
        Debug.Log(json1);

        PlayerPrefs.SetString("mapsave", json);
        PlayerPrefs.SetString("mapsave1", json1);
    }
    private void LoadMap()
    {
        string json = PlayerPrefs.GetString("mapsave");
        string json1 = PlayerPrefs.GetString("mapsave1");
        state = JsonUtility.FromJson<GroundState>(json);
        state1 = JsonUtility.FromJson<GroundState>(json1);

        int stateCount = state.data.Count;


        for (int i = 0; i < stateCount; i++)
        {
            GameObject go = groundContainer.GetChild(state1.data[i]).gameObject;
            int states = state.data[i];

            Vector3 curPos = go.transform.position;
            go.SetActive(false);

            switch (states)
            {
                case 4:
                    Instantiate(diamondPrefab, curPos, Quaternion.identity, groundContainer);
                    break;
                case 3:
                    Instantiate(goldPrefab, curPos, Quaternion.identity, groundContainer);
                    break;
                case 2:
                    Instantiate(silverPrefab, curPos, Quaternion.identity, groundContainer);
                    break;
                case 1:
                    Instantiate(bronzePrefab, curPos, Quaternion.identity, groundContainer);
                    break;
                default:
                    print("Incorrect intelligence level.");
                    break;
            }
        }
    }
}