using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    #region Variables
    //variable que indica el gameobject de la casilla (el prefab)
    public GameObject casilla;
    //variables que indican el ancho y el alto
    private int alto, ancho;
    //matriz que será el mapa del juego
    public GameObject[][] map;
    //numero de bombas
    private int bombsNum;
    //instancia de la clase MapGenerator
    public static MapGenerator instance;

    //Gameobjects de los diferentes paneles que activaremos
    public GameObject PanelUI, PanelDerrota, PanelVictoria, PanelJuego;
    //inputs que utilizaremos para tomar los valores de alto ancho y bombas
    public TMP_InputField alto_imp, ancho_imp, bombas_imp;
    //boton de jugar
    public Button Jugar_btn;
    //textos que utilizaremos para modificar su valor
    public TextMeshProUGUI Jugar_txt, Puntuacion_txt;

    //varible que nos indica si hemos terminado la partida
    public bool islose = false;

    //variable que nos indica el numero de casillas que necesitamos para ganar (la inicializamos en 20 para que no nos de ningun error al empezar la partida)
    public int casillasGanar = 20;
    //variable que se ira sumando hasta llegar al numero de casillas para ganar
    public int contadorGanar;

    //variable que nos indica la puntuacion
    public int puntuacion = 0;

    public bool colocarBandera = false;
    public GameObject ImageBamdera;
    #endregion

    #region Metodos Unity
    private void Start()
    {
        instance = this;
        //activamos y desactivamos los distintos paneles del canvas
        PanelUI.SetActive(true);
        PanelDerrota.SetActive(false);
        PanelJuego.SetActive(false);
        PanelVictoria.SetActive(false);
    }

    private void Update()
    {
        //metodo que comprueba si el boton es interactable
        comprobarBoton();

        //le indico la puntuacion
        Puntuacion_txt.text = puntuacion.ToString();

        //si el contador es igual al numero de casillas para ganar, entonces salta la ventana de haber ganado
        if(contadorGanar >= casillasGanar)
        {
            islose = true;
            PanelVictoria.SetActive(true);
        }
    }
    #endregion

    #region Metodos Propios
    public int GetBombsAround(int x, int y)
    {
        int contador = 0;
        //hace las distintas comprobaciones para saber si hay bombas alrededor de al casilla que hemos pulsado
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
        //asignamos que la puntuacion y el contador es cero
        puntuacion = 0;
        contadorGanar = 0;

        //desactivamos y activamos los diferentes paneles
        PanelJuego.SetActive(true);
        PanelDerrota.SetActive(false);
        PanelVictoria.SetActive(false);
        PanelUI.SetActive(false);

        //decimos que no se ha terminado la partida
        islose = false;

        //bucle para destruir el mapa (ya que si empezamos una nueva partida, se tiene que destruir)
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                Destroy(map[i][j]);
            }
        }

        //indicamos cuales seran nuestras dimensiones del mapa y las bombas que habrá
        alto = int.Parse(alto_imp.text);
        ancho = int.Parse(ancho_imp.text);
        bombsNum = int.Parse(bombas_imp.text);

        //indicamos cual será la cantidad de casillas que habrá que pulsar paar ganar
        casillasGanar = (alto * ancho) - bombsNum;

        //indicamos el ancho de mapa
        map = new GameObject[ancho][];

        //indicamos el alto del mapa
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

        //indicamos adonde queremos que se dirija la camara
        Camera.main.transform.position = new Vector3((float)(ancho / 2 - 0.5f), (float)(alto / 2 - 0.5f), -10);

        //indicamos donde van a ir las bombas
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
        //si el texto de el alto, ancho o las bombas esta vacio el boton de jugar no es interactuable
        if (alto_imp.text == "" || ancho_imp.text == "" || bombas_imp.text == "")
        {
            Jugar_btn.interactable = false;
        }
        else
        {
            //si el numero de las bombas es igual o mayor al numero de casillas que hay, entonces sigue sin ser el boton interactuable
            if(int.Parse(bombas_imp.text) >= (int.Parse(alto_imp.text) * int.Parse(ancho_imp.text)))
            {
                Jugar_txt.text = "Pon un numero de bombas menor";
            }
            else
            {
                //si las condiciones se cumplen se acttiva el boton
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
        puntuacion = 0;
        contadorGanar = 0;
        casillasGanar = 20;
        islose = false;

        //bucle para destruir el mapa
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
                Destroy(map[i][j]);
            }
        }

        PanelVictoria.SetActive(false);
        PanelDerrota.SetActive(false);
        PanelUI.SetActive(true);
        PanelJuego.SetActive(false);
    }

    public void ModoBanderaOn()
    {
        ImageBamdera.SetActive(true);
    }

    public void ModoBanderaOff()
    {
        ImageBamdera.SetActive(false);
    }
    #endregion
}
