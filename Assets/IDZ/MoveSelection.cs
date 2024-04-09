using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;



public struct RealtimePlayerData 
{
    public float right;
    public float left;
    public float forward;
    public float backward;
    public float leftDiagonal;
    public float rightDiagonal;
}

public class MoveSelection : MonoBehaviour
{
   
    public RealtimePlayerData data;
    public GameObject currentPlayer;
    public List<GameObject> player;
    public Button checkCorrdiantes;
   List <Vector2> highlightRightLeftCoordinates = new List <Vector2>();
   
    private void Start()
    {
        checkCorrdiantes.onClick.AddListener(delegate { HighlightPlayerMoves(); ; });
       
    }



    void HighlightPlayerMoves()
    {


        HorizontalHighlight();

        //VerticalHighlight();
       
    }

    private void HorizontalHighlight()
    {




        if (player.Any(p => p.transform.position.x < currentPlayer.transform.position.x) && player.Any(p => p.transform.position.y == currentPlayer.transform.position.y))
        {
            float coordn;

            coordn = player.Max(p => p.transform.position.x);
            Debug.Log("coordn : " + coordn);

            if (player.Any(p => p.transform.position.x > currentPlayer.transform.position.x) && player.Any(p => p.transform.position.y == currentPlayer.transform.position.y))
            {
                float coordn1;

                coordn1 = player.Min(p => p.transform.position.x);

                Debug.Log("coordn1 : " + coordn1);


                for (float i = coordn1; i < coordn; i++)
                {
                    Vector3 newpos = new Vector3(i, currentPlayer.transform.position.y, 0);
                  
                    GameObject g = new GameObject();
                    Vector2 coord = new Vector2(i, currentPlayer.transform.position.y);
                    g.transform.position = coord;
                    Debug.Log("Cordinates X : " + i);
                    if (player.Any(p => p.transform.position == newpos))
                    {
                       

                    }
                    else
                    {

                      
                    }


                }
            }
        }
    }











    private void VerticalHighlight()
    {
        for (float i = 0.5f; i < 7.5f; i++)
        {
            Vector3 newpos = new Vector3( currentPlayer.transform.position.x,i, 0);
            
            if (player.Any(p => p.transform.position == newpos))
            {
                Debug.Log("Cordinates Y : " + i);

            }
            else
            {
              
                GameObject g = new GameObject();
                Vector2 coord = new Vector2( currentPlayer.transform.position.x,i);
                g.transform.position = coord;
            }


        }

       
    }

}
