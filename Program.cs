using System;
using System.Collections.Generic;

namespace DemoCompositeLugar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Administrador de Lugares con Patrón Composite ===\n");

            // Crear el administrador
            var administrador = new AdministradorLugares();
            
            //Se puede capturar la estructura jerárquica básica desde el propio JSON
            // Configurar jerarquía para Saludsa
            var configSaludsa = new ConfiguracionJerarquia(
                "Jerarquía Saludsa",
                new List<string> { "País", "Provincia", "Ciudad", "Sucursal" }
            );

            // Configurar jerarquía para Serviálamo
            var configServialamo = new ConfiguracionJerarquia(
                "Jerarquía Serviálamo",
                new List<string> { "País", "Región", "Departamento", "Ciudad", "Agencia" }
            );

            // Registrar companias
            var saludsa = new Compania("SALUDSA", "Saludsa", configSaludsa);
            var servialamo = new Compania("SERVIA", "Serviálamo", configServialamo);

            administrador.RegistrarCompania(saludsa);
            administrador.RegistrarCompania(servialamo);

            Console.WriteLine("companias registradas:");
            Console.WriteLine($"- {saludsa.Nombre} ({saludsa.Codigo})");
            Console.WriteLine($"- {servialamo.Nombre} ({servialamo.Codigo})\n");

            // === Ejemplo con Saludsa ===
            Console.WriteLine("=== Construyendo jerarquía para Saludsa ===\n");

            //Con la jerarquía ya creada lo que resta es ingresar la data, que obviamente se sacaría también del JSON
            // Crear la jerarquía: País -> Provincia -> Ciudad -> Sucursal
            administrador.CrearLugar("SALUDSA", "EC", "Ecuador", "País");
            administrador.CrearLugar("SALUDSA", "PICH", "Pichincha", "Provincia", "EC");
            administrador.CrearLugar("SALUDSA", "GYE", "Guayas", "Provincia", "EC");
            administrador.CrearLugar("SALUDSA", "UIO", "Quito", "Ciudad", "PICH");
            administrador.CrearLugar("SALUDSA", "GYE-C", "Guayaquil", "Ciudad", "GYE");
            administrador.CrearLugar("SALUDSA", "UIO-1", "Sucursal Quito Norte", "Sucursal", "UIO", 50000);
            administrador.CrearLugar("SALUDSA", "UIO-2", "Sucursal Quito Sur", "Sucursal", "UIO", 45000);
            administrador.CrearLugar("SALUDSA", "GYE-1", "Sucursal Guayaquil Centro", "Sucursal", "GYE-C", 60000);
            administrador.CrearLugar("SALUDSA", "GYE-2", "Sucursal Guayaquil Norte", "Sucursal", "GYE-C", 55000);

            Console.WriteLine("Jerarquía creada para Saludsa:");
            MostrarJerarquia(administrador.ObtenerLugar("EC"), 0);

            // Sumarizar ventas
            Console.WriteLine("\n=== Reporte de Ventas Sumarizadas (Saludsa) ===");
            var reporteSaludsa = administrador.ObtenerReporteVentas("SALUDSA");
            foreach (var item in reporteSaludsa)
            {
                var lugar = administrador.ObtenerLugar(item.Key);
                Console.WriteLine($"{lugar.Nombre} ({lugar.Nivel}): ${item.Value:N2}");
            }

            // === Ejemplo con Serviálamo ===
            Console.WriteLine("\n\n=== Construyendo jerarquía para Serviálamo ===\n");

            //Con la jerarquía ya creada lo que resta es ingresar la data, que obviamente se sacaría también del JSON
            // Crear la jerarquía: País -> Región -> Departamento -> Ciudad -> Agencia
            administrador.CrearLugar("SERVIA", "CO", "Colombia", "País");
            administrador.CrearLugar("SERVIA", "REG-N", "Región Norte", "Región", "CO");
            administrador.CrearLugar("SERVIA", "REG-S", "Región Sur", "Región", "CO");
            administrador.CrearLugar("SERVIA", "DPT-ANT", "Antioquia", "Departamento", "REG-N");
            administrador.CrearLugar("SERVIA", "DPT-VAL", "Valle del Cauca", "Departamento", "REG-S");
            administrador.CrearLugar("SERVIA", "MED", "Medellín", "Ciudad", "DPT-ANT");
            administrador.CrearLugar("SERVIA", "CAL", "Cali", "Ciudad", "DPT-VAL");
            administrador.CrearLugar("SERVIA", "MED-1", "Agencia Medellín Centro", "Agencia", "MED", 75000);
            administrador.CrearLugar("SERVIA", "MED-2", "Agencia Medellín El Poblado", "Agencia", "MED", 80000);
            administrador.CrearLugar("SERVIA", "CAL-1", "Agencia Cali Norte", "Agencia", "CAL", 70000);
            administrador.CrearLugar("SERVIA", "CAL-2", "Agencia Cali Sur", "Agencia", "CAL", 65000);

            Console.WriteLine("Jerarquía creada para Serviálamo:");
            MostrarJerarquia(administrador.ObtenerLugar("CO"), 0);

            // Sumarizar ventas
            Console.WriteLine("\n=== Reporte de Ventas Sumarizadas (Serviálamo) ===");
            var reporteServialamo = administrador.ObtenerReporteVentas("SERVIA");
            foreach (var item in reporteServialamo)
            {
                var lugar = administrador.ObtenerLugar(item.Key);
                Console.WriteLine($"{lugar.Nombre} ({lugar.Nivel}): ${item.Value:N2}");
            }

            // === Demostración de eliminación ===
            Console.WriteLine("\n\n=== Demostración: Eliminando una sucursal ===\n");
            Console.WriteLine("Eliminando Sucursal Quito Sur (UIO-2)...");
            administrador.EliminarLugar("UIO-2");

            Console.WriteLine("\nJerarquía actualizada para Saludsa:");
            MostrarJerarquia(administrador.ObtenerLugar("EC"), 0);

            Console.WriteLine("\n=== Reporte de Ventas Actualizado (Saludsa) ===");
            var reporteActualizado = administrador.ObtenerReporteVentas("SALUDSA");
            foreach (var item in reporteActualizado)
            {
                var lugar = administrador.ObtenerLugar(item.Key);
                Console.WriteLine($"{lugar.Nombre} ({lugar.Nivel}): ${item.Value:N2}");
            }

            Console.WriteLine("\n\n=== Demostración completada ===");
        }

        static void MostrarJerarquia(ILugar lugar, int nivel)
        {
            string indentacion = new string(' ', nivel * 2);
            Console.WriteLine($"{indentacion}├─ {lugar.Nombre} ({lugar.Nivel}) [Código: {lugar.Codigo}]");

            if (lugar is LugarContenedor contenedor)
            {
                foreach (var hijo in contenedor.ObtenerHijos())
                {
                    MostrarJerarquia(hijo, nivel + 1);
                }
            }
        }
    }
}

