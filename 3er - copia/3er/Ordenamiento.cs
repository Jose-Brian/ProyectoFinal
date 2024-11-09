using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3er
{
    public class Ordenamiento
    {
        public void InsertionSortDescendente(ref List<int> lista)
        {
            for (int i = 1; i < lista.Count; i++)
            {
                int num = lista[i];
                int j = i - 1;

                
                while (j >= 0 && lista[j] < num)
                {
                    lista[j + 1] = lista[j];
                    j--;
                }

                lista[j + 1] = num;
            }
        }

        public void MostrarPuntuacionesDescendente(NodoAVL raiz)
        {
            if (raiz == null)
            {
                Console.WriteLine("No hay puntuaciones para mostrar.");
                return;
            }

            List<int> puntuaciones = new List<int>();
            ExtraerPuntuaciones(raiz, puntuaciones);

            InsertionSortDescendente(ref puntuaciones);

            foreach (var puntuacion in puntuaciones)
            {
                Console.WriteLine($"Puntuación: {puntuacion}");
            }
        }
        private void ExtraerPuntuaciones(NodoAVL nodo, List<int> puntuaciones)
        {
            if (nodo != null)
            {
                ExtraerPuntuaciones(nodo.Izquierda, puntuaciones);
                puntuaciones.Add(nodo.Puntuacion);
                ExtraerPuntuaciones(nodo.Derecha, puntuaciones);
            }
        }
    }
}
