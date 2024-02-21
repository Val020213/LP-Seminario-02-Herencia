# Seminario II Herencia Múltiple

[Problema a implementar](./seminario_02_herencia.md)

## Implementación de la solución del problema

- [C#](./Csharp/Program.cs)
- [C++](./C++/main.cpp)
- [Python](./Python/main.ipynb)

## Solución propuesta en `C#`

Como en `C#` no se define la herencia múltiple, se utilizan interfaces para simular su comportamiento.
A continuación se muestra **_el diagrama de clases_** de la solución propuesta.

```mermaid
graph LR
    Persona --> Plantilla

    Plantilla --> Trabajador
    Plantilla --> Estudiante
    Trabajador --> Profesor

    IE[Interfaz Recibidor de Clases] --> Estudiante
    IP[Interfaz Impartidor de Clases] --> Profesor

    Profesor --> PA[Profesor Adiestrado]
    Estudiante --> AA[Alumno Ayudante]

    IE[Interfaz Recibidor de Clases] --> PA[Profesor Adiestrado]
    IP[Interfaz Impartidor de Clases] --> AA[Alumno Ayudante]
```

En una primera parte del problema tenemos que toda `Persona` se identifica por un nombre,
y que a su vez toda persona que recibe un pago es `Plantilla` de la universidad.

```csharp
 public class Persona
    {
        public string Nombre;
        public Persona(string nombre) { Nombre = nombre; }
    }

    public class Plantilla : Persona
    {
        public float Salario;
        public Plantilla(string nombre, float salario) : base(nombre)
        {
            Salario = salario;
        }
        public void CobrarSalario() => System.Console.WriteLine("Cobrar salario como plantilla");
    }

    public class Trabajador : Plantilla
    {
        public Trabajador(string nombre, float salario) : base(nombre, salario) { }
    }

```

Luego en el problema se introducen dos nuevas clases, `Estudiante` y `Trabajador`, ambas son plantilla de la universidad. Además, todo `Profesor` es un trabajador. Tanto el Estudiante como el Profesor deben implementar dos funciones: Recibir Clases e Impartir Clases, respectivamente.

Inicialmente, podríamos pensar en implementar estos métodos directamente en las clases Estudiante y Profesor. Sin embargo, más adelante se introducen dos nuevas clases: `Alumno Ayudante`, que es un estudiante capaz de impartir clases, y `Profesor Adiestrado`, que es un profesor capaz de recibir clases.

Para abordar esta situación, se introduce una `interfaz` para cada uno de estos métodos.

```csharp
   public interface IRecibidorDeClases
    {
        public int HorasRecibirClase { get; set; }
        public void RecibirClase() => Console.WriteLine("Recibiendo clase");
    }

    public interface IImpartidorDeClase
    {
        public int HorasImpartirClase { get; set; }
        public void ImpartirClase() => Console.WriteLine("Impartiendo clase");
    }
```

Finalmente, se implementan las clases `Estudiante`, `Profesor`, `Alumno Ayudante` y `Profesor Adiestrado`.

```csharp
    public class Profesor : Trabajador, IImpartidorDeClase
    {
        // se tiene que reedefinir la propiedad HorasImpartirClase
        public int HorasImpartirClase { get; set; }es
        public Profesor(string nombre, float salario, int horasClase) : base(nombre, salario)
        {
            HorasImpartirClase = horasClase;
        }
    }

    public class ProfesorAdiestrado : Profesor, IRecibidorDeClases
    {
        public int HorasRecibirClase { get; set; }
        public ProfesorAdiestrado(string nombre, float salario, int horasImpartirClase, int HorasRecibirClase) : base(nombre, salario, horasImpartirClase)
        {
            this.HorasRecibirClase = HorasRecibirClase;
        }
        public void RecibirClase() => Console.WriteLine("Recibiendo clase como profesor adiestrado");

    }

    public class Estudiante : Plantilla, IRecibidorDeClases
    {
        public int HorasRecibirClase { get; set; }

        public Estudiante(string nombre, float salario, int horasRecibirClase) : base(nombre, salario)
        {
            HorasRecibirClase = horasRecibirClase;
        }

    }
    public class AlumnoAyudante : Estudiante, IImpartidorDeClase
    {
        public int HorasImpartirClase { get; set; }
        public AlumnoAyudante(string nombre, float salario, int horasRecibirClase, int horasImpartirClase)
        : base(nombre, salario, horasRecibirClase)
        {
            HorasImpartirClase = horasImpartirClase;
        }

        public void ImpartirClase() => Console.WriteLine("Impartiendo clase como alumno ayudante");
    }
```

Como podemos observar al tener las propiedades `HorasImpartirClase` y `HorasRecibirClase` en las interfaces, cada clase que implemente las interfaces `IImpartidorDeClase` e `IRecibidorDeClases` tiene que redefinir estas propiedades. Lo cual puede ser un problema si se tiene que implementar muchas clases que sobre estas interfaces o si se tiene que cambiar el nombre de estas propiedades.
(No DRY)

Por otro lado la implentación dada cumple los requisitos, veamos el siguiente ejemplo:

```csharp
        Console.WriteLine("Probemos si las clases y las interfaces están bien definidas");
        AlumnoAyudante alumnoAyudante = new AlumnoAyudante("Alumno Ayudante", 1000, 10, 5);

        Console.WriteLine("Alumno ayudante se comporta como trabajador?");
        Console.WriteLine(alumnoAyudante is Trabajador); // false

        Console.WriteLine("Alumno ayudante se comporta como profesor?");
        Console.WriteLine(alumnoAyudante is Profesor); // false

        Console.WriteLine("Alumno ayudante se comporta como estudiante?");
        Console.WriteLine(alumnoAyudante is Estudiante); // true

        Console.WriteLine("Alumno ayudante se comporta como IImpartidorDeClase?");
        Console.WriteLine(alumnoAyudante is IImpartidorDeClase); // true
```

### Escenarios en `C#`

Para no pasar por alto varios conceptos importantes de `C#` en la herencia, proponemos varios escenarios para analizar. Puede que estos escenarios no sean comunes o esten forzados, pero nos var a permitir entender.

#### Escenario 1

A la universidad se le asignó un presupuesto para contratar el transporte de los `estudiantes becados`
y `profesores afectados`.
Ambas formas de coger el transporte son diferentes.

```csharp

    interface ITransporteEstudiante
    {
        public void CogerBus() => System.Console.WriteLine("Cogiendo el bus como estudiante");
    }

    interface ITransporteProfesor
    {
        public void CogerBus() => System.Console.WriteLine("Cogiendo el bus como profesor");
    }

```

Luego de un tiempo se decide que un grupo especial de personas puedan coger ambos tipos de transporte
y se les llama `PersonalTransportado`.

```csharp
    class PersonalTransportado : ITransporteEstudiante, ITransporteProfesor
    {
        public PersonalTransportado() { }
    }
```

Ahora, ¿Qué ocurre el siguiente código?

```csharp
    PersonalTransportado personalTransportado = new PersonalTransportado();
    personalTransportado.CogerBus(); // ¿Qué imprime?
```

```mermaid

graph LR
    ITransporteEstudiante -- CogerBus --> PersonalTransportado
    ITransporteProfesor -- CogerBus --> PersonalTransportado
```

El código anterior no compila, ya que el método `CogerBus` es ambiguo. Esto es un ejemplo de ambigüedad en la herencia múltiple.

Pero ¿Cómo podemos solucionar estos casos? Analicemos y comparemos las distintas soluciones.

En este problema queremos conservar ambos comportamientos por separado, y queremos que `PersonalTransportado` pueda coger ambos tipos de transporte por lo que no podemos simplemente eliminar un metodo `CogerBus` de una de las interfaces. En cambio:

```tsx
    PersonalTransportado personalTransportado = new PersonalTransportado();

    ((ITransporteEstudiante)personalTransportado).CogerBus(); // Cogiendo el bus como estudiante

    ((ITransporteProfesor)personalTransportado).CogerBus(); // Cogiendo el bus como profesor
```

#### Escenario 2

A la universidad se le asignó un presupuesto para contratar seguridad debido a la llegada
de personal extranjero con malas intenciones, y robos que han ocurrido recientemente.

Los `custodios`, como es costumbre, hacen guardia, y el personal de `seguridad` puede hacer guardia como custodio o
puede hacer guardia espantando turistas como seguridad. Se quiere además que el personal de seguridad y
los custodios no se confundan pues responden a diferentes jefes.

```csharp
    interface ICustodio
    {
        public void HacerGuardia() => System.Console.WriteLine("Haciendo guardia como custodio");
    }

    interface ISeguridad
    {
        public void HacerGuardia() => System.Console.WriteLine("Haciendo guardia como seguridad");
    }
```

Ahora, podemos definir `PersonalDeSeguridad` y `Custodio` como:

```csharp
    public class Seguridad : ICustodio, ISecurity
    {
        public Seguridad() { }
        void ICustodio.Guardia() => System.Console.WriteLine("Haciendo guardia como seguridad en el puesto de custodio");
        void ISeguridad.Guardia() => System.Console.WriteLine("Haciendo guardia como seguridad");
    }

    class Custodio : ICustodio
    {
        public Custodio() { }
        void ICustodio.Guardia() => System.Console.WriteLine("Haciendo guardia como custodio");
    }
```

Una vez más, tenemos un problema de colisión de interfaces. Y esta vez, lo solucionamos haciendo uso de la implementación explícita de interfaces, la podemos usar para solucionar problemas de herencia multiple o para definir explicitamente un comportamiento como ocurre con `Custodio`. Veamos:

```tsx
        Custodio custodio = new Custodio();

        // custodio.Guardia(); // Error de compilación debido a la delaración explícita de la interfaz
        ((ICustodio)custodio).Guardia(); // Haciendo guardia como custodio

        Seguridad seguridad = new Seguridad();
        ((ICustodio)seguridad).Guardia(); // Haciendo guardia como seguridad en el puesto de custodio
        ((ISecurity)seguridad).Guardia(); // Haciendo guardia como seguridad
```

Incluso podemos usar las interfaces como tipos:

```tsx
        ICustodio custodioDelta = new Seguridad();
        custodioDelta.Guardia();
```

#### Escenario 3

A veces no sabemos de antemano qué comportamiento va a tener un método, o queremos que se pueda redefinir en el futuro por alguna razón. Para esto existe `virtual` y `override` en `C#`.

```csharp
    class A
    {
        public virtual void Metodo() => System.Console.WriteLine("Metodo de A");
    }

    class B : A
    {
        public override void Metodo() => System.Console.WriteLine("Metodo de B");
    }
```

Luego, podemos hacer:

```csharp
    A a = new A();
    a.Metodo(); // Metodo de A

    B b = new B();
    b.Metodo(); // Metodo de B

    A ab = new B();
    ab.Metodo(); // Metodo de B

    B b2 = new B();
    ((A)b2).Metodo(); // Metodo de B
```

#### Escenario 4

Podemos ocultar un método de una clase base con la palabra clave `new`.

```csharp
    class C
    {
        public void Metodo() => System.Console.WriteLine("Metodo de C");
    }

    class D : C
    {
        public new void Metodo() => System.Console.WriteLine("Metodo de D");
    }
```

Luego, podemos hacer:

```csharp
    C c = new C();
    c.Metodo(); // Metodo de C

    D d = new D();
    d.Metodo(); // Metodo de D

    C cd = new D();
    cd.Metodo(); // Metodo de C
```

Veamos que en el último caso, a diferencia de overrite en el escenario 3, el método de la clase base es llamado.

#### Escenario 5

En otros tenemos un clase que realmente no se debe materializar, pero que tiene comportamientos que queremos que se hereden.

```csharp
    abstract class E
    {
        public abstract void Metodo();
    }

    class F : E
    {
        public override void Metodo() => System.Console.WriteLine("Metodo de F");
    }
```

Este es un ejemplo de una clase abstracta. No se puede instanciar, pero se puede heredar. Y sus metodos abstractos deben ser implementados por las clases que la hereden, usando override.

### Visibilidad de miembros en `C#`

En C#, la visibilidad de un miembro de una clase se controla mediante modificadores de acceso. Los principales modificadores son public, private, protected e internal.

- public: El miembro es accesible desde cualquier parte del código.
- private: El miembro solo es accesible dentro de la propia clase.
- protected: El miembro es accesible dentro de la propia clase y sus clases derivadas.
- internal: El miembro es accesible solo dentro del mismo ensamblado.

En C#, los miembros de una interfaz son siempre públicos y no pueden tener modificadores de acceso.

## Solución propuesta en `C++`

En `C++` la herencia múltiple es soportada. A continuación se muestra `el diagrama de clases` de la solución propuesta.

```mermaid
graph LR
    Persona --> Plantilla
    Plantilla --> Trabajador
    Trabajador --> Profesor
    Profesor --> PA[Profesor Adiestrado]
    Plantilla --> Estudiante
    Estudiante --> AA[Alumno Ayudante]

    Persona --> IE[Recibidor Clases]
    Persona --> IP[Impartidor Clases]

    IE[Recibidor Clases] --> Estudiante
    IE[Recibidor Clases] --> PA[Profesor Adiestrado]
    IP[Impartidor Clases] --> Profesor
    IP[Impartidor Clases] --> AA[Alumno Ayudante]
```

A continuación se muestra la implementación de las clases en `C++`.

```cpp
    class Persona
    {
    public:
        string Nombre;
        Persona(string nombre) : Nombre(nombre) {}
    };

    class IRecibirClase : public Persona
    {
    public:
        int HorasImpartirClase;
        IRecibirClase(string nombre, int horasImpartirClase) : Persona(nombre) {}
        void RecibirClase() { cout << "Recibiendo clase" << endl; }
    };

    class IImpartirClase : public Persona
    {
    public:
        int HorasRecibirClase;
        IImpartirClase(string nombre, int horasRecibirClase) : Persona(nombre) {}
        void ImpartirClase() { cout << "Impartiendo clase" << endl; }
    };

    class Plantilla : public Persona
    {
    public:
        float Salario;
        Plantilla(string nombre, float salario) : Persona(nombre), Salario(salario) {}
        void CobrarSalario() { cout << "Cobrar salario como plantilla" << endl; }
    };

    class Trabajador : public Plantilla
    {
    public:
        Trabajador(string nombre, float salario) : Plantilla(nombre, salario) {}
    };

    class Profesor : public Trabajador, public IImpartirClase
    {
    public:
        Profesor(string nombre, float salario, int horasImpartirClase) : Trabajador(nombre, salario), IImpartirClase(nombre, horasImpartirClase) {}
    };

    class ProfesorAdiestrado : public Profesor, public IRecibirClase
    {
    public:
        ProfesorAdiestrado(string nombre, float salario, int horasImpartirClase, int horasRecibirClase) : Profesor(nombre, salario, horasImpartirClase), IRecibirClase(nombre, horasRecibirClase) {}
    };

    class Estudiante : public Plantilla, public IRecibirClase
    {
    public:
        Estudiante(string nombre, float salario, int horasRecibirClase) : Plantilla(nombre, salario), IRecibirClase(nombre, horasRecibirClase) {}
    };

    class AlumnoAyudante : public Estudiante, public IImpartirClase
    {
    public:
        AlumnoAyudante(string nombre, float salario, int horasRecibirClase, int horasImpartirClase) : Estudiante(nombre, salario, horasRecibirClase), IImpartirClase(nombre, horasImpartirClase) {}
    };
```

Como podemos observar, la implementación en `C++` es muy similar a la de `C#`. Sin embargo no pudimos ahorrar tener que repetir las propiedades HorasRecibirClase y HorasImpartirClase en cada clase derivada.

Probemos si las clases están bien definidas:

```cpp
    cout << "Probemos si las clases y las interfaces están bien definidas" << endl;
    AlumnoAyudante alumnoAyudante("Alumno Ayudante", 1000, 500, 1000);

    cout << "Alumno ayudante se comporta como estudiante?" << endl;
    cout << (dynamic_cast<Estudiante *>(&alumnoAyudante) != nullptr) << endl; // true
    {...}
```

Parece estar todo bien excepto que:

```cpp
    cout << alumnoAyudante.nombre << endl; // error de compilacion ambiguedad
```

Esto ocurre porque `AlumnoAyudante` hereda de `Estudiante` y `IImpartirClase`, y ambas tienen una propiedad `Nombre`, ya que ambos heredan de Plantilla.

### Escenarios en `C++`

Una forma de solucionar heredar miembros repetidos es hacer uso de la herencia virtual.

```cpp
    class Animal {
    public:
        virtual void eat() { std::cout << "I'm eating generic food.\n"; }
    };

    class Mammal : virtual public Animal {
    };

    class WingedAnimal : virtual public Animal {

    };

    class Bat : public Mammal, public WingedAnimal {
    };

```

Cuando ejecutamos el siguiente código:

```cpp
    Bat bat;
    bat.eat(); // I'm eating generic food.
```

Sin embargo, si hicieramos lo siguiente:

```cpp
    class Mammal : virtual public Animal {
    public:
        void eat() { std::cout << "I'm eating mammal food.\n"; }
    };
```

La salida hubiera sido `I'm eating mammal food.`

En `C++`, la ventaja de utilizar la herencia virtual es que solo se guarda una copia de los miembros heredados, evitando así problemas de ambigüedad. En el segundo caso, la salida es `I'm eating mammal food.` porque el método `eat` de la clase `Mammal` sobrescribe el método `eat` de la clase `Animal`. Sin embargo, si también hubiéramos redefinido el método `eat` en la clase `WingedAnimal`, se presentaría el mismo problema de ambigüedad.

Problemos otra forma de solucionar ambiguedades:

```cpp
    class A
    {
    public:
        A(){};
        void f() { cout << "A::f" << endl; }
    };

    class B : public A
    {
    public:
        B(){};
        void f() { cout << "B::f" << endl; }
    };

    class C : public A
    {
    public:
        C(){};
        void f() { cout << "C::f" << endl; }
    };

    class D : public B, public C
    {
    public:
        D(){};
    };
```

Si hacemos:

```cpp
    D d;
    d.B::f(); // B::f
    d.C::f(); // C::f
    // d.f(); // Error de compilación
```

Es un comportamiento similar al de `C#` con la implementación explícita de interfaces.

### Visibilidad de miembros en `C++`

En C++, la visibilidad también se controla mediante modificadores de acceso.

- public: El miembro es accesible desde cualquier parte del código.
- private: El miembro solo es accesible dentro de la propia clase.
- protected: El miembro es accesible dentro de la propia clase y sus clases derivadas.

## Solución propuesta en `Python`

Ver [notebook](./Python/main.ipynb) con la solución propuesta en `Python`.

En `Python` existe la herencia múltiple. A continuación se muestra `el diagrama de clases` de la solución propuesta.

```mermaid
graph LR
    Persona --> Plantilla
    Persona --> IE[Recibidor]
    Persona --> IP[Impartidor]

    Plantilla --> Trabajador
    Plantilla --> Estudiante

    Trabajador --> Profesor
    IP[Impartidor] --> Profesor

    Profesor --> PA[Profesor Adiestrado]
    IE[Recibidor] --> PA[Profesor Adiestrado]
    IE[Recibidor] --> Estudiante
    Estudiante --> AA[Alumno Ayudante]
    IP[Impartidor] --> AA[Alumno Ayudante]
```

A continuación se muestra la implementación de las clases en `Python`.

```python
    class Persona:
        def __init__(self, nombre):
            self.Nombre = nombre


    class Plantilla(Persona):
        def __init__(self, nombre, salario):
            Persona.__init__(self, nombre)
            self.Salario = salario
        def cobrar_salario(self):
            print("Cobrando salario")


    class Impartidor(Persona):
        def __init__(self, nombre, horasImpartirClase):
            Persona.__init__(self, nombre)
            self.horasImpartirClase = horasImpartirClase
        def impartir_clases(self):
            print("Impartiendo clases")


    class Recibidor(Persona):
        def __init__(self, nombre, horasRecibirClase):
            Persona.__init__(self, nombre)
            self.horasRecibirClase = horasRecibirClase

        def recibir_clases(self):
            print("Recibiendo clases")

    class Trabajador(Plantilla):
        def __init__(self, nombre, salario):
            Plantilla.__init__(self, nombre, salario)
```

En `Python` como mismo en `C++` tenemos la ventaja de que no es necesario repetir las propiedades HorasRecibirClase y HorasImpartirClase en cada clase derivada.

La función `super()` en Python se utiliza para llamar a métodos en la clase padre. En el caso de la herencia múltiple, `super()` sigue el orden definido por el método de resolución de orden de clase (MRO) y sigue escalando en los antecesores de la clase, provocando resultados inesperados. Por lo tanto, es mejor evitar el uso de `super()` en la herencia múltiple y llamar a los métodos de la clase padre directamente.

```python
    class Profesor(Trabajador, Impartidor):
        def __init__(self, nombre, salario, horasImpartirClase):
            Trabajador.__init__(self, nombre, salario)
            Impartidor.__init__(self, nombre, horasImpartirClase)
        def impartir_clases(self):
            print("Impartiendo clases como profesor")

    class ProfesorAdiestrado(Profesor, Recibidor):
        def __init__(self, nombre, salario, horasRecibirClase, horasImpartirClase):
            Profesor.__init__(self, nombre, salario, horasImpartirClase)
            Recibidor.__init__(self, nombre, horasRecibirClase)
        def calcularHorasTotales(self):
            return self.horasImpartirClase + self.horasRecibirClase


    class Estudiante(Recibidor, Plantilla):
        def __init__(self, nombre, salario, horasRecibirClase):
            Plantilla.__init__(self, nombre, salario)
            Recibidor.__init__(self, nombre, horasRecibirClase)


    class AlumnoAyudante(Estudiante, Impartidor):
        def __init__(self, nombre, salario, horasImpartirClase, horasRecibirClase):
            Estudiante.__init__(self, nombre, salario, horasRecibirClase)
            Impartidor.__init__(self, nombre, horasImpartirClase)
        def calcularHorasTotales(self):
            return self.horasImpartirClase + self.horasRecibirClase
```

Veamos qué nos permite hacer la implementación:

```python
    def testImpartirClase(profesor: Profesor):
    profesor.impartir_clases()

    def testAumentoDeHorasClase(profesor: Profesor):
        profesor.horasImpartirClase += 1000
        print(
            f"Las horas clase del profesor {profesor.Nombre} han aumentado a {profesor.horasImpartirClase}"
        )
```

Creando dos funciones para probar la consistencia de las clases, y usando el siguiente código:

```python
    profesor1 = Profesor("Profesor 1", 1000, 10)
    print(f"Nombre: {profesor1.Nombre}")
    testImpartirClase(profesor1)

    AlumnoAyudante1 = AlumnoAyudante("Alumno Ayudante 1", 1000, 10, 20)

    testImpartirClase(AlumnoAyudante1)
    trabajador = Trabajador("Trabajador 1", 1000)
    trabajador.horasImpartirClase = 10
    testAumentoDeHorasClase(trabajador) # Imprime 1010
```

Podemos comprobar que en python se trata por igual las clases y no se verifica su tipo, basta con que tenga las propiedades y métodos necesarios que se exigen en la función. Cualquier clase puede ser usada como cualquier otra sin importar su tipo.

Y seguro pensamos que al igual que en `C++` la herencia múltiple nos causará problemas de ambigüedad. Veamos:

```python
    print(f"Nombre: {AlumnoAyudante1.Nombre}") #No ocurre error de ambiguedad
```

Esto es debido a que en `Python` no hay ambigüedad en la herencia múltiple, ya que el método de resolución de orden de clase (MRO) resuelve el problema. El MRO es el orden en el que se buscan los métodos en las clases base. El MRO se calcula en tiempo de ejecución y se almacena en el atributo `__mro__` de la clase.

```python
    print(AlumnoAyudante.__mro__)
    #<class '__main__.AlumnoAyudante'>, <class '__main__.Estudiante'>, <class '__main__.Recibidor'>, <class '__main__.Plantilla'>, <class '__main__.Impartidor'>, <class '__main__.Persona'>, <class 'object'>
```

Probemos otros ejemplos:

```python

    print(
        f"Ayudante es subclase de estudiante? {issubclass(AlumnoAyudante1.__class__, Estudiante)}" # True
    )
    print(
        f"Ayudante es subclase de impartidor? {issubclass(AlumnoAyudante1.__class__, Impartidor)}" # True
    )
    print(
        f"Ayudante es subclase de trabajador? {issubclass(AlumnoAyudante1.__class__, Trabajador)}" # False
    )

    print(
        f"AlumnoAyudante1 recibe {AlumnoAyudante1.horasImpartirClase} horas clase" # 10
    )
    print(
        f"AlumnoAyudante1 imparte {AlumnoAyudante1.horasRecibirClase} horas clase"  # 20
    )
    print(
        "Calculando el total de horas clase de AlumnoAyudante1: ", AlumnoAyudante1.calcularHorasTotales() # 30
    )
```

A pesar de que `AlumnoAyudante` hereda de `Estudiante` e `Impartidor`, no hereda de `Trabajador` y aunque tenga las propiedades y métodos necesarios para ser un `Trabajador`, no lo es.

### Escenarios en `Python`

#### No solo atributos, funciones con MRO

**Escenario:** Los alumnos becados tienen asignado un **bus escolar** para ellos, y los profesores tienen asignado **otro bus**,ambos protocolos de coger el bus **son diferentes**, pero el nombre del método es el mismo. Luego de un tiempo, la dirección decide que ambos grupos de personas pueden coger **el bus que deseen**, por lo que se incluye que se herede de ambas clases

```python
    class CogerBus:
        def CogerBus(self):
            print("Cogiendo bus")

    class BusProfesores(CogerBus):
        def CogerBus(self):
            print("Cogiendo bus de profesores")

    class BusAlumnos(CogerBus):
        def CogerBus(self):
            print("Cogiendo bus de alumnos")

    class AlumnoBecado(BusAlumnos, BusProfesores):
        pass

    class ProfesorAfectado(BusProfesores, BusAlumnos):
        pass
```

Si hacemos:

```python
    alumno = AlumnoBecado()
    alumno.CogerBus() # Cogiendo bus de alumnos

    profesor = ProfesorAfectado()
    profesor.CogerBus() # Cogiendo bus de profesores
```

En este caso, el método `CogerBus` de `BusAlumnos` es llamado, ya que `AlumnoBecado` hereda de `BusAlumnos` primero. De igual forma, el método `CogerBus` de `BusProfesores` es llamado, ya que `ProfesorAfectado` hereda de `BusProfesores` primero.

#### Composición en `Python`

En python se puede hacer uso de la composición para evitar ciertos problemas que se presentan con la herencia múltiple.

```python
    class AlumnoAyudanteComposition:
        def __init__(self, nombre, salario_profesor, salario_estudiante, horasClaseRecibidas, horasClaseImpartidas):
            self.estudiante = Estudiante(nombre, salario_estudiante, horasClaseRecibidas)
            self.profesor = Profesor(nombre, salario_profesor, horasClaseImpartidas)
            self.Salario = salario_profesor + salario_estudiante

        def cobrar_salario(self):
            self.profesor.cobrar_salario()
            self.estudiante.cobrar_salario()

        def impartir_clase(self):
            self.profesor.impartir_clases()

        def recibir_clase(self):
            self.estudiante.recibir_clases()
```

Ahora tenemos un nuevo objeto que tiene comportamiento de profesor y de estudiante, y no hereda de Profesor ni de Estudiante. Puede impartir clases y a su vez recibir clases.

Lo que ocurre aqui es que `AlumnoAyudanteComposition` tiene un `Estudiante` y un `Profesor` como atributos, y delega las responsabilidades a estos objetos. De esta forma, evitamos problemas de ambigüedad y mantenemos el código limpio.

Pero igual traer ciertos problemas:

```python
    print(
        f"Ayudante es subclase de Estudiante? {issubclass(AlumnoAyudanteComposition, Estudiante)}" # False
    )
    print(
        f"Ayudante es subclase de Impartidor? {issubclass(AlumnoAyudanteComposition, Impartidor)}" # False
    )
```

Esto imprime `False` en ambos casos, ya que `AlumnoAyudanteComposition` no es subclase de `Estudiante` ni de `Impartidor`. Sin embargo, `AlumnoAyudanteComposition` tiene las propiedades y métodos necesarios para ser un `Estudiante` y un `Impartidor`. Y podría ser usado como tal.

#### Mixins en `Python`

Los mixins son clases que no se supone que se instancien, sino que se usen para agregar funcionalidades a otras clases. En `Python`, los mixins se pueden usar para simular la herencia múltiple.

```python
   class Vehicle:
    def __init__(self, name):
        self.name = name

class FlyableMixin:
    def fly(self):
        print(f"{self.name} is flying")

class Airplane(Vehicle, FlyableMixin):
    pass
```

Ahora, `Airplane` tiene el comportamiento de `Vehicle` y `FlyableMixin`. Podemos hacer:

```python
    plane = Airplane("Airplane")

    car.fly()  # Esto dará un error porque Vehicle no tiene el método fly
    plane.fly()  # Esto imprimirá "Airplane is flying"
```

### Visibilidad de miembros en `Python`

En Python, la visibilidad se controla mediante convenciones y no a través de modificadores de acceso explícitos.

- Los miembros precedidos por un guion bajo (por ejemplo, \_miembro) indican que son considerados como "privados" y no deberían ser accedidos directamente desde fuera de la clase.
- Sin embargo, esto es más una convención que una restricción y el acceso directo es posible.

```python
    class A:
        def __init__(self):
            self.__private = "Soy privado"
```
