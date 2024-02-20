#include <bits/stdc++.h>
using namespace std;

class Persona
{
public:
    string Nombre;
    Persona(string nombre) : Nombre(nombre) {}
};

class IRecibirClase
{
public:
    void RecibirClase() { cout << "Recibiendo clase" << endl; }
};

class IImpartirClase
{
public:
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
    int HorasClase;
    Profesor(string nombre, float salario, int horasClase) : Trabajador(nombre, salario), HorasClase(horasClase) {}
    void ImpartirClase() { cout << "Impartiendo clase como profesor" << endl; }
};

class ProfesorAdiestrado : public Profesor, public IRecibirClase
{
public:
    ProfesorAdiestrado(string nombre, float salario, int horasClase) : Profesor(nombre, salario, horasClase) {}
    void RecibirClase() { cout << "Recibiendo clase como profesor adiestrado" << endl; }
};

class Estudiante : public Plantilla, public IRecibirClase
{
public:
    int HorasClase;
    Estudiante(string nombre, float salario, int horasClase) : Plantilla(nombre, salario), HorasClase(horasClase) {}
    void RecibirClase() { cout << "Recibiendo clase como estudiante" << endl; }
};

class AlumnoAyudante : public Estudiante, public IImpartirClase
{
public:
    AlumnoAyudante(string nombre, float salario, int horasClase) : Estudiante(nombre, salario, horasClase) {}
    void ImpartirClase() { cout << "Impartiendo clase como alumno ayudante" << endl; }
};

class TransporteEstudiante
{
public:
    void CogerBus() { cout << "Cogiendo el bus como estudiante" << endl; }
};

class TransporteProfesor
{
public:
    void CogerBus() { cout << "Cogiendo el bus como profesor" << endl; }
};

// class PersonalTransportado : public TransporteEstudiante, public TransporteProfesor // error de ambigüedad
// {
// public:
//     PersonalTransportado() {}
// };

class PersonalTransportado : public TransporteEstudiante, public TransporteProfesor
{
public:
    PersonalTransportado() {}
    void CogerBus() { cout << "Cogiendo el bus como personal transportado" << endl; }
};

// otros ejemplos explicitos
class PersonalTransportado2 : public TransporteEstudiante, public TransporteProfesor
{
public:
    PersonalTransportado2() {}
    void CogerBus() { TransporteEstudiante::CogerBus(); }
};

void escenarioAmbiguedad()
{
    PersonalTransportado personalTransportado;
    personalTransportado.CogerBus();
    PersonalTransportado2 personalTransportado2;
    personalTransportado2.CogerBus();
}

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);

    cout << "Probemos si las clases y las interfaces están bien definidas" << endl;
    cout << "Alumno ayudante se comporta como trabajador?" << endl;
    AlumnoAyudante alumnoAyudante("Alumno Ayudante", 1000, 500);
    // cout << (dynamic_cast<Trabajador*>(&alumnoAyudante) != nullptr) << endl; // error
    // cout << "Alumno ayudante se comporta como profesor?" << endl;
    // cout << (dynamic_cast<Profesor*>(&alumnoAyudante) != nullptr) << endl; // error
    cout << "Alumno ayudante se comporta como estudiante?" << endl;
    cout << (dynamic_cast<Estudiante *>(&alumnoAyudante) != nullptr) << endl; // true

    escenarioAmbiguedad();
}

//     Escenario propuesto para ejemplificar la herencia múltiple en c++ (igual que en c#)
//     Pongamos el siguiente escenario para ejemplificar las ambigüedades que pueden surgir al usar multiple herencia.
//     A la universidad se le asignó un presupuesto para contratar el transporte de los estudiantes becados
//     y profesores afectados. Se tiene que un grupo de personas(Personal Transportado) pueden coger ambos transportes.
//     Ambas formas de coger el transporte son diferentes.

//     interface ITransporteEstudiante
//     {
//         public void CogerBus() => System.Console.WriteLine("Cogiendo el bus como estudiante");
//     }

//     interface ITransporteProfesor
//     {
//         public void CogerBus() => System.Console.WriteLine("Cogiendo el bus como profesor");
//     }

//     class PersonalTransportado : ITransporteEstudiante, ITransporteProfesor
//     {
//         public PersonalTransportado() { }

//     }

//     public static void escenarioAmbiguedad()
//     {
//         PersonalTransportado personalTransportado = new PersonalTransportado();
//         ((ITransporteEstudiante)personalTransportado).CogerBus(); // Cogiendo el bus como estudiante
//         ((ITransporteProfesor)personalTransportado).CogerBus(); // Cogiendo el bus como profesor
//         // personalTransportado.CogerBus(); // Error de compilación
//     }

//     /*
//     Ejemplo de declaración de interfaces explícitas
//     A veces, una clase puede implementar dos interfaces que tienen un método con el mismo nombre.
//     En este caso, se puede usar la declaración de interfaces explícitas para resolver la ambigüedad.
//     Por ejemplo, en la universidad se le asignó un presupuesto para contratar seguridad debido a la llegada
//     de personal extranjero con malas intenciones.
//     Los custodios son responsables de hacer guardia como parte de su trabajo de seguridad.
//     */
//     public interface ISecurity
//     {
//         public void Guardia() => throw new NotImplementedException();
//     }

//     public interface ICustodio
//     {
//         public void Guardia() => throw new NotImplementedException();
//     }

//     public class Custodio : ICustodio
//     {
//         public Custodio() { }
//         void ICustodio.Guardia() => System.Console.WriteLine("Haciendo guardia como custodio");
//     }

//     public class Seguridad : ICustodio, ISecurity
//     {
//         public Seguridad() { }
//         void ICustodio.Guardia() => System.Console.WriteLine("Haciendo guardia como seguridad en el puesto de custodio");
//         void ISecurity.Guardia() => System.Console.WriteLine("Haciendo guardia como seguridad");
//     }

//     public static void escenarioGuardia()
//     {
//         Custodio custodio = new Custodio();
//         // custodio.Guardia(); // Error de compilación debido a la delaración explícita de la interfaz
//         ((ICustodio)custodio).Guardia(); // Haciendo guardia como custodio

//         Seguridad seguridad = new Seguridad();
//         ((ICustodio)seguridad).Guardia(); // Haciendo guardia como seguridad en el puesto de custodio
//         ((ISecurity)seguridad).Guardia(); // Haciendo guardia como seguridad
//         ICustodio custodioDelta = new Seguridad();
//         custodioDelta.Guardia();
//     }
// }
