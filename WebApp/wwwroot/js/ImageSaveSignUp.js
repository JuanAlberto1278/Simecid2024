$(document).ready(function () {
    $('#uploadForm').submit(function (e) {
        e.preventDefault(); // Evita el envío del formulario por defecto

        var formData = new FormData($(this)[0]);

        $.ajax({
            url: '/SignUp/UploadFile',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                // Maneja la respuesta exitosa (opcional)
                console.log('El archivo se ha cargado correctamente');
            },
            error: function (xhr, status, error) {
                // Maneja el error (opcional)
                console.error('Error al cargar el archivo:', error);
            }
        });
    });
});