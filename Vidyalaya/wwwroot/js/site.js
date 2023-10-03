$(document).ready(function () {
    bindDatatable();
});
//data table
function bindDatatable() {
    datatable = $('#School_Table')
        .DataTable
        ({
            "sAjaxSource": "/Schools/GetData",
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "filter": true,
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
                {
                    "data": "sid",
                    render: function (data, type, row, meta) {
                        return row.sId
                    }
                },
                {
                    "data": "sname",
                    render: function (data, type, row, meta) {
                        return '<a href="#" data-bs-toggle="tooltip" style="text-decoration:none; color: black" onclick="Emp_Edit(' + row.sid + ')">' + row.sName + '</a>';
                    }
                },
                {
                    "data": "saddress",
                    render: function (data, type, row, meta) {
                        return row.sAddress
                    }
                },
                {
                    "data": "scity",
                    render: function (data, type, row, meta) {
                        return row.sCity
                    }
                },
                {
                    "data": "sstate",
                    render: function (data, type, row, meta) {
                        return row.sState
                    }
                },
                {
                    render: function (data, type, row, meta) {

                        return '<button type="button" class="btn btn-info" onclick="Show_Details(' + row.sId + ')" ">Details</button>    | <a class="btn btn-primary" href="Schools/Edit/' + row.sId + '">Edit</a> | <a class="btn btn-danger" href="Schools/Delete/' + row.sId + '">Delete</a>';
                    }

                },
                {
                    "width": "50%",
                    render: function (data, type, row, meta) {

                        return '<button type="button" class="btn btn-warning" onclick="Add_Activities(' + row.sId + ')" ">Add Activites</button> | <button type="button" class="btn btn-success" onclick="Add_Classes(' + row.sId + ')" ">Add Class</button> | <button type="button" class="btn btn-info" onclick="Add_Teachers(' + row.sId + ')" ">Add Teacher</button>';
                    }
                }
            ]
        });

}

////<a class="btn btn-primary" onclick="takeif(' + row.sId + ')" href="Schools/Details/' + row.sId + '">Details</a>
var Show_Details = function (id) {
          $.ajax({
          "url": "/Activities/GetActivities/" + id,
            "dataType": "json",
            success: function (aaData) {
               ({
                   "data":"aaData",
                   "sAjaxSource": "/Activities/GetActivities/" + id,
                    "bServerSide": true,
                    "bProcessing": true,
                    "bSearchable": true,
                    "filter": true,
                    "destroy": true,
                    "language": {
                        "emptyTable": "No record found.",
                       "processing":
                            '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
                    },
                    "columns": [
                       {
                           "data": "aid",
                            render: function (data, type, row, meta) {
                                return row.aId
                            }
                        },
                       {
                            "data": "atitle",
                            render: function (data, type, row, meta) {
                               return '<a href="#" data-bs-toggle="tooltip" class="btn btn-primary" onclick="Emp_Edit(' + row.sid + ')">' + row.aTitle + '</a>';
                            }
                        },
                        {
                            "data": "adescription",
                            render: function (data, type, row, meta) {
                                return row.aDescription
                            }
                        },
                        //{
                        //    render: function (data, type, row, meta) {
                        //        return '<a  class="btn btn-primary"  href="Schools/Details/' + row.sId + '" onclick="takeif(' + row.sId + ')">Details</a> | <a class="btn btn-primary" href="Schools/Edit/' + row.sId + '">Edit</a> | <a class="btn btn-primary" href="Schools/Delete/' + row.sId + '">Delete</a>';
                        //},
                       //{
                        //    render: function (data, type, row, meta) {
                        //        return '<button type="button" onclick="Add_Act(' + row.sId + ')" ">Add_Activites</button>';
                        //    }
                            
                    ]
                });
            }
        });
   
    window.location.replace("/Schools/Details/" + id);
    
}

$(document).ready(function () {
    $("#geeks").tabs({
        active: 0
    })
})

//$(function () { $("#tabs").tabs(); });
var Add_Classes = function (id) {
    $.ajax({
        type: "post",
        url: "/Classes/Create1/",
        success: function (partialViewData) {
            $('#CreateContainer').html(partialViewData);
            $("#Classes").modal('show');
            $('#liWithID').val(id);
        }
    })
}

