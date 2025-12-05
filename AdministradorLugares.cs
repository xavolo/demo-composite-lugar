using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoCompositeLugar
{
    /// <summary>
    /// Administrador principal para el manejo de lugares jerárquicos por compania.
    /// Gestiona las companias, sus jerarquías y las operaciones sobre los lugares.
    /// </summary>
    public class AdministradorLugares
    {
        private Dictionary<string, Compania> companias;
        private Dictionary<string, ILugar> lugaresPorCodigo;

        public AdministradorLugares()
        {
            companias = new Dictionary<string, Compania>();
            lugaresPorCodigo = new Dictionary<string, ILugar>();
        }

        /// <summary>
        /// Registra una nueva compania con su configuración de jerarquía.
        /// </summary>
        public void RegistrarCompania(Compania compania)
        {
            if (companias.ContainsKey(compania.Codigo))
            {
                throw new InvalidOperationException($"La compania con código {compania.Codigo} ya está registrada.");
            }

            companias[compania.Codigo] = compania;
        }

        /// <summary>
        /// Obtiene una compania por su código.
        /// </summary>
        public Compania ObtenerCompania(string codigoCompania)
        {
            if (!companias.ContainsKey(codigoCompania))
            {
                throw new KeyNotFoundException($"No se encontró la compania con código {codigoCompania}.");
            }

            return companias[codigoCompania];
        }

        /// <summary>
        /// Crea un nuevo lugar en la jerarquía de una compania.
        /// </summary>
        public ILugar CrearLugar(string codigoCompania, string codigo, string nombre, string nivel, string? codigoPadre = null, decimal ventas = 0)
        {
            var compania = ObtenerCompania(codigoCompania);

            // Verificar que el nivel existe en la configuración
            if (!compania.ConfiguracionJerarquia.Niveles.Contains(nivel))
            {
                throw new ArgumentException($"El nivel '{nivel}' no existe en la configuración de jerarquía de la compania {compania.Nombre}.");
            }

            // Verificar que no exista ya un lugar con ese código
            if (lugaresPorCodigo.ContainsKey(codigo))
            {
                throw new InvalidOperationException($"Ya existe un lugar con el código {codigo}.");
            }

            ILugar nuevoLugar;

            // Si es el nivel hoja, crear una hoja, sino un contenedor
            if (compania.ConfiguracionJerarquia.EsNivelHoja(nivel))
            {
                nuevoLugar = new LugarHoja(codigo, nombre, nivel, codigoPadre, ventas);
            }
            else
            {
                nuevoLugar = new LugarContenedor(codigo, nombre, nivel, codigoPadre);
            }

            // Si tiene padre, agregarlo como hijo del padre
            if (!string.IsNullOrEmpty(codigoPadre))
            {
                if (lugaresPorCodigo.ContainsKey(codigoPadre))
                {
                    var padre = lugaresPorCodigo[codigoPadre];
                    if (padre is LugarContenedor contenedorPadre)
                    {
                        contenedorPadre.AgregarHijo(nuevoLugar);
                        if (nuevoLugar is LugarHoja hoja)
                        {
                            hoja.EstablecerPadre(padre);
                        }
                        else if (nuevoLugar is LugarContenedor contenedor)
                        {
                            contenedor.EstablecerPadre(padre);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"El lugar padre {codigoPadre} no puede tener hijos (es una hoja).");
                    }
                }
                else
                {
                    throw new KeyNotFoundException($"No se encontró el lugar padre con código {codigoPadre}.");
                }
            }
            else
            {
                // Si no tiene padre, debe ser la raíz
                if (compania.RaizJerarquia != null)
                {
                    throw new InvalidOperationException($"La compania {compania.Nombre} ya tiene una raíz de jerarquía.");
                }
                compania.RaizJerarquia = nuevoLugar;
            }

            lugaresPorCodigo[codigo] = nuevoLugar;
            return nuevoLugar;
        }

        /// <summary>
        /// Elimina un lugar de la jerarquía.
        /// </summary>
        public bool EliminarLugar(string codigo)
        {
            if (!lugaresPorCodigo.ContainsKey(codigo))
            {
                return false;
            }

            var lugar = lugaresPorCodigo[codigo];

            // Si tiene padre, eliminarlo de los hijos del padre
            if (!string.IsNullOrEmpty(lugar.CodigoPadre))
            {
                if (lugaresPorCodigo.ContainsKey(lugar.CodigoPadre))
                {
                    var padre = lugaresPorCodigo[lugar.CodigoPadre];
                    if (padre is LugarContenedor contenedorPadre)
                    {
                        contenedorPadre.EliminarHijo(lugar);
                    }
                }
            }
            else
            {
                // Si es la raíz, buscar la compania y limpiar su raíz
                foreach (var compania in companias.Values)
                {
                    if (compania.RaizJerarquia != null && compania.RaizJerarquia.Codigo == codigo)
                    {
                        compania.RaizJerarquia = null;
                        break;
                    }
                }
            }

            // Si es un contenedor, eliminar todos sus hijos recursivamente
            if (lugar is LugarContenedor contenedor)
            {
                var hijos = contenedor.ObtenerHijos();
                foreach (var hijo in hijos)
                {
                    EliminarLugar(hijo.Codigo);
                }
            }

            lugaresPorCodigo.Remove(codigo);
            return true;
        }

        /// <summary>
        /// Obtiene un lugar por su código.
        /// </summary>
        public ILugar ObtenerLugar(string codigo)
        {
            if (!lugaresPorCodigo.ContainsKey(codigo))
            {
                throw new KeyNotFoundException($"No se encontró el lugar con código {codigo}.");
            }

            return lugaresPorCodigo[codigo];
        }

        /// <summary>
        /// Obtiene todos los lugares de una compania.
        /// </summary>
        public List<ILugar> ObtenerLugaresPorCompanias(string codigoCompanias)
        {
            var compania = ObtenerCompania(codigoCompanias);
            var lugares = new List<ILugar>();

            if (compania.RaizJerarquia != null)
            {
                RecopilarLugares(compania.RaizJerarquia, lugares);
            }

            return lugares;
        }

        /// <summary>
        /// Recopila todos los lugares de forma recursiva desde un lugar raíz.
        /// </summary>
        private void RecopilarLugares(ILugar lugar, List<ILugar> lista)
        {
            lista.Add(lugar);

            if (lugar is LugarContenedor contenedor)
            {
                foreach (var hijo in contenedor.ObtenerHijos())
                {
                    RecopilarLugares(hijo, lista);
                }
            }
        }

        /// <summary>
        /// Sumariza las ventas desde un lugar específico hacia arriba en la jerarquía.
        /// </summary>
        public decimal SumarizarVentasDesdeLugar(string codigoLugar)
        {
            var lugar = ObtenerLugar(codigoLugar);
            return lugar.SumarizarVentas();
        }

        /// <summary>
        /// Obtiene el reporte de ventas sumarizadas para toda la jerarquía de una compania.
        /// </summary>
        public Dictionary<string, decimal> ObtenerReporteVentas(string codigoCompanias)
        {
            var compania = ObtenerCompania(codigoCompanias);
            var reporte = new Dictionary<string, decimal>();

            if (compania.RaizJerarquia != null)
            {
                GenerarReporteVentas(compania.RaizJerarquia, reporte);
            }

            return reporte;
        }

        /// <summary>
        /// Genera el reporte de ventas de forma recursiva.
        /// </summary>
        private void GenerarReporteVentas(ILugar lugar, Dictionary<string, decimal> reporte)
        {
            var ventasSumarizadas = lugar.SumarizarVentas();
            reporte[lugar.Codigo] = ventasSumarizadas;

            if (lugar is LugarContenedor contenedor)
            {
                foreach (var hijo in contenedor.ObtenerHijos())
                {
                    GenerarReporteVentas(hijo, reporte);
                }
            }
        }

        /// <summary>
        /// Obtiene todas las companias registradas.
        /// </summary>
        public List<Compania> ObtenerTodasLascompanias()
        {
            return companias.Values.ToList();
        }
    }
}

