using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
namespace CSharpCoprime
{
    class Coprime
    {
        private ArrayList arr;
        private static DateTime Jan1st1970 = new DateTime
                    (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static bool fi = false; // Flag de muestra de entrada
        private static bool fr = false; // Flag de muestra de resultados
        private static bool ft = false; // Flag de muestra de tiempo de computación
        private static bool c = false;  // Flag de limpieza de fichero de resultados

        public Coprime (String path) 
        {
            try
            {
                arr = new NumberFileManager().ReadFromFile(path);
            } catch (IOException)
            {
                Console.WriteLine("Can't read data from file: " + path);
                arr = new ArrayList();
            }
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("For macOS, first: mcs Coprime.cs NumberFileManager.cs PermutationIterator.cs");
                Console.WriteLine("Syntax: mono Coprime.exe [-fi | -fr | -ft | -c] input_filename");
                Console.WriteLine("(You can input more than one filename at once)");
                Console.WriteLine("-fi: Show read numbers from file(s)");
                Console.WriteLine("-fr: Show results (the sets of numbers in which all are coprime)");
                Console.WriteLine("-ft: Show time spent computing the results");
                Console.WriteLine("-c:  Clear the results log file before writing new results");
                return;
            }

            ArrayList paths = new ArrayList();
            String arg;
            for (int i = 0; i < args.Length; i++)
            {
                arg = args[i];
                switch (arg)
                {
                    case "-fi":
                        fi = true;
                        continue;
                    case "-fr":
                        fr = true;
                        continue;
                    case "-ft":
                        ft = true;
                        continue;
                    case "-c":
                        c = true;
                        continue;
                }
                paths.Add(arg);
            }

            if (c) new NumberFileManager().CleanResults();

            // Procesamos cada uno de los ficheros introducidos por parámetro
            String stringAux;
            for (int i = 0; i < paths.Count; i++)
            {
                stringAux = (String)paths[i];
                if (paths.Count > 1)
                {
                    Console.WriteLine("Reading from file: " + stringAux);
                }

                Coprime c = new Coprime(stringAux);
                if (fi) c.ShowInput(); // Mostramos los valores leídos del fichero
                            
                PermutationIterator pi = new PermutationIterator(c.arr.Count);

                double t = CurrentTimeMillis();
                while (pi.HasNext())
                {
                    bool[] b = (bool[])pi.Next();
                    // aux contiene el subconjunto de números representados por
                    // la combinación
                    ArrayList aux = c.ParseCombinationValues(b);
                    if (c.IsCoprime(aux))
                    {
                        // Todos los números del subconjunto son coprimos
                        if (fr) ToString(aux);
                    }
                }
                t = CurrentTimeMillis() - t;
                new NumberFileManager().WriteResult(stringAux, t); // Escribimos el resultado en el fichero de resultados
                if (ft) Console.WriteLine("Process time:\t" + t / 1000);
            }
        }

        // Adaptamos la combinación resultante a un formato agradable a la vista
        public static void ToString (ArrayList aux)
        {
            String res = "[";
            for (int i = 0; i < aux.Count; i ++)
            {
                res += aux[i] + ", ";
            }
            res = res.Substring(0, res.Length - 2);
            res += "]";
            Console.WriteLine(res);
        }

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        /** 
         * Devuelve verdadero si todos los números del ArryList arr son coprimos
         * entre sí, y devuelve falso si no.
         */
        public bool IsCoprime(ArrayList arr)
        {
            if (arr.Count <= 1)
            {
                return false;
            }
            for (int i = 0; i < arr.Count; i++)
            {
                for (int j = i + 1; j < arr.Count; j++)
                {
                    if (IsCoprimeAux((int)arr[i], (int)arr[j]) != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /** 
          * Método auxilir que devuelve el máximo común divisor de dos números.
          */
        private static int IsCoprimeAux(int a, int b)
        {
            int aux;
            while (b != 0)
            {
                aux = a;
                a = b;
                b = aux % b;
            }
            return a;
        }

        /** 
          * Devuelve un ArrayList de enteros con los elementos de arr representados
          * en la combinación comb.
          */
        public ArrayList ParseCombinationValues(bool[] comb)
        {
            ArrayList res = new ArrayList();
            for (int i = 0; i < comb.Length; i++)
            {
                if (comb[i])
                {
                    res.Add((int)arr[i]);
                }
            }
            return res;
        }

        /** 
         * Muestra por la salida estándar los valores leídos.
         */
        public void ShowInput()
        {
            Console.WriteLine("*** Input");
            for (int i = 0; i < arr.Count; i++)
            {
                Console.WriteLine(i + " => " + (int)arr[i]);
            }
            Console.WriteLine("-------------------------------");
        }
    }
}
