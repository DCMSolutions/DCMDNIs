using System;
using System.Collections.Generic;

namespace DCMDNIs.Server.Models;

public partial class Dni
{
    public int Id { get; set; }

    public int? Numero { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public bool? Habilitado { get; set; }
}
