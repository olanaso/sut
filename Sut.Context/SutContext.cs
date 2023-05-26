namespace Sut.Context
{
    using Entities;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class SutContext : DbContext
    {
        public DbSet<Expediente> Expediente { get; set; }
        public DbSet<Procedimiento> Procedimiento { get; set; }
        public DbSet<ProcedimientoCargos> ProcedimientoCargos { get; set; }
        public DbSet<ProcedimientoCargosApe> ProcedimientoCargosApe { get; set; }
        public DbSet<ProcedimientoCargosOtros> ProcedimientoCargosOtros { get; set; }
        public DbSet<ProcedimientoCategoria> ProcedimientoCategoria { get; set; }
        public DbSet<UnidadOrganica> UnidadOrganica { get; set; }
        public DbSet<MetaDato> MetaDato { get; set; }
        public DbSet<NotaCiudadano> NotaCiudadano { get; set; }
        public DbSet<TablaAsme> TablaAsme { get; set; }
        
        public DbSet<SedeGrupo> SedeGrupo { get; set; }
        public DbSet<Sede> Sede { get; set; }
        public DbSet<ProcedimientoSede> ProcedimientoSede { get; set; }
        public DbSet<Requisito> Requisito { get; set; }
        public DbSet<ArchivoAdjunto> ArchivoAdjunto { get; set; }
        public DbSet<ArMotivoAdjunto> ArMotivoAdjunto { get; set; }
        public DbSet<RequisitoFormulario> RequisitoFormulario { get; set; }
        public DbSet<RequisitoProcedimiento> RequisitoProcedimiento { get; set; }
        public DbSet<Norma> Norma { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }        
        public DbSet<ComentarioSeccion> ComentarioSeccion { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Recurso> Recurso { get; set; } 
        public DbSet<DetalleMaestro> DetalleMaestro { get; set; } 
        public DbSet<GrupoEntidad> GrupoEntidad { get; set; }
        public DbSet<CalendarioActividades> CalendarioActividades { get; set; }
        public DbSet<Inductor> Inductor { get; set; }
        public DbSet<UnidadMedida> UnidadMedida { get; set; }
        public DbSet<Actividad> Actividad { get; set; }

        public DbSet<InformeDocumentos> InformeDocumentos { get; set; }
        public DbSet<Informe> Informe { get; set; }

        public DbSet<ProcedimientoUndOrgResponsable> ProcedimientoUndOrgResponsable { get; set; }
        public DbSet<NotificacionExpedientes> NotificacionExpedientes { get; set; }
        
        public DbSet<IncentivosFormulas> IncentivosFormulas { get; set; }
        
        public DbSet<Incentivos> Incentivos { get; set; }
        public DbSet<Bandeja> Bandeja { get; set; }

        
        public DbSet<IncentivosFormulasCorte> IncentivosFormulasCorte { get; set; }
        
        public DbSet<IncentivosCorte> IncentivosCorte { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<Correos> Correos { get; set; }
        public DbSet<RolMenu> RolMenu { get; set; }
        public DbSet<PlazoAtencion> PlazoAtencion { get; set; }
        
        public DbSet<TablaAsmeReproduccion> TablaAsmeReproduccion { get; set; }
        public DbSet<ActividadRecurso> ActividadRecurso { get; set; }
        public DbSet<AnexoPersonal> AnexoPersonal { get; set; }
        public DbSet<AnexoIdentificable> AnexoIdentificable { get; set; }
        public DbSet<AnexoNoIdentificable> AnexoNoIdentificable { get; set; }
        public DbSet<FactorDedicacion> FactorDedicacion { get; set; }
        public DbSet<InductorValor> InductorValor { get; set; }
        public DbSet<RecursoCosto> RecursoCosto { get; set; }
        public DbSet<Asignacion> Asignacion { get; set; }
        public DbSet<Entidad> Entidad { get; set; }
        public DbSet<BaseLegal> BaseLegal { get; set; }
        public DbSet<BaseLegalNorma> BaseLegalNorma { get; set; }
        public DbSet<Tupa> Tupa { get; set; }
        public DbSet<PasoSeguir> PasoSeguir { get; set; }
        public DbSet<Ubigeo> Ubigeo { get; set; }
        public DbSet<ProcedimientoDatoAdicional> ProcedimientoDatoAdicional { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }
        public DbSet<MiembroEquipo> MiembroEquipo { get; set; }
        public DbSet<MapaProcedimiento> MapaProcedimiento { get; set; }
        public DbSet<Departamento> Departamento { get; set; }


        public DbSet<FeriadosAnuales> FeriadosAnuales { get; set; }
        
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Distrito> Distrito { get; set; }
        public DbSet<PlanTrabajo> PlanTrabajo { get; set; }
        public DbSet<ResolucionEquipo> ResolucionEquipo { get; set; }
        public DbSet<ExpedienteNorma> ExpedienteNorma { get; set; }
        public DbSet<Dato> Dato { get; set; }
        public DbSet<MaestroFijo> MaestroFijo { get; set; }
        public DbSet<Menuayuda> Menuayuda { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MaestroFijoDatoAdicional> MaestroFijoDatoAdicional { get; set; }
        public DbSet<MaestroFijoPasoSeguir> MaestroFijoPasoSeguir { get; set; }
        public DbSet<MaestroFijoSede> MaestroFijoSede { get; set; }
        public DbSet<SedeUnidadOrganica> SedeUnidadOrganica { get; set; }
        public DbSet<MaestroFijoNotaCiudadano> MaestroFijoNotaCiudadano { get; set; }
        public DbSet<UndOrgRecepcionDocumentos> UndOrgRecepcionDocumentos { get; set; }
        public DbSet<MaestroFijoUndOrgRecepcionDocum> MaestroFijoUndOrgRecepcionDocum { get; set; }
        public DbSet<Enumerado> Enumerado { get; set; }

        public DbSet<UIT> UIT { get; set; }
        public DbSet<Observacion> Observacion { get; set; }


        public DbSet<DocumentosNorma> DocumentosNorma { get; set; }
        public DbSet<Auditoria> Auditoria    { get; set; }
        public DbSet<IngresoSUT> IngresoSUT { get; set; }
        public DbSet<Comunicado> Comunicado { get; set; }


        public DbSet<VCalidad> VCalidad { get; set; }
        public DbSet<VCalidadRequisitos> VCalidadRequisitos { get; set; }
        public DbSet<VListaEntidadACR> VListaEntidadACR { get; set; }

        public DbSet<PreguntasFrecuentes> PreguntasFrecuentes { get; set; }


        public DbSet<VCalidadRequisitos_4_2> VCalidadRequisitos_4_2 { get; set; }
        public DbSet<VCalidadRequisitos_5_4> VCalidadRequisitos_5_4 { get; set; }
        public DbSet<VCalidadRequisitos_CABECERA> VCalidadRequisitos_CABECERA { get; set; }



        public DbSet<VCalidadEXANTE> VCalidadEXANTE { get; set; }
        public DbSet<VCalidadRequisitos_4_2EXANTE> VCalidadRequisitos_4_2EXANTE { get; set; }
        public DbSet<VCalidadRequisitos_5_4EXANTE> VCalidadRequisitos_5_4EXANTE { get; set; }
        public DbSet<VCalidadRequisitos_CABECERAEXANTE> VCalidadRequisitos_CABECERAEXANTE { get; set; }
        public DbSet<VListaEntidadACREXANTE> VListaEntidadACREXANTE { get; set; }




        public SutContext()
            : base("name=SUTEntities")
        {
            Database.SetInitializer<SutContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.HasDefaultSchema("sut");
        }

    }
}