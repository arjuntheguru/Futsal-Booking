"use strict";

//Column Defination for the grid
const columnDefs = [
    { headerName: 'Name', field: 'name' },
    { headerName: 'Registration No', field: 'registrationNo' },
    { headerName: 'Address', field: 'address' },
    { headerName: 'Email', field: 'email'},
    { headerName: 'Contact No', field: 'contactNo' }       
];

//Function to set the data for the grid
const setGridData = () => {

    $.ajax({
        url: 'api/FutsalCompany/Get',
        method: 'GET',
        success: (data) => {
            gridOptions.api.setRowData(data);
            console.log(data);
        }
    });
};


//Settings for the Futsal Company grid
let gridOptions = {
    columnDefs: columnDefs,
    rowHeight: 40,
    defaultColDef : {
        sortable: true,
        filter: true
    },
    paginationAutoPageSize: true,
    pagination: true,
    accentedSort: true,
    onGridSizeChanged: (params) => {
        params.api.sizeColumnsToFit();
    }
};

const Clear = () => {

}

//Function specifying rules for validating the form
const futsalCompanyValidation = () => {

    $('#futsalCompanyForm').validate({
        rules: {
            name: {
                required: true,
                maxlength: 50,
                isOnlyWhiteSpace: true
            },
            registrationNo: {
                required: true
            },
            address: {
                required: true
            },
            contactNo: {
                required: true
            }
        }
    });
}

$(document).ready(function () {
    var futsalCompanyGrid = document.querySelector('#futsalCompanyGrid');

    new agGrid.Grid(futsalCompanyGrid, gridOptions);

    setGridData();

    $('#addBtn').click(function () {
        console.log('Button Pressed');
        $('#futsalCompanyTitle').html("Add Futsal Company");
        Clear();
        $('#futsalCompanyForm').validate().destroy();
        futsalCompanyValidation();
        $('#futsalCompanyForm').validate().resetForm();
    });

    $('#btnSave').on('click', function () {
        if ($('#futsalCompanyForm').valid()) {
            Save();
        }
    });
});