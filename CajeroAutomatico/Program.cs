using System; 
class Program
{
    static void Main()
    {
        int cuenta; 
        int pin; 

        int cuentaExistente = 123456; 
        int pinExistente = 1111; 

        bool cuentaValida = false; 

        Console.WriteLine("Bienvenido a DEVBANK!!");

        while (!cuentaValida)
        {
            Console.WriteLine("Ingresa tú número de cuenta: "); 
            cuenta = Convert.ToInt32(Console.ReadLine());             

            if (cuenta == cuentaExistente)
            {
                int intentos = 0; 
                bool pinCorrecto = false; 
                while (intentos < 3 && !pinCorrecto)
                {
                    Console.WriteLine("Ingresa tú PIN: "); 
                    pin = Convert.ToInt32(Console.ReadLine());  
                    if (pin == pinExistente)
                    {
                        Console.WriteLine("Has iniciado sesión correctamente."); 
                        pinCorrecto = true; 
                        cuentaValida = true; 

                        double saldo = 500000; 
                        int opcion = 0; 

                        while(opcion != 3)
                        {
                            Console.WriteLine("-----DEVBANK------"); 
                            Console.WriteLine("1.- Consultar saldo ---"); 
                            Console.WriteLine("2.- Retirar dinero ---"); 
                            Console.WriteLine("3.- Salir ---");
                            Console.WriteLine("-----SELECCIONA UNA OPCION------");
                            opcion = Convert.ToInt32(Console.ReadLine()); 

                            switch (opcion)
                            {
                                case 1: 
                                    Console.WriteLine("Su saldo actual es: $" + saldo); 
                                    break; 
                                case 2: 
                                    Console.WriteLine("Ingresa el monto a retirar: ");
                                    double retiro = Convert.ToDouble(Console.ReadLine()); 

                                    if (retiro <= saldo )
                                    {
                                        saldo -= retiro; 
                                        Console.WriteLine("Retiro exitoso!"); 
                                        Console.WriteLine("Saldo disponible: $" + saldo); 
                                    } else
                                    {
                                        Console.WriteLine("Saldo insuficiente"); 
                                    }
                                    break;     
                                case 3: 
                                    Console.WriteLine("Gracias por usar nuestro cajero DevBank"); 
                                    break;
                                default: 
                                    Console.WriteLine("Opción Inválida"); 
                                    break;     
                            }       

                        }
                    } else
                    {
                        intentos++; 
                        Console.WriteLine("PIN Incorrecto");
                    }
                }
                if (!pinCorrecto)
                {
                    Console.WriteLine("Cuenta Bloqueada por intentos fallidos!"); 
                    break; 
                }
                
            } else
            {
                Console.WriteLine("La cuenta no existe"); 
            }
            
        }

    }
    
}