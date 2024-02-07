using RPG.Controllers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Interactions
{
    public class Portal : MonoBehaviour, IInteractable
    {
        public static Dictionary<string, Portal> spawnedToOtherScene = new Dictionary<string, Portal>();

        [HideInInspector] public string currentScene;
        public string destinationScene; // 목표 씬
        public Portal destination; // 동일 씬에 있는 목표 포탈
        public bool isBusy;

        private void Awake()
        {
            currentScene = SceneManager.GetActiveScene().name;

            if (string.IsNullOrEmpty(destinationScene) == false)
            {
                spawnedToOtherScene.Add(destinationScene, this);
            }

            SceneManager.LoadScene("Dummy", LoadSceneMode.Additive);
        }

        private void OnDestroy()
        {
            if (string.IsNullOrEmpty(destinationScene) == false)
            {
                spawnedToOtherScene.Remove(destinationScene);
            }
        }

        public void Interaction(Interactor interactor)
        {
            if (interactor.TryGetComponent(out PlayerController playerController))
            {
                Teleport(playerController.id);
            }
        }

        /// <summary>
        /// Player 를 목표 포탈로 이동시키는 함수
        /// </summary>
        /// <param name="playerID"> 이동시키려는 player id </param>
        public void Teleport(int playerID)
        {
            if (isBusy)
                return;

            // 현재 씬에 목표 포탈이 있을경우
            if (destination)
            {
                PlayerController.spawned[playerID].transform.position
                    = destination.transform.position; // 플레이어를 목표 포탈위치로이동
                Busy(1.0f); // 사용한 포탈은 1초동안 못탐
                destination.Busy(1.0f); // 도착한 포탈도 1초동안 못탐
            }
            // 없으면 목표 씬으로 이동
            else
            {
                PortalManager.instance.TeleportToOtherScene(playerID, destinationScene);
            }
        }

        public void Busy(float duration)
        {
            isBusy = true;
            Invoke("Ready", duration);
        }

        public void Ready()
        {
            isBusy = false;
        }
    }
}