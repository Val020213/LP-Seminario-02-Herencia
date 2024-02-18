#include <bits/stdc++.h>
using namespace std;

class Persona
{
public:
    string Nombre;
    Persona(string nombre) : Nombre(nombre) {}
};

class Plantilla : public Persona
{
public:
    float Salario;
    int HorasClase;
    Plantilla(string nombre, float salario, int horasClase) : Persona(nombre), Salario(salario), HorasClase(horasClase) {}
};

class Docente
{
    void ImpartirClase() {}
};

class Alumno
{
    void RecibirClase() {}
};

class Asalariado
{
    void CobrarSalarioTrabajador() {}
};

class Trabajador : public Plantilla, Asalariado
{
public:
    Trabajador(string nombre, float salario, int horasClase) : Plantilla(nombre, salario, horasClase) {}
};

class Profesor : public Trabajador, Docente
{
public:
    Profesor(string nombre, float salario, int horasClase) : Trabajador(nombre, salario, horasClase) {}
};

class ProfesorAdiestrado : public Profesor, Alumno
{
public:
    ProfesorAdiestrado(string nombre, float salario, int horasClase) : Profesor(nombre, salario, horasClase) {}
};

class Estudiante : public Plantilla
{
public:
    Estudiante(string nombre, float salario, int horasClase) : Plantilla(nombre, salario, horasClase) {}
    void CobrarSalarioEstudiante() {}
};

class AlumnoAyudante : public Estudiante, Asalariado
{
public:
    float SalarioProfesor;
    AlumnoAyudante(string nombre, float salario, float salarioProfesor, int horasClase) : Estudiante(nombre, salario, horasClase), SalarioProfesor(salarioProfesor) {}
};

class ProfesorAfectado
{
public:
    void CogerTransporte() { cout << "Coger transporte de Profesor"; }
};

class AlumnoBecado
{
public:
    void CogerTransporte() { cout << "Coger transporte de Alumno"; }
};

class Transportado : public ProfesorAfectado, AlumnoBecado
{
public:
    Transportado() {}
};

int escenario1()
{
    Transportado t = Transportado();
    // t.CogerTransporte(); // Ambiguedad error de compilacion
    t.ProfesorAfectado::CogerTransporte(); // resolucion de ambiguedad
    return 0;
}

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    escenario1();
    return 0;
}

// Problema de Ambiguedad
