$(function () {

    $("#User_Management").removeClass("nav-item").addClass("nav-item menu-is-opening menu-open");

    $("#Employees_List").css({
        "background": "rgb(226, 53, 53)",
        "border-radius": "5px",
        "line-height": "1"
    });

    //$(".content-wrapper").css({
    //    "background": "#171d28"
    //});

    var data = document.getElementById("count_number").innerHTML;
    $("#numberbyapplication").html(data);

    $(".content-wrapper").css({
        "background": "#1e1e1e"
    });

    $.ajax({
        type: "GET",
        url: `/Ajax/Get_Employees_Edit_Form_First`,
        dataType: "json",
        success: function (data) {
            EditFormData(data, "model_show");
            DeleteFormData(data, "model_show");
        }
    })

    $.ajax({
        type: "GET",
        url: `/Ajax/Get_Employees_List`,
        dataType: "json",
        success: function (data) {
            GetData(data, "employeeListData");
        }
    })

    $.ajax({
        type: "GET",
        url: `/Ajax/Get_Employees_Asset_List_First`,
        dataType: "json",
        success: function (data) {
            FirstData(data, "Employee_Detail");
            AssetList(data, "tableList");

        }
    })

    $.ajax({
        type: "GET",
        url: `/Ajax/Get_Employees_Asset_Model`,
        dataType: "json",
        success: function (data) {
            AssetDetailFormData(data, "model_show");
        }
    })



    function EditFormData(data, name) {



        $("#" + name).append(`

            <div class="shadow_box modal-window" id="open-modal-edit">
					<a href="#" title="Close" class="modal-close">
						<img src="/Images/close.png" />
					</a>

					<section class="container_form">
						<header>Editing</header>
                        
						<form action="/Ajax/Form_Edit_Employee" method="post" class="form">
                            
							<div class="column_form">
								<div class="input-box_form">
									<label>First Name</label>
									<input type="text" value="${data[0].first_name}" name="first_name" placeholder="Enter first name" required />
								</div>
								<div class="input-box_form">
									<label>Last Name</label>
									<input type="text" value="${data[0].last_name}" name="last_name" placeholder="Enter last name" required />
								</div>
							</div>
							<div class="column_form">
								<div class="input-box_form">
									<label>Department</label>
									<div class="select-box">
										<select id="DepartmentType" name="department" required>
											<option hidden>Department</option>
											<option value="Technology">Technology</option>
											<option value="Customer Service">Customer Service</option>
											<option value="Finance">Finance</option>
											<option value="Strategic Planning">Strategic Planning</option>
											<option value="Internal Audit">Internal Audit</option>
											<option value="Human Resources">Human Resources</option>
											<option value="Sales">Sales</option>
											<option value="Marketing">Marketing</option>
										</select>
									</div>
								</div>
								<div class="input-box_form">
									<label>Position</label>
									<div class="select-box">
										<select id="Positiontype" name="position" required>
											<option hidden>Position</option>
											<option value="Staff">Staff</option>
											<option value="Manager">Manager</option>
											<option value="CEO">CEO</option>
											<option value="COO">COO</option>
											<option value="CFO">CFO</option>
										</select>
									</div>
								</div>
							</div>


                            <input type="text" value="${data[0].employee_id}" name="employee_id" style="display:none" />
							<div class="column_form">
								<div class="input-box_form">
									<label>Email Address</label>
									<input type="text" name="email_address" value="${data[0].email_address}" placeholder="Enter email address" required />
								</div>
								<div class="input-box_form">
									<label>Phone Number</label>
									<input type="text" name="extension" value="${data[0].extension}" placeholder="Enter phone number" required />
								</div>
							</div>

							<div class="column_form">
								<div class="input-box_form">
									<label>Username</label>
									<input type="text" name="username" value="${data[0].username}" placeholder="Enter username" required />
								</div>
								<div class="input-box_form">
									<label>Password</label>
									<input type="text" name="password" value="${data[0].password}" placeholder="Enter password" required />
								</div>
							</div>



							<button style="font-weight:bold;" type="submit">Submit</button>
						</form>
					</section>
				</div>

        `)


        var desiredValue = `${data[0].department}`;
        var selectElement = document.getElementById("DepartmentType");

        for (var i = 0; i < selectElement.options.length; i++) {
            if (selectElement.options[i].value === desiredValue) {
                selectElement.options[i].selected = true;
                break;
            }
        }

        var desiredValue2 = `${data[0].position}`;
        var selectElement2 = document.getElementById("Positiontype");

        for (var i = 0; i < selectElement2.options.length; i++) {
            if (selectElement2.options[i].value === desiredValue2) {
                selectElement2.options[i].selected = true;
                break;
            }
        }

    }

    function DeleteFormData(data, name) {



        $("#" + name).append(`

            <div class="shadow_box modal-window" id="open-modal-delete">
					<a href="#" title="Close" class="modal-close">
						<img src="/Images/close.png" />
					</a>

					<section class="container_form">
						<header>Confirmation!</header>
						<form action="/Ajax/Form_Delete_Employee" method="post" class="form" style="display:flex;justify-content:center;flex-direction:column;align-items:center;">
		                    <img src="/Images/avatar/${data[0].avatar}" 
                            style="width:100px;height:100px;border-radius:50%;margin-bottom:1rem;"/>
                            <input type="text" value="${data[0].employee_id}" name="employee_id" style="display:none" />
                            <p style="font-weight: bold;">${data[0].last_name} ${data[0].first_name}</p>
                            
							<button style="font-weight:bold;" type="submit">Deleting</button>
                            <span style="margin-top:1rem;"><span style="color:red;font-weight: bold;">Tip:</span> Please confirm before deciding to delete, because the data cannot be repaired after deletion!</span>
						</form>
					</section>
				</div>

        `)



    }

    function GetData(data, name) {

        $.each(data, function (x, y) {

            var tag = y.department;
            var department = '';
            if (tag == "Sales") {
                department = `<div class="text-xs py-1 px-2 leading-none bg-blue-100 text-blue-500 rounded-md getdata_depatment" style="width:80%;">${y.department}</div>`
            }
            else if (tag == "Marketing") {
                department = `<div class="text-xs py-1 px-2 leading-none bg-green-100 text-green-600 rounded-md getdata_depatment" style="width:80%;">${y.department}</div>`
            }
            else if (tag == "Human Resources") {
                department = `<div class="text-xs py-1 px-2 leading-none bg-yellow-100 text-yellow-600 rounded-md getdata_depatment" style="width:80%;">${y.department}</div>`
            }
            else if (tag == "Finance") {
                department = `<div class="text-xs py-1 px-2 leading-none rounded-md getdata_depatment" style="width:80%;background: rgb(219, 190, 137);color:rgb(166, 119, 33);">${y.department}</div>`
            }
            else if (tag == "Customer Service") {
                department = `<div class="text-xs py-1 px-2 leading-none rounded-md getdata_depatment" style="width:80%;background: rgb(206, 219, 124);color:rgb(112, 122, 17);">${y.department}</div>`
            }
            else if (tag == "Strategic Planning") {
                department = `<div class="text-xs py-1 px-2 leading-none rounded-md getdata_depatment" style="width:80%;background: rgb(124, 219, 186);color:rgb(35, 141, 125);">${y.department}</div>`
            }
            else if (tag == "Technology") {
                department = `<div class="text-xs py-1 px-2 leading-none rounded-md getdata_depatment" style="width:80%;background: rgb(219, 124, 130);color:rgb(144, 14, 14);">${y.department}</div>`
            }
            else if (tag == "Internal Audit") {
                department = `<div class="text-xs py-1 px-2 leading-none rounded-md getdata_depatment" style="width:80%;background: rgb(198, 143, 222);color:rgb(113, 26, 139);">${y.department}</div>`
            }

            var num = y.number;
            var cart = '';
            if (num != 0) {
                cart = `<span class="badge badge-info right" style="background:red;">${y.number}</span>`
            }

            $("#" + name).append(`

                <bottom id="${y.employee_id}" class="bg-white p-2 w-full flex flex-col rounded-md shadow employee_detail" style="cursor: pointer;">
        	        <div class="flex xl:flex-row flex-col items-center text-gray-900 dark:text-white pb-2 mb-2 xl:border-b border-gray-200 border-opacity-75 dark:border-gray-700 w-full" style="font-size:0.3rem;font-weight:bold;">
        	        	<img src="/Images/avatar/${y.avatar}" class="w-7 h-7 mr-2 rounded-full" alt="profile" />
        	        	${y.last_name} ${y.first_name}
                    
        	        </div>
        	        <div class="flex items-center w-full" style="display:flex;justify-content:space-between">
        	        	${department}
        	            ${cart}
        	        </div>
                </bottom>

            `)



        })

        $(".employee_detail").click(function () {

            var cla = $(this).attr("id")
            console.log("id=" + cla)
            $.ajax({
                type: "POST",
                url: `/Ajax/Get_Employees_Asset_List_Other`,
                dataType: "json", data: {
                    employee_id: cla
                },
                success: function (data) {

                    $("#Employee_Detail").empty();
                    $("#tableList").empty();
                    $("#model_show").empty();

                    if (data[0].b == false) {
                        Add_Employee_Form(data, "model_show");
                        FirstData(data, "Employee_Detail");
                        EditFormData(data, "model_show");
                        DeleteFormData(data, "model_show");

                    } else {
                        Add_Employee_Form(data, "model_show");
                        FirstData(data, "Employee_Detail");
                        AssetList(data, "tableList");
                        EditFormData(data, "model_show");
                        DeleteFormData(data, "model_show");
                        AssetDetailFormData(data, "model_show");
                    }



                }
            })

        })

    }

    function FirstData(data, name) {
        $("#" + name).append(`

             <div class="flex w-full items-center">
    	        <div class="flex items-center text-white first_data_name" style="font-size:1rem;">
    	        	<img src="/Images/avatar/${data[0].avatar}" class="w-12 mr-4 rounded-full" alt="profile" />
    	        	${data[0].name}
    	        </div>
    	        <div class="ml-auto sm:flex hidden items-center justify-end">

    	        	<div class="text-right text_detail">
                        <a href="#open-modal-edit" class="text-white " style="font-size:0.5rem;">Edit</a>
    	        		<a href="#open-modal-delete" class="text-white " style="font-size:0.5rem;">Delete</a>
    	        	</div>

    	        </div>
            </div>
         `)
    }

    function AssetList(data, name) {
        $.each(data, function (x, y) {

            var type = y.condition_out;
            var condition_out = ``;
            if (type == "New") {
                condition_out = `<td class="border-b border-gray-500 dark:border-gray-800 text-red-500">
                    <div style="padding-left:2.8rem;" class="condition_out">
                    ${y.condition_out}
                    </div>
                </td>`
            } else if (type == "Normal") {
                condition_out = `<td class="border-b border-gray-200 dark:border-gray-800 text-red-500">
                    <div style="padding-left:2.4rem;" class="condition_out">
                    ${y.condition_out}
                    </div>
                </td>`
            } else if (type == "Minor Damage") {
                condition_out = `<td class="border-b border-gray-200 dark:border-gray-800 text-red-500">
                    <div style="padding-left:0.9rem;" class="condition_out">
                    ${y.condition_out}
                    </div>
                </td>`
            } else if (type == "Needs Maintenance") {
                condition_out = `<td class="border-b border-gray-200 dark:border-gray-800 text-red-500">
                    <div style="padding-left:0rem;" class="condition_out">
                    ${y.condition_out}
                    </div>
                </td>`
            }

            $("#" + name).append(`
  
                <tr class="tr_detail" id="${y.employee_asset}" onclick="window.location.href='#open-modal-item-asset-model';">

    	            <td class="sm:p-3 py-2 px-1 border-b border-gray-500 dark:border-gray-800">
    	            	<div class="flex items-center asset_type_code" >
    	            		<img src="/Images/assets/${y.assetImage}" style="border-radius: 5px;width:25px;height:25px;margin-right: 11px;"/>
    	            		${y.asset_type_code}
    	            	</div>
    	            </td>
    	            ${condition_out}
    	            <td class="sm:p-3 py-2 px-1 border-b border-gray-500 dark:border-gray-800 md:table-cell hidden">
                        <div class="description" style="width: 100px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                          ${y.description}
                        </div>
                    </td>
                    <td class="sm:p-3 py-2 px-1 border-b border-gray-500 dark:border-gray-800 md:table-cell hidden" style="white-space: nowrap;overflow: hidden;text-overflow: ellipsis;width: 150px;">
    	            	<div class="flex items-center">
    	            		<div class="sm:flex hidden flex-col date_out" style="margin-right:10px;">
    	            			${y.date_out}
    	            		</div>
    	            	</div>
    	            </td>
    	            
    	            <td class="sm:p-3 py-2 px-1 border-b border-gray-500 dark:border-gray-800 md:table-cell hidden" style="white-space: nowrap;overflow: hidden;text-overflow: ellipsis;width: 150px;">
    	            	<div class="flex items-center">
    	            		<div class="sm:flex hidden flex-col other_details" style="margin-right:10px;">
    	            			${y.other_details}
    	            		</div>
    	            	</div>
    	            </td>

                </tr>

            `)




        })


        $(".tr_detail").click(function () {

            var cla = $(this).attr("id")

            $.ajax({
                type: "POST",
                url: `/Ajax/Get_Employees_Asset_Detail_Model`,
                dataType: "json", data: {
                    employeeassetID: cla
                },
                success: function (data) {
                    console.log(123)
                    $("#open-modal-item-asset-model").empty();
                    Add_AssetDetailFormData(data, "open-modal-item-asset-model")


                }
            })

        })

    }

    function AssetDetailFormData(data, name) {
        $("#" + name).append(`

            <div id="open-modal-item-asset-model" class="shadow_box_asset modal-window_asset asset_detail_employee_data"></div>

        `)
    }

    function Add_AssetDetailFormData(data, name) {

        $("#" + name).append(`

            <a href="#" title="Close" class="modal-close">
                <img src="/Images/close.png"/>
            </a>


            <div class="blog-card">
                <div class="meta">
                  <div class="photo" style="background-image: url('/Images/assets/${data[0].assetImage}')"></div>
                  <ul class="details">
                    <li class="author"><img src="/Images/dateicon.png" style="width:15px;margin-right:10px;"/>2023-08-13</li>
                    <li class="date"><img src="/Images/dateicon.png" style="width:15px;margin-right:10px;"/>2023-08-28</li>
                  </ul>
                </div>
                <div class="description">
                  <h1>${data[0].description}</h1>
                  <h2>${data[0].asset_type_code}</h2>
                  <p>${data[0].other_details}</p>
                </div>
            </div>


        `)
    }

    function Add_Employee_Form(data, name) {
        $("#" + name).append(`

            <div class="shadow_box modal-window" id="open-modal">
						<a href="#" title="Close" class="modal-close">
							<img src="/Images/close.png" />
						</a>

						<section class="container_form">
							<header>Employee Registration Form</header>
							<form action="/Ajax/Form_Add_Employee" method="post" class="form">
								<div class="column_form">
									<div class="input-box_form">
										<label>First Name</label>
										<input type="text" name="first_name" placeholder="Enter first name" required />
									</div>
									<div class="input-box_form">
										<label>Last Name</label>
										<input type="text" name="last_name" placeholder="Enter last name" required />
									</div>
								</div>
								<div class="column_form">
									<div class="input-box_form">
										<label>Department</label>
										<div class="select-box">
											<select name="department">
												<option hidden>Department</option>
												<option>Technology</option>
												<option>Customer Service</option>
												<option>Finance</option>
												<option>Strategic Planning</option>
												<option>Internal Audit</option>
												<option>Human Resources</option>
												<option>Sales</option>
												<option>Marketing</option>
											</select>
										</div>
									</div>
									<div class="input-box_form">
										<label>Position</label>
										<div class="select-box">
											<select name="position">
												<option hidden>Position</option>
												<option>Staff</option>
												<option>Manager</option>
												<option>CEO</option>
												<option>COO</option>
												<option>CFO</option>
											</select>
										</div>
									</div>
								</div>



								<div class="column_form">
									<div class="input-box_form">
										<label>Email Address</label>
										<input type="email" name="email_address" placeholder="Enter email address" required />
									</div>
									<div class="input-box_form">
										<label>Phone Number</label>
										<input type="text" name="extension" placeholder="Enter phone number" required />
									</div>
								</div>

								<div class="column_form">
									<div class="input-box_form">
										<label>Username</label>
										<input type="text" name="username" placeholder="Enter username" required />
									</div>
									<div class="input-box_form">
										<label>Password</label>
										<input type="text" name="password" placeholder="Enter password" required />
									</div>
								</div>



								<button style="font-weight:bold;" type="submit">Submit</button>
							</form>
						</section>
					</div>

        `)
    }


    $("#search_employees").keyup(function () {

        $.ajax({
            type: "POST",
            url: "/Ajax/Search_Employees",
            dataType: "json",
            data: {
                Search: $(this).val()
            },
            success: function (data) {
                $("#employeeListData").empty();
                GetData(data, "employeeListData")
            }
        })
    })

    /* function getSearch(data,)*/









})




