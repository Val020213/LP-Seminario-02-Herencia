using System;
public static class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Probemos si las clases y las interfaces están bien definidas");
        AlumnoAyudante alumnoAyudante = new AlumnoAyudante("Alumno Ayudante", 1000, 500, 10, 5);
        System.Console.WriteLine("Alumno ayudante se comporta como trabajador?");
        System.Console.WriteLine(alumnoAyudante is Trabajador); // false
        System.Console.WriteLine("Alumno ayudante se comporta como profesor?");
        System.Console.WriteLine(alumnoAyudante is Profesor); // false

        System.Console.WriteLine("Alumno ayudante se comporta como estudiante?");
        System.Console.WriteLine(alumnoAyudante is IEstudiante); // true
        System.Console.WriteLine("Alumno ayudante se comporta como Iprofesor?");
        System.Console.WriteLine(alumnoAyudante is IProfesor); // true 

        Escenario();
    }

//     /* 
//     Pongamos el siguiente escenario para ejemplificar las ambigüedades que pueden surgir al usar interfaces 
//     y herencia.
//     A la universidad se le asigno un presupuesto para contratar el transporte de los estudiantes becados 
//     y profesores afectados.
//     Ambas formas de coger el transporte son diferentes.
//     */

//     interface ITransporte
//     {
//         public void CogerOmnibus();
//     }

//     interface IBecado : ITransporte
//     {
//         public new void CogerOmnibus() => System.Console.WriteLine("Cogiendo omnibus como becado");
//     }

//     interface IAfectado : ITransporte
//     {
//         public new void CogerOmnibus() => System.Console.WriteLine("Cogiendo omnibus como afectado");
//     }

//     class EstudianteBecado : Estudiante, IBecado
//     {
//         public EstudianteBecado(string nombre, float salario, int horasClase) : base(nombre, salario, horasClase) { }
//         public void CogerOmnibus() => System.Console.WriteLine("Cogiendo omnibus como estudiante becado");
//     }

//     class ProfesorAfectado : Profesor, IAfectado
//     {
//         public ProfesorAfectado(string nombre, float salario, int horasClase) : base(nombre, salario, horasClase) { }
//         public void CogerOmnibus() => System.Console.WriteLine("Cogiendo omnibus como profesor afectado");
//     }

//     public static void Escenario()
//     {
//         EstudianteBecado estudianteBecado = new EstudianteBecado("Estudiante Becado", 1000, 10);
//         ProfesorAfectado profesorAfectado = new ProfesorAfectado("Profesor Afectado", 1000, 10);
//         System.Console.WriteLine("Estudiante becado se comporta como IAfectado?");
//         System.Console.WriteLine(estudianteBecado is IAfectado); // false
//         System.Console.WriteLine("Profesor afectado se comporta como IBecado?");
//         System.Console.WriteLine(profesorAfectado is IBecado); // true
//         System.Console.WriteLine("Estudiante becado se comporta como ITransporte?");
//         System.Console.WriteLine(estudianteBecado is ITransporte); // true
//         System.Console.WriteLine("Profesor afectado se comporta como ITransporte?");
//         System.Console.WriteLine(profesorAfectado is ITransporte); // true

//     }

//     /*
//     Luego de un tiempo, el presupuesto se redujo y con ello el viaje de los omnibus,
//     por lo que al personal de la universidad, se les dio la oportunidad de
//     coger ambos tipos de transporte. Sin importar si son estudiantes o profesores.
//     */

//     class PersonalTransporte : IBecado, IAfectado
//     {
//         public void CogerOmnibus() => {};
// }

// public static void Escenario2()
// {
//     PersonalTransporte personalTransporte = new PersonalTransporte();
//     System.Console.WriteLine(PersonalTransporte.CogerOmnibus());

// }

}

public interface IAsalariado
{
    public void CobrarSalarioTrabajador() => System.Console.WriteLine("Cobrando salario como asalariado");
}

public interface IEstudiante
{
    public void RecibirClase() => System.Console.WriteLine("Recibiendo clase");
}

public interface IProfesor
{
    public void ImpartirClase() => System.Console.WriteLine("Impartiendo clase");
}

public class Persona
{
    public string Nombre;
    public Persona(string nombre) { Nombre = nombre; }
}

public class Plantilla : Persona
{
    public float Salario;
    public int HorasClase;
    public Plantilla(string nombre, float salario, int horasClase) : base(nombre)
    {
        Salario = salario;
        HorasClase = horasClase;
    }
}

public class Trabajador : Plantilla, IAsalariado
{
    public Trabajador(string nombre, float salario, int horasClase) : base(nombre, salario, horasClase) { }
    public void CobrarSalarioTrabajador() => System.Console.WriteLine("Cobrando salario como trabajador");
}

public class Profesor : Trabajador, IProfesor
{
    public Profesor(string nombre, float salario, int horasClase) : base(nombre, salario, horasClase) { }
    public void ImpartirClase() => System.Console.WriteLine("Impartiendo clase como profesor");
}

public class ProfesorAdiestrado : Profesor, IEstudiante
{
    public ProfesorAdiestrado(string nombre, float salario, int horasClase) : base(nombre, salario, horasClase) { }
    public void RecibirClase() => System.Console.WriteLine("Recibiendo clase como profesor adiestrado");

}

public class Estudiante : Plantilla, IEstudiante
{
    public Estudiante(string nombre, float salario, int horasClase) : base(nombre, salario, horasClase) { }
    public void RecibirClase() => System.Console.WriteLine("Recibiendo clase como estudiante");
}
public class AlumnoAyudante : Estudiante, IProfesor, IAsalariado
{
    public float SalarioProfesor;
    public int HorasClaseProfesor;
    public AlumnoAyudante(string nombre, float salario, float salarioProfesor, int horasClase, int horasClaseProfesor)
    : base(nombre, salario, horasClase)
    {
        SalarioProfesor = salarioProfesor;
        HorasClaseProfesor = horasClaseProfesor;
    }
    public void CobrarSalarioTrabajador() => System.Console.WriteLine("Cobrando salario como alumno ayudante");
    public void ImpartirClase() => System.Console.WriteLine("Impartiendo clase como alumno ayudante");
}

