using UnityEngine;
using System.Collections;

public class AssetSwitch : MonoBehaviour {

	//public Mesh newMesh;
	public GameObject newObject;
	
	private MeshRenderer renderer;
	private MeshFilter meshfilter;
	private Material[] oldMaterials;
	private Material[] newMaterials;
	private Mesh oldMesh;
	private Mesh newMesh;
	
	void Awake()
	{
		meshfilter = GetComponent<MeshFilter>();
		renderer = GetComponent<MeshRenderer>();
		
		oldMesh = meshfilter.mesh;
		newMesh = newObject.GetComponent<MeshFilter>().mesh;
		
		oldMaterials = renderer.materials;
		newMaterials = newObject.GetComponent<MeshRenderer>().materials;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.J))
		{
			meshfilter.mesh = newMesh;
			//newMesh = oldMesh;
			oldMesh = meshfilter.mesh;
			
			renderer.materials = newMaterials;
		}
	}
}
