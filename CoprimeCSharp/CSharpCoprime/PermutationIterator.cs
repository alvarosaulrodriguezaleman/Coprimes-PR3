using System;
namespace CSharpCoprime
{
    public class PermutationIterator
    {
        private bool[] comb;
        private bool hasNext;

        public PermutationIterator(int size)
        {
            hasNext = (size != 0);
            comb = new bool[size];
        }

        public bool HasNext() 
        {
            return hasNext;
        }

        public bool[] Next() {
            if (!hasNext) 
            {
                throw new InvalidOperationException(
                    "No quedan más permutaciones posibles");
            }

            // Generamos una nueva permutación
            for (int i = 0; i < comb.Length; i++){
                if (!comb[i]) 
                {
                    comb[i] = true;
                    break;
                }
                comb[i] = false;
            }

            // Comprobamos que el array de permutaciones sigue teniendo espacio
            hasNext = false;
            bool b;
            for (int i = 0; i < comb.Length; i ++) 
            {
                b = comb[i];

                if (b == false) 
                {
                    hasNext = true;
                    break;
                }
            }
            return comb;
        }
    }
}
