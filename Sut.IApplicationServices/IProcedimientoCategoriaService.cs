using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IProcedimientoCategoriaService
    {
        ProcedimientoCategoria GetOne(long ProcedimientoCategoriaid);

        List<ProcedimientoCategoria> Lsitaprocedimientocategoria(long ExpedienteId, long ProcedimientoCategoriaid);


        List<ProcedimientoCategoria> Lsitaprocedimientocategoriavalor0(long ExpedienteId);
        List<ProcedimientoCategoria> Lsitaprocedimientocategoriavalor(long ProcedimientoId);

        void Save(ProcedimientoCategoria obj);
        void Modificar(ProcedimientoCategoria obj);
        void Eliminar(ProcedimientoCategoria obj);

    }
}
