using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GM.State
{
    public class GMMainState : GMStateBase
    {
        public override void Init()
        {
            base.Init();
        }
        public override void LoadScene()
        {
            GMSceneManager.Instance.LoadSceneAsync_Single(GMScene.Stage01);
        }

        protected override void OnLoadCompleteScene()
        {
            base.OnLoadCompleteScene();
            base.LoadScene();
        }
        public override void LoadUI()
        {
            GMUIManager.Instance.LoadUIController(GMUIAddress.GMPage_InGame);
            base.LoadUI();
        }
        public override void LoadCharacter()
        {
            GMCharacterManager.Instance.OnCompleteCreateCharacter += OnCreateComplete;
            GMCharacterManager.Instance.CreateMyCharacter();
        }
        public void OnCreateComplete()
        {
            GMCharacterManager.Instance.OnCompleteCreateCharacter -= OnCreateComplete;

            GMPlayerScript myScript = GMCharacterManager.Instance.GetMyCharacter();
            if (myScript != null)
                GMCameraManager.Instance.SetCinemachineFollow(myScript.transform);

            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene == null || scene.IsValid() == false)
                    continue;

                if (scene.name == "Stage01")
                {
                    GameObject[] rootObjects = scene.GetRootGameObjects();
                    if (rootObjects == null || rootObjects.Length == 0)
                        continue;
                    for (int index = 0; index < rootObjects.Length; ++index)
                    {
                        PolygonCollider2D collider = FindObjectInChildren(rootObjects[index]);
                        if (collider == null)
                            continue;

                        GMCameraManager.Instance.SetConfinerBoundingShape(collider);
                        base.LoadCharacter();
                        return;
                    }
                }
            }

            base.LoadCharacter();
        }
        public PolygonCollider2D FindObjectInChildren(GameObject parent)
        {
            // �θ� ��ü���� PolygonCollider2D �˻�
            PolygonCollider2D collider = parent.GetComponent<PolygonCollider2D>();
            if (collider != null)
            {
                return collider;
            }

            // �ڽ� ��ü�� ��ȸ
            foreach (Transform child in parent.transform)
            {
                PolygonCollider2D childCollider = FindObjectInChildren(child.gameObject);
                if (childCollider != null)
                {
                    return childCollider;
                }
            }

            // ������ null ��ȯ
            return null;
        }
    }
}