#include <bits/stdc++.h>
using namespace std;

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

/// Escenario de ambigüedad
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
    AlumnoAyudante alumnoAyudante("Alumno Ayudante", 1000, 500, 1000);

    // cout << alumnoAyudante.nombre << endl; // ambiguedad
    cout << "Alumno ayudante se comporta como estudiante?" << endl;
    cout << (dynamic_cast<Estudiante *>(&alumnoAyudante) != nullptr) << endl; // true

    escenarioAmbiguedad();
}
