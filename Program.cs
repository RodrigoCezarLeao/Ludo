using System;

namespace Ludo
{
    public class Ludo
    {
        public static void Main()
        {
            Tabuleiro t = new Tabuleiro();
            t.ImprimeTabuleiro();

            while (true)
            {                
                int resultadoJogada = t.Peoes[0].JogarDado();
                if (resultadoJogada == 1)
                {
                    t.Peoes[0].JogarDado();
                }
                    
                if (resultadoJogada > 0)
                {
                    t.ImprimeTabuleiro();
                }
                          
            }


            Console.ReadLine();
        }
    }
}