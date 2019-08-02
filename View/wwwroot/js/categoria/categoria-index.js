$(function () {
    $("#categoria-index").DataTable({
        ajax: "https://localhost:5001/categoria/obtertodos",
        serverSide: true,
        columns: [
            { data: 'id' },
            { data: 'nome' },
            { data: 'id' }
        ]
    });
})