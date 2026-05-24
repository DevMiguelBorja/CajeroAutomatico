# CajeroAutomatico

Creado Por MiguelBorja 
Problematica: Un nuevo Banco en la ciudad para desarrolladores necesita implementar la logica inicial para el funcionamiento de sus cajeros, se debe validar pin y numero de cuenta, adicional bloquear aquellos usuarios que ingresan un pin invalido mas de 3 veces.

Este algoritmo es una simulación al funcionamiento de un cajero Automatico
su estructura es basada en bucles While que validan información booleana y permiten acceder al menú de opciones 
por ahora se cuenta con 3 accione, mostrar el saldo disponible , hacer un retiro y salir (Se termina el bucle)
se utilizaron estructuras condicionales if para validar cuenta y validar que el saldo no sea menor al retiro. 
Tambien se utilizó un contador para validar el numero de intentos permitidos hasta bloquear su cuenta. 
 
Arquitectura: 
El proyecto fue refactorizado siguiendo principios de modularidad y responsabilidad única.

FUNCIONALIDADES
Inicio de sesión mediante número de cuenta y PIN.
Consulta de saldo.
Retiro de dinero.
Simulación de CDT con interés compuesto.
Generación de reporte de transacciones.
Validación de entradas usando TryParse.
Control de intentos de autenticación.