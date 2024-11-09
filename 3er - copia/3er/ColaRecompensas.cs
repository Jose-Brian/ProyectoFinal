using System;
using System.Collections.Generic;

namespace _3er
{
    public class ColaRecompensas
    {
        private Queue<int> recompensas;

        public ColaRecompensas()
        {
            recompensas = new Queue<int>();

            // Añadimos algunas recompensas de vida iniciales
            recompensas.Enqueue(10);
            recompensas.Enqueue(15);
            recompensas.Enqueue(20);
            recompensas.Enqueue(25);
        }

        public int ObtenerRecompensa()
        {
            return recompensas.Count > 0 ? recompensas.Dequeue() : 10;
        }

        public void AgregarRecompensa(int recompensa)
        {
            recompensas.Enqueue(recompensa);
        }

        public bool HayRecompensas()
        {
            return recompensas.Count > 0;
        }
    }
}
