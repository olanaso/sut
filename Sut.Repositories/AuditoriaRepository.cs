using Sut.Context;
using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Sut.Repositories.Procedimientos;
using System.Data;
using Sut.Repositories.Configuracion;

namespace Sut.Repositories
{
    public class AuditoriaRepository : BaseRepository<Auditoria>, IAuditoriaRepository
    {
        public AuditoriaRepository(IContext context) : base(context) { }

        public Auditoria GetOne(long Auditoriaid)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Auditoria
                        .SingleOrDefault(x => x.AuditoriaID == Auditoriaid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Auditoria> GetAll(Auditoria filtro)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Auditoria
                    .Include("Entidad")
                              .Where(x => x.EntidadId == filtro.EntidadId)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<Auditoria> GetAllAdmin()
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;

                return ctx.Auditoria
                    .Include("Entidad")
                        .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



  
       

        public int InsertarusuarioAsig(UsuarioAsignacion auditoria)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.EntidadId, auditoria.EntidadId);
                parameters.Add(ProcedimientoAuditoria.Usuario, auditoria.UsuarioId); 
                parameters.Add(ProcedimientoAuditoria.UserCreacion, auditoria.UserCreacion);
                parameters.Add(ProcedimientoAuditoria.FecCreacion, auditoria.FecCreacion);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.InsertarusuarioAsig,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int QuitarusuarioAsig(UsuarioAsignacion auditoria)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.EntidadId, auditoria.EntidadId);
                parameters.Add(ProcedimientoAuditoria.Usuario, auditoria.UsuarioId); 

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.QuitarusuarioAsig,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }


        public int InsertarusuarioAsigActividad(GrupoEntidad auditoria)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.EntidadId, auditoria.EntidadId);
                parameters.Add(ProcedimientoAuditoria.CalendarioActividadesId, auditoria.CalendarioActividadesId);
                parameters.Add(ProcedimientoAuditoria.UserCreacion, auditoria.UserCreacion);
                parameters.Add(ProcedimientoAuditoria.FecCreacion, auditoria.FecCreacion);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.InsertarusuarioAsigActividad,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int QuitarusuarioAsigActividad(GrupoEntidad auditoria)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.EntidadId, auditoria.EntidadId);
                parameters.Add(ProcedimientoAuditoria.CalendarioActividadesId, auditoria.CalendarioActividadesId);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.QuitarusuarioAsigActividad,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

         

        public new void Save(Auditoria obj)
        {
            try
            {
                SutContext ctx = Context.GetContext() as SutContext;
                ctx.Entry(obj).State = obj.AuditoriaID > 0 ? EntityState.Modified : EntityState.Added;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int registrarAuditoria(Auditoria auditoria)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.EntidadId, auditoria.EntidadId);
                parameters.Add(ProcedimientoAuditoria.SectorId, auditoria.SectorId);
                parameters.Add(ProcedimientoAuditoria.ProvinciaId, auditoria.ProvinciaId);
                parameters.Add(ProcedimientoAuditoria.Usuario, auditoria.Usuario);
                parameters.Add(ProcedimientoAuditoria.Actividad, auditoria.Actividad);
                parameters.Add(ProcedimientoAuditoria.Pantalla, auditoria.Pantalla);
                parameters.Add(ProcedimientoAuditoria.UserCreacion, auditoria.UserCreacion);
                parameters.Add(ProcedimientoAuditoria.FecCreacion, auditoria.FecCreacion);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.registrarAuditoria,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int PresentarAprobar(long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.PresentarAprobar,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

         public List<UnidadOrganica> GetAllListaUnidadOrganica(long pEntidadId, long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);
                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);

                var _idAuditoria = conexion.Query<UnidadOrganica>(
                                        ProcedimientoAuditoria.ListarUnidadOrganica,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        public List<RequisitoFormulario> GetAllListaReq(long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);

                var _idAuditoria = conexion.Query<RequisitoFormulario>(
                                        ProcedimientoAuditoria.LISTARARCHIVOSPA,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        public List<RequisitoFormulario> GetAllListaReqID(long pProcedimientoId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ProcedimientoId, pProcedimientoId);

                var _idAuditoria = conexion.Query<RequisitoFormulario>(
                                        ProcedimientoAuditoria.LISTARARCHIVOSPAID,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Expediente> listagrafico(long pSectorId, long pNivelGobiernoId, string pAcronimo)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.SectorId, pSectorId);
                parameters.Add(ProcedimientoAuditoria.NivelGobiernoId, pNivelGobiernoId);
                parameters.Add(ProcedimientoAuditoria.Acronimo, pAcronimo);

                var _idAuditoria = conexion.Query<Expediente>(
                                        ProcedimientoAuditoria.calculoexpediente,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        public List<FactorDedicacion> rptFactorTupa_NoTupa(long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);

                var _idAuditoria = conexion.Query<FactorDedicacion>(
                                        ProcedimientoAuditoria.RptFactorTupaNoTupa,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        
        public List<Auditoria> ListaRptEquipoTrabajo()
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();


                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.LISTAEQUIPOTRABAJO,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        public List<Auditoria> ListaRptUsuario()
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();
                 

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.LISTAUSUARIO,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> ListaRptExpediente()
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters(); 

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.LISTAEXPEDIENTE,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        public List<Auditoria> listaObs_NORMA(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsNORMA,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> listaObs_NORMALISTA(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsNORMALST,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> listaObs_ArAdjunto(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsArAdjunto,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        public List<Auditoria> listaObs_ciudadano(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsCiudadano,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        public List<Auditoria> listaObs_DatoGenerales(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsDatosGenerales,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> listaObs_DatoGeneralesComp(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsDatosGeneralesComp,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        
        public List<Auditoria> listaObs_STL(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsSTL,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> listaObs_TablaAsme(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsTablaAsme,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
		
		/*JJJMSP2*/
		public List<CompararPA> getProcedimientoComparar(long pEntidadId1, long pExpedienteId1, long pExpedienteId2)
        {
            using (var conexion = Conexion.GetConexionSUT())
            {
                var parameters = new DynamicParameters();
                parameters.Add(ProcedimientoAuditoria.EntidadId1, pEntidadId1);
                parameters.Add(ProcedimientoAuditoria.ExpedienteId1, pExpedienteId1);
                parameters.Add(ProcedimientoAuditoria.ExpedienteId2, pExpedienteId2);
                
                var lista = conexion.Query<CompararPA>(
                                        ProcedimientoAuditoria.paSutProcedimientoComparar,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                //var listastring = lista.Select(r => new CompararPA { Campo1 = r }).ToList();
                return lista.ToList();
            }
        }
		
        public List<Auditoria> listaObs_ResumenCosto(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsResumenCosto,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> listaObs_PersonalCosto(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsPersonalCosto,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> listaObs_MATERIALFUNGIBLE(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsMaterialFungibleCosto,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> listaObs_SERVICIOIDENTIFICABLE(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsCostosdeServiciosIdentificablesCosto,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> listaObs_MATERIALNOFUNGIBLE(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsCostosdeMaterialNoFungibleCosto,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> listaObs_SERVICIOTERCEROS(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsCostosdeServiciosdeTercerosCosto,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        public List<Auditoria> listaObs_DEPRECIACIONAMORIZACION(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsCostosdeDepreciacionyAmortizacionCosto,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> listaObs_COSTOSFIJOS(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsCostosFijosCosto,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
        public List<Auditoria> listaObs_VALORESINDUCTORES(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsConfiguracionInductorCosto,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }
 
        public List<Auditoria> listaObs_General(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.listaObsGeneral,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        public List<Auditoria> listaObs_INDUCTORSIGNACIONCOSTOS(long pExpedienteId, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<Auditoria>(
                                        ProcedimientoAuditoria.ListaObsConfiguracionAsignacionCosto,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.ToList();
            }
        }

        public int AlimpiarPosicion(long pExpedienteId, long pProcedimientoId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.ProcedimientoID, pProcedimientoId);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.limpiarposicion,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int GenerarNumeracion(long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId); 

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.GenerarNumeracion,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }
        public int BotonSiguienteAtras_INDEX(long pExpedienteId, long pExplorarNum)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.ExplorarNum, pExplorarNum);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.BotonSiguienteAtrasINDEX,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int ResetearFactor(long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId); 

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.ResetearFactor,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }
        

        public int ActualizarLicencias(long pUsuario)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.Usuario, pUsuario);
                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.ActualizarLicencias,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int ValidarUnidadOrganicaId(long pUnidadOrganicaId, long pEntidadId, string pDescripcion)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);
                parameters.Add(ProcedimientoAuditoria.Descripcion, pDescripcion);
                parameters.Add(ProcedimientoAuditoria.UnidadOrganicaId, pUnidadOrganicaId);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.ValidarUnidadOrganicaId,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int ValidarRecursoId(long pRecursoId, long pEntidadId, string pDescripcion)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);
                parameters.Add(ProcedimientoAuditoria.Descripcion, pDescripcion);
                parameters.Add(ProcedimientoAuditoria.RecursoId, pRecursoId);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.ValidarRecursoId,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }


        public int CambiarEstado(long pExpedienteId, long pEstadoExpediente, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.EstadoExpediente, pEstadoExpediente);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.CambiarEstadoExpediente,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int CambiarEstadoSustento(TipoSustento pTipoEstado, long pProcedimientoId, long pEstado, long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.TipoEstado,(short)pTipoEstado);
                parameters.Add(ProcedimientoAuditoria.ProcedimientoId, pProcedimientoId);
                parameters.Add(ProcedimientoAuditoria.Estado, pEstado);
                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.CambiarEstadoSustento,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }
        public int CambiarEstadoProceso(TipoSustento pTipoEstado, long pProcedimientoId, long pEstado, long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.TipoEstado, (short)pTipoEstado);
                parameters.Add(ProcedimientoAuditoria.ProcedimientoId, pProcedimientoId);
                parameters.Add(ProcedimientoAuditoria.Estado, pEstado);
                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.CambiarEstadoProceso,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }


        public int ObservarTtotal(long pExpedienteId, long pNum)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.Num, pNum);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.OBSTOTAL,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);

                return _idAuditoria.FirstOrDefault();

            }
        }
        public int ValidarTotal(long pExpedienteId, long pNum)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.Num, pNum);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.ValidarTOTAL,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);

                return _idAuditoria.FirstOrDefault();

            }
        }
        public int ValidarInductorId(long pInductorId, long pEntidadId, string pDescripcion)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);
                parameters.Add(ProcedimientoAuditoria.Descripcion, pDescripcion);
                parameters.Add(ProcedimientoAuditoria.InductorId, pInductorId);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.ValidarInductorId,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int CancelarObservacion(Observacion filtro, int pEstadoCambio, string pUsuarioCreacion, string pUserCreacion, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ObservacionId, filtro.ObservacionId);
                parameters.Add(ProcedimientoAuditoria.CodValidacion, filtro.CodValidacion);
                parameters.Add(ProcedimientoAuditoria.ProcedimientoId, filtro.ProcedimientoId);
                parameters.Add(ProcedimientoAuditoria.ExpedienteId, filtro.ExpedienteId);
                parameters.Add(ProcedimientoAuditoria.Estado, filtro.Estado);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);
                parameters.Add(ProcedimientoAuditoria.UsuarioCreacion, pUsuarioCreacion);
                parameters.Add(ProcedimientoAuditoria.Pantalla, filtro.Pantalla);
                parameters.Add(ProcedimientoAuditoria.UserCreacion, pUserCreacion);
                parameters.Add(ProcedimientoAuditoria.EstadoCambio, pEstadoCambio);


                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.CancelarObservacion,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int AgregarObservacion(Observacion filtro, int pEstadoCambio, string pUsuarioCreacion, string pUserCreacion, long pEntidadId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ObservacionId, filtro.ObservacionId);
                parameters.Add(ProcedimientoAuditoria.Descripcion, filtro.Descripcion);
                parameters.Add(ProcedimientoAuditoria.CodValidacion, filtro.CodValidacion);
                parameters.Add(ProcedimientoAuditoria.Estado, filtro.Estado);
                parameters.Add(ProcedimientoAuditoria.ExpedienteId, filtro.ExpedienteId);
                parameters.Add(ProcedimientoAuditoria.ProcedimientoId, filtro.ProcedimientoId);
                parameters.Add(ProcedimientoAuditoria.RequisitoId, filtro.RequisitoId);
                parameters.Add(ProcedimientoAuditoria.TablaAsmeId, filtro.TablaAsmeId);
                parameters.Add(ProcedimientoAuditoria.ActividadId, filtro.ActividadId);
                parameters.Add(ProcedimientoAuditoria.BaseLegalId, filtro.BaseLegalId);
                parameters.Add(ProcedimientoAuditoria.BaseLegalNormaId, filtro.BaseLegalNormaId);
                parameters.Add(ProcedimientoAuditoria.RecursoCostoId, filtro.RecursoCostoId);
                parameters.Add(ProcedimientoAuditoria.RecursoId, filtro.RecursoId);
                parameters.Add(ProcedimientoAuditoria.EntidadId, pEntidadId);
                parameters.Add(ProcedimientoAuditoria.CodPadre, filtro.CodPadre);
                parameters.Add(ProcedimientoAuditoria.CodHijo, filtro.CodHijo);
                parameters.Add(ProcedimientoAuditoria.TipoEstado, filtro.TipoEstado);
                parameters.Add(ProcedimientoAuditoria.NombreCampo, filtro.NombreCampo);
                parameters.Add(ProcedimientoAuditoria.UsuarioCreacion, pUsuarioCreacion);
                parameters.Add(ProcedimientoAuditoria.Pantalla, filtro.Pantalla);
                parameters.Add(ProcedimientoAuditoria.UserCreacion, pUserCreacion);
                parameters.Add(ProcedimientoAuditoria.EstadoCambio, pEstadoCambio);


                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.AgregarObservacion,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
                    
            }
        }

        public int AnularExpediente(long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.AnularExpediente,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public string VerificarEliminar(long UnidadOrganicaId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.UnidadOrganicaId, UnidadOrganicaId);
                var _idAuditoria = conexion.Query<string>(
                                        ProcedimientoAuditoria.VerificarEliminar,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.First();
            }
        }
        public string  VerificarRecursoEspera(long pProcedimientoId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ProcedimientoId, pProcedimientoId);
                var _idAuditoria = conexion.Query<string>(
                                        ProcedimientoAuditoria.EliminarRecuroEspera,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }
      
        public string VerificarEliminareditar(long UnidadOrganicaId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.UnidadOrganicaId, UnidadOrganicaId);
                var _idAuditoria = conexion.Query<string>(
                                        ProcedimientoAuditoria.VerificarEliminarEditar,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.First();
            }
        }


        public string VerificarEliminarReq(long RequisitoId, long TipoRequisito, long estadoExpediente)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.RequisitoId, RequisitoId);
                parameters.Add(ProcedimientoAuditoria.TipoRequisito, TipoRequisito);
                parameters.Add(ProcedimientoAuditoria.estadoExpediente, estadoExpediente);
                var _idAuditoria = conexion.Query<string>(
                                        ProcedimientoAuditoria.VerificarEliminarRequisitoId,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.First();
            }
        }

        public int ExcluirEntidadMEF(long pSectorId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.SectorId, pSectorId); 
                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.ExcluirEntidadMEF,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }
         


        public string VerificarEliminarSede(long SedeId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.SedeId, SedeId);
                var _idAuditoria = conexion.Query<string>(
                                        ProcedimientoAuditoria.VerificarEliminarSede,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.First();
            }
        }

        public string VerificarEliminarRecursoId(long RecursoId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.RecursoId, RecursoId);
                var _idAuditoria = conexion.Query<string>(
                                        ProcedimientoAuditoria.VerificarEliminarRecurso,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.First();
            }
        }

        public string VerificarEliminarRecursoIdEditar(long RecursoId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.RecursoId, RecursoId);
                var _idAuditoria = conexion.Query<string>(
                                        ProcedimientoAuditoria.VerificarEliminarRecursoEditar,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.First();
            }
        }



        public string VerificarEliminarInductorId(long InductorId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.InductorId, InductorId);
                var _idAuditoria = conexion.Query<string>(
                                        ProcedimientoAuditoria.VerificarEliminarInductor,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.First();
            }
        }

        public string VerificarEliminarInductorIdEditar(long InductorId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.InductorId, InductorId);
                var _idAuditoria = conexion.Query<string>(
                                        ProcedimientoAuditoria.VerificarEliminarInductorEditar,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.First();
            }
        }



        public int ActualizarObservacion(long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.actualizarobservacion,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int ActualizarResolver(long pProcedimientoId, long pPzoReconResol)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ProcedimientoId, pProcedimientoId);
                parameters.Add(ProcedimientoAuditoria.PzoReconResol, pPzoReconResol);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.actualizarReconResol,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }
        public int ActualizarResolverDet(long pProcedimientoId, long pPzoReconResol)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ProcedimientoId, pProcedimientoId);
                parameters.Add(ProcedimientoAuditoria.PzoReconResol, pPzoReconResol);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.actualizarReconResolDet,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }

        public int Eliminar_ActividadBlanco(long pTablaAsmeId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.TablaAsmeId, pTablaAsmeId); 

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.eliminarActividadBlanco,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }


        public int LimpiarObs_Expediente(long pExpedienteId, string pUsuarioCreacion, string pUserCreacion)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.UsuarioCreacion, pUsuarioCreacion);
                parameters.Add(ProcedimientoAuditoria.UserCreacion, pUserCreacion);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.LimpiarObsExpediente,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        } 
        public int ActualizarSubsanar(long pExpedienteId)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.actualizarSubsanar,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }


        public int ValidarProcedimiento(long pExpedienteId, long pFicha)
        {
            using (var conexion = Conexion.GetConexionSUT())//_connectionFactory?.GetConnectionSUT)
            {
                var parameters = new DynamicParameters();

                parameters.Add(ProcedimientoAuditoria.ExpedienteId, pExpedienteId);
                parameters.Add(ProcedimientoAuditoria.Ficha, pFicha);

                var _idAuditoria = conexion.Query<int>(
                                        ProcedimientoAuditoria.ValidarProcedimiento,
                                        parameters,
                                        commandTimeout: 0,
                                        commandType: CommandType.StoredProcedure);
                return _idAuditoria.FirstOrDefault();
            }
        }



    }
}
