$(function () {

    $("#Application_Management").removeClass("nav-item").addClass("nav-item menu-is-opening menu-open");

    $("#Application_List").css({
        "background": "rgb(226, 53, 53)",
        "border-radius": "5px",
        "line-height": "1"
    });

    var data = document.getElementById("count_number").innerHTML;
    $("#numberbyapplication").html(data);

    $(".content-wrapper").css({
        "background": "#1e1e1e"
    });

    $.ajax({
        type: "GET",
        url: `/Ajax/Get_Application_Data_List`,
        dataType: "json",
        success: function (data) {
            GetData(data, "list_asset");
        }
    })

    function GetData(data, name) {

        $.each(data, function (x, y) {

            $("#" + name).append(`

                    <div class="products-row">
						<button class="cell-more-button">
							<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-more-vertical"><circle cx="12" cy="12" r="1" /><circle cx="12" cy="5" r="1" /><circle cx="12" cy="19" r="1" /></svg>
						</button>
						<div class="product-cell image">
							<img id="img" src="/Images/assets/${y.assetImage}" alt="product">
							<span id="name_dsa">${y.description}</span>
						</div>
						<div id="n1" class="product-cell category"><span class="cell-label">Category:</span>${y.name}</div>
						<div id="n2" class="product-cell sales"><span class="cell-label">Sales:</span>${y.date_out}</div>
						<div id="n3" class="product-cell sales"><span class="cell-label">Sales:</span>${y.date_return}</div>
                        <div class="product-cell sales botton_application_agree agree_butn" id="${y.employee_asset}">Agree</div>
                        <div class="product-cell stock botton_application_delete delete_butn" id="${y.employee_asset}">Reject</div>
					</div>

            `)

        })

        $(".agree_butn").click(function () {

            var cla = $(this).attr("id")

            $.ajax({
                type: "POST",
                url: `/Ajax/Agree_Application`,
                dataType: "json", data: {
                    employee_asset_id: cla
                },
                success: function (data) {
                    GetData_Or();
                }
            })

        })

        $(".delete_butn").click(function () {

            var cla = $(this).attr("id")

            $.ajax({
                type: "POST",
                url: `/Ajax/Delete_Application`,
                dataType: "json", data: {
                    employee_asset_id: cla
                },
                success: function (data) {
                    GetData_Or();
                }
            })

        })




    }

    function GetData_Or() {

        $.ajax({
            type: "GET",
            url: `/Ajax/Get_Application_Data_List`,
            dataType: "json",
            success: function (data) {

                $("#list_asset").empty();
                if (data.length != 0) {
                    $("#numberbyapplication").html(data[0].count);
                    GetData(data, "list_asset");
                } else {
                    $("#numberbyapplication").html(0);
                }

            }
        })

    }


})