
$(function () {

    $("#Asset_Inventory").removeClass("nav-item").addClass("nav-item menu-is-opening menu-open");

    $("#Asset_List").css({
        "background": "rgb(226, 53, 53)",
        "border-radius": "5px",
        "line-height": "1"
    });



    $(".content-wrapper").css({
        "background": "#1e1e1e"
    });

    $("#Search_Data").keyup(function () {

        $.ajax({

            type: "POST",
            url: "/Ajax/Search_Assets",
            dataType: "json",
            data: {
                Search: $(this).val()
            },
            success: function (data) {

                $("#Assets_list_Data").empty();
                $("#cv").empty();
                GetData(data, "Assets_list_Data")

                var rowShown = 8;
                $(".container_cc").css("display", "block");
                var rowsTotal = $("#Assets_list_Data .show").length;
                var rowShown = 8;
                var numPages = rowsTotal / rowShown;
                $("#Assets_list_Data .show").slice(0, rowShown).show();

                for (var i = 0; i < numPages; i++) {
                    var pageNum = i + 1;
                    $("#cv").append(`<li><a class="back" rel='${i}'>${pageNum}</a></li>`)
                    $(".back").first().css("background", "red");
                }

                $("#cv a").click(function () {
                    $(".back").css("background", "#171717");
                    $(this).css("background", "red");

                    var currPage = $(this).attr("rel")
                    page = currPage

                    var startItem = currPage * rowShown;
                    var endItem = startItem + rowShown;
                    var index = $(this).parent().index();

                    $("#Assets_list_Data .show").css("opacity", "0").hide().slice(startItem, endItem).css("display", "block").animate({ opacity: 1 }, 300)

                })

                $("#prev").click(function () {

                    if (page != 0) {
                        var num = page - 1;
                        page = num;
                        var startItem = num * rowShown;
                        var endItem = startItem + rowShown;

                        $(".back").css("background", "#171717");
                        $(".back").eq(page).css("background", "red");

                        $("#Assets_list_Data .show").css("opacity", "0").hide().slice(startItem, endItem).css("display", "block").animate({ opacity: 1 }, 300)
                        //$("#mypro .show").css("opacity","0").hide().slice(startItem,endItem).css("display","table-row").animate({opacity:1},300)
                    }

                })

                $("#next").click(function () {
                    var n = $(".back").last().attr("rel");

                    if (page != n) {
                        var num1 = parseInt(page) + 1;
                        page = num1;
                        var startItem = num1 * rowShown;
                        var endItem = startItem + rowShown;

                        $(".back").css("background", "#171717");
                        $(".back").eq(page).css("background", "red");

                        $("#Assets_list_Data .show").css("opacity", "0").hide().slice(startItem, endItem).css("display", "block").animate({ opacity: 1 }, 300)
                    }

                })
            }
        })
    })


    var page = 0;

    $.ajax({
        type: "GET",
        url: "/Ajax/Get_Assets_List",
        dataType: "json",
        success: function (data) {
            GetData(data, "Assets_list_Data");

            $(".container_cc").css("display", "block");
            var rowsTotal = $("#Assets_list_Data .show").length;
            var rowShown = 8;
            var numPages = rowsTotal / rowShown;
            console.log("numPages = " + numPages)
            console.log("rowsTotal = " + rowsTotal)

            $("#Assets_list_Data .show").slice(0, 8).show();


            for (var i = 0; i < numPages; i++) {
                var pageNum = i + 1;
                $("#cv").append(`<li><a class="back" rel='${i}'>${pageNum}</a></li>`)
                $(".back").first().css("background", "red");
            }

            $("#cv a").click(function () {
                $(".back").css("background", "#171717");
                $(this).css("background", "red");

                var currPage = $(this).attr("rel")
                page = currPage

                var startItem = currPage * rowShown;
                var endItem = startItem + rowShown;
                var index = $(this).parent().index();

                $("#Assets_list_Data .show").css("opacity", "0").hide().slice(startItem, endItem).css("display", "block").animate({ opacity: 1 }, 300)

            })

            $("#prev").click(function () {

                if (page != 0) {
                    var num = page - 1;
                    page = num;
                    var startItem = num * rowShown;
                    var endItem = startItem + rowShown;

                    $(".back").css("background", "#171717");
                    $(".back").eq(page).css("background", "red");

                    $("#Assets_list_Data .show").css("opacity", "0").hide().slice(startItem, endItem).css("display", "block").animate({ opacity: 1 }, 300)
                }

            })

            $("#next").click(function () {
                var n = $(".back").last().attr("rel");

                if (page != n) {
                    var num1 = parseInt(page) + 1;
                    page = num1;
                    var startItem = num1 * rowShown;
                    var endItem = startItem + rowShown;

                    $(".back").css("background", "#171717");
                    $(".back").eq(page).css("background", "red");

                    $("#Assets_list_Data .show").css("opacity", "0").hide().slice(startItem, endItem).css("display", "block").animate({ opacity: 1 }, 300)
                }

            })

        }//success
    })//ajax





















    //$.ajax({
    //    type: "GET",
    //    url: `/Ajax/Get_Assets_List`,
    //    dataType: "json",
    //    success: function (data) {
    //        GetData(data, "Assets_list_Data");
    //    }
    //})

    function GetData(data, name) {

        $.each(data, function (x, y) {

            var status = y.status;
            var status_code = ``;
            if (status == true) {
                status_code = `<span class="status active">Active</span>`
            } else {
                status_code = `<span class="status disabled">Disabled</span>`
            }

            $("#" + name).append(`

                <div class="products-row show">
                    <a href="/Admin/Assets_Detail?asset_id=${y.asset_id}" style="text-decoration: none;">

					    <div class="product-cell image">
					    	<img src="/Images/assets/${y.assetImage}" alt="product">
					    	<span class="get_data_description">${y.description}</span>
					    </div>
					    <div class="product-cell category"><span class="cell-label">Category:</span>${y.asset_type_code}</div>
					    <div class="product-cell status-cell">
					    	<span class="cell-label">Status:</span>
					    	${status_code}
					    </div>
					    <div class="product-cell sales"><span class="cell-label">Stock:</span>${y.number_in_stock}</div>
					    <div class="product-cell stock"><span class="cell-label">Assigned:</span>${y.number_assigned}</div>
					    <div class="product-cell price"><span class="cell-label">Other Detail:</span><div style="width:130px;overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">${y.other_details}</div></div>
				    </a>
                </div>

            `)

        })

    }

    //$("#Search_Data").keyup(function () {

    //    $.ajax({
    //        type: "POST",
    //        url: "/Ajax/Search_Assets",
    //        dataType: "json",
    //        data: {
    //            Search: $(this).val()
    //        },
    //        success: function (data) {
    //            $("#Assets_list_Data").empty();
    //            GetData(data, "Assets_list_Data")
    //        }
    //    })
    //})

    $("#Move_To_Add_AssetPage").click(function () {
        window.location.href = "/Admin/Assets_Add";
    })

})








document.querySelector(".jsFilter").addEventListener("click", function () {
    document.querySelector(".filter-menu").classList.toggle("active");
});

document.querySelector(".grid").addEventListener("click", function () {
    document.querySelector(".list").classList.remove("active");
    document.querySelector(".grid").classList.add("active");
    document.querySelector(".products-area-wrapper").classList.add("gridView");
    document
        .querySelector(".products-area-wrapper")
        .classList.remove("tableView");
});

document.querySelector(".list").addEventListener("click", function () {
    document.querySelector(".list").classList.add("active");
    document.querySelector(".grid").classList.remove("active");
    document.querySelector(".products-area-wrapper").classList.remove("gridView");
    document.querySelector(".products-area-wrapper").classList.add("tableView");
});

var modeSwitch = document.querySelector('.mode-switch');
modeSwitch.addEventListener('click', function () {
    document.documentElement.classList.toggle('light');
    modeSwitch.classList.toggle('active');
});