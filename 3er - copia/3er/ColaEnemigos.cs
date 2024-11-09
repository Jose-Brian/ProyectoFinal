using System;
using System.Collections.Generic;

namespace _3er
{
    public class ColaEnemigos
    {
        private Queue<string> enemigos;

        public ColaEnemigos()
        {
            enemigos = new Queue<string>();
        }

        public void AgregarEnemigo(string enemigo)
        {
            enemigos.Enqueue(enemigo);
        }

        public string ProximoEnemigo()
        {
            return enemigos.Count > 0 ? enemigos.Dequeue() : null;
        }

        public bool HayEnemigos()
        {
            return enemigos.Count > 0;
        }
    }
}
