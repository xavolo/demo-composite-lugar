namespace DemoCompositeLugar
{
    /// <summary>
    /// Representa una compania con su configuración de jerarquía de lugares.
    /// </summary>
    public class Compania
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public ConfiguracionJerarquia ConfiguracionJerarquia { get; set; }
        public ILugar? RaizJerarquia { get; set; }

        public Compania(string codigo, string nombre, ConfiguracionJerarquia configuracionJerarquia)
        {
            Codigo = codigo;
            Nombre = nombre;
            ConfiguracionJerarquia = configuracionJerarquia;
            RaizJerarquia = null;
        }
    }
}

