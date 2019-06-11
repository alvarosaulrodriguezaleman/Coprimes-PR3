class MyIterator():

    #Inicializa el iterador con un array de booleanos para obtener combinaciones
    def __init__(self, size):
        self.hasNext = bool(size)
        self.comb = [False] * size

    def __iter__(self):
        return self

    #Devuelve la siguiente combinación de la iteración
    def __next__(self):
        if self.hasNext:

            for pos, i in enumerate(self.comb):
                if not i:
                    self.comb[pos] = True
                    break
                else:
                    self.comb[pos] = False

            #Si todos los valores son True se paran las combinaciones
            self.hasNext = False
            for i in self.comb:
                if not i:
                    self.hasNext = True
                    break
                    
            #Devuelve la siguiente combinación
            return self.comb

        else:
            raise StopIteration

    def hasNext(self):
        return self.hasNext

    def next(self):
        return self.__next__()