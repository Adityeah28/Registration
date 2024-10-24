$(document).ready(function ()){
    loadDataTable();
});

fucntion loadDataTable(){
    dataTable = $('#tblData').DataTable({
        "ajax":url: '/admin/product/getall'
    },
        "columns": [
            { data: 'name',"width":"15%" },
            { data: 'Address', "width": "15%" },
            { data: 'salary', "width": "15%" },
            { data: 'office', "width": "15%" }
    ]}
);
}




