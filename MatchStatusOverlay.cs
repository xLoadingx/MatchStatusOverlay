using MelonLoader;
using UnityEngine;
using System.Collections;
using RumbleModdingAPI;
using RumbleModUI;
using UnityEngine.UI;
using Il2CppRUMBLE.Players.Subsystems;
using Il2CppRUMBLE.Players;
using Il2CppRUMBLE.Managers;

namespace RumbleWarCrimes
{
    public class MatchStatusOverlay : MelonMod
    {
        private bool isInitialized;
        private bool initializedUI;
        private string currentScene = "Loader";

        GameObject localNameplateClone;
        GameObject oppoentNameplateClone;
        GameObject localHealthClone;
        GameObject remoteHealthClone;

        GameObject localNameplateImageObject;
        GameObject remoteNameplateImageObject;
        GameObject localHealthImageObject;
        GameObject remoteHealthImageObject;
        RawImage localRawNameplateImage;
        RawImage localRawHealthImage;
        RawImage remoteRawNameplateImage;
        RawImage remoteRawHealthImage;

        GameObject renderParent;

        GameObject canvas;

        //private IEnumerator InitializeWithPause()
        //{
        //    yield return new WaitForSeconds(3f);
        //    Initialize();
        //}

        public void Initialize()
        {
            if (!isInitialized)
            {
                //if (!initializedUI)
                //{
                //    initializedUI = true;

                //    localNameplateImageObject = new GameObject("Local Nameplate");
                //    localNameplateImageObject.transform.SetParent(Calls.GameObjects.DDOL.GameInstance.UI.GetGameObject().transform);
                //    localRawNameplateImage = localNameplateImageObject.AddComponent<RawImage>();
                //    localNameplateImageObject.transform.localScale = Vector3.one * 1.3f;

                //    remoteNameplateImageObject = new GameObject("Opponent Nameplate");
                //    remoteNameplateImageObject.transform.SetParent(Calls.GameObjects.DDOL.GameInstance.UI.GetGameObject().transform);
                //    remoteRawNameplateImage = remoteNameplateImageObject.AddComponent<RawImage>();
                //    remoteNameplateImageObject.transform.localScale = Vector3.one * 1.3f;

                //    localHealthImageObject = new GameObject("Local Health");
                //    localHealthImageObject.transform.SetParent(Calls.GameObjects.DDOL.GameInstance.UI.GetGameObject().transform);
                //    localRawHealthImage = localHealthImageObject.AddComponent<RawImage>();
                //    localHealthImageObject.transform.localScale = Vector3.one * 1.3f;

                //    remoteHealthImageObject = new GameObject("Local Health");
                //    remoteHealthImageObject.transform.SetParent(Calls.GameObjects.DDOL.GameInstance.UI.GetGameObject().transform);
                //    remoteRawHealthImage = remoteHealthImageObject.AddComponent<RawImage>();
                //    remoteHealthImageObject.transform.localScale = Vector3.one * 1.3f;
                //}

                //localNameplateImageObject.SetActive(currentScene == "Map0" || currentScene == "Map1");
                //remoteNameplateImageObject.SetActive((currentScene == "Map0" || currentScene == "Map1") && PlayerManager.instance.AllPlayers.Count > 1);
                //localHealthImageObject.SetActive(currentScene == "Map0" || currentScene == "Map1");
                //remoteHealthImageObject.SetActive((currentScene == "Map0" || currentScene == "Map1") && PlayerManager.instance.AllPlayers.Count > 1);

                //if (currentScene == "Map0" || currentScene == "Map1")
                //{
                //    renderParent = new GameObject("Match Status Overlay");

                //    InitializeLocalNameplate();
                //    InitializeLocalHealth();

                //    if (Calls.Players.GetPlayerByControllerType(ControllerType.Remote).Controller != null)
                //    {
                //        InitializeRemoteNameplate();
                //        InitializeRemoteHealth();
                //    }
                //}

                isInitialized = true;
            }
        }

        private void InitializeLocalNameplate()
        {
            GameObject localNameplateCamera = new GameObject("Local Nameplate Camera");
            localNameplateCamera.transform.SetParent(renderParent.transform);
            Camera meshCamera = localNameplateCamera.AddComponent<Camera>();
            meshCamera.clearFlags = CameraClearFlags.SolidColor;
            meshCamera.backgroundColor = new Color(0, 0, 0, 0);

            RenderTexture renderTexture = new RenderTexture(512, 512, 16);
            renderTexture.Create();
            meshCamera.targetTexture = renderTexture;
            meshCamera.orthographic = false;
            meshCamera.transform.position = new Vector3(0, -1000, 1);
            meshCamera.transform.rotation = Quaternion.Euler(0, 180, 0);

            GameObject nameplate = Calls.Players.GetLocalPlayer().Controller.transform.GetChild(8).gameObject;
            localNameplateClone = GameObject.Instantiate(nameplate);
            localNameplateClone.transform.SetParent(renderParent.transform);
            localNameplateClone.active = true;
            localNameplateClone.transform.position = new Vector3(0, -1000, 0);

            localRawNameplateImage.texture = renderTexture;

            RectTransform rectTransform = localNameplateImageObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(300, 300);
            rectTransform.anchoredPosition = new Vector2(180, -90);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            localNameplateClone.active = true;

            PlayerNameTag nameTag = localNameplateClone.GetComponent<PlayerNameTag>();
            nameTag.followTarget = null;
            nameTag.parentController = Calls.Players.GetLocalPlayer().Controller.GetComponent<PlayerController>();
            nameTag.UpdatePlayerBPText();
            nameTag.UpdatePlayerNameTagColor();
            nameTag.UpdatePlayerNameText();
            nameTag.UpdatePlayerRankIcon();
            nameTag.UpdatePlayerTitleText();
        }

