using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public static class Helpers
    {
        public static string[] seqPeoes = new string[4] { "G", "Y", "B", "R" };
        public static int[] casasSegurasGerais = new int[8] {1, 9, 14, 22, 27, 35, 40, 48};    
        public static int[,] posBasePeoes = { { 1, 1 }, { 1, 3 }, { 3, 1 }, { 3, 3 }, { 1, 11 }, { 1, 13 }, { 3, 11 }, { 3, 13 }, { 11, 11 }, { 11, 13 }, { 13, 11 }, { 13, 13 }, { 11, 1 }, { 11, 3 }, { 13, 1 }, { 13, 3 } };
        public static Posicao PosicaoSequencialParaCoordenada(int num)
        {
            Posicao p = new Posicao();
            switch (num)
            {
                case 0:
                    p.Linha = 6;
                    p.Coluna = 0;
                    break;
                case 1:
                    p.Linha = 6;
                    p.Coluna = 1;
                    break;
                case 2:
                    p.Linha = 6;
                    p.Coluna = 2;
                    break;
                case 3:
                    p.Linha = 6;
                    p.Coluna = 3;
                    break;
                case 4:
                    p.Linha = 6;
                    p.Coluna = 4;
                    break;
                case 5:
                    p.Linha = 6;
                    p.Coluna = 5;
                    break;
                case 6:
                    p.Linha = 5;
                    p.Coluna = 6;
                    break;
                case 7:
                    p.Linha = 4;
                    p.Coluna = 6;
                    break;
                case 8:
                    p.Linha = 3;
                    p.Coluna = 6;
                    break;
                case 9:
                    p.Linha = 2;
                    p.Coluna = 6;
                    break;
                case 10:
                    p.Linha = 1;
                    p.Coluna = 6;
                    break;
                case 11:
                    p.Linha = 0;
                    p.Coluna = 6;
                    break;
                case 12:
                    p.Linha = 0;
                    p.Coluna = 7;
                    break;
                case 13:
                    p.Linha = 0;
                    p.Coluna = 8;
                    break;
                case 14:
                    p.Linha = 1;
                    p.Coluna = 8;
                    break;
                case 15:
                    p.Linha = 2;
                    p.Coluna = 8;
                    break;
                case 16:
                    p.Linha = 3;
                    p.Coluna = 8;
                    break;
                case 17:
                    p.Linha = 4;
                    p.Coluna = 8;
                    break;
                case 18:
                    p.Linha = 5;
                    p.Coluna = 8;
                    break;
                case 19:
                    p.Linha = 6;
                    p.Coluna = 9;
                    break;
                case 20:
                    p.Linha = 6;
                    p.Coluna = 10;
                    break;
                case 21:
                    p.Linha = 6;
                    p.Coluna = 11;
                    break;
                case 22:
                    p.Linha = 6;
                    p.Coluna = 12;
                    break;
                case 23:
                    p.Linha = 6;
                    p.Coluna = 13;
                    break;
                case 24:
                    p.Linha = 6;
                    p.Coluna = 14;
                    break;
                case 25:
                    p.Linha = 7;
                    p.Coluna = 14;
                    break;
                case 26:
                    p.Linha = 8;
                    p.Coluna = 14;
                    break;
                case 27:
                    p.Linha = 8;
                    p.Coluna = 13;
                    break;
                case 28:
                    p.Linha = 8;
                    p.Coluna = 12;
                    break;
                case 29:
                    p.Linha = 8;
                    p.Coluna = 11;
                    break;
                case 30:
                    p.Linha = 8;
                    p.Coluna = 10;
                    break;
                case 31:
                    p.Linha = 8;
                    p.Coluna = 9;
                    break;
                case 32:
                    p.Linha = 9;
                    p.Coluna = 8;
                    break;
                case 33:
                    p.Linha = 10;
                    p.Coluna = 8;
                    break;
                case 34:
                    p.Linha = 11;
                    p.Coluna = 8;
                    break;
                case 35:
                    p.Linha = 12;
                    p.Coluna = 8;
                    break;
                case 36:
                    p.Linha = 13;
                    p.Coluna = 8;
                    break;
                case 37:
                    p.Linha = 14;
                    p.Coluna = 8;
                    break;
                case 38:
                    p.Linha = 14;
                    p.Coluna = 7;
                    break;
                case 39:
                    p.Linha = 14;
                    p.Coluna = 6;
                    break;
                case 40:
                    p.Linha = 13;
                    p.Coluna = 6;
                    break;
                case 41:
                    p.Linha = 12;
                    p.Coluna = 6;
                    break;
                case 42:
                    p.Linha = 11;
                    p.Coluna = 6;
                    break;
                case 43:
                    p.Linha = 10;
                    p.Coluna = 6;
                    break;
                case 44:
                    p.Linha = 9;
                    p.Coluna = 6;
                    break;
                case 45:
                    p.Linha = 8;
                    p.Coluna = 5;
                    break;
                case 46:
                    p.Linha = 8;
                    p.Coluna = 4;
                    break;
                case 47:
                    p.Linha = 8;
                    p.Coluna = 3;
                    break;
                case 48:
                    p.Linha = 8;
                    p.Coluna = 2;
                    break;
                case 49:
                    p.Linha = 8;
                    p.Coluna = 1;
                    break;
                case 50:
                    p.Linha = 8;
                    p.Coluna = 0;
                    break;
                case 51:
                    p.Linha = 7;
                    p.Coluna = 0;
                    break;
                case 52:
                    p.Linha = 7;
                    p.Coluna = 1;
                    break;
                case 53:
                    p.Linha = 7;
                    p.Coluna = 2;
                    break;
                case 54:
                    p.Linha = 7;
                    p.Coluna = 3;
                    break;
                case 55:
                    p.Linha = 7;
                    p.Coluna = 4;
                    break;
                case 56:
                    p.Linha = 7;
                    p.Coluna = 5;
                    break;
                case 72:
                    p.Linha = 7;
                    p.Coluna = 6;
                    break;
                case 57:
                    p.Linha = 1;
                    p.Coluna = 7;
                    break;
                case 58:
                    p.Linha = 2;
                    p.Coluna = 7;
                    break;
                case 59:
                    p.Linha = 3;
                    p.Coluna = 7;
                    break;
                case 60:
                    p.Linha = 4;
                    p.Coluna = 7;
                    break;
                case 61:
                    p.Linha = 5;
                    p.Coluna = 7;
                    break;
                case 73:
                    p.Linha = 6;
                    p.Coluna = 7;
                    break;
                case 62:
                    p.Linha = 7;
                    p.Coluna = 13;
                    break;
                case 63:
                    p.Linha = 7;
                    p.Coluna = 12;
                    break;
                case 64:
                    p.Linha = 7;
                    p.Coluna = 11;
                    break;
                case 65:
                    p.Linha = 7;
                    p.Coluna = 10;
                    break;
                case 66:
                    p.Linha = 7;
                    p.Coluna = 9;
                    break;
                case 74:
                    p.Linha = 7;
                    p.Coluna = 8;
                    break;
                case 67:
                    p.Linha = 13;
                    p.Coluna = 7;
                    break;
                case 68:
                    p.Linha = 12;
                    p.Coluna = 7;
                    break;
                case 69:
                    p.Linha = 11;
                    p.Coluna = 7;
                    break;
                case 70:
                    p.Linha = 10;
                    p.Coluna = 7;
                    break;
                case 71:
                    p.Linha = 9;
                    p.Coluna = 7;
                    break;
                case 75:
                    p.Linha = 8;
                    p.Coluna = 7;
                    break;


            }
            return p;
        }

        public static int CoordenadaParaPosicaoSequencial(Posicao p)
        {
            for (int num = 0; num < 76; num++)
            {
                Posicao temp = PosicaoSequencialParaCoordenada(num);
                if (temp.Linha == p.Linha && temp.Coluna == p.Coluna)
                {
                    return num;
                }
            }

            return -1;
        }

        public static int JogarDado()
        {
            Random random = new Random();
            int valorDado = random.Next(1, 7);
            return valorDado;            
        }

        public static void Aguarde(int segundos = 5)
        {
            Console.WriteLine($"...");
            Thread.Sleep(segundos * 1000);
        }

        public static void LogText(string text, ref string logBatch)
        {
            Console.WriteLine(text);
            logBatch += text + "\n";
        }
    }
}
