using UnityEngine;

public class ChessGrid : MonoBehaviour
{
    public GameObject prefab;
    float gridSize = 8.5f;
    public GameObject parent;
    void Start()
    {
        CreateChessboard();
    }

    void CreateChessboard()
    {
        for (float row = 0.5f; row < gridSize; row ++)
        {
            for (float col = 0.5f; col < gridSize; col++)
            {
                Vector2 pos = new Vector2(row, col);

              
                if ((Mathf.FloorToInt(row) + Mathf.FloorToInt(col)) % 2 == 0)
                {
                    Instantiate(prefab, pos, Quaternion.identity,parent.transform);
                }
            }
        }
    }
}
