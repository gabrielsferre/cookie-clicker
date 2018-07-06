using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropriedadesGoldenCoin : MonoBehaviour {

    //fator que multiplica a variavel 'taxaGanho' da classe "PropriedadesDinheiro" para calcular o bonus da taxa
    public float fatorBonusTaxa = 7F;

    //tempo de bonus da taxa em segundos
    public float tempoBonusTaxa = 5F;

    //fator que multiplica a variavel 'taxaGanho' da classe "PropriedadesDinheiro" para calcular o bonus de dinheiro
    public float fatorBonusDinheiro = 10F*60F;

    //probabilidade da golden coin aparecer em um fixed frame
    private float probabilidade = 0.3F;

    //menor intervalo entre duas golden coins em segundos
    private float intervaloMinimo = 5F;

    //flag que habilita ou nao o surgimento de uma nova golden coin
    private bool goldenCoinHabilitada = true;

    //tempo que a golden coin fica na tela ate desaparecer
    private float intervaloDesaparecimento = 10F;

    //probabilidade do update ser aumento do dinheiro
    private float probabilidadeDinheiro = 1.0F;

    //probabilidade do update ser aumento da taxa de ganho por segundo
    //private float probabilidadeTaxa = 0.5F;

    //prefab da goldenCoin
    public GameObject goldenCoinPrefab;

    //GameObject da golden coin
    private GameObject goldenCoin;

	// Use this for initialization
	void Start () {
        //Usa 'checaSurgimento()' a cada 'intervaloMinimo' segundos
        InvokeRepeating("checaSurgimento", 0, intervaloMinimo);
	}

    //define se a moeda ira surgir e cria a moeda caso necessario
    private void checaSurgimento()
    {
        //caso a golden coin esteja habilitada
        if (goldenCoinHabilitada)
        {
            if (testaProbabilidadeDeSurgimento())
            {
                criaGoldenCoin();
            }
        }
    }

    //usa 'probabilidade' para retornar 'true' ou 'false'
    private bool testaProbabilidadeDeSurgimento()
    {
        float numAleatorio = Random.value;

        return (probabilidade > numAleatorio) ? true : false;
    }

    //da coordenadas aleatorias dentro de um retangulo com altura e largura especificados e origem no centro
    private Vector3 posicaoAleatoria(float comprimento, float altura)
    {
        Vector3 posicao = new Vector3();

        posicao.x = (float)(Random.value * comprimento) - comprimento/2F;
        posicao.y = (float)(Random.value * altura) - altura/2F;
        posicao.z = 0;

        return posicao;
    }

    //cria golden coin numa posicao aleatoria da main camera
    private void criaGoldenCoin()
    {
        Camera camera = Camera.main;

        Vector3 posicao = posicaoAleatoria(camera.aspect*camera.orthographicSize*2F, camera.orthographicSize*2F);

        string tipoDeBonus = sorteiaBonus();

        goldenCoinHabilitada = false;

        goldenCoin = Instantiate(goldenCoinPrefab, posicao, Quaternion.identity);

        goldenCoin.GetComponent<ControleGoldenCoin>().tipoDeBonus = tipoDeBonus;

        Invoke("apagaGoldenCoin", intervaloDesaparecimento);
    }

    //apaga golden coin caso ela ainda exista
    private void apagaGoldenCoin()
    {
        //caso goldenCoin nao seja null e a golden coin ainda nao tenha sido clicada
        if (goldenCoin != null && !goldenCoin.GetComponent<ControleGoldenCoin>().foiClicado)
        {
            Destroy(goldenCoin);
            goldenCoinHabilitada = true;
        }
    }

    //determina qual sera o beneficio dado pela golden coin
    //pode retornar "dinheiro" ou "taxa"
    public string sorteiaBonus()
    {

        float sorteio = Random.value;

        if( sorteio <= probabilidadeDinheiro)
        {
            return "dinheiro";
        }
        else
        {
            return "taxa";
        }
    }



    //desabilita o aparecimento de golden coins
    public void iniciaBonusTaxa()
    {
        goldenCoinHabilitada = false;
    }

    //habilita o aparecimento de mais golden coins
    public void finalizaBonus()
    {
        goldenCoinHabilitada = true;
    }
}
