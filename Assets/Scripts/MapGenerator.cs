using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject casilla;
    public int alto, ancho;
    public GameObject[][] map;
    public int bombsNum;
    public static MapGenerator instance;

    private void Start()
    {

        instance = this;


        map = new GameObject[ancho][];
        
        for(int i = 0; i< map.Length; i++)
        {
            map[i] = new GameObject[alto];
        }
        
        //bucle para generar el mapa
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                map[i][j] = Instantiate(casilla, new Vector2(i, j), Quaternion.identity);
                map[i][j].GetComponent<Casilla>().posx = i;
                map[i][j].GetComponent<Casilla>().posy = j;
            }
        }


        Camera.main.transform.position = new Vector3 ((float)(ancho/2 - 0.5f), (float)(alto/2- 0.5f), -10);

        for(int i = 0; i< bombsNum; i++)
        {
            int x = Random.Range(0, ancho);
            int y = Random.Range(0, alto);

            if (!map[x][y].GetComponent<Casilla>().isBomb)
            {
                map[x][y].GetComponent<Casilla>().isBomb = true;
                //map[x][y].GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                i--;
            }
        }
    }


    public int GetBombsAround(int x, int y)
    {
        int contador = 0;
        
        if(x > 0 && y < (alto - 1) && map[x - 1][y +1].GetComponent<Casilla>().isBomb)
        {
            contador++;
        } 
        
        if(y < (alto - 1) && map[x][y +1].GetComponent<Casilla>().isBomb)
        {
            contador++;
        }

        if ((x < ancho -1) && y < (alto - 1) && map[x + 1][y + 1].GetComponent<Casilla>().isBomb)
        {
            contador++;
        }

        if (x > 0 && map[x - 1][y].GetComponent<Casilla>().isBomb)
        {
            contador++;
        }

        if ((x < ancho - 1) && map[x + 1][y].GetComponent<Casilla>().isBomb)
        {
            contador++;
        }

        if (x > 0 && y > 0 && map[x - 1][y - 1].GetComponent<Casilla>().isBomb)
        {
            contador++;
        }

        if (y > 0 && map[x][y - 1].GetComponent<Casilla>().isBomb)
        {
            contador++;
        }

        if ((x < ancho - 1) && y > 0 && map[x + 1][y - 1].GetComponent<Casilla>().isBomb)
        {
            contador++;
        }

        return contador;
    }
}
