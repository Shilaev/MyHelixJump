using System.Collections.Generic;
using System.Security.Cryptography;
using DefaultNamespace;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 200;
    private Vector2 _lastTapPos;
    private Vector3 _startRotation;

    private void Awake()
    {
        _startRotation = transform.localEulerAngles;

        _helixDistance = _topPlatrofmPosition.localPosition.y - (_goalPlatformPosition.localPosition.y + 1f);
        LoadStage(0);
    }

    private void Update()
    {
        RotateWithArrows(_rotationSpeed);
        RotateWithMouse();
    }

    private void RotateWithArrows(float rotationSpeed)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.rotation *= Quaternion.Euler(0f, -_rotationSpeed * Time.deltaTime, 0f);
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.rotation *= Quaternion.Euler(0f, _rotationSpeed * Time.deltaTime, 0f);
    }

    private void RotateWithMouse()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 curTapPos = Input.mousePosition;
            if (_lastTapPos == Vector2.zero) _lastTapPos = curTapPos;

            var delta = _lastTapPos.x - curTapPos.x;
            _lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * delta);
        }

        if (Input.GetMouseButtonUp(0)) _lastTapPos = Vector2.zero;
    }


    [SerializeField] private Transform _topPlatrofmPosition;
    [SerializeField] private Transform _goalPlatformPosition;
    [SerializeField] private GameObject _helixPlatformPrefab;
    [SerializeField] private List<Stage> _stages = new List<Stage>();

    private float _helixDistance;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    public void LoadStage(int stageNumber)
    {
        Stage stage = _stages[Mathf.Clamp(stageNumber, 0, _stages.Count - 1)];

        if (stage == null)
        {
            Debug.LogError("No stage " + stageNumber + $" found in _stages List.");
            return;
        }

        // Background color
        Camera.main.backgroundColor = _stages[stageNumber].backgroundColor;
        // Ball color
        GameObject.FindObjectOfType<BallController>().GetComponent<MeshRenderer>().material.color =
            _stages[stageNumber].ballColor;
        // Platforms Color
        foreach (var platformPiece in GameObject.FindGameObjectsWithTag("PlatformPiece"))
            platformPiece.GetComponent<MeshRenderer>().material.color = _stages[stageNumber].platformPartColor;
        // GoalPlatform color
        foreach (var goalPlatformPiece in GameObject.FindGameObjectsWithTag("GoalPlatform"))
            goalPlatformPiece.GetComponent<MeshRenderer>().material.color = _stages[stageNumber].goalPlatformColor;

        // Reset Helix Rotation
        this.transform.localEulerAngles = _startRotation;

        // Destroy the old platforms
        foreach (GameObject platform in spawnedPlatforms) Destroy(platform);

        // create new platforms
        float levelDistance = _helixDistance / stage.platforms.Count;
        float spawnPosY = _topPlatrofmPosition.localPosition.y;

        for (int i = 0; i < stage.platforms.Count; i++)
        {
            spawnPosY -= levelDistance;

            // Spawn platform
            GameObject platform = Instantiate(_helixPlatformPrefab, transform);
            platform.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedPlatforms.Add(platform);

            // Add Gaps in platforms
            // 12 - blocks in each platrform
            int disableBlocksCount = 12 - stage.platforms[i].blockCount;
            var disabledBlocks = new List<GameObject>();

            while (disabledBlocks.Count < disableBlocksCount)
            {
                GameObject randomBlock = platform.transform
                    .GetChild(Random.Range(0, platform.transform.childCount))
                    .gameObject;
                if (disabledBlocks.Contains(randomBlock) == false)
                {
                    randomBlock.SetActive(false);
                    disabledBlocks.Add(randomBlock);
                }
            }

            // Add Death parts
            var activeBlocks = new List<GameObject>();
            foreach (Transform platfrom in platform.transform)
            {
                if (platfrom.gameObject.activeInHierarchy)
                {
                    activeBlocks.Add(platfrom.gameObject);
                }
            }

            var deathBlocks = new List<GameObject>();
            while (deathBlocks.Count < stage.platforms[i].deathBlockCount)
            {
                GameObject randomBlock = activeBlocks[Random.Range(0, activeBlocks.Count)];

                if (deathBlocks.Contains(randomBlock) == false)
                {
                    randomBlock.gameObject.AddComponent<DeathBlock>();
                    deathBlocks.Add(randomBlock);
                }
            }

        }

    }

}