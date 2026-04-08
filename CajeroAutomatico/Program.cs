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

        cuentas.Add(new Cuenta(123456, 1111, 500000)); 
        cuentas.Add(new Cuenta(654321, 2222, 300000)); 

        Console.WriteLine("Bienvenido a DEVBANK!!");

        Cuenta cuentaActual = Login();  

        if (cuentaActual != null)
        {
            Menu(cuentaActual); 
            GenerarReporte(cuentaActual); 
        }

        static  Cuenta Login(){
            while (true){
                Console.WriteLine("Ingresa tu número de cuenta: "); 
                int numero = Convert.ToInt32(Console.ReadLine()); 

                Cuenta cuenta = cuentas.Find(c => c.Numero == numero); 

                if (cuenta != null)
                {
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
                        else
                        {
                            intentos ++; 
                            Console.WriteLine("PIN incorrecto!"); 
                        }
                    }

                    Console.WriteLine("Cuenta bloqueada!"); 
                    return null; 
                }
                else
                {
                    Console.WriteLine("cuenta no existe"); 
                }

            }
        }

        static void Menu (Cuenta cuenta)
        {
            int opcion = 0; 

            while (opcion != 3)
            {
                Console.WriteLine("----- DEVBANK -----");
                Console.WriteLine("1. Consultar saldo");
                Console.WriteLine("2. Retirar dinero");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");

                opcion = Convert.ToInt32(Console.ReadLine()); 

                switch (opcion)
                {
                    case 1: 
                        Console.WriteLine($"Saldo actual: ${cuenta.Saldo}"); 
                        break; 
                    case 2: 
                        Console.WriteLine("Monto a retirar: "); 
                        double retiro = Convert.ToDouble(Console.ReadLine()); 

                        if(retiro <= cuenta.Saldo)
                        {
                         cuenta.Saldo -= retiro; 
                            cuenta.Retiros.Add(retiro); 
                         Console.WriteLine("Rtiro exitoso!");     
                        }
                        else
                        {
                            Console.WriteLine("Saldo insuficiente");         
                        }
                    break; 
                    case 3: 
                        Console.WriteLine("Gracias por usar DevBANK"); 
                        break; 
                    default: 
                        Console.WriteLine("Opcion inválida"); 
                        break;        
                }
            }
        }

        static void GenerarReporte(Cuenta cuenta)
        {
            Console.WriteLine("----REPORTE DE TRANSACCIONES -----"); 

            double totalRetirado = 0; 
            double mayorRetiro = 0; 

            for (int i = 0; i < cuenta.Retiros.Count; i++)
            {
                totalRetirado += cuenta.Retiros[i]; 

                if(cuenta.Retiros[i] > mayorRetiro)
                {
                    mayorRetiro = cuenta.Retiros[i]; 
                }
            }

            int cantidadRetiros = cuenta.Retiros.Count; 
            double promedio = cantidadRetiros > 0 ? totalRetirado / cantidadRetiros: 0; 

            Console.WriteLine($"Cantidad de retiros: {cantidadRetiros}"); 
            Console.WriteLine($"Total retirado: {totalRetirado}");
            Console.WriteLine($"Promedio de retiros: {cantidadRetiros}"); 
            Console.WriteLine($"Retiro mayor: {mayorRetiro}");  
            Console.WriteLine($"Saldo Final: ${cuenta.Saldo}");
        }
                    
    }
    
}
