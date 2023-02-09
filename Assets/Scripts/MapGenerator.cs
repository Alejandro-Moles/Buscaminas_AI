using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public GameObject casilla;
    private int alto, ancho;
    public GameObject[][] map;
    private int bombsNum;
    public static MapGenerator instance;

    public GameObject PanelUI, PanelDerrota, PanelVictoria;
    public TMP_InputField alto_imp, ancho_imp, bombas_imp;
    public Button Jugar_btn;
    public TextMeshProUGUI Jugar_txt;

    public bool islose = false;

    public int casillasGanar = 20;
    public int contadorGanar;

    private void Start()
    {
        instance = this;
        PanelUI.SetActive(true);
        PanelDerrota.SetActive(false);
        PanelVictoria.SetActive(false);
    }

    private void Update()
    {
        comprobarBoton();

        if(contadorGanar >= casillasGanar)
        {
            islose = true;
            PanelVictoria.SetActive(true);
        }
    }


    public int GetBombsAround(int x, int y)
    {
        int contador = 0;

        if (x > 0 && y < (alto - 1) && map[x - 1][y + 1].GetComponent<Casilla>().isBomb)
        {
            contador++;
        }

        if (y < (alto - 1) && map[x][y + 1].GetComponent<Casilla>().isBomb)
        {
            contador++;
        }

        if ((x < ancho - 1) && y < (alto - 1) && map[x + 1][y + 1].GetComponent<Casilla>().isBomb)
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


    public void GenerarMapa()
    {
        contadorGanar = 0;

        PanelDerrota.SetActive(false);
        islose = false;
        //bucle para generar el mapa
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                Destroy(map[i][j]);
            }
        }

        alto = int.Parse(alto_imp.text);
        ancho = int.Parse(ancho_imp.text);
        bombsNum = int.Parse(bombas_imp.text);

        casillasGanar = (alto * ancho) - bombsNum;

        PanelUI.SetActive(false);

        map = new GameObject[ancho][];

        for (int i = 0; i < map.Length; i++)
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


        Camera.main.transform.position = new Vector3((float)(ancho / 2 - 0.5f), (float)(alto / 2 - 0.5f), -10);

        for (int i = 0; i < bombsNum; i++)
        {
            int x = Random.Range(0, ancho);
            int y = Random.Range(0, alto);

            if (!map[x][y].GetComponent<Casilla>().isBomb)
            {
                map[x][y].GetComponent<Casilla>().isBomb = true;
            }
            else
            {
                i--;
            }
        }
    }

    private void comprobarBoton()
    {
        if (alto_imp.text == "" || ancho_imp.text == "" || bombas_imp.text == "")
        {
            Jugar_btn.interactable = false;
        }
        else
        {
            if(int.Parse(bombas_imp.text) >= (int.Parse(alto_imp.text) * int.Parse(ancho_imp.text)))
            {
                Jugar_txt.text = "Pon un numero de bombas menor";
            }
            else
            {
                Jugar_txt.text = "Jugar";
                Jugar_btn.interactable = true;
            } 
        }
    }


    public void TerminarPartida()
    {
        islose = true;
        PanelDerrota.SetActive(true);
    }

    public void MenuInicio()
    {
        contadorGanar = 0;
        casillasGanar = 20;
        islose = false;
        //bucle para generar el mapa
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                Destroy(map[i][j]);
            }
        }

        PanelDerrota.SetActive(false);
        PanelUI.SetActive(true);
    }
}
