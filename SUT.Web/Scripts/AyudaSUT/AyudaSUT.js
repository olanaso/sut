//creando el modal

let tituloModal = null
let descripcionModal = null
let videoModal = null
let navItems = null
let enlaceActivo = null;


(function (window) {



    addStyles();
    createBottonFlotable();
    createModal();


    fetchData();
    mostrarNavLinks();


})(window);


function addStyles() {

    var css = `


	  /* CSS del video*/
#video {
    width: 95%;
    height: 50%;
}


/* CSS del Scroll*/
::-webkit-scrollbar {
    width: 8px; 
}

::-webkit-scrollbar-thumb {
    background-color: #e74b4b; 
}

::-webkit-scrollbar-thumb:hover {
    background-color: #dc0101; 
}

     #btnAyudaSUT {
    position: fixed;
    bottom: 10px;
    left: 10px;
    width: 50px;
    height: 50px;
    border-radius: 50%;
    background-color:red;
    color: white;
    text-align: center;
    border: none;
    font-size:25px;
    cursor: pointer;
    transition: transform 0.3s;  /* Esto hace que el botón cambie de tamaño de forma suave */
}

#btnAyudaSUT:hover {
    background-color:red;
    transform: scale(1.2);  /* Esto hace que el botón sea 1.2 veces más grande cuando se apunta */
}

.text-white{
color:#FFFFFF;
}

.close {
     opacity: 1;
}

#btnAyudaSUT[data-tooltip]:hover:after {
    content: attr(data-tooltip);
    position: absolute;
    top: -35px;
    left: 180%;
    transform: translateX(-50%);
    background-color: #000;
    color: #fff;
    padding: 5px 10px;
    border-radius: 5px;
    font-size: 14px;
    white-space: nowrap;
}

#videoModal{

.bg-danger {
    background-color: #ff0000;
}

.bg-secondary {
    background-color: #4f4f4f;
}

.bg-dark {
    background-color: #000000;
}

.p-1{
    padding: 1rem;
}

.d-flex {
    display: flex;
}

.container-titulo{
    display: flex;
    flex-direction: row;
    color: #ffffff;
}


.list-group-item.activeP {
    background-color: #ff0000; /* Cambia #ff0000 al color de fondo deseado */
    color: #ffffff; /* Cambia #ffffff al color de texto deseado */
}

.list-group-item:activeP {
    color: #ffffff; /* Cambia #ffffff al color de texto deseado cuando se selecciona */
}

.container-duracion{
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
}

.container-numero{
    display: flex;
    align-self: center;
    justify-content: center;
}

.text-semibold{
    font-weight: 600;
  }

.text-white{
    color: #ffffff;
}

.text-black{
    color: #000000;
}

.container-video{
    display: flex;
    justify-content: center;
    align-items: center;
}

.container-video{
    display: flex;
    justify-content: center;
    align-items: center;
}

.nav-item-container {
    display: flex;
    align-items: center;
  }




/* CSS del Scroll*/
::-webkit-scrollbar {
    width: 8px; 
}

::-webkit-scrollbar-thumb {
    background-color: #e74b4b; 
}

::-webkit-scrollbar-thumb:hover {
    background-color: #dc0101; 
}

}



           `;

    var head = document.head || document.getElementsByTagName('head')[0];
    var style = document.createElement('style', '');
    style.type = 'text/css';
    if (style.styleSheet) {
        style.styleSheet.cssText = css;
    } else {
        style.appendChild(document.createTextNode(css));
    }
    head.appendChild(style);

}

function createBottonFlotable() {
    var ButtonAyudaSUT = `

  <button id="btnAyudaSUT"  data-tooltip="Ayuda Técnica SUT">?</button>
`;

    var container = document.getElementById("ayudaSUT");
    container.insertAdjacentHTML('beforeend', ButtonAyudaSUT);

    AbrirModal();

}

