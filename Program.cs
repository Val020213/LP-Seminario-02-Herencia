using System;
public static class Program
{
    public static void Main(string[] args)
    {

    }
}

public interface IAsalariado
{
    public void CobrarSalario() => throw new NotImplementedException();
}

public interface IEstudiante
{
    public void RecibirClase() => throw new NotImplementedException();
}

public interface IProfesor
{
    public void ImpartirClase() => throw new NotImplementedException();
}

public class Persona
{
    public string Nombre;
}

public class Plantilla : Persona
{
    public float Salario;
    public int HorasClase;
}

public class Trabajador : Plantilla, IAsalariado { }

public class Profesor : Trabajador, IProfesor { }

public class ProfesorAdiestrado : Profesor, IEstudiante { }

public class Estudiante : Plantilla, IEstudiante
{
    public void CobrarSalarioEstudiante() => throw new NotImplementedException();
}
public class AlumnoAyudante : Estudiante, IProfesor, IAsalariado
{
    public float SalarioProfesor;
}

