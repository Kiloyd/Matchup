using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject player;

    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform startTrans;
    [SerializeField]
    private Transform finishTrans;
    [SerializeField]
    private Transform itemGroupTrans;

    private GameObject[] items;
    private CinemachineVirtualCamera camFollow;

    private void Start()
    {
        Time.timeScale = 1f;
        camFollow = FindObjectOfType<CinemachineVirtualCamera>();

        items = new GameObject[itemGroupTrans.childCount];
        for (int i = 0; i < itemGroupTrans.childCount; ++i)
            items[i] = itemGroupTrans.GetChild(i).gameObject;

        if (player == null)
        {
            player = Instantiate<GameObject>(playerPrefab);
            player.transform.position = startTrans.position;
            camFollow.Follow = player.transform;
            camFollow.LookAt = player.transform;
        }
    }
    public void gameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    public void gameReload()
    {
        //SceneManager.LoadScene(0);
        player.GetComponent<Player>().playerReset();
        player.transform.position = startTrans.position;

        foreach (GameObject go in items)
            go.SetActive(true);

        finishTrans.gameObject.SetActive(true);

        finishCancel();
    }

    public void finishCancel()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
