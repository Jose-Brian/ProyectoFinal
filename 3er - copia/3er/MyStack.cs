using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3er
{
    public class MyStack<T>
    {
        private List<T> items = new List<T>();

        public void Push(T item)
        {
            items.Add(item);
        }

        public T Pop()
        {
            if (items.Count == 0)
                throw new InvalidOperationException("Está vacío");
            T item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return item;
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }


        public void ClearM()
        {
            items.Clear();
        }
    }

}
