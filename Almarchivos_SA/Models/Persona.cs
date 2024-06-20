using Almarchivos_SA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Persona
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id_Persona { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombres { get; set; }

    [Required]
    [StringLength(100)]
    public string Apellidos { get; set; }

    [Required]
    [StringLength(50)]
    public string Numero_Identificacion { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string Tipo_Identificacion { get; set; }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Fecha_Creacion { get; set; }

}
