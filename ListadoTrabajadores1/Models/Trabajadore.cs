﻿using System;
using System.Collections.Generic;

namespace ListadoTrabajadores1.Models;

public partial class Trabajadore
{
    public int Id { get; set; }

    public string? TipoDocumento { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? Nombres { get; set; }

    public string? Sexo { get; set; }

    public int? IdDepartamento { get; set; }

    public int? IdProvincia { get; set; }

    public int? IdDistrito { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Distrito? IdDistritoNavigation { get; set; }

    public virtual Provincium? IdProvinciaNavigation { get; set; }
}
