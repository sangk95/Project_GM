using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GM.State
{
    public class GMStateStage01 : GMStateBase
    {
        public override void InitStep()
        {
            base.InitStep();
        }
        public override void SceneStep()
        {
            GMSceneManager.Instance.LoadSceneAsync_Single(GMScene.Stage01);
        }
        public override void OnLoadCompleteScene()
        {
            base.OnLoadCompleteScene();
            base.SceneStep();
        }
        public override void UIStep()
        {
            // UI �ε�
            base.UIStep();
        }
        public override void PlayerStep()
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
                        base.PlayerStep();
                        return;
                    }
                }
            }

            base.PlayerStep();
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