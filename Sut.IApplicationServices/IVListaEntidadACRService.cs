﻿using Sut.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.IApplicationServices
{
    public interface IVListaEntidadACRService
    { 
        List<VListaEntidadACR> GetAll();

        List<VListaEntidadACR> GetAllLikePagin(VListaEntidadACR filtro, int pageIndex, int pageSize, ref int totalRows);


    }
}
