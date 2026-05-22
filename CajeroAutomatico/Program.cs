using System;
using System.Collections.Generic;

class Cuenta
{
    public int Numero { get; set; }
    public int Pin { get; set; }
    public double Saldo { get; set; }
    public List<double> Retiros { get; set; }

    public Cuenta(int numero, int pin, double saldo)
    {
        Numero = numero;
        Pin = pin;
        Saldo = saldo;
        Retiros = new List<double>();
    }
}

class Program
{
    static List<Cuenta> cuentas = new List<Cuenta>();

    static void Main()
    {
        InicializarCuentas();

        Console.WriteLine("Bienvenido a DEVBANK!");

        Cuenta cuentaActual = Login();

        if (cuentaActual != null)
        {
            MostrarMenu(cuentaActual);
            GenerarReporte(cuentaActual);
        }
    }

    static void InicializarCuentas()
    {
        cuentas.Add(new Cuenta(123456, 1111, 500000));
        cuentas.Add(new Cuenta(654321, 2222, 300000));
    }

    static Cuenta Login()
    {
        while (true)
        {
            Console.Write("Ingresa tu número de cuenta: ");

            int numero = Convert.ToInt32(Console.ReadLine());

            Cuenta cuenta = cuentas.Find(c => c.Numero == numero);

            if (cuenta == null)
            {
                Console.WriteLine("La cuenta no existe.");
                continue;
            }

            int intentos = 0;

            while (intentos < 3)
            {
                Console.Write("Ingresa tu PIN: ");

                int pin = Convert.ToInt32(Console.ReadLine());

                if (pin == cuenta.Pin)
                {
                    Console.WriteLine("Login exitoso!");
                    return cuenta;
                }

                intentos++;
                Console.WriteLine("PIN incorrecto.");
            }

            Console.WriteLine("Cuenta bloqueada.");
            return null;
        }
    }

    static void MostrarMenu(Cuenta cuenta)
    {
        int opcion;

        do
        {
            Console.WriteLine("\n----- DEVBANK -----");
            Console.WriteLine("1. Consultar saldo");
            Console.WriteLine("2. Retirar dinero");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");

            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    ConsultarSaldo(cuenta);
                    break;

                case 2:
                    RealizarRetiro(cuenta);
                    break;

                case 3:
                    Console.WriteLine("Gracias por usar DevBANK.");
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

        } while (opcion != 3);
    }

    static void ConsultarSaldo(Cuenta cuenta)
    {
        Console.WriteLine($"Saldo actual: ${cuenta.Saldo}");
    }

    static void RealizarRetiro(Cuenta cuenta)
    {
        Console.Write("Monto a retirar: ");

        double retiro = Convert.ToDouble(Console.ReadLine());

        if (retiro <= 0)
        {
            Console.WriteLine("El monto debe ser mayor a cero.");
            return;
        }

        if (retiro > cuenta.Saldo)
        {
            Console.WriteLine("Saldo insuficiente.");
            return;
        }

        cuenta.Saldo -= retiro;
        cuenta.Retiros.Add(retiro);

        Console.WriteLine("Retiro exitoso!");
    }

    static void GenerarReporte(Cuenta cuenta)
    {
        Console.WriteLine("\n---- REPORTE DE TRANSACCIONES ----");

        double totalRetirado = 0;
        double mayorRetiro = 0;

        foreach (double retiro in cuenta.Retiros)
        {
            totalRetirado += retiro;

            if (retiro > mayorRetiro)
            {
                mayorRetiro = retiro;
            }
        }

        int cantidadRetiros = cuenta.Retiros.Count;

        double promedio = cantidadRetiros > 0
            ? totalRetirado / cantidadRetiros
            : 0;

        Console.WriteLine($"Cantidad de retiros: {cantidadRetiros}");
        Console.WriteLine($"Total retirado: ${totalRetirado}");
        Console.WriteLine($"Promedio de retiros: ${promedio}");
        Console.WriteLine($"Mayor retiro: ${mayorRetiro}");
        Console.WriteLine($"Saldo final: ${cuenta.Saldo}");
    }
}