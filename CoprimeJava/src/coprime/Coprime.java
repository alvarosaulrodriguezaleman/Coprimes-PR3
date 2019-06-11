package coprime;

import static coprime.NumberFileManager.*;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Iterator;

public class Coprime implements Iterable {
    private ArrayList<Integer> arr; // ArrayList de valores
    private static boolean fi = false; // Flag de muestra de entrada
    private static boolean fr = false; // Flag de muestra de resultados
    private static boolean ft = false; // Flag de muestra de tiempo de computación
    private static boolean c = false;  // Flag de limpieza de fichero de resultados
    
    public Coprime(String path) {
        try {
            arr = new NumberFileManager().readFromFile(path);
        } catch (IOException ex) {
            System.out.println("Can't read data from file: " + path);
            arr = new ArrayList<>();
        }
    }
    
    public static void main(String[] args) {
        if (args.length == 0) {
            System.out.println("Syntax: java -jar Coprime.jar [-fi | -fr | -ft | -c] input_filename");
            System.out.println("(You can input more than one filename at once)");
            System.out.println("-fi: Show read numbers from file(s)");
            System.out.println("-fr: Show results (the sets of numbers in which all are coprime)");
            System.out.println("-ft: Show time spent computing the results");
            System.out.println("-c:  Clear the results log file before writing new results");
            return;
        }
        ArrayList<String> paths = new ArrayList();
        for (String arg : args) {
            switch (arg) {
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
            paths.add(arg);
        }
        
        if (c) cleanResults();
        
        // Procesamos cada uno de los ficheros introducidos por parámetro
        for (String string : paths) {
            if (paths.size() > 1) {
                System.out.println("Reading from file: " + string);
            }
            Coprime c = new Coprime(string);
            if (fi) c.showInput(); // Mostramos los valores leídos del fichero

            Iterator pi = c.iterator();
            double t = System.currentTimeMillis();
            while (pi.hasNext()) {
                boolean[] b = (boolean[]) pi.next();
                // aux contiene el subconjunto de números representados por
                // la combinación.
                ArrayList<Integer> aux = c.parseCombinationValues(b);
                if (c.isCoprime(aux)) {
                    // Todos los números del subconjunto son coprimos
                    if (fr) System.out.println(aux);
                }
            }
            t = System.currentTimeMillis() - t;
            writeResult(string, t); // Escribimos el resultado en el fichero de resultados
            if (ft) System.out.println("Process time:\t" + t/1000);
        }
        
    }
    
    /**
     * Devuelve verdadero si todos los números del ArrayList arr son coprimos
     * entre sí, y devuelve falso si no.
     */
    public boolean isCoprime(ArrayList<Integer> arr) {
        if (arr.size() <= 1) {
            return false;
        }
        for (int i = 0; i < arr.size(); i++) {
            for (int j = i + 1; j < arr.size(); j++) {
                if (isCoprimeAux(arr.get(i), arr.get(j)) != 1) {
                    return false;
                }
            }
        }
        return true;
    }
    
    /**
     * Método auxiliar que devuelve el máximo común divisor de dos números.
     */
    private static int isCoprimeAux(int a, int b) {
        int aux;
        while (b != 0) {
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
    public ArrayList<Integer> parseCombinationValues(boolean[] comb) {
        ArrayList<Integer> res = new ArrayList<>();
        for (int i = 0; i < comb.length; i++) {
            if (comb[i]) {
                res.add(arr.get(i));
            }
        }
        return res;
    }
    
    /**
     * Muestra por la salida estándar los valores leídos.
     */
    public void showInput() {
        System.out.println("*** Input");
        for (int i = 0; i < arr.size(); i++) {
            System.out.println(i + " => " + arr.get(i));
        }
        System.out.println("-------------------------------");
    }

    @Override
    public Iterator iterator() {
        return new PermutationIterator(arr.size());
    }
}

