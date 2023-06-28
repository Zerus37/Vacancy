using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TerrainGenerator : MonoBehaviour
{

	[SerializeField]
	private int _xSize, _zSize;
	private Vector3[] _vertices;
	[SerializeField] private bool needBoundCollider = false;
	[SerializeField] private MeshCollider myCollider;
	[SerializeField] private float perlinGrainSize = 4f;
	[SerializeField] private float hightScale = 4f;
	[SerializeField] private float widthScale = 1f;
	[SerializeField] private Vector2 angleMax = new Vector2(0.3f,0.3f);
	[SerializeField] private Vector2 angleBase = new Vector2(0f,0f);
	[SerializeField] private NavMeshSurface myNavMesh;

	[SerializeField] private GameObject monsterPrefab;
	[SerializeField] private float monsterDensityPerCell = 1f;
	
	private float perlinBase = 0f;

	void Start()
	{
		GenerateMesh();
		if(needBoundCollider)
			BoundCollider();
	}

	private void BoundCollider()
	{
		if(myCollider == null)
			myCollider = this.GetComponent<MeshCollider>();
		if(myCollider == null)
		{
			Debug.Log(this.name+" must have MeshCollider, or link for this");
			return;
		}

		myCollider.sharedMesh = this.gameObject.GetComponent<MeshFilter>().mesh;
	}

	private void GenerateMesh()
	{
		Mesh _mesh = new Mesh();
		this.gameObject.GetComponent<MeshFilter>().mesh = _mesh;
		_mesh.name = "Grid";

		// У юнити есть лимит в 65535 вершин на меш
		if ((_xSize + 1) * (_zSize + 1) > 65530) 
		{
			_mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;// Без этого меши 256*256 квадратов и более начинают отображаться некорректно
		}
		_vertices = new Vector3[(_xSize + 1) * (_zSize + 1)];
		Vector2[] _uvs = new Vector2[_vertices.Length];

		float perlinBase = Random.Range(0f, 1f);

		Vector2 angle = new Vector2(Random.Range(-angleMax.x, angleMax.x),Random.Range(-angleMax.y, angleMax.y));
		angle += angleBase;

		for (int i = 0, z = 0; z <= _zSize; z++)
		{
			for (int x = 0; x <= _xSize; x++, i++)
			{
				float y = Mathf.PerlinNoise(((float)x*widthScale+transform.position.x) / 64f*perlinGrainSize+perlinBase, ((float)z*widthScale+transform.position.z)/64f*perlinGrainSize+perlinBase)*hightScale;
				y += Mathf.PerlinNoise(((float)x*widthScale+transform.position.x) / 64f * perlinGrainSize / 4 + perlinBase, ((float)z*widthScale+transform.position.z) / 64f * perlinGrainSize / 4 + perlinBase) * hightScale * 8;
				y += angle.x*x+angle.y*z;
				_vertices[i] = new Vector3(x*widthScale, y, z*widthScale);
				_uvs[i] = new Vector2(x,z);
			}
		}
		_mesh.vertices = _vertices;
		_mesh.uv = _uvs;

		int[] triangles = new int[_xSize * _zSize * 6];

		int nextRow = _xSize+1;
		for (int i = 0, x = 0, z = 0; i < _xSize * _zSize * 6; i += 6)
		{
			int basePoint = x + z*_xSize+z; // Base point
			triangles[i] = basePoint;
			triangles[i + 1] = basePoint + nextRow;
			triangles[i + 2] = basePoint + 1;

			triangles[i + 3] = basePoint + 1;
			triangles[i + 4] = basePoint + nextRow;
			triangles[i + 5] = basePoint + nextRow+1;

			x += 1;
			if (x == _xSize)
			{
				x = 0;
				z += 1;
			}
		}

		_mesh.triangles = triangles;
		_mesh.RecalculateNormals(); // Исправить проблемы со светом, и нормалями плоскостей
		_mesh.RecalculateBounds();
		
		myNavMesh.BuildNavMesh();

		foreach (Vector3 point in _vertices)
		{
			if(point.x == 0f || point.z == 0f)
			{

			}
			else
			{
				Instantiate(monsterPrefab,point,Quaternion.identity);
			}
		}
	}

	private Vector3 GetRandonPointOnCell()
	{
		return new Vector3();
	}
	
	private void OnDrawGizmos()
	{
		if (_vertices == null)
		{
			return;
		}

		Gizmos.color = Color.red;
		foreach (Vector3 gizmoPosition in _vertices)
		{
			Gizmos.DrawSphere(gizmoPosition+transform.position, 0.2f);
		}
	}
}
