using System.Collections.Generic;
using System.Linq;

namespace DemoCompositeLugar
{
    /// <summary>
    /// Clase concreta que representa un lugar contenedor (puede tener hijos).
    /// Implementa el patrón Composite como contenedor.
    /// </summary>
    public class LugarContenedor : ILugar
    {
        private List<ILugar> hijos;
        private ILugar? padre;

        public LugarContenedor(string codigo, string nombre, string nivel, string? codigoPadre = null)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.nivel = nivel;
            this.codigoPadre = codigoPadre;
            this.ventas = 0;
            this.hijos = new List<ILugar>();
        }

        public override List<ILugar> ObtenerHijos()
        {
            return new List<ILugar>(hijos);
        }

        public override bool AgregarHijo(ILugar lugar)
        {
            if (lugar == null)
                return false;

            // Verificar que no sea el mismo lugar
            if (lugar.Codigo == this.codigo)
                return false;

            // Verificar que no esté ya agregado
            if (hijos.Any(h => h.Codigo == lugar.Codigo))
                return false;

            // Establecer el padre del lugar hijo
            lugar.CodigoPadre = this.codigo;
            hijos.Add(lugar);
            return true;
        }

        public override bool EliminarHijo(ILugar lugar)
        {
            if (lugar == null)
                return false;

            var lugarAEliminar = hijos.FirstOrDefault(h => h.Codigo == lugar.Codigo);
            if (lugarAEliminar != null)
            {
                hijos.Remove(lugarAEliminar);
                lugar.CodigoPadre = null;
                return true;
            }

            return false;
        }

        public override ILugar? ObtenerPadre()
        {
            return padre;
        }

        /// <summary>
        /// Sumariza las ventas de este contenedor y todos sus hijos recursivamente.
        /// </summary>
        public override decimal SumarizarVentas()
        {
            decimal totalVentas = this.ventas;

            // Sumar las ventas de todos los hijos
            foreach (var hijo in hijos)
            {
                totalVentas += hijo.SumarizarVentas();
            }

            return totalVentas;
        }

        public override void EstablecerPadre(ILugar? lugarPadre)
        {
            this.padre = lugarPadre;
            if (lugarPadre != null)
            {
                this.codigoPadre = lugarPadre.Codigo;
            }
        }

        /// <summary>
        /// Obtiene el número de hijos directos.
        /// </summary>
        public int CantidadHijos()
        {
            return hijos.Count;
        }
    }
}

