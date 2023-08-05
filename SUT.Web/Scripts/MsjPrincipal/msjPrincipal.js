
// Configurar Quill
var quill = new Quill('#editor-container', {
    theme: 'snow',
    modules: {
        toolbar: [
            [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
            ['bold', 'italic', 'underline', 'strike'],
            [{ 'list': 'ordered' }, { 'list': 'bullet' }, { 'indent': '-1' }, { 'indent': '+1' }],
            ['link', 'image', 'video'],
            [{ 'color': [] }],
            [{ 'align': [] }], // Opciones de alineación del texto
            ['clean']
        ]
    },
    placeholder: 'Escribe aquí...',
});

// Agregar evento para cambiar color del texto
var colorPicker = document.querySelector('.ql-color .ql-picker-options');
if (colorPicker) {
    var colors = ['red', 'orange', 'yellow', 'green', 'blue', 'purple', 'black', 'white', 'gray'];
    colors.forEach(function (color) {
        var option = document.createElement('span');
        option.style.backgroundColor = color;
        option.setAttribute('data-value', color);
        colorPicker.appendChild(option);
    });
}

// Agregar evento al botón para obtener el HTML generado
//var getHtmlButton = document.getElementById('get-html-btn');
//getHtmlButton.addEventListener('click', function () {
//    var html = quill.root.innerHTML;
//    console.log(html); // Aquí puedes hacer lo que quieras con el HTML generado
//});


var html = '<p>Este es un contenido actualizado.</p>';
quill.clipboard.dangerouslyPasteHTML(html);