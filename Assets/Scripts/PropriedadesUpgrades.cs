using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropriedadesUpgrades : MonoBehaviour {

    //o numero de cores(ou niveis) de upgrades para uma construcao
    public const int numeroDeCoresConstrucoes = 9;

    //numero de tipos diferentes de construcoes no jogo
    public const int numeroDeConstrucoes = PropriedadesConstrucoes.numeroDeConstrucoes;

    //os arrays com nome do tipo "update(...)" sao vetores com valores booleanos que indicam se o jogador possui ou nao o update correspondente

    //indice da linha representa o tipo de construcao 
    //indice da coluna representa o nivel do upgrade
    public bool[,] upgradesConstrucoes = new bool[numeroDeConstrucoes, numeroDeCoresConstrucoes];

    //multiplica o preco do upgrade a cada nivel
    private float aumentoPrecoConstrucao = 100f;

    //matriz com preco dos upgrades
    //indice da linha representa o tipo de construcao 
    //indice da coluna representa o nivel do upgrade
    public float[,] precoUpgradesConstrucoes = new float[numeroDeConstrucoes, numeroDeCoresConstrucoes];

    //array que contem o preco do primeiro upgrade de cada construcao
    public float[] precoPrimeirosUpgradesConstrucoes = new float[numeroDeConstrucoes];

    //numero de upgrades que aumentam diretamente uma porcentagem da taxa de dinheiro produzido por segundo
    public const int numeroAumentoTaxa = 12;

    //array com os updates que aumentam a taxa de dinheiro produzido por segundo
    //o array esta em ordem crescente de preco
    public bool[] upgradesAumentoTaxa = new bool[numeroAumentoTaxa];

    //array com os aumentos na taxa de dinheiro produzido por segundo
    //cada posicao no array represenda um upgrade diferente, em ordem crescente de preco
    public float[] aumentosTaxa = { 0.1f, 0.2f, 0.4f, 0.1f, 0.2f, 0.4f, 0.1f, 0.2f, 0.4f, 0.1f, 0.2f, 0.4f };

    //array com preco dos upgrades que aumentam a taxa de dinheiro produzido por segundo
    //cada posicao no array representa um upgrade diferente
    public float[] precoAumentosTaxa = { 1e3f, 15e3f, 200e3f, 1e6f, 15e6f, 200e6f, 1e9f, 15e9f, 200e9f, 1e12f, 15e12f, 200e12f };

    //numero de upgrades que aumentam a frequencia de aparecimento da golden coin
    public const int numeroFrequenciaGoldenCoin = 4;

    //array com os updates que aumentam a frequencia de aparecimento da golden coin
    public bool[] upgradesFrequenciaGoldenCoin = new bool[numeroFrequenciaGoldenCoin];

    //array com os aumentos na frequencia de aparecimento da moeda
    //o array esta em ordem crescente de preco
    public float[] aumentosFreguenciaGoldenCoin = { 1.2f, 1.2f, 1.5f, 1.5f };

    //array com preco dos upgrades que aumentam a frequencia de aparecimento da golden coin
    //cada posicao no array representa um upgrade diferente
    public float[] precoAumentosFrequenciaGoldenCoin = { 1e3f, 15e3f, 200e3f, 1e6f, 15e6f, 200e6f, 1e9f, 15e9f, 200e9f, 1e12f, 15e12f, 200e12f };

    void Awake()
    {
        //define o preco dos upgrades para construcoes
        definePrecosUpgradesConstrucoes();
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //define o preco dos upgrades de construcoes a partir dos precos iniciais
    private void definePrecosUpgradesConstrucoes()
    {
        //para cada tipo de construcao
        for(int i = 0; i < numeroDeConstrucoes; i++)
        {
            //preco inicial da construcao
            float precoInicial = precoPrimeirosUpgradesConstrucoes[i];

            //para cada upgrade de uma certa construcao
            for(int j = 0; j < numeroDeCoresConstrucoes; j++)
            {
                //define o preco do upgrade
                precoUpgradesConstrucoes[i,j] = precoInicial*Mathf.Pow(aumentoPrecoConstrucao, j+1);
            }
        }
    }
}
