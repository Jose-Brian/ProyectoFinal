using System;

namespace _3er
{
    public class NodoAVL
    {
        public int Puntuacion { get; set; }
        public NodoAVL? Izquierda { get; set; } // Permitir valor nulo
        public NodoAVL? Derecha { get; set; } // Permitir valor nulo
        public int Altura { get; set; }

        public NodoAVL(int puntuacion)
        {
            Puntuacion = puntuacion;
            Altura = 1;
        }
    }

    public class ArbolAVL
    {
        public NodoAVL? Raiz { get; private set; } // Permitir valor nulo

        public ArbolAVL()
        {
            Raiz = null;
        }

        public void Insertar(int puntuacion)
        {
            Raiz = InsertarAVL(Raiz, puntuacion);
        }

        private NodoAVL InsertarAVL(NodoAVL? nodo, int puntuacion)
        {
            if (nodo == null)
                return new NodoAVL(puntuacion);

            if (puntuacion < nodo.Puntuacion)
                nodo.Izquierda = InsertarAVL(nodo.Izquierda, puntuacion);
            else
                nodo.Derecha = InsertarAVL(nodo.Derecha, puntuacion);

            nodo.Altura = 1 + Math.Max(ObtenerAltura(nodo.Izquierda), ObtenerAltura(nodo.Derecha));

            return nodo;
        }

        private int ObtenerAltura(NodoAVL? nodo)
        {
            return nodo == null ? 0 : nodo.Altura;
        }
    }
}
