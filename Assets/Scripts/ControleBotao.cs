using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControleBotao : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //Variaveis

    //script que controla o dinheiro
    private PropriedadesDinheiro propriedadesDinheiro;

    //script que contem o audio
    private RecursosDeAudio recursosDeAudio;

    //transform para o prefab do mini burguer
    public Transform miniBurguer;

    //string para o caminho do prefab do texto up
    public GameObject textoUp;

    //coordenada z em que surgirá o miniburguer
    private float zMiniBurguer = -4;

    //Button que sera usado para dar dinheiro no click
    private Button botao;

    //Porcentagem de aumento do icone quando o mouse estiver sobre ele
    private float porcentagemAumento = 0.1F;

    //Tempo de aumento/reducao do icone em segundos
    private float tempoAumento = 0.05F;

    //Dimensoes do botao
    private Vector2 dimensoes;

    //Dimensoes quando o mouse esta sobre o botao
    private Vector2 dimensoesAumentadas;

    //RectTransform do botao;
    private RectTransform rectTransform;

    //Se o mouse esta ou nao em cima do botao
    private bool mouseEmCima = false;

    // Use this for initialization
    void Start()
    {

        //inicializa propriedadesDinheiro
        propriedadesDinheiro = GameObject.FindGameObjectWithTag("propriedadesDinheiro").GetComponent<PropriedadesDinheiro>();

        //incializa 'recursosDeAudio'
        recursosDeAudio = GameObject.FindGameObjectWithTag("recursosDeAudio").GetComponent<RecursosDeAudio>();

        //adquire a componente 'Button' do GameObject do script
        botao = GetComponent<Button>();

        //inicializa rectTransform
        rectTransform = GetComponent<RectTransform>();

        //inicializa dimensoes do botao e dimensoes aumentadas
        dimensoes = rectTransform.sizeDelta;
        dimensoesAumentadas = dimensoes * (1 + porcentagemAumento);

        //Adiciona o aumento de dinheiro no click do botao
        botao.onClick.AddListener(executaNoClick);
    }

    // Update is called once per frame
    void Update()
    {
        seMouseEmCima();
        seMouseFora();
    }

    private void executaNoClick()
    {
        //aumenta dinheiro
        propriedadesDinheiro.aumentaDinheiroNoClick();

        //toca som do click
        recursosDeAudio.tocaEfeito("somCliqueLogo");

        //cria mini burguer
        criaMiniBurguer();

        //cria texto up
        criaTextoUp();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEmCima = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseEmCima = false;
    }

    //usada se mouse estiver em cima do icone
    private void seMouseEmCima()
    {
        //se mouse estiver em cima do botao
        if (mouseEmCima)
        {
            //se o botao esquerdo do mouse nao estiver pressionado
            if (!Input.GetMouseButton(0))
            {
                //se o tamanho nao for maximo
                if (rectTransform.sizeDelta.x < dimensoesAumentadas.x || rectTransform.sizeDelta.y < dimensoesAumentadas.y)
                {
                    aumentaIcone();
                }
            }
            else
            {
                //se o tamanho nao for o minimo
                if (rectTransform.sizeDelta.x > dimensoes.x || rectTransform.sizeDelta.y > dimensoes.y)
                {
                    diminuiIcone();
                }
            }
        }
        
    }

    //usada se mouse nao estiver no icone
    private void seMouseFora()
    {
        if(!mouseEmCima)
        {
            //se o tamanho nao for o minimo
            if (rectTransform.sizeDelta.x > dimensoes.x || rectTransform.sizeDelta.y > dimensoes.y)
            {
                diminuiIcone();
            }
        }
    }

    //aumenta o icone se ele nao estiver em seu tamanho maximo
    private void aumentaIcone()
    {
        Vector2 aumento = dimensoes * porcentagemAumento * Time.deltaTime / tempoAumento;

        float aumentoX = System.Math.Min(dimensoesAumentadas.x, rectTransform.sizeDelta.x + aumento.x);
        float aumentoY = System.Math.Min(dimensoesAumentadas.y, rectTransform.sizeDelta.y + aumento.y);
        rectTransform.sizeDelta = new Vector2(aumentoX, aumentoY);
    }

    //diminui o icone ase ele nao estiver em seu tamanho minimo
    private void diminuiIcone()
    {
        Vector2 decremento = dimensoes * porcentagemAumento * Time.deltaTime / tempoAumento;

        float decrementoX = System.Math.Max(dimensoes.x, rectTransform.sizeDelta.x - decremento.x);
        float decrementoY = System.Math.Max(dimensoes.y, rectTransform.sizeDelta.y - decremento.y);
        rectTransform.sizeDelta = new Vector2(decrementoX, decrementoY);
    }

    //cria um mini burguer no lugar em que o mouse estiver apontando
    private void criaMiniBurguer()
    {
        Vector3 posicao = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicao.z = zMiniBurguer;
        Instantiate(miniBurguer, posicao, Quaternion.identity);
    }

    private void criaTextoUp()
    {
        Vector3 posicao = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicao.z = zMiniBurguer;

        //instancia objeto de texto
        GameObject texto = Instantiate<GameObject>(textoUp, posicao, Quaternion.identity, GameObject.FindGameObjectWithTag("popUpCanvas").transform);

        texto.GetComponent<RectTransform>().position = posicao;
    } 
}