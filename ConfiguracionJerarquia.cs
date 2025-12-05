using System.Collections.Generic;

namespace DemoCompositeLugar
{
    /// <summary>
    /// Define la configuración de la jerarquía de lugares para una compania.
    /// Especifica los niveles desde el más alto (raíz) hasta el más bajo (hoja).
    /// </summary>
    public class ConfiguracionJerarquia
    {
        public string Nombre { get; set; }
        public List<string> Niveles { get; set; }

        public ConfiguracionJerarquia(string nombre, List<string> niveles)
        {
            Nombre = nombre;
            Niveles = niveles;
        }

        /// <summary>
        /// Obtiene el nivel raíz (el primero de la lista).
        /// </summary>
        public string? NivelRaiz()
        {
            return Niveles.Count > 0 ? Niveles[0] : null;
        }

        /// <summary>
        /// Obtiene el nivel hoja (el último de la lista).
        /// </summary>
        public string? NivelHoja()
        {
            return Niveles.Count > 0 ? Niveles[Niveles.Count - 1] : null;
        }

        /// <summary>
        /// Verifica si un nivel es el nivel hoja (más bajo).
        /// </summary>
        public bool EsNivelHoja(string nivel)
        {
            return nivel == NivelHoja();
        }

        /// <summary>
        /// Obtiene el nivel siguiente en la jerarquía.
        /// </summary>
        public string? ObtenerNivelSiguiente(string nivelActual)
        {
            int indice = Niveles.IndexOf(nivelActual);
            if (indice >= 0 && indice < Niveles.Count - 1)
            {
                return Niveles[indice + 1];
            }
            return null;
        }

        /// <summary>
        /// Obtiene el nivel anterior en la jerarquía.
        /// </summary>
        public string? ObtenerNivelAnterior(string nivelActual)
        {
            int indice = Niveles.IndexOf(nivelActual);
            if (indice > 0)
            {
                return Niveles[indice - 1];
            }
            return null;
        }
    }
}