function createModal() {
    let Modal = `

        <div class="modal fade" id="videoModal" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <div class="row">
                        <div class="col-sm-11 container-titulo">
                      
                            <h4 class="p-1 text-semibold ">     <svg xmlns="http://www.w3.org/2000/svg" width="32" height="24" viewBox="0 0 16 12" fill="none">
<path d="M0 1.75C0 0.784 0.784 0 1.75 0H14.25C15.216 0 16 0.784 16 1.75V10.25C16 10.7141 15.8156 11.1592 15.4874 11.4874C15.1592 11.8156 14.7141 12 14.25 12H1.75C1.28587 12 0.840752 11.8156 0.512563 11.4874C0.184374 11.1592 0 10.7141 0 10.25L0 1.75ZM1.75 1.5C1.6837 1.5 1.62011 1.52634 1.57322 1.57322C1.52634 1.62011 1.5 1.6837 1.5 1.75V10.25C1.5 10.388 1.612 10.5 1.75 10.5H14.25C14.3163 10.5 14.3799 10.4737 14.4268 10.4268C14.4737 10.3799 14.5 10.3163 14.5 10.25V1.75C14.5 1.6837 14.4737 1.62011 14.4268 1.57322C14.3799 1.52634 14.3163 1.5 14.25 1.5H1.75Z" fill="white"/>
<path d="M6 8.55902V3.44202C5.99985 3.39767 6.0115 3.35407 6.03375 3.31571C6.05601 3.27735 6.08807 3.2456 6.12665 3.22371C6.16522 3.20183 6.20892 3.1906 6.25327 3.19118C6.29762 3.19176 6.34101 3.20413 6.379 3.22702L10.643 5.78602C10.6799 5.80827 10.7103 5.83966 10.7315 5.87716C10.7526 5.91465 10.7638 5.95697 10.7638 6.00002C10.7638 6.04306 10.7526 6.08538 10.7315 6.12288C10.7103 6.16037 10.6799 6.19176 10.643 6.21402L6.379 8.77302C6.34108 8.79586 6.29778 8.80822 6.25352 8.80884C6.20926 8.80947 6.16563 8.79833 6.12708 8.77656C6.08854 8.7548 6.05646 8.72319 6.03413 8.68497C6.01181 8.64675 6.00003 8.60328 6 8.55902Z" fill="white"/>
</svg>
Video tutoriales</h4>
                        </div>
                        <div class="col-sm-1 p-1" >
                            <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar" style="right:30px">
                        <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" viewBox="0 0 20 20" fill="none">
<path d="M3.05288 17.1929C2.09778 16.2704 1.33596 15.167 0.811868 13.9469C0.287778 12.7269 0.0119157 11.4147 0.000377568 10.0869C-0.0111606 8.7591 0.241856 7.44231 0.744665 6.21334C1.24747 4.98438 1.99001 3.86786 2.92893 2.92893C3.86786 1.99001 4.98438 1.24747 6.21334 0.744665C7.44231 0.241856 8.7591 -0.0111606 10.0869 0.000377568C11.4147 0.0119157 12.7269 0.287778 13.9469 0.811868C15.167 1.33596 16.2704 2.09778 17.1929 3.05288C19.0145 4.9389 20.0224 7.46493 19.9996 10.0869C19.9768 12.7089 18.9251 15.217 17.0711 17.0711C15.217 18.9251 12.7089 19.9768 10.0869 19.9996C7.46493 20.0224 4.9389 19.0145 3.05288 17.1929ZM11.5229 10.1229L14.3529 7.29288L12.9429 5.88288L10.1229 8.71288L7.29288 5.88288L5.88288 7.29288L8.71288 10.1229L5.88288 12.9529L7.29288 14.3629L10.1229 11.5329L12.9529 14.3629L14.3629 12.9529L11.5329 10.1229H11.5229Z" fill="white"/>
</svg>
                            </button>
                        </div>
                    </div>
            </div>
                <div class="modal-body">
                    <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="text-center bg-secondary text-semibold">
                                <p class="fw-medium p-1 text-white"> Videos para el uso </p>
                            </div>
                            <ul class="list-group" id="nav-items">
                            </ul>
                        </div>
                    <div class="col-sm-7">
                        <div class="bg-dark">
                            <div class="text-white p-1 text-semibold" id="titulo-video">
                              Seleleccione un video
                            </div>
                            <div class="container-video"> 
                                <div class="embed-responsive embed-responsive-4by3" id="video">
                                 <h1>No hay video seleccionado</h1>
                                </div>
                            </div>
                            <div id="desc-video" class="text-white p-1"> Seleleccione un video </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </div>
        </div>
        </div>



`;
    var container = document.getElementById("ayudaSUT");
    container.insertAdjacentHTML('beforeend', Modal);

    tituloModal = document.querySelector('#titulo-video');
    descripcionModal = document.querySelector('#desc-video')
    videoModal = document.querySelector('#video');
    navItems = document.querySelector('#nav-items');
    enlaceActivo;

    navItems.style.maxHeight = '405px';
    navItems.style.overflowY = 'auto';



}

