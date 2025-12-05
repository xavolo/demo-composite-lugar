using System.Collections.Generic;

namespace DemoCompositeLugar
{
    /// <summary>
    /// Interfaz abstracta que define la estructura común para todos los lugares en la jerarquía.
    /// Implementa el patrón Composite.
    /// </summary>
    public abstract class ILugar
    {
        protected string codigo = string.Empty;
        protected string? codigoPadre;
        protected string nivel = string.Empty;
        protected string nombre = string.Empty;
        protected decimal ventas;

        public string Codigo { get => codigo; set => codigo = value; }
        public string? CodigoPadre { get => codigoPadre; set => codigoPadre = value; }
        public string Nivel { get => nivel; set => nivel = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public decimal Ventas { get => ventas; set => ventas = value; }

        /// <summary>
        /// Obtiene la lista de hijos del lugar (solo para contenedores).
        /// </summary>
        public abstract List<ILugar> ObtenerHijos();

        /// <summary>
        /// Agrega un hijo al lugar (solo para contenedores).
        /// </summary>
        public abstract bool AgregarHijo(ILugar lugar);

        /// <summary>
        /// Elimina un hijo del lugar (solo para contenedores).
        /// </summary>
        public abstract bool EliminarHijo(ILugar lugar);

        /// <summary>
        /// Obtiene el padre del lugar.
        /// </summary>
        public abstract ILugar? ObtenerPadre();

        /// <summary>
        /// Sumariza las ventas desde este lugar hacia arriba en la jerarquía.
        /// </summary>
        public abstract decimal SumarizarVentas();

        /// <summary>
        /// Define el padre de lugar.
        /// </summary>
        public abstract void EstablecerPadre(ILugar? lugar);
    }
}

