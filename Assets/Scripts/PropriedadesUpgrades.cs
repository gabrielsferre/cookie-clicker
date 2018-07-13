using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropriedadesUpgrades : MonoBehaviour {

    //o numero total de upgrades
    public const int numeroDeUpgrades = 79;

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
    public float[] precoPrimeirosUpgradesConstrucoes = { 1e2f, 1e3f, 1e4f, 1e5f, 1e6f, 1e7f, 1e8f};

    //matriz com sprites dos upgrades de construcoes
    public Sprite[,] spritesUpgradesConstrucoes = new Sprite[numeroDeConstrucoes, numeroDeCoresConstrucoes];

    private string caminhoSpritesUpgradesConstrucoes = "Sprites/FoodClickerUpgrades-Sheet";

    //indice da primeira sprite de upgrade de construcao
    private int indiceDaSpriteInicial = 16;

    //numero de upgrades que aumentam diretamente uma porcentagem da taxa de dinheiro produzido por segundo
    public const int numeroAumentoTaxa = 12;

    //array com os updates que aumentam a taxa de dinheiro produzido por segundo
    //o array esta em ordem crescente de preco
    public bool[] upgradesAumentoTaxa = new bool[numeroAumentoTaxa];

    //array com os aumentos na taxa de dinheiro produzido por segundo
    //cada posicao no array represenda um upgrade diferente, em ordem crescente de preco
    public float[] aumentosTaxa = { 0.01f, 0.02f, 0.04f, 0.01f, 0.02f, 0.04f, 0.01f, 0.02f, 0.04f, 0.01f, 0.02f, 0.04f };

    //array com preco dos upgrades que aumentam a taxa de dinheiro produzido por segundo
    //cada posicao no array representa um upgrade diferente
    public float[] precoAumentosTaxa = { 1e3f, 15e3f, 200e3f, 1e6f, 15e6f, 200e6f, 1e9f, 15e9f, 200e9f, 1e12f, 15e12f, 200e12f };

    //array com sprites dos upgrades de taxa
    public Sprite[] spritesUpgradesAumentosTaxa = new Sprite[numeroAumentoTaxa];

    //numero de upgrades que aumentam o dinheiro ganho com o clique no logo icone do hamburguer
    public const int numeroAumentoClique = 1;

    //array com os updates que aumentam o dinheiro ganho com o clique
    //o array esta em ordem crescente de precos
    public bool[] upgradesAumentoClique = new bool[numeroAumentoClique];

    //array com os valores das fracoes da taxa de dinheiro produzido por segundo que serão acrescentados ao clique
    //cada posicao no array represenda um upgrade diferente, em ordem crescente de preco
    public float[] aumentosClique = { 0.1f };

    //array com preco dos upgrades que aumentam o dinheiro ganho com o clique
    //o array esta em ordem crescente de precos
    public float[] precoAumentosClique = { 1000f };

    //array com sprites dos upgrades do clique
    public Sprite[] spritesUpgradesAumentosClique = new Sprite[numeroAumentoClique];

    //numero de upgrades que aumentam a frequencia de aparecimento da golden coin
    public const int numeroFrequenciaGoldenCoin = 3;

    //array com os updates que aumentam a frequencia de aparecimento da golden coin
    public bool[] upgradesFrequenciaGoldenCoin = new bool[numeroFrequenciaGoldenCoin];

    //array com os aumentos na frequencia de aparecimento da moeda
    //o array esta em ordem crescente de preco
    public float[] aumentosFreguenciaGoldenCoin = { 1.5f, 1.5f, 1.5f };

    //array com preco dos upgrades que aumentam a frequencia de aparecimento da golden coin
    //cada posicao no array representa um upgrade diferente
    public float[] precoAumentosFrequenciaGoldenCoin = { 1e6f, 1e9f, 150e9f};

    //array com sprites dos upgrades do clique
    public Sprite[] spritesUpgradeAumentosFrequenciaGoldenCoin = new Sprite[numeroFrequenciaGoldenCoin];

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
                precoUpgradesConstrucoes[i,j] = precoInicial*Mathf.Pow(aumentoPrecoConstrucao, j);
            }
        }
    }

    //carrega as sprites dos upgrades de construcoes
    //funcao supoe que todas as sprites de upgrades de construcoes estao juntas no sprite sheet e na ordem certa
    private void carregaSpritesUpgradesConstrucoes()
    {
        //array com todas as sprites de updates
        Sprite[] sprites = Resources.LoadAll<Sprite>(caminhoSpritesUpgradesConstrucoes);

        //variavel auxiliar que ira percorrer o array com todas as sprites
        int aux = indiceDaSpriteInicial;

        //para cada tipo de construcao
        for (int i = 0; i < numeroDeConstrucoes; i++)
        {
            //para cada upgrade de uma certa construcao
            for (int j = 0; j < numeroDeCoresConstrucoes; j++)
            {
                //define o preco do upgrade
                spritesUpgradesConstrucoes[i, j] = sprites[aux];
                aux++;
            }
        }
    }

    //ativa o upgrade de construcao com a linha e coluna correspondente
    public void ativaUpgradeConstrucao(int linha, int coluna)
    {
        upgradesConstrucoes[linha, coluna] = true;
    }

    //ativa upgrade de taxa com o index correspondente
    public void ativaUpgradeTaxa(int index)
    {
        upgradesAumentoTaxa[index] = true;
    }

    //ativa upgrade de clique com o index correspondente
    public void ativaUpgradeClique(int index)
    {
        upgradesAumentoClique[index] = true;
    }

    //ativa upgrade de frequencia de aparecimento da golden coin com o index correspondente
    public void ativaUpgradeFrequenciaGoldenCoin(int index)
    {
        upgradesFrequenciaGoldenCoin[index] = true;
    }
}
