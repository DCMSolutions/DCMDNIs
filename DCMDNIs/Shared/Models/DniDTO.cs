using System;
using System.Collections.Generic;

namespace DCMDNIs.Shared.Models;

public partial class DniDTO
{
    public int Id { get; set; }

    public int? Numero { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public bool? Habilitado { get; set; }

    public string? Mensaje { get; set; }

}