        private void InitializeRemoteNameplate()
        {
            GameObject remoteNameplateCamera = new GameObject("Remote Nameplate Camera");
            remoteNameplateCamera.transform.SetParent(renderParent.transform);
            Camera meshCamera = remoteNameplateCamera.AddComponent<Camera>();
            meshCamera.clearFlags = CameraClearFlags.SolidColor;
            meshCamera.backgroundColor = new Color(0, 0, 0, 0);

            RenderTexture renderTexture = new RenderTexture(512, 512, 16);
            renderTexture.Create();
            meshCamera.targetTexture = renderTexture;
            meshCamera.orthographic = false;
            meshCamera.transform.position = new Vector3(100, -1000, 1);
            meshCamera.transform.rotation = Quaternion.Euler(0, 180, 0);

            GameObject nameplate = Calls.Players.GetPlayerByControllerType(ControllerType.Remote).Controller.transform.GetChild(8).gameObject;
            oppoentNameplateClone = GameObject.Instantiate(nameplate);
            oppoentNameplateClone.transform.SetParent(renderParent.transform);
            oppoentNameplateClone.active = true;
            oppoentNameplateClone.transform.position = new Vector3(100, -1000, 0);

            remoteRawNameplateImage.texture = renderTexture;

            RectTransform rectTransform = localNameplateImageObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(300, 300);
            rectTransform.anchoredPosition = new Vector2(-180, -90);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            localNameplateClone.active = true;

            PlayerNameTag nameTag = localNameplateClone.GetComponent<PlayerNameTag>();
            nameTag.followTarget = null;
            nameTag.parentController = Calls.Players.GetPlayerByControllerType(ControllerType.Remote).Controller.GetComponent<PlayerController>();
            nameTag.UpdatePlayerBPText();
            nameTag.UpdatePlayerNameTagColor();
            nameTag.UpdatePlayerNameText();
            nameTag.UpdatePlayerRankIcon();
            nameTag.UpdatePlayerTitleText();
        }

        private void InitializeLocalHealth()
        {
            GameObject localHealthCamera = new GameObject("Local Health Camera");
            localHealthCamera.transform.SetParent(renderParent.transform);
            Camera meshCamera = localHealthCamera.AddComponent<Camera>();
            meshCamera.clearFlags = CameraClearFlags.SolidColor;
            meshCamera.backgroundColor = new Color(0, 0, 0, 0);

            RenderTexture renderTexture = new RenderTexture(512, 512, 16);
            renderTexture.Create();
            meshCamera.targetTexture = renderTexture;
            meshCamera.orthographic = false;
            meshCamera.transform.position = new Vector3(100, -1000.182f, 1.72f);
            meshCamera.transform.rotation = Quaternion.identity;

            GameObject health = Calls.Players.GetLocalHealthbarGameObject();
            localHealthClone = GameObject.Instantiate(health);
            localHealthClone.transform.localScale = new Vector3(1, 3, 1);
            localHealthClone.transform.SetParent(renderParent.transform);
            localHealthClone.transform.GetChild(1).gameObject.SetActive(true);
            localHealthClone.active = true;
            localHealthClone.transform.position = new Vector3(100, -1000, -0.4255f);
            localHealthClone.transform.GetChild(1).transform.position = new Vector3(100, -1000.182f, 1.6932f);

            localRawHealthImage.texture = renderTexture;

            RectTransform rectTransform = localHealthImageObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(300, 300);
            rectTransform.anchoredPosition = new Vector2(200, -190);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            localHealthClone.active = true;
        }

        private void InitializeRemoteHealth()
        {
            GameObject remoteHealthCamera = new GameObject("Remote Health Camera");
            remoteHealthCamera.transform.SetParent(renderParent.transform);
            Camera meshCamera = remoteHealthCamera.AddComponent<Camera>();
            meshCamera.clearFlags = CameraClearFlags.SolidColor;
            meshCamera.backgroundColor = new Color(0, 0, 0, 0);

            RenderTexture renderTexture = new RenderTexture(512, 512, 16);
            renderTexture.Create();
            meshCamera.targetTexture = renderTexture;
            meshCamera.orthographic = false;
            meshCamera.transform.position = new Vector3(100, -1000.182f, 1.72f);
            meshCamera.transform.rotation = Quaternion.identity;

            GameObject health = Calls.Players.GetLocalHealthbarGameObject();
            remoteHealthClone = GameObject.Instantiate(health);
            remoteHealthClone.transform.localScale = new Vector3(1, 3, 1);
            remoteHealthClone.transform.SetParent(renderParent.transform);
            remoteHealthClone.transform.GetChild(1).gameObject.SetActive(true);
            remoteHealthClone.active = true;
            remoteHealthClone.transform.position = new Vector3(100, -1000, -0.4255f);
            remoteHealthClone.transform.GetChild(1).transform.position = new Vector3(100, -1000.182f, 1.6932f);

            remoteRawHealthImage.texture = renderTexture;

            RectTransform rectTransform = remoteHealthImageObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(300, 300);
            rectTransform.anchoredPosition = new Vector2(-200, -190);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            remoteHealthClone.GetComponent<PlayerHealth>().parentController = Calls.Players.GetPlayerByControllerType(ControllerType.Remote).Controller.GetComponent<PlayerController>();
            remoteHealthClone.active = true;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            currentScene = sceneName;
            isInitialized = false;

            //if (currentScene != "Loader")
            //{
            //    MelonCoroutines.Start(InitializeWithPause());
            //}
        }
    }
}
