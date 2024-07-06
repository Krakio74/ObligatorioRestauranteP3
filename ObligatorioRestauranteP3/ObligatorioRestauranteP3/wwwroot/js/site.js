// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// scroll con click y arrastrar
const slider = document.querySelector('.restaurant-section');
let isDown = false;
let startX;
let scrollLeft;

slider.addEventListener('mousedown', (e) => {
    isDown = true;
    slider.classList.add('active');
    startX = e.pageX - slider.offsetLeft;
    scrollLeft = slider.scrollLeft;
});

slider.addEventListener('mouseleave', () => {
    isDown = false;
    slider.classList.remove('active');
});

slider.addEventListener('mouseup', () => {
    isDown = false;
    slider.classList.remove('active');
});

slider.addEventListener('mousemove', (e) => {
    if (!isDown) return;
    e.preventDefault();
    const x = e.pageX - slider.offsetLeft;
    const walk = (x - startX) * 3;
    slider.scrollLeft = scrollLeft - walk;
    console.log(walk);
});
document.addEventListener('DOMContentLoaded', function () {
    // Event listener para el cambio en el select de tipo de usuario
    document.getElementById('tipoUsuario').addEventListener('change', function () {
        var tipoUsuario = this.value;
        var camposCliente = document.getElementById('camposCliente');
        var camposEmpleado = document.getElementById('camposEmpleado');

        if (tipoUsuario === 'cliente') {
            camposCliente.style.display = 'block';
            camposEmpleado.style.display = 'none';
        } else if (tipoUsuario === 'empleado') {
            camposCliente.style.display = 'none';
            camposEmpleado.style.display = 'block';
        }
    });

    // Event listener para el botón de añadir detalle en el formulario de orden
    document.getElementById('addDetalleBtn').addEventListener('click', function () {
        var detallesContainer = document.getElementById('detallesContainer');
        var newDetalle = document.createElement('div');
        newDetalle.classList.add('detalle');
        newDetalle.innerHTML = '<label for="menuId">Menu Id:</label>' +
            '<input type="number" name="menuId">' +
            '<label for="restauranteId">Restaurante Id:</label>' +
            '<input type="number" name="restauranteId">' +
            '<label for="cantidad">Cantidad:</label>' +
            '<input type="number" name="cantidad">';
        detallesContainer.appendChild(newDetalle);
    });

    // Event listener para el envío del formulario de orden
    document.getElementById('ordenForm').addEventListener('submit', function (event) {
        event.preventDefault();

        var detalles = [];
        var detalleElements = document.querySelectorAll('#detallesContainer .detalle');
        detalleElements.forEach(function (detalleElement) {
            var menuId = detalleElement.querySelector('input[name="menuId"]').value;
            var restauranteId = detalleElement.querySelector('input[name="restauranteId"]').value;
            var cantidad = detalleElement.querySelector('input[name="cantidad"]').value;
            detalles.push({ MenuId: menuId, RestauranteId: restauranteId, Cantidad: cantidad });
        });

        var ordenData = {
            ReservaId: document.getElementById('reservaId').value,
            Total: document.getElementById('total').value,
            Descuento: document.getElementById('descuento').value,
            Detalles: detalles
        };

        fetch('/Ordens/Create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(ordenData)
        })
            .then(response => response.json())
            .then(data => {
                alert('Orden creada correctamente');
                // Aquí puedes redirigir a otra página o realizar acciones adicionales después de crear la orden
                window.location.href = '/Ordens/Index'; // Ejemplo de redirección a la lista de órdenes
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Error al crear la orden');
            });
    });
});

document.getElementById('imagen').addEventListener('change', function (event) {
    var input = event.target;
    var reader = new FileReader();

    reader.onload = function () {
        var imageUrl = reader.result;
        var imageFrame = document.querySelector('.ow-imageframe');

        // Cambiar el estilo de fondo del elemento con la imagen cargada
        imageFrame.style.backgroundImage = 'url(' + imageUrl + ')';
    };

    reader.readAsDataURL(input.files[0]);
});
document.getElementById('Reserva').addEventListener('click', function () {
    var ResId = this.value;
    alert(ResId);
});






