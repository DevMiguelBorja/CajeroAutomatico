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
     const double TASA_ANUAL = 0.12; 

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

    /// <summary>
    /// Contiene las cuentas existentes para simular la base de datos.
    /// </summary>
       static void InicializarCuentas()
    {
        cuentas.Add(new Cuenta(123456, 1111, 500000));
        cuentas.Add(new Cuenta(654321, 2222, 300000));
    }


    /// <summary>
    /// Procesa la validacion del inicio de sesión del usuario a traves de un ciclo while que busca y Valida 
    /// el modelo cuenta.
    /// </summary>
    /// <returns>El exito de del inicio de sesión para entrar al menu principal.</returns>
    static Cuenta Login()
    {
        while (true)
        {
            Console.Write("Ingresa tu número de cuenta: ");

            int numero = LeerEntero("Ingrea el numero de cuenta: ");

            Cuenta cuenta = cuentas.Find(c => c.Numero == numero);

            if (cuenta == null)
            {
                Console.WriteLine("La cuenta no existe.");
                continue;
            }

            int intentos = 0;

            while (intentos < 3)
            {

                int pin = LeerEntero("Ingresa tú PIN");

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

    /// <summary>
    /// Muestra el menú principal del cajero y
    /// coordina las operaciones disponibles.
    /// </summary>
    /// <param name="cuenta">
    /// Cuenta autenticada del usuario.
    /// </param>
    static void MostrarMenu(Cuenta cuenta)
    {
        int opcion;

        do
        {
            Console.WriteLine("\n----- DEVBANK -----");
            Console.WriteLine("1. Consultar saldo");
            Console.WriteLine("2. Retirar dinero");
            Console.WriteLine("3. Simular CDT");
            Console.WriteLine("4. Salir "); 

            opcion = LeerEntero("Selecciona una opción");

            switch (opcion)
            {
                case 1:
                    ConsultarSaldo(cuenta);
                    break;

                case 2:
                    RealizarRetiro(cuenta);
                    break;

                case 3:
                   SimularCDT(); 
                   break; 

                case 4:
                    Console.WriteLine("Gracias por usar DevBANK.");
                    break; 

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

        } while (opcion != 3);
    }

    /// <summary>
    /// Muestra el saldo actual de la cuenta.
    /// </summary>
    /// <param name="cuenta">
    /// Cuenta de la cual se consultará el saldo.
    /// </param>
    static void ConsultarSaldo(Cuenta cuenta)
    {
        Console.WriteLine($"Saldo actual: ${cuenta.Saldo}");
    }

    /// <summary>
    /// Solicita y procesa un retiro de dinero.
    /// </summary>
    /// <param name="cuenta">
    /// Cuenta desde la cual se realizará el retiro.
    /// </param>

    static void RealizarRetiro(Cuenta cuenta)
    {

        double retiro = LeerDouble("Monto a retirar");

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

        /// <summary>
    /// Genera y muestra un reporte de transacciones
    /// realizadas por la cuenta.
    /// </summary>
    /// <param name="cuenta">
    /// Cuenta de la cual se generará el reporte.
    /// </param>
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
            ? totalRetirado / cantidadRetiros: 0;

        Console.WriteLine($"Cantidad de retiros: {cantidadRetiros}");
        Console.WriteLine($"Total retirado: ${totalRetirado}");
        Console.WriteLine($"Promedio de retiros: ${promedio}");
        Console.WriteLine($"Mayor retiro: ${mayorRetiro}");
        Console.WriteLine($"Saldo final: ${cuenta.Saldo}");
    }

    /// <summary>
    /// Ejecuta la simulación de un CDT solicitando
    /// monto y plazo al usuario.
    /// </summary>
    static void SimularCDT()
    {

        Console.WriteLine("----- SIMULAR CDT -------");

        double monto = LeerDouble("Ingrese el monto a invertir");  

        if (monto <= 0)
        {
            Console.WriteLine("El monto debe ser mayor a cero."); 
            return; 
        }

        int meses = LeerEntero("Ingrese el plazo en meses: ");

        if(meses <= 0)
        {
            Console.WriteLine("El plazo debe ser mayor a cero"); 
            return; 
        } 

        double tasaMensual = TASA_ANUAL/12; 

        double valorFinal = monto * Math.Pow(1 + tasaMensual, meses);

        double intereses = valorFinal - monto; 

        Console.WriteLine("---------- RESULTADO --------"); 
        Console.WriteLine($"Monto invertido ${monto}");
        Console.WriteLine($"plazo: {meses} meses"); 
        Console.WriteLine($"Intereses generados: ${intereses}"); 
        Console.WriteLine($"Valor Final: ${valorFinal}");  

    }

    /// <summary>
    /// Lee y valida una entrada numérica entera
    /// desde consola.
    /// </summary>
    /// <param name="mensaje">
    /// Mensaje mostrado al usuario.
    /// </param>
    /// <returns>
    /// Número entero válido ingresado por el usuario.
    /// </returns>
    static int LeerEntero(string mensaje)
    {
        int valor; 

        while (true)
        {
            Console.WriteLine(mensaje); 

            bool esValido = int.TryParse(Console.ReadLine(), out valor); 
            if (esValido)
            {
                return valor; 
            }

            Console.WriteLine("Entrada Invalida. debes ingresar un numero válido"); 
        }
    }
    /// <summary>
    /// Lee y valida una entrada numérica decimal
    /// desde consola.
    /// </summary>
    /// <param name="mensaje">
    /// Mensaje mostrado al usuario.
    /// </param>
    /// <returns>
    /// Número decimal válido ingresado por el usuario.
    /// </returns>
     static double LeerDouble(string mensaje)
    {
        double valor; 

        while (true)
        {
            Console.WriteLine(mensaje); 

            bool esValido = double.TryParse(Console.ReadLine(), out valor); 
            
            if (esValido)
            {
                return valor; 
            }

            Console.WriteLine("Entrada Invalida. debes ingresar un numero válido"); 
        }
    }
}