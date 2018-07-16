using System.Collections;
using System.Collections.Generic;

public class Upgrade {

    //enum com os tipos de upgrade
    public enum tiposDeUpgrade {construcao, taxa, clique, goldenCoin };

    //index do upgrade no array de upgrades
    //usado em todos os tipos de upgrades a nao ser no de construcoes
    public int index;

    //linha e coluna do upgrade na matriz de upgrade
    //usado apenas em upgrades de construcoes
    public int linha;
    public int coluna;

    //preco do upgrade
    public float preco;

    //condicoes para o upgrade ir para a loja
    //se nao for um upgrade de construcao
    //quantidade de dinheiro do jogador
    public float condicaoDinheiro;

    //se for um upgrade de construcao
    //numero de construcoes necessarias
    public int condicaoConstrucao;

    //tipo de upgrade
    public tiposDeUpgrade tipo;
}
