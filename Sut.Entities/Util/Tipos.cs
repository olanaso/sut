namespace Sut.Entities
{
    #region Seguridad

    public enum Rol : short
    {
        Administrador = 1,
        Coordinador = 2,
        RegistradorProcesos = 3,
        RegistradorLegal = 4,
        RegistradorCostos = 5,
        Ratificador = 6,
        Evaluador = 7,
        EntidadFiscalizadora = 8,
        Incentivos = 9,
        usuariolocador = 10,

        AccesoEstandar = 11,
        EvaluadorCoordinador = 12,
        CalendarioActividades = 14,
    }

    #endregion

    public enum TipoEnumerado : int
    {
        ENUM_TIPO_NORMA_APROBACION = 1
    }

    public enum DatoCopia
    {
        Todo,
        InfoAlCiudadano,
        InfoBasica,
        TipoAtencion,
        Requisito,
        BaseLegal,
        DatosGeneralesYSTL,
        SustentoCalificacion,
        SustentoRequisito,
        TablaASME,
        Nuevo
    }

    public enum TipoRequisito : int
    {
        General = 1,
        Especifico = 2
    }

    public enum TipoDato : int
    {
        CategoriaProcedimiento = 1,
        TipoProcedimiento = 2,
        TipoDatoAdicionalProc = 3,
        TipoSede = 4,
        TipoNotaCiudadano = 5
    }

    public enum EstadoMiembroEquipo : short
    {
        Activo = 1,
        Baja = 2
    }

    public enum EstadoUsuario : short
    {
        Activo = 1,
        Suspendido = 2,
        Baja = 3
    }

    public enum AsignarEntidades : short
    {
        Seleccionar = 0,
        Si = 1,
        No = 2
    }

    public enum CorreoActivar : int
    {
        NoActivo = 0,
        Activo = 1,
    }

    public enum Correo : short
    {
        PresentarEnvio = 1,
        PresentarRecibido = 2,
        SustentarEnvio = 3,
        SustentarRecibido = 4,
        ObservarEnvio = 5,
        ObservarRecibido = 6,
        PublicarEnvio = 7,
        PublicarRecibido = 8,
        AprobarEnvio = 9,
        AprobarRecibido = 10,
    }

    public enum TipoTupa : short
    {
        Normal = 1,
        Estandar = 2
    }

    public enum EstadoTupa : short
    {
        Vigente = 1,
        NoVigente = 2,
        Derogado = 3,
        Anulado = 4
    }

    public enum TipoExpediente : short
    {
        CargaInicial = 1,
        ExpedienteRegular = 2,
        
    }

    public enum EstadoExpediente : short
    {
        EnProceso = 1,
        Presentado = 2,
        Aprobado = 3,
        Observado = 4,
        Anulado = 5,
        Subsanado = 6,
        Publicado = 7,

    }

    public enum TipoSustento : short
    {
        DatosGenerales = 1,
        SustentoTecnico = 2,
        SustentoLegal = 3,
        SustentoCostos = 4,
        InfCdno = 5,
        TablaAsme = 6
    }

    public enum TipoProcedimiento : short
    {
        Procedimiento = 1,
        Servicio = 2,
        Estandar = 3,
        ACR = 4,
        EstandarServicio = 5,
        NOACR = 6
    }

    public enum EstadoOpcion : short
    {
        SinAccion = 0,
        Proceso = 1,
        Terminado = 2,
        Observado = 3,
        Subsanado = 4
    }

    public enum Renovacio : int
    {
        Si = 1,
        No = 2, 
    }

    public enum Respuesta : int
    {
        Si = 1,
        No = 2,
    }
    public enum NivelPadre : int
    {
        Padre = 1,
        Hijo = 2,
    }

    public enum TipoRegla : int
    {
        Evaluador = 1,
        Ratificador = 2,
        Ninguno = 3,
    }
    public enum Autorizacionmef : int
    {
        NoAutorizar = 3,
        Enviar = 1,
        Autorizar = 2,
    }

    public enum Plazorenovacion : int
    {

        seleccione = 0,
        Indeterminado=1,
        mes1 = 2,
        mes2 = 3,
        mes3 = 4,
        mes4 = 5,
        mes5 = 6,
        mes6 = 7,
        mes7 = 8,
        mes8 = 9,
        mes9 = 10,
        mes10 = 11,
        mes11 = 12,
        anio1 = 13,
        anio2 = 14,
        anio3 = 15,
        anio4 = 16,
        anio5 = 17,
        anio6 = 18,
        anio7 = 19,
        anio8 = 20,
        anio9 = 21,
        anio10 = 22,
        anio20 = 23,
    }

    public enum TipoNormasid : short
    {
    DecretoSupremo = 1,
    ResoluciónMinisterial = 2,
    ConsejoDirectivodelOrganismoRegulador = 3,
    ResolucióndelÓrganodeDireccióndelOrganismoTécnicoEspecializado = 4,
    ResolucióndelTitulardelOrganismoTécnicoEspecializado = 5,
    ResolucióndelTitulardelOrganismoConstitucionalmenteAutónomo = 6,
    OrdenanzaRegional = 7,
    DecretoRegional = 8,

    }


    public enum TipoMapaProcedimiento : short
    {
        Categoria = 1,
        Tematica = 2,
        Tipo = 3,
        SubTipo = 4,
        Especifica = 5
    }

    public enum OperacionExpediente : short
    {
        Nuevo = 1,
        Modificacion = 2,
        Eliminacion = 3,
        Ninguna = 4,
        Ocultar = 5
    }

    public enum CalificacionProcedimiento : short
    {
        Ninguno = 0,
        Automatica = 1,
        SilencioPositivo = 2,
        SilencioNegativo = 3,
        Enblanco = 4
    }

    public enum Periodos : short
    {
        Evalucion1 = 0,
        Evalucion2 = 1, 
    }

    public enum TipoPlazo : short
    {
        habiles = 154,
        calendarios = 155,
        meses = 156
    }

    public enum FiltrosOrdenar : short
    {
        Ninguno = 0,
        Ascendente = 1,
        Descendente = 2, 
    }
    public enum TipoRecurso : short
    {
        Personal = 1,
        MaterialFungible = 2,
        ServicioIdentificable = 3,
        MaterialNoFungible = 4,
        ServicioTerceros = 5,
        Depreciacion = 6,
        Fijos = 7
    }

    public enum TipoActividad : short
    {
        Ninguno = 0,
        Operacion = 1,
        Revision = 2,
        Traslado = 3,
        Espera = 4,
        Archivo = 5
    }

    public enum TipoDetalleMaestro : short
    {
        Personal = 1,
        MaterialFungible = 2,
        ServicioIdentificable = 3,
        MaterialNoFungible = 4,
        ServicioTerceros = 5,
        Depreciacion = 6,
        Fijos = 7,
        Inductores=8,
        UnidadOrganica=9,        
    }


    public enum TipoValor : short
    {
        Ninguno = 0,
        VA = 1,
        Control = 2,
        SVA = 3
    }

    public enum TipoOrdenPa : short
    {
        Ninguno = 0,
        Sistema = 1,
        UnidadOrganica = 2,
        Bloque = 3,
        UnidadOrganicaBloque = 4,
    }
    public enum TipoBaseLegal : short
    {
        Norma = 1,
        Adjunto = 2,
        RutaInternet = 3
    }

    public enum listarenovacion : short
    {
        Norma = 1,
        Adjunto = 2,
        RutaInternet = 3
    }

    public enum Pagina : short
    {
        NotaCiudadano = 1
    }

    public enum Reporte : short {
        Expediente = 1,
        Procedimiento = 2
    }

}
