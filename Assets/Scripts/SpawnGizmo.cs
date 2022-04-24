using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnGizmo : MonoBehaviour
{
    public List<GameObject> modelList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if(Application.isPlaying)
		{
            DestroyModels();
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyModels()
	{
        for(int i=0; i<modelList.Count; i++)
		{
            Destroy(modelList[i]);
		}
        modelList.Clear();
	}

    public void ToggleModelGizmo(bool val)
	{
        for (int i = 0; i < modelList.Count; i++)
		{
            modelList[i].SetActive(val);
		}
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
	{
        if (Selection.activeGameObject == this.gameObject || 
            Selection.activeGameObject == GetComponentsInChildren<Transform>().FirstOrDefault(e => e.gameObject))
		{
            ToggleModelGizmo(true);
		}
        else
		{
            ToggleModelGizmo(false);
		}
	}
#endif
}
