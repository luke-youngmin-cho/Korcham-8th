using UnityEngine;
using RPG.Singletons;
using System.Collections;
using UnityEngine.SceneManagement;
using RPG.Controllers;

namespace RPG.Interactions
{
    public class PortalManager : SingletonMonoBase<PortalManager>
    {
        private bool _isBusy;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            // sceneUnloaded -> activeSceneChanged -> sceneLoaded
            SceneManager.sceneUnloaded += (scene) =>
            {
                Debug.Log($"Scene {scene.name} has unloaded.");
            };
            SceneManager.activeSceneChanged += (prev, next) =>
            {
                Debug.Log($"Scene has changed {prev.name} to {next.name}");
            };
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                Debug.Log($"Scene {scene.name} has loaded with {mode} mode.");
            };
        }

        public void TeleportToOtherScene(int playerId, string sceneName)
        {
            if (_isBusy)
                return;

            _isBusy = true;
            StartCoroutine(C_TeleportToOtherScene(playerId, sceneName));
        }

        private IEnumerator C_TeleportToOtherScene(int playerId, string sceneName)
        {
            string prevSceneName = SceneManager.GetActiveScene().name;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            while (asyncOperation.isDone == false)
                yield return null;

            yield return new WaitUntil(() => PlayerController.spawned.ContainsKey(playerId)); // 넘어 온 씬에서 캐릭터가 생성되길 기다림
            yield return new WaitUntil(() => Portal.spawnedToOtherScene.ContainsKey(prevSceneName)); // 목표로했던 포탈이 초기화 되길 기다림

            Portal portal = Portal.spawnedToOtherScene[prevSceneName];
            portal.Busy(1.0f);
            PlayerController.spawned[playerId].transform.position = portal.transform.position;
            _isBusy = false;
        }
    }
}
