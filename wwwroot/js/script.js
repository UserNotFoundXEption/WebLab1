
$(document).ready(function () {
    const socket = new WebSocket("ws://127.0.0.1:2137/ws");

    socket.onopen = function () {
        console.log("Connected to WebSocket server.");
    };

    socket.onmessage = function (event) {
        console.log("Otrzymano wiadomość:", event.data);
        const message = JSON.parse(event.data);
        $('#result').html("Result is: " + message);
    };

    socket.onclose = function () {
        console.log("Disconnected from WebSocket server.");
    };

    socket.onerror = function (error) {
        console.error("WebSocket error: ", error);
    };

    function sendMessage(type, number) {
        const message = JSON.stringify({ type: type, number: number });
        socket.send(message);
    }

    // Handle Factorial button click
    $('#factorialButton').click(function () {
        const number = parseInt($('#number').val(), 10);
        if (isNaN(number) || number < 0 || number > 20) {
            $('#result').html('Error: Please enter a number between 0 and 20 for factorial.');
            return;
        }
        sendMessage("factorial", number);
    });

    // Handle Fibonacci button click
    $('#fibonacciButton').click(function () {
        const number = parseInt($('#number').val(), 10);
        if (isNaN(number) || number < 1 || number > 100) {
            $('#result').html('Error: Please enter a number between 1 and 100 for Fibonacci.');
            return;
        }
        sendMessage("fibonacci", number);
    });
});





/*$(document).ready(function () {

    function isNumber(value) {
        return !isNaN(value) && isFinite(value);
    }

    $('#calculationForm').on('submit', function (event) {
        event.preventDefault();
    });


    $('#factorialButton').click(function () {
        const number = $('#number').val();

        if (!isNumber(number)) {
            $('#result').html('Error: Value is not a number');
            return;
        }

        $.ajax({
            type: 'POST',
            url: '/Home/Factorial',
            data: { number: number },
            success: function (response) {
                $('#result').html('Factorial: ' + response.result);
            },
            error: function () {
                $('#result').html('Error in calculation.');
            }
        });
    });

    // Handle Fibonacci button click
    $('#fibonacciButton').click(function () {
        const number = $('#number').val();

        if (!isNumber(number)) {
            $('#result').html('Error: Value is not a number');
            return;
        }

        $.ajax({
            type: 'POST',
            url: '/Home/Fibonacci',
            data: { number: number },
            success: function (response) {
                $('#result').html('Fibonacci: ' + response.result);
            },
            error: function () {
                $('#result').html('Error in calculation.');
            }
        });
    });
});*/
