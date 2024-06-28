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

            for (int i = 0; i < qntJogadores; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int indiceArrayPeoes = (i * 4 + j);
                    this.Peoes[indiceArrayPeoes] = new Peao(Helpers.seqPeoes[i], (indiceArrayPeoes), new Posicao(Helpers.posBasePeoes[indiceArrayPeoes, 0], Helpers.posBasePeoes[indiceArrayPeoes, 1]));
                }
            }            
        }

        public bool JogadorGanhouOJogo(string corJogador)
        {
            Peao[] peoes = this.PeoesDoJogador(corJogador);

            Peao[] peoesFinalizados = peoes.Where(x => x.Status == 2).ToArray();

            return peoesFinalizados.Length == 4;
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

        public Peao[] PosicaoEstaOcupada(Posicao p)
        {
            Peao[] peoes = new Peao[16];
            int cont = 0;
            foreach(Peao peao in this.Peoes)
            {
                if ( (peao.Status > 0) && peao.Posicao.Linha == p.Linha && peao.Posicao.Coluna == p.Coluna)
                {
                    peoes[cont] = peao;
                    cont++;
                }
            }
            

            Peao[] retornoPeoes = new Peao[cont];
            int cont2 = 0;
            foreach(Peao peao in peoes)
            {
                if (peao != null)
                {
                    retornoPeoes[cont2] = peao;
                    cont2++;
                }
            }

            return retornoPeoes;
        }

        public void ImprimeTabuleiro()
        {
            //15 x 15
            string legenda = "";

            Console.WriteLine("\n----------------------------------------------------------------------");

            for (int i=0; i<15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (i == 0 && j == 2)
                    {
                        Console.Write(" G");
                        continue;
                    }
                    else if (i == 0 && j == 13)
                    {
                        Console.Write("Y ");
                        continue;
                    }
                    else if (i == 10 && j == 2)
                    {
                        Console.Write(" R");
                        continue;
                    }
                    else if (i == 10 && j == 13)
                    {
                        Console.Write("B ");
                        continue;
                    }

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
                        Peao[] temp = PosicaoEstaOcupada(posicaoAtual);
                        if (temp.Length == 1)
                        {
                            Console.Write($" {temp[0].Cor}{temp[0].Id} ");
                        }else if (temp.Length > 1)
                        {
                            legenda += "\n@@";
                            foreach(Peao p in temp)
                            {
                                legenda += $" {p.Cor}{p.Id} / ";
                            }
                            Console.Write(" @@ ");
                            legenda += "\n";
                        }
                        else
                        {
                            Console.Write(" __ ");
                        }
                    }
                }

                Console.WriteLine();
            }

            if (legenda != null && legenda != "")
            {
                Console.WriteLine("\nLegenda (Casas Compartilhadas)");
                Console.WriteLine(legenda);
            }

            Console.WriteLine("\n----------------------------------------------------------------------");
        }


        
    }
}
