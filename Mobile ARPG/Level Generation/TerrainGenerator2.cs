using UnityEngine;

public class TerrainGenerator2 : MonoBehaviour
{
	[SerializeField] private TerrainData myTerrain;

	[SerializeField] private float perlinGrainCount = 2f;
	
	[Range(0f,0.2f)]
	[SerializeField] private float angleMaxX = 0.2f;
	[Range(0f,0.2f)]
	[SerializeField] private float angleMaxZ = 0.2f;

	[Range(-0.2f,0.2f)]
	[SerializeField] private float angleBaseX = 0f;
	[Range(-0.2f,0.2f)]
	[SerializeField] private float angleBaseZ = 0f;
	
	private Vector2 angleMax = new Vector2(0.2f,0.2f);
	private Vector2 angleBase = new Vector2(0f,0f);
	private float yBase = 0f;

	private int _xSize, _zSize;
	private  float[,] heights;

	void Start()
	{
		angleMax = new Vector2(angleMaxX,angleMaxZ);
		angleBase = new Vector2(angleBaseX,angleBaseZ);

		RenderTexture heightTexture = myTerrain.heightmapTexture;
		_xSize = heightTexture.height;
		_zSize = heightTexture.width;
		heights = new float[_xSize,_zSize];
		
		float perlinBase = Random.Range(0f, 1f);

		Vector2 angle = new Vector2(Random.Range(-angleMax.x, angleMax.x), Random.Range(-angleMax.y, angleMax.y));
		angle += angleBase;

		if(angle.x < 0 || angle.y < 0)
		{
			yBase = Mathf.Min(Mathf.Min(angle.x,angle.y),angle.x+angle.y)*-1;
		}

		angle /= _xSize;


		for (int z = 0; z < _zSize; z++)
		{
			for (int x = 0; x < _xSize; x++)
			{
				float y = Mathf.PerlinNoise(((float)x+transform.position.x)/ (float)_xSize*perlinGrainCount+perlinBase,((float)z+transform.position.z) / (float)_zSize * perlinGrainCount+perlinBase);
				y += Mathf.PerlinNoise(((float)x+transform.position.x) / (float)_xSize * perlinGrainCount / 4+perlinBase, ((float)z + transform.position.z) / (float)_zSize * perlinGrainCount / 4+perlinBase) / 8;
				y /= 16;
				y += angle.x*x+angle.y*z;
				y += yBase;

				heights[x,z] = y;
			}
		}

		myTerrain.SetHeights(0,0,heights);
	}
}
