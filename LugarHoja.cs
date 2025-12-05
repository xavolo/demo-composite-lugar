namespace DemoCompositeLugar
{
    /// <summary>
    /// Clase concreta que representa un lugar hoja (no puede tener hijos).
    /// Implementa el patrón Composite como hoja.
    /// </summary>
    public class LugarHoja : ILugar
    {
        private ILugar? padre;

        public LugarHoja(string codigo, string nombre, string nivel, string? codigoPadre = null, decimal ventas = 0)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.nivel = nivel;
            this.codigoPadre = codigoPadre;
            this.ventas = ventas;
        }

        public override List<ILugar> ObtenerHijos()
        {
            // Una hoja no tiene hijos
            return new List<ILugar>();
        }

        public override bool AgregarHijo(ILugar lugar)
        {
            // Una hoja no puede tener hijos
            return false;
        }

        public override bool EliminarHijo(ILugar lugar)
        {
            // Una hoja no tiene hijos
            return false;
        }

        public override ILugar? ObtenerPadre()
        {
            return padre;
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
        /// Sumariza las ventas de esta hoja (solo retorna sus propias ventas).
        /// </summary>
        public override decimal SumarizarVentas()
        {
            return this.ventas;
        }
    }
}

