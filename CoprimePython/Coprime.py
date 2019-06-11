from Iterator import MyIterator

class MyNumbers():

    def __init__(self, items):
        self.items = items

    #Método que se encarga de la ejecución de fuerza bruta
    def getCombination(self, output = False):
        obj = MyIterator(len(self.items))
        itert = iter(obj)
        try:
            #Mientras existan más combinaciones se seguirá ejecutando
            while itert.hasNext:
                p = next(itert)
                #Generadora que devuelve la combinación
                aux = [ self.items[pos] for pos in range(0,len(p)) if p[pos]]
                if self.checkList(aux) and output and len(aux) > 1:
                    #Si son coprimos y deseamos mostrarlo, mostramos la combinación
                    print(aux)

        except StopIteration as e:
            print("No more iterations possibles")

    #Comprueba que los elementos de lista son coprimos
    def checkList(self,aux):
        for pos in range(0, len(aux)):
            for posNext in range(pos + 1, len(aux)):
                if self.checkCoprime(aux[pos], aux[posNext]) != 1:
                    return False
        return True

    #Comprueba si dos números son coprimos
    def checkCoprime(self, a, b):
        while b != 0:
            aux = a
            a = b
            b = aux % b
        return a