var Add_Activities = function (id) {
    $.ajax({
        type: "post",
        url: "/Activities/Create1/",
        success: function (partialViewData) {
            $('#CreateContainer').html(partialViewData);
            $("#Activite").modal('show');
            $('#liWithID').val(id);
        }
    })
}

var Add_Teachers = function (id) {
    $.ajax({
        type: "post",
        url: "/Teachers/Create1/",
        success: function (partialViewData) {
            $('#CreateContainer').html(partialViewData);
            $("#Teachers").modal('show');
            $('#liWithID').val(id);
        }
    })
}


function Activities_(id) {
    datatable = $('#Activities_Table')
        .DataTable
        ({
            "sAjaxSource": "/Activities/GetActivities/" + id,
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "filter": true,
            "destroy": true,
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
                {

                    "data": "aid",
                    render: function (data, type, row, meta) {
                        return row.aId
                    }
                },
                {
                    "data": "atitle",
                    render: function (data, type, row, meta) {
                        return '<a href="#" data-bs-toggle="tooltip" style="text-decoration:none; color: black" onclick="Emp_Edit(' + row.sid + ')">' + row.aTitle + '</a>';
                    }
                },
                {
                    "data": "adescription",
                    render: function (data, type, row, meta) {
                        return row.aDescription
                    }
                },
                //{
                //    render: function (data, type, row, meta) {
                //        return '<a  class="btn btn-primary"  href="Activities/Details/' + row.aId + '>Details</a> | <a class="btn btn-info" href="Activities/Edit/' + row.aId + '">Edit</a> | <a class="btn btn-primary" href="Activities/Delete/' + row.aId + '">Delete</a>';
                //    }
                //},
            ]
        });

}


function Class_(id) {
 
    datatable = $('#Class_Table')
        .DataTable
        ({
            "sAjaxSource": "/Classes/GetClasses/" + id,
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "filter": true,
            "destroy": true,
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
                {

                    "data": "cid",
                    render: function (data, type, row, meta) {
                        return row.cId
                    }
                },
                {
                    "data": "cname",
                    render: function (data, type, row, meta) {
                        return '<a href="#" data-bs-toggle="tooltip" style="text-decoration:none; color: black" onclick="Emp_Edit(' + row.sid + ')">' + row.cName + '</a>';
                    }
                },
                {
                    "data": "csubject",
                    render: function (data, type, row, meta) {
                        return row.cSubject
                    }
                },
                {
                    "data": "cstandard",
                    render: function (data, type, row, meta) {
                        return row.cStandard
                    }
                },
                {
                    "data": "croomno",
                    render: function (data, type, row, meta) {
                        return row.cRoomNo
                    }
                },
                //{
                //    render: function (data, type, row, meta) {
                //        return '<a  class="btn btn-primary"  href="Classes/Details/' + row.aId + '>Details</a> | <a class="btn btn-primary" href="Classes/Edit/' + row.aId + '">Edit</a> | <a class="btn btn-primary" href="Classes/Delete/' + row.aId + '">Delete</a>';
                //    }
                //},
            
            ]
        });
    
}


function Teacher_(id) {
    datatable = $('#Teacher_Table')
        .DataTable
        ({
            "sAjaxSource": "/Teachers/GetTeachers/" + id,
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "filter": true,
            "destroy": true,
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },
            "columns": [
                {
                    "sWidth": "10%",
                    "data": "tid",
                    render: function (data, type, row, meta) {
                        return row.tId
                    }
                },
                {
                    "data": "tname",
                    render: function (data, type, row, meta) {
                        return '<a href="#" data-bs-toggle="tooltip" style="text-decoration:none; color: black" onclick="Emp_Edit(' + row.sid + ')">' + row.tName + '</a>';
                    }
                },
                {
                    "data": "tsubject",
                    render: function (data, type, row, meta) {
                        return row.tSubject
                    }
                },
                {
                    "data": "tstandard",
                    render: function (data, type, row, meta) {
                        return row.tStandard
                    }
                },
                //{
                    
                //    render: function (data, type, row, meta) {
                //        return '<a  class="btn btn-primary"  href="Schools/Details/' + row.aId + '>Details</a> | <a class="btn btn-primary" href="Schools/Edit/' + row.aId + '">Edit</a> | <a class="btn btn-primary" href="Schools/Delete/' + row.aId + '">Delete</a>';
                //    }
                //},
            ]
        });

}