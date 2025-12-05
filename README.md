# Administrador de Lugares con Patrón Composite

Este proyecto implementa un administrador simple para el manejo de lugares que se encadenan de forma jerárquica, utilizando el patrón de diseño **Composite**.

## Características

- ✅ Jerarquías de lugares configurables por compania
- ✅ Cada compania puede tener su propia estructura jerárquica
- ✅ Agregar y eliminar elementos de la jerarquía
- ✅ Sumarización de ventas desde el nivel más bajo hasta el más alto
- ✅ Implementación del patrón Composite según el diagrama especificado

## Estructura del Proyecto

### Clases Principales

#### `ILugar` (Clase Abstracta)
Interfaz base que define la estructura común para todos los lugares en la jerarquía. Implementa el patrón Composite.

**Propiedades:**
- `Codigo`: Identificador único del lugar
- `CodigoPadre`: Código del lugar padre en la jerarquía
- `Nivel`: Nivel jerárquico del lugar (ej: "País", "Provincia", "Ciudad")
- `Nombre`: Nombre descriptivo del lugar
- `Ventas`: Ventas asociadas al lugar

**Métodos:**
- `ObtenerHijos()`: Obtiene la lista de hijos
- `AgregarHijo(ILugar)`: Agrega un hijo al lugar
- `EliminarHijo(ILugar)`: Elimina un hijo del lugar
- `ObtenerPadre()`: Obtiene el lugar padre
- `SumarizarVentas()`: Sumariza las ventas recursivamente

#### `LugarContenedor` (Clase Concreta)
Representa un lugar que puede contener otros lugares (nodos intermedios de la jerarquía).

#### `LugarHoja` (Clase Concreta)
Representa un lugar que no puede tener hijos (nivel más bajo de la jerarquía, donde se registran las ventas).

#### `Companias`
Representa una compania con su configuración de jerarquía de lugares.

#### `ConfiguracionJerarquia`
Define la configuración de la jerarquía de lugares para una compania, especificando los niveles desde el más alto (raíz) hasta el más bajo (hoja).

#### `AdministradorLugares`
Clase principal que gestiona las companias, sus jerarquías y las operaciones sobre los lugares.

## Ejemplos de Uso

### Configuración de Jerarquías

**Saludsa:**
- País → Provincia → Ciudad → Sucursal

**Serviálamo:**
- País → Región → Departamento → Ciudad → Agencia

### Ejemplo de Código

```csharp
// Crear el administrador
var administrador = new AdministradorLugares();

// Configurar jerarquía para Saludsa
var configSaludsa = new ConfiguracionJerarquia(
    "Jerarquía Saludsa",
    new List<string> { "País", "Provincia", "Ciudad", "Sucursal" }
);

// Registrar compania
var saludsa = new Companias("SALUDSA", "Saludsa", configSaludsa);
administrador.RegistrarCompanias(saludsa);

// Crear lugares
administrador.CrearLugar("SALUDSA", "EC", "Ecuador", "País");
administrador.CrearLugar("SALUDSA", "PICH", "Pichincha", "Provincia", "EC");
administrador.CrearLugar("SALUDSA", "UIO", "Quito", "Ciudad", "PICH");
administrador.CrearLugar("SALUDSA", "UIO-1", "Sucursal Quito Norte", "Sucursal", "UIO", 50000);

// Obtener reporte de ventas
var reporte = administrador.ObtenerReporteVentas("SALUDSA");
```

## Sumarización de Ventas

Las ventas se sumarizan automáticamente desde el nivel más bajo (hoja) hacia arriba en la jerarquía. Por ejemplo:

- Si una Sucursal tiene ventas de $50,000
- La Ciudad que la contiene sumará esas ventas
- La Provincia sumará las ventas de todas sus Ciudades
- El País sumará las ventas de todas sus Provincias

## Diagrama del Patrón Composite

El proyecto implementa el siguiente modelo:

```
ILugar (abstracta)
├── LugarContenedor (puede tener hijos)
└── LugarHoja (no puede tener hijos)
```

## Requisitos

- .NET 6.0 o superior

## Ejecución

```bash
dotnet run
```

El programa ejecutará una demostración completa mostrando:
1. Registro de companias con diferentes jerarquías
2. Construcción de jerarquías de lugares
3. Sumarización de ventas
4. Eliminación de elementos de la jerarquía

## Autor

Implementado según especificaciones del patrón Composite.

