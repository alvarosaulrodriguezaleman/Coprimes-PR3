import time
import sys
from Coprime import MyNumbers

class ExecuteNumbers():

    def __init__(self,fi = False, fr = False, ft = False):
        self.fi = fi
        self.fr = fr
        self.ft = ft

    #Obtiene los tiempos de ejecución de los ficheros y escribe en un fichero el resultado
    def getTime(self):
        t = time.time()
        myclass = MyNumbers(self.numbers)
        myclass.getCombination(self.fr)
        runtime = time.time()-t
        result = "Test {} Process time: {}".format(self.filename,(runtime))
        self.writeResults(result,"PYTHON_RESULTS.txt")
        if self.ft:
            print(result)

    #Obtiene la ristra de valores del fichero pasado por parámetro
    def getNumbers(self,filename):
        self.filename = filename
        file = open(filename, 'r')
        self.numbers = [int(line) for line in file]
        file.close()
        if self.fi:
            print("Input ->")
            for num in self.numbers:
                print("=> ",num)
            print("-----------------")

    #Escribe en un fichero los resultados pasados por parámetro
    def writeResults(self,Str,filename):
        file = open(filename,'a')
        file.write(Str + "\n")
        file.close()

#Limpia el fichero pasado por parámetro
def cleanResults(filename):
    open(filename, 'w').close()

#Maneja la entrada de datos por consola
def manageInput(input = ()):
    if len(input) == 1:
        print("Syntax = Python Main.py {-fi|-fr|-ft|-c} filename.txt")
        print("You can input more than one filename at once")
        print("-fi: Show read numbers from files")
        print("-fr: Show result (the sets of numbers in which all are coprime)")
        print("-ft: Show time spent computing the results")
        print("-c: Clear the results log file before writing new results")
        return 1
    else:
        if "-c" in input:
            cleanResults("PYTHON_RESULTS.txt")
        exe = ExecuteNumbers("-fi" in input,"-fr" in input,"-ft" in input)
        filenames = [file for file in input if "txt" in file]
        if len(filenames) == 0:
            print("No se han insertado archivos donde leer")
        #Recorre los ficheros y los va ejecutando
        for file in filenames:
            exe.getNumbers(file)
            exe.getTime()

manageInput(sys.argv)