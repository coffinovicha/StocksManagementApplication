
document.addEventListener("DOMContentLoaded", function () {
    const token = document.querySelector("#FinnhubToken")?.value;
    const stockSymbol = document.getElementById("StockSymbol")?.value;

    if (!token || !stockSymbol) return;

    const socket = new WebSocket(`wss://ws.finnhub.io?token=${token}`);

    socket.addEventListener('open', function () {
        socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': stockSymbol }));
    });

    socket.addEventListener('message', function (event) {
        const eventData = JSON.parse(event.data);

        if (eventData.type === "error") {
            $(".price").text(eventData.msg);
            return;
        }

        if (eventData.data && eventData.data[0]) {
            const updatedPrice = eventData.data[0].p;
            $(".price").text(updatedPrice.toFixed(2));
        }
    });

    window.addEventListener('beforeunload', () => socket.close());
});