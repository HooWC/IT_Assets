$(function () {

    $.ajax({
        type: "GET",
        url: `/Ajax/Get_Employees_Asset_List`,
        dataType: "json",
        success: function (data) {
            console.log(data)
            GetData(data, "asset_list");
        }
    })
    $.ajax({
        type: "GET",
        url: `/Ajax/Get_Return`,
        dataType: "json",
        success: function (data) {
            GetReturnData(data, "modal_return");
        }
    })

    $.ajax({
        type: "GET",
        url: `/Ajax/Get_Employees_History_List`,
        dataType: "json",
        success: function (data) {
            GetData_History(data, "history");
        }
    })

    $.ajax({
        type: "GET",
        url: `/Ajax/Get_Employees_Documents`,
        dataType: "json",
        success: function (data) {
            GetDocuments(data, "asset_list_get");
        }
    })

    $("#search_all_asset").keyup(function () {

        $.ajax({
            type: "POST",
            url: "/Ajax/Search_Assets_List_Employee",
            dataType: "json",
            data: {
                Search: $(this).val()
            },
            success: function (data) {
                $("#asset_list_get").empty();
                GetDocuments(data, "asset_list_get")
            }
        })
    })

    function GetReturnData(data, name) {
        $("#" + name).append(`

            <div class="shadow_box modal-window" id="open-modal-return"></div>

        `)
    }

    function GetData(data, name) {

        $.each(data, function (x, y) {

            $("#" + name).append(`

            
					<div class="project-box-wrapper">
						<div class="project-box" style="background-color: #383636;">
							<div class="project-box-header">
								<span style="font-weight :bold;">${y.date_out} - ${y.date_return}</span>
								<div class="more-wrapper">
									<a href="#open-modal-return" class="open_return" id="${y.id}">
										<img src="/Images/returnIcon.png" style="width:20px;cursor: pointer;" />
									</a>









								</div>
							</div>

							<div class="project-box-content-header">
								<img src="/Images/assets/${y.image}" style="border-radius: 50%;width:40px;height:40px;background-size: cover;object-fit: cover;" />
								<p class="box-content-header" style="font-weight :bold;">${y.description}</p>
								<p class="box-content-subheader">${y.other_details}</p>
							</div>
							<div class="box-progress-wrapper">
								<p class="box-progress-header">Return</p>
								<div class="box-progress-bar">
									<span class="box-progress" style="width: ${y.count}%; background-color: #ff942e"></span>
								</div>
								<p class="box-progress-percentage">${y.count}%</p>
							</div>
							<div class="project-box-footer" style="margin-top:5px;">
								<div class="participants">
									<img src="/Images/${y.avatar}" alt="participant">
								</div>
								<div class="days-left" style="color: black;">
									${y.left_day} Days Left
								</div>
							</div>
						</div>
					</div>


            `)

        })

        $(".open_return").click(function () {

            var cla = $(this).attr("id")

            $.ajax({
                type: "POST",
                url: `/Ajax/Get_Return_Model`,
                dataType: "json", data: {
                    employeeassetID: cla
                },
                success: function (data) {
                    $("#open-modal-return").empty();
                    Add_ReturnFormData(data, "open-modal-return")


                }
            })

        })

    }

    function Add_ReturnFormData(data, name) {

        $("#" + name).append(`

            <a href="#" title="Close" class="modal-close">
				<img src="/Images/close.png" />
			</a>


			<div class="container_div">
				<header>Return IT Asset</header>

				<form action="/Ajax/ReturnAsset_Employee_emp" method="post" style="min-height: 265px;">
				<input type="text" value="${data.id}" name="id" style="display:none"/>
				<div class="form first">
						<div class="details personal">
							<span class="title">Are you sure you want to return this asset?</span>
							
							<div class="fields">
								<div class="input-field">
									<label>Condition Returned Select</label>
									<select name="selectValue" required>
										<option disabled selected>Select Condition</option>
										<option value="Normal">Normal</option>
										<option value="Minor Damage">Minor Damage</option>
										<option value="Needs Maintenance">Needs Maintenance</option>
										<option value="Irreparable">Irreparable</option>
									</select>
								</div>


							</div>

						</div>



						<div class="buttons">
							<button class="sumbit" type="submit">
								<span class="btnText">Return</span>
								<i class="uil uil-navigator"></i>
							</button>
						</div>
						<span style="color:black;"><span style="color:red;font-weight: bold;">Tip:</span> Please make sure you have double-checked the status and completeness of the asset. You will not be able to undo this action once the return confirmation is complete.</span>

					</div>


				</form>
			</div>


        `)
    }

    function GetData_History(data, name) {

        $.each(data, function (x, y) {

            $("#" + name).append(`

				<div class="message-box">
					<img src="/Images/assets/${y.image}" alt="profile image">
					<div class="message-content">
						<div class="message-header">
							<div class="name">${y.description}</div>
							<div class="star-checkbox">

								<span style="font-weight: bold;color:red;">${y.condition}</span>
							</div>
						</div>
						<p class="message-line">
							${y.other_details}
						</p>
						<p class="message-line time">
							${y.date_out} - ${y.date_return}
						</p>
					</div>
				</div>


            `)

        })

    }

    function GetDocuments(data, name) {

        $.each(data, function (x, y) {

            $("#" + name).append(`

				<tr>
					<td> ${y.id} </td>
					<td> <img src="/Images/assets/${y.image}" alt="">${y.description}</td>
					<td> ${y.type} </td>
					<td>
						<p class="status ${y.color}">${y.status}</p>
					</td>
					<td> <strong> ${y.stock} </strong></td>
				</tr>

			`)

        })

    }

})















document.addEventListener('DOMContentLoaded', function () {
    var modeSwitch = document.querySelector('.mode-switch');

    modeSwitch.addEventListener('click', function () {
        document.documentElement.classList.toggle('dark');
        modeSwitch.classList.toggle('active');
    });


});