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
        public Tabuleiro() 
        { 
            this.Peoes = new Peao[1];
            this.Peoes[0] = new Peao("G", 1);
            //this.Peoes[0].Posicao = Helpers.PosicaoSequencialParaCoordenada(54);
            //this.Peoes[0].Status = 3;

            /*this.Peoes = new Peao[16];
            for (int i = 0; i<4; i++)
            {
                this.Peoes[i*4] = new Peao("G", i * 4);
                this.Peoes[i*4 + 1] = new Peao("Y", i * 4 + 1);
                this.Peoes[i*4 + 2] = new Peao("B", i * 4 + 2);
                this.Peoes[i*4 + 3] = new Peao("R", i * 4 + 3);
            }*/
            
        }

        public string PosicaoEstaOcupada(Posicao p)
        {
            foreach(Peao peao in this.Peoes)
            {
                if ( (peao.Status > 0) && peao.Posicao.Linha == p.Linha && peao.Posicao.Coluna == p.Coluna)
                {
                    return peao.Cor;
                }
            }

            return "";
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
                        Console.Write("   ");
                    }else
                    {
                        string temp = PosicaoEstaOcupada(posicaoAtual);
                        if (temp != "")
                        {
                            Console.Write($" {temp} ");
                        }else
                        {
                            Console.Write(" _ ");
                        }
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("\n----------------------------------------------------------------------");
        }


        
    }
}
