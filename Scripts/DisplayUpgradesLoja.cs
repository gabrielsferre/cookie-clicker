using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//classe atribuida ao panel onde ficam os upgrades
public class DisplayUpgradesLoja : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    //dimensoes do panel quando o mouse nao esta sobre ele
    private Vector2 dimensoesPanelPadrao;

    //dimensoes dos botoes de upgrades
    private Vector2 dimensoesUpgrade;

    //numero de botoes por linha
    private int botoesPorLinha = 4;

    //lista com os botoes
    List<GameObject> listaBotoes = new List<GameObject>();

    //prefab de botao que sera usado para criar os botoes de upgrade
    public GameObject prefabBotao;

    //script que controla os upgrades
    protected PropriedadesUpgrades propriedadesUpgrades;

    // Use this for initialization
    void Start()
    {

        //inicializa propriedadesUpgrades
        propriedadesUpgrades = GameObject.FindGameObjectWithTag("propriedadesUpgrades").GetComponent<PropriedadesUpgrades>();

        //inicializa dimensoes do panel e dos icones dos upgrades
        inicializaDimensoes();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }**/

    //inicializa dimensoes do panel e dos icones dos upgrades
    private void inicializaDimensoes()
    {
        dimensoesPanelPadrao = GetComponent<RectTransform>().sizeDelta;

        dimensoesUpgrade = new Vector2(dimensoesPanelPadrao.x / botoesPorLinha, dimensoesPanelPadrao.y);
    }


    public void atualizaLoja()
    {
        //apaga todos os botoes
        apagaBotoes();

        //insere botoes de novo
        insereBotoesMenor();
    }

    //apaga todos os botoes da loja
    private void apagaBotoes()
    {

        for (int i = 0; i < listaBotoes.Count; i++)
        {
            Destroy(listaBotoes[i]);
            listaBotoes.RemoveAt(i);
        }
    }

    //insere botoes no panel quando ele esta em sua forma reduzida
    private void insereBotoesMenor()
    {
        //upgrades que devem estar na loja
        List<Upgrade> listaUpgrades = propriedadesUpgrades.upgradesLoja;

        //usado para incrementar a posicao na qual os botoes sao colocados na tela
        float incrementoX = 0;

        for (int i = 0; i < listaUpgrades.Count && i < botoesPorLinha; i++)
        {
            //cria botao
            GameObject botao = Instantiate(prefabBotao, gameObject.transform);

            //adiciona botao ah lista de botoes da loja
            listaBotoes.Add(botao);

            //define posicao do botao
            Vector3 posicao = new Vector3( (dimensoesUpgrade.x - dimensoesPanelPadrao.x)/2 + incrementoX, 0, 0);
            botao.GetComponent<RectTransform>().anchoredPosition = posicao;

            Upgrade upgrade = listaUpgrades[i];

            //atribui um scrip ao botao
            switch ( upgrade.tipo)
            {
                case Upgrade.tiposDeUpgrade.clique:
                    botao.AddComponent<ControleBotaoUpgradeClique>();
                    botao.GetComponent<ControleBotaoUpgradeClique>().upgrade = upgrade;
                    break;
                case Upgrade.tiposDeUpgrade.construcao:
                    botao.AddComponent<ControleBotaoUpgradeConstrucao>();
                    botao.GetComponent<ControleBotaoUpgradeConstrucao>().upgrade = upgrade;
                    break;
                case Upgrade.tiposDeUpgrade.goldenCoin:
                    botao.AddComponent<ControleBotaoUpgradeGoldenCoin>();
                    botao.GetComponent<ControleBotaoUpgradeGoldenCoin>().upgrade = upgrade;
                    break;
                case Upgrade.tiposDeUpgrade.taxa:
                    botao.AddComponent<ControleBotaoUpgradeTaxa>();
                    botao.GetComponent<ControleBotaoUpgradeTaxa>().upgrade = upgrade;
                    break;
                default:
                    break;
            }

            //define posicao para o proximo botao
            incrementoX += dimensoesUpgrade.x;
        }
    }
}
