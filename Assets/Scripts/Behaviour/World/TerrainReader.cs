using UnityEngine;

public class TerrainReader
{
    private Terrain m_Terrain;
    private Vector3 m_PlayerPosition;
    private float[] m_TextureValues = new float[3];
    private int m_PositionX;
    private int m_PositionZ;

    public float[] GetTerrainTexture(Terrain terrain, Vector3 playerPosition)
    {
        m_Terrain = terrain;
        m_PlayerPosition = playerPosition;
        ConvertPosition();
        CheckTexture();
        return m_TextureValues;
    }

    private void ConvertPosition()
    {
        Vector3 terrainPosition = m_PlayerPosition - m_Terrain.transform.position;
        Vector3 mapPosition = new Vector3(terrainPosition.x / m_Terrain.terrainData.size.x, 0f, terrainPosition.z / m_Terrain.terrainData.size.z);
        float coordinateX = mapPosition.x * m_Terrain.terrainData.alphamapWidth;
        float coordinateZ = mapPosition.z * m_Terrain.terrainData.alphamapHeight;
        m_PositionX = Mathf.RoundToInt(coordinateX);
        m_PositionZ = Mathf.RoundToInt(coordinateZ);
    }

    private void CheckTexture()
    {
        float[,,] alphaMap = m_Terrain.terrainData.GetAlphamaps(m_PositionX, m_PositionZ, 1, 1);
        m_TextureValues[0] = alphaMap[0, 0, 0]; // snow
        m_TextureValues[1] = alphaMap[0, 0, 1]; // stone
        m_TextureValues[2] = alphaMap[0, 0, 2]; // dirt
    }
}
