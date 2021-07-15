using System;
namespace Wass.Back.Empresa.Models.Enum
{
    public enum Transaction
    {
        Insert = 1,
        Update = 2,
        Delete = 3,
        InsertMasive = 4,
        UpdateMasive = 5
    }

    public enum EmpresaEstados
    {
        Creada = 1,
        Rechazada = 2,
        Activada = 3
    }
}