function AbrirModal() {

    $('#btnAyudaSUT').on('click', function (e) {

        $('#videoModal').modal('show')
    })

}

function obtenerRutaCompleta(url) {
    const pathSegments = new URL(url).pathname.split('/');
    const lastSegment = pathSegments[pathSegments.length - 1];

    const pathWithoutNumber = (!isNaN(lastSegment) && isFinite(lastSegment))
        ? pathSegments.slice(0, -1).join('/')
        : pathSegments.join('/');

    return pathWithoutNumber;
}

//Llamando
function fetchData() {

    let model = [];

    model.push({ name: 'parameter4', value: obtenerRutaCompleta(window.location.href) })


    console.log(model)

    $.ajax({
        type: 'POST',
        url: "/Simplificacion/Video/ListarVideoPath",
        data: model,
        dataType: 'json',
        beforeSend: function () {
            // $("#mensajes").hide();
        },
        complete: function () {

        },
        success: function (data) {
            if (data.result == '') {
                $('#ayudaSUT').hide()

            } else {
               
                let processdata = JSON.parse(data.result)
                if (processdata.length > 0) {
                    mostrarNavLinks(processdata)
                }
               
            }


        },
        error: function (result) {
            //sut.error.show('mensajes', result.responseText);
        }
    });





}
function mostrarNavLinks(navLinks) {
    navLinks.forEach((navlink, index) => {
        const { titulo, url, duracion, Descripcion } = navlink;
        const navItem = document.createElement('DIV');
        const navLink = document.createElement('A');
        const numDiv = document.createElement('DIV');
        const numSpan = document.createElement('SPAN')
        const duracionDiv = document.createElement('DIV');
        const duracionImg = document.createElement('div');
        const duracionPrrfo = document.createElement('P');

        navItem.classList.add('list-group-item', 'd-flex');
        navLink.classList.add('text-black');
        numDiv.classList.add('m-1', 'rounded', 'flex-grow-0', 'p-1');
        numSpan.classList.add('badge', 'bg-danger', 'rounded-circle');
        duracionDiv.classList.add('container-duracion', 'flex-grow-1');
        duracionPrrfo.classList.add('small');

        numSpan.textContent = `${index + 1}`
        numSpan.style.fontSize = '20px';

        navLink.style.width = '100%';

        navLink.textContent = `${titulo}`;
        navLink.href = url;

        duracionImg.innerHTML = `<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
<path d="M12 6V12H18" stroke="#EE7777" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
<path d="M12 22C17.523 22 22 17.523 22 12C22 6.477 17.523 2 12 2C6.477 2 2 6.477 2 12C2 17.523 6.477 22 12 22Z" stroke="#EE7777" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
</svg > `
        duracionImg.style.height = "20px"

        duracionPrrfo.textContent = `${duracion} min `

        navLink.addEventListener('click', (e) => {
            e.preventDefault();
            if (enlaceActivo) {
                enlaceActivo.querySelector('a').style.color = 'black';
            }

            navLink.style.color = 'white';

            mostrarTitulo(titulo);
            mostrarDescrip(Descripcion,url);
            mostrarVideo(url);

            enlaceActivo = navItem;

            activarEnlaceActivo();

        });

        numDiv.appendChild(numSpan);

        duracionDiv.appendChild(duracionImg);
        duracionDiv.appendChild(duracionPrrfo);

        navItem.appendChild(numDiv);
        navItem.appendChild(navLink);
        navLink.appendChild(duracionDiv);

        navItems.appendChild(navItem);

    });
}


function activarEnlaceActivo() {
    const enlaces = document.querySelectorAll('.list-group-item');
    enlaces.forEach(enlace => enlace.classList.remove('activeP', 'bg-danger'));

    enlaceActivo.classList.add('activeP', 'bg-danger');
   
}

function mostrarTitulo(titulo) {
    tituloModal.textContent = titulo;

}

function mostrarVideo(url) {
  
    videoModal.innerHTML = '';

    const iframeVideo = document.createElement('iframe');
    iframeVideo.style = " position: absolute";
    iframeVideo.src = url;

    videoModal.appendChild(iframeVideo);
}

function mostrarDescrip(Descripcion,url) {
   
    descripcionModal.innerHTML = `${Descripcion} <br>
<a target="_blank" href="${url}">Ver video nueva pestaña</a>
`

}