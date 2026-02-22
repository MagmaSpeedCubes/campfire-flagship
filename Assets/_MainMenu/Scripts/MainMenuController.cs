using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Minigame[] Minigames;
    [SerializeField] private GameObject MinigamePrefab;

    [Header("Settings")]
    [SerializeField] private float Spacing = 2.0f;
    [SerializeField] private float SideDepth = 1.0f;
    [SerializeField] private float SideRotation = 45.0f;
    [SerializeField] private float LerpSpeed = 5.0f;
    [Space]
    [SerializeField] private long NextPositionBarcode;
    [SerializeField] private long SelectBarcode;
    [SerializeField] private long LastPositionBarcode;
    [SerializeField] long resetBarcode;

    [Header("Debug")]
    [SerializeField] private List<GameObject> LoadedMinigames = new List<GameObject>();
    [SerializeField] private int SelectedIndex = 0;

    private void Start()
    {
        ReadBarcode.Instance.OnBarcodeScanned.AddListener(OnScan);

        foreach (Minigame minigame in Minigames)
        {
            GameObject minigameObj = Instantiate(MinigamePrefab, transform);
            MinigameController controller = minigameObj.GetComponent<MinigameController>();
            controller.SetMinigame(minigame);
            LoadedMinigames.Add(minigameObj);
        }
    }

    private void Update()
    {
        UpdatePositions();
    }

    public void OnScan(string barcode)
    {
        if (NextPositionBarcode == long.Parse(barcode))
        {
            SelectedIndex++;
            if (SelectedIndex >= LoadedMinigames.Count) SelectedIndex = 0;
        }
        else if (SelectBarcode == long.Parse(barcode))
        {
            Minigame selectedMinigame = Minigames[SelectedIndex];
            UnityEngine.SceneManagement.SceneManager.LoadScene(selectedMinigame.SceneName);
        }
        else if (LastPositionBarcode == long.Parse(barcode))
        {
            SelectedIndex--;
            if (SelectedIndex < 0) SelectedIndex = LoadedMinigames.Count - 1;
        }
        else if (resetBarcode == long.Parse(barcode))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void UpdatePositions()
    {
        for (int i = 0; i < LoadedMinigames.Count; i++)
        {
            Transform targetTransform = LoadedMinigames[i].transform;

            int relativeIndex = i - SelectedIndex;

            Vector3 targetPos = Vector3.zero;
            targetPos.x = relativeIndex * Spacing;

            if (relativeIndex != 0)
            {
                targetPos.z = SideDepth;
            }

            float rotY = 0;
            if (relativeIndex > 0) rotY = -SideRotation;
            else if (relativeIndex < 0) rotY = SideRotation;

            Quaternion targetRot = Quaternion.Euler(0, rotY, 0);

            targetTransform.localPosition = Vector3.Lerp(targetTransform.localPosition, targetPos, Time.deltaTime * LerpSpeed);
            targetTransform.localRotation = Quaternion.Slerp(targetTransform.localRotation, targetRot, Time.deltaTime * LerpSpeed);
        }
    }
}