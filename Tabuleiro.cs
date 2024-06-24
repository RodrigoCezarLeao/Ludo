using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public class Tabuleiro
    {
        public Peao[] Peoes { get; set; }
        public int QntJogadores { get; set; }
        public Tabuleiro(int qntJogadores)
        {
            this.QntJogadores = qntJogadores;
            this.Peoes = new Peao[qntJogadores * 4];

            
            int[,] posBasePeoes = { {1,1}, {1,3}, {3,1}, {3,3}, {1,11}, {1,13}, {3,11}, {3,13}, {11,11}, {11,13}, {13,11}, {13,13}, {11,1}, {11,3}, {13,1}, {13,3} };
            for (int i = 0; i < qntJogadores; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int indiceArrayPeoes = (i * 4 + j);
                    this.Peoes[indiceArrayPeoes] = new Peao(Helpers.seqPeoes[i], (indiceArrayPeoes + 1), new Posicao(posBasePeoes[indiceArrayPeoes, 0], posBasePeoes[indiceArrayPeoes, 1]));
                }
            }            
        }

        public Peao[] PeoesDoJogador(string jogador)
        {
            return this.Peoes.Where(x => x.Cor == jogador).ToArray();
        }

        public Peao[] PeoesDoJogadorQueEstaoNaBase(string jogador)
        {
            return this.Peoes.Where(x => x.Cor == jogador && x.Status == 0).ToArray();
        }
        public string ImprimirPeoesDaBase(string jogador)
        {
            Peao[] temp = this.PeoesDoJogadorQueEstaoNaBase(jogador);
            return string.Join(", ", temp.Select(x => x.Cor + x.Id));
        }
        public bool JogadorTemPeaoNaBase(string jogador)
        {
            return this.Peoes.Where(x => x.Cor == jogador && x.Status == 0).ToArray().Length > 0;
        }
        public Peao[] PeoesDoJogadorQuePodemAndarNormalmente(string jogador)
        {
            return this.Peoes.Where(x => x.Cor == jogador && (x.Status == 1 || x.Status == 4)).ToArray();
        }
        public string ImprimirPeoesQuePodemAndarNormalmente(string jogador)
        {
            Peao[] temp = this.PeoesDoJogadorQuePodemAndarNormalmente(jogador);
            return string.Join(", ", temp.Select(x => x.Cor + x.Id));
        }
        public bool JogadorTemPeaoQuePodemAndarNormalmente(string jogador)
        {
            return this.Peoes.Where(x => x.Cor == jogador && (x.Status == 1 || x.Status == 4)).ToArray().Length > 0;
        }
        public Peao[] PeoesDoJogadorQuePodemTerminar(string jogador, int valorDado)
        {
            return this.Peoes.Where(x => x.Cor == jogador && x.PeaoPodeTerminar(valorDado)).ToArray();
        }
        public string ImprimirPeoesQuePodemTerminar(string jogador, int valorDado)
        {
            Peao[] temp = this.PeoesDoJogadorQuePodemTerminar(jogador, valorDado);
            return string.Join(", ", temp.Select(x => x.Cor + x.Id));
        }
        public bool JogadorTemPeaoQuePodemTerminar(string jogador, int valorDado)
        {
            return this.Peoes.Where(x => x.Cor == jogador && x.PeaoPodeTerminar(valorDado)).ToArray().Length > 0;
        }

        public Peao? SelecionarPeaoPeloId(string? id)
        {
            return this.Peoes.FirstOrDefault(x => x.Id == id);
        }

        public Peao? PosicaoEstaOcupada(Posicao p)
        {
            foreach(Peao peao in this.Peoes)
            {
                if ( (peao.Status > 0) && peao.Posicao.Linha == p.Linha && peao.Posicao.Coluna == p.Coluna)
                {
                    return peao;
                }
            }

            return null;
        }

        public void ImprimeTabuleiro()
        {
            //15 x 15

            Console.WriteLine("\n----------------------------------------------------------------------");

            for (int i=0; i<15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Posicao posicaoAtual = new Posicao(i, j);

                    int num = Helpers.CoordenadaParaPosicaoSequencial(posicaoAtual);

                    if (num == -1)
                    {
                        bool flag = true;
                        foreach(Peao peao in this.Peoes)
                        {
                            if (peao.Posicao.Linha == posicaoAtual.Linha && peao.Posicao.Coluna == posicaoAtual.Coluna)
                            {
                                Console.Write($" {peao.Cor}{peao.Id} ");
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            Console.Write("    ");
                        }
                    }else
                    {
                        Peao? temp = PosicaoEstaOcupada(posicaoAtual);
                        if (temp != null)
                        {
                            Console.Write($" {temp.Cor}{temp.Id} ");
                        }else
                        {
                            Console.Write(" __ ");
                        }
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("\n----------------------------------------------------------------------");
        }


        
    }
}
