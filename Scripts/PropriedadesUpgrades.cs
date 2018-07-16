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

    //quantidades de construcoes de um certo tipo que o jogador precisa pra desbloquear o upgrade
    //cada elemento eh aplicado em uma coluna da matriz de upgrades de construcao
    public int[] condicoesConstrucoes = { 1, 10, 25, 50, 100, 150, 250, 500, 1000 };

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

    //quantidades de dinheiro que o jogador deve acumular para o upgrade aparecer na loja
    public float[] condicoesAumentosTaxa = { 1e2f, 15e2f, 200e2f, 1e3f, 15e3f, 200e3f, 1e6f, 15e6f, 200e6f, 1e9f, 15e9f, 200e9f };

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

    //quantidades de dinheiro que o jogador deve acumular para o upgrade aparecer na loja
    public float[] condicoesAumentosClique = { 100f };

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

    //quantidades de dinheiro que o jogador deve acumular para o upgrade aparecer na loja
    public float[] condicoesAumentosFrequenciaGoldenCoin = { 1e3f, 1e6f, 150e3f };

    //array com sprites dos upgrades do clique
    public Sprite[] spritesUpgradeAumentosFrequenciaGoldenCoin = new Sprite[numeroFrequenciaGoldenCoin];

    //upgrades que estao fora da loja
    public List<Upgrade> upgradesForaLoja = new List<Upgrade>();

    //upgrades que estao na loja na ordem do mais barato para o mais caro
    public List<Upgrade> upgradesLoja = new List<Upgrade>();

    //script que controla o dinheiro
    private PropriedadesDinheiro propriedadesDinheiro;

    //script que controla as construcoes
    private PropriedadesConstrucoes propriedadesConstrucoes;

    //script que controla o panel com os upgrades
    private DisplayUpgradesLoja displayUpgradesLoja;

    void Awake()
    {
        //define o preco dos upgrades para construcoes
        definePrecosUpgradesConstrucoes();

        //coloca upgrades na lista dos que nao estao na loja
        insereUpgradesNaListaForaLoja();

        //carrega as sprites dos upgrades
        carregaSpritesUpgradesConstrucoes();
    }

	// Use this for initialization
	void Start () {

        //inicializa propriedadesDinheiro
        propriedadesDinheiro = GameObject.FindGameObjectWithTag("propriedadesDinheiro").GetComponent<PropriedadesDinheiro>();

        //inicializa propriedadesConstrucoes
        propriedadesConstrucoes = GameObject.FindGameObjectWithTag("propriedadesConstrucoes").GetComponent<PropriedadesConstrucoes>();

        //inicializa displayUpgradesLoja
        displayUpgradesLoja = GameObject.FindGameObjectWithTag("panelUpgrades").GetComponent<DisplayUpgradesLoja>();
    }
	
	// Update is called once per frame
	void Update () {

        //atualiza os upgrades da loja
        atualizaLoja();
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
                //spritesUpgradesConstrucoes[i, j] = sprites[aux];
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

    //cria e insere upgrades na lista de upgrades que estao fora da loja
    //eh chamada no inicio do jogo, colocando todos os upgrades na lista dos fora da loja
    private void insereUpgradesNaListaForaLoja()
    {
        //upgrades de construcao
        //para cada tipo de construcao
        for (int i = 0; i < numeroDeConstrucoes; i++)
        {
            //para cada upgrade de uma certa construcao
            for (int j = 0; j < numeroDeCoresConstrucoes; j++)
            {
                //cria upgrade
                Upgrade upgrade = new Upgrade();
                upgrade.linha = i;
                upgrade.coluna = j;
                upgrade.preco = precoUpgradesConstrucoes[i, j];
                upgrade.condicaoConstrucao = condicoesConstrucoes[j];
                upgrade.tipo = Upgrade.tiposDeUpgrade.construcao;

                //adiciona upgrade ah lista dos fora da loja
                upgradesForaLoja.Add(upgrade);
            }
        }

        //upgrades de aumento da taxa de dinheiros por segundo
        for(int i = 0; i < numeroAumentoTaxa; i++)
        {
            //cria upgrade
            Upgrade upgrade = new Upgrade();
            upgrade.index = i;
            upgrade.preco = precoAumentosTaxa[i];
            upgrade.condicaoDinheiro = condicoesAumentosTaxa[i];
            upgrade.tipo = Upgrade.tiposDeUpgrade.taxa;

            //adiciona upgrade ah lista dos fora da loja
            upgradesForaLoja.Add(upgrade);
        }

        //upgrades de aumento do dinheiro ganho no clique do icone
        for (int i = 0; i < numeroAumentoClique; i++)
        {
            //cria upgrade
            Upgrade upgrade = new Upgrade();
            upgrade.index = i;
            upgrade.preco = precoAumentosClique[i];
            upgrade.condicaoDinheiro = condicoesAumentosClique[i];
            upgrade.tipo = Upgrade.tiposDeUpgrade.clique;

            //adiciona upgrade ah lista dos fora da loja
            upgradesForaLoja.Add(upgrade);
        }

        //upgrades de aumento da frequencia de aparecimento da golden coin
        for (int i = 0; i < numeroFrequenciaGoldenCoin; i++)
        {
            //cria upgrade
            Upgrade upgrade = new Upgrade();
            upgrade.index = i;
            upgrade.preco = precoAumentosFrequenciaGoldenCoin[i];
            upgrade.condicaoDinheiro = condicoesAumentosFrequenciaGoldenCoin[i];
            upgrade.tipo = Upgrade.tiposDeUpgrade.goldenCoin;

            //adiciona upgrade ah lista dos fora da loja
            upgradesForaLoja.Add(upgrade);
        }
    }

    //insere upgrade na lista dos upgrades que estao na loja
    //insere esse upgrade de forma que a ordem da lista se mantenha ( do mais barato para o mais caro )
    private void insereUpgradeNaListaLoja( Upgrade upgrade )
    {
        upgradesLoja.Add(upgrade);

        //ordena lista de upgrades na loja
        upgradesLoja.Sort((x, y) => x.preco.CompareTo(y.preco));
    }

    //retira um upgrade da lista de upgrades que estarao na loja
    public void removeUpgradeDaListaLoja( Upgrade upgrade )
    {
        //remove upgrade
        upgradesLoja.Remove(upgrade);

        //atualiza display da loja
        displayUpgradesLoja.atualizaLoja();
    }

    //checa se o upgrade pode entrar na loja
    private bool checaCondicoesLoja( Upgrade upgrade )
    {
        switch( upgrade.tipo)
        {
            case Upgrade.tiposDeUpgrade.clique:
            case Upgrade.tiposDeUpgrade.taxa:
            case Upgrade.tiposDeUpgrade.goldenCoin:
                //se a quantidade de dinheiro for maior ou igual ah condicao
                if(upgrade.condicaoDinheiro <= propriedadesDinheiro.dinheiro)
                {
                    return true;
                }
                break;
            
            case Upgrade.tiposDeUpgrade.construcao:
                //se o numero de construcoes for maior ou igual ah condicao
                if( upgrade.condicaoConstrucao <= propriedadesConstrucoes.quantidadesConstrucoes[upgrade.linha] )
                {
                    return true;
                }
                break;

            default: break;
        }

        return false;
    }

    //checa dentre os upgrades que nao estao na loja quais podem entrar
    //insere na lista da loja aqueles que ja podem entrar
    private void atualizaLoja()
    {
        for(int i = 0; i < upgradesForaLoja.Count; i++)
        {
            Upgrade upgrade = upgradesForaLoja[i];

            //se satisfizer a condicao de entrada
            if( checaCondicoesLoja(upgrade))
            {
                insereUpgradeNaListaLoja(upgrade);

                //tira o upgrade da lista dos que nao estao na loja
                upgradesForaLoja.RemoveAt(i);

                //atualiza display da loja
                displayUpgradesLoja.atualizaLoja();
            }
        }
    }
}
