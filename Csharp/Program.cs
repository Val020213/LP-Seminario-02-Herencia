﻿using System;
public static class Program
{
    #region  Diseño de clases e interfaces
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
    #endregion

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

        System.Console.WriteLine();
        escenarioAmbiguedad();
        System.Console.WriteLine();
        escenarioGuardia();
    }

    /* 
    Pongamos el siguiente escenario para ejemplificar las ambigüedades que pueden surgir al usar interfaces 
    y herencia.
    A la universidad se le asigno un presupuesto para contratar el transporte de los estudiantes becados 
    y profesores afectados. Y se tiene que un grupo de personas(Personal Transportado) pueden coger ambos transportes.
    Ambas formas de coger el transporte son diferentes.
    */


    interface TransporteEstudiante
    {
        public void CogerBus() => System.Console.WriteLine("Cogiendo el bus como estudiante");
    }

    interface TransporteProfesor
    {
        public void CogerBus() => System.Console.WriteLine("Cogiendo el bus como profesor");
    }

    class PersonalTransportado : TransporteEstudiante, TransporteProfesor
    {
        public PersonalTransportado() { }

    }

    public static void escenarioAmbiguedad()
    {
        PersonalTransportado personalTransportado = new PersonalTransportado();
        ((TransporteEstudiante)personalTransportado).CogerBus(); // Cogiendo el bus como estudiante
        ((TransporteProfesor)personalTransportado).CogerBus(); // Cogiendo el bus como profesor
        // personalTransportado.CogerBus(); // Error de compilación
    }


    /* 
    Ejemplo de declaración de interfaces explicitas
    A veces, una clase puede implementar dos interfaces que tienen un método con el mismo nombre.
    En este caso, se puede usar la declaración de interfaces explícitas para resolver la ambigüedad. 
    Por ejemplo, en la universidad se le asignó un presupuesto para contratar seguridad debido a la llegada 
    de personal extranjero con malas intenciones. 
    Los custodios son responsables de hacer guardia como parte de su trabajo de seguridad.
    */
    public interface ISecurity
    {
        public void Guardia() => throw new NotImplementedException();
    }

    public interface ICustodio
    {
        public void Guardia() => throw new NotImplementedException();
    }

    public class Custodio : ICustodio
    {
        public Custodio() { }
        void ICustodio.Guardia() => System.Console.WriteLine("Haciendo guardia como custodio");
    }

    public class Seguridad : ICustodio, ISecurity
    {
        public Seguridad() { }
        void ICustodio.Guardia() => System.Console.WriteLine("Haciendo guardia como seguridad en el puesto de custodio");
        void ISecurity.Guardia() => System.Console.WriteLine("Haciendo guardia como seguridad");
    }

    public static void escenarioGuardia()
    {
        Custodio custodio = new Custodio();
        // custodio.Guardia(); // Error de compilación
        ((ICustodio)custodio).Guardia(); // Haciendo guardia como custodio

        Seguridad seguridad = new Seguridad();
        ((ICustodio)seguridad).Guardia(); // Haciendo guardia como seguridad en el puesto de custodio
        ((ISecurity)seguridad).Guardia(); // Haciendo guardia como seguridad
    }
}
