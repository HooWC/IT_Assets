$(function () {

    $("#Asset_Inventory").removeClass("nav-item").addClass("nav-item menu-is-opening menu-open");

    $("#Asset_List").css({
        "background": "rgb(226, 53, 53)",
        "border-radius": "5px",
        "line-height": "1"
    });

    var data = document.getElementById("count_number").innerHTML;
    $("#numberbyapplication").html(data);

    $.ajax({
        type: "POST",
        url: `/Ajax/Assets_Detail`,
        dataType: "json",
        data: {
            asset_id: $("#getAssetID").html()
        },
        success: function (data) {
            GetData(data, "container_info");
        }
    })

    function GetData(data, name) {

        var tag_checking = data.other_details;
        var tag = [];
        if (tag_checking.includes("|"))
            tag = data.other_details.split("|");
        else
            tag.push(data.other_details)

        var type_data = "";
        for (var i = 0; i < tag.length; i++) {
            type_data += `<div class="option">${tag[i]}</div>`
        }







        $("#" + name).append(`


			<div class="container">
				<div class="info">
					<h3 class="name">${data.asset_type_code}</h3>
					<h1 class="slogan">${data.description}</h1>
					<p class="price">${data.inventory_other_details}</p>
					<div class="attribs">
						<div class="attrib size">
							<p class="header">Tag</p>
							<div class="options">
								${type_data}
							</div>
						</div>

						<div class="attrib color">
							<p class="header">Other Detail</p>
							<div class="detail_others">
								<p class="title_p">Number in Stock:</p>
								<p class="title_span">${data.number_in_stock}</p>
							</div>
							<div class="detail_others">
								<p class="title_p">Number Assigned:</p>
								<p class="title_span">${data.number_assigned}</p>
							</div>
							<div class="detail_others">
								<p class="title_p">Status</p>
								<p class="title_span">${data.status}</p>
							</div>
							<div class="detail_others">
								<p class="title_p">Asset Type Decription:</p>
								<p class="title_span">${data.assettype_detail}</p>
							</div>
						</div>
					</div>
					<div class="buttons">
						<a href="#open-modal-edit-asset" class="button" id="edit_btn">Edit Asset</a>
						<a href="#open-modal" class="button colored">Delete</a>
					</div>
				</div>








                 <div class="shadow_box modal-window" id="open-modal-edit-asset">
        <a href="#" title="Close" class="modal-close">
            <img src="/Images/close.png"/>
        </a>

        
        <div class="profile-card js-profile-card container_div">
        <form action="/Ajax/Edit_Asset_Form" method="post" enctype="multipart/form-data">
          <div class="profile-card__img">
            <input type="text" value="${data.asset_id}" name="asset_id" style="display:none" />
            <input id="myIcon" type="file" name="file" onchange="ChooseImg(this)">
            <img src="/Images/assets/${data.assetImage}" id="img_avatar" alt="profile card" onclick="myIcon.click()">
          </div>
      
          <header>Editing</header>
      
          
              <div class="form first">
                  <div class="details personal">
                      <span class="title">Personal Details</span>
    
                      <div class="fields">
                          <div class="input-field">
                              <label>Asset Type</label>
                              <select id="assetType" name="asset_type_code" required>
                                <option disabled selected>Select asset type</option>
                                <option value="LAP">LAP</option>
                                <option value="RTR">RTR</option>
                                <option value="PHN">PHN</option>
                                <option value="CAM">CAM</option>
                                <option value="KBD">KBD</option>
                                <option value="TAB">TAB</option>
                                <option value="MSE">MSE</option>
                               </select>
                          </div>
    
                          <div class="input-field">
                            <label>Asset Description</label>
                            <input type="text" name="description" value="${data.description}" placeholder="Enter asset description" required>
                          </div>
    
                          <div class="input-field">
                            <label>Stock Number</label>
                            <input type="text" name="stock" value="${data.number_in_stock}" placeholder="Enter number" required>
                        </div>
    
                          <div class="input-field">
                            <label>Status</label>
                            <select id="status_type" name="status" required>
                                <option disabled selected>Select statu</option>
                                <option value="True">True</option>
                                <option value="False">False</option>
                               </select>
                          </div>
    
                          <div class="input-field">
                              <label>Type Description</label>
                              <input type="text" name="asset_type_description" value="${data.assettype_detail}" placeholder="Enter type description" required>
                          </div>
    
                          <div class="input-field">
                            <label>Other Detail</label>
                            <input type="text" name="Inventoryotherdetails" value="${data.other_in_detail}" placeholder="Enter other detail" required>
                          </div>
    
                      </div>
                  </div>
    
                  <div class="buttons">
                    <button class="sumbit" type="submit">
                        <span class="btnText">Submit</span>
                        <i class="uil uil-navigator"></i>
                    </button>
                </div>
                   
              </div>
        </form>
              
          
      
          
      
        </div>
       

       

    </div>
    




























                <div id="open-modal" class="modal-window">
                    <a href="#" title="Close" class="modal-close">
                        <img src="/Images/close.png"/>
                    </a>

                    
                        <div>
                        <form action="/Ajax/Delete_Asset_Form" method="post">
                          <input type="text" value="${data.asset_id}" name="asset_id" style="display:none" />
                          <h1 style="font-weight: bold;">Recognize!</h1>
                          <div style="font-size:0.8rem;">You can't recover the data if you delete it. Are you sure you want to delete it.</div>
                          <br>
                          <div><small style="color: rgb(155, 155, 155);font-weight: bold;">Confirm 👇</small></div>
                            <button class="btnaaa trigger" type="submit">DELETE</a>
                            </form>
                        </div>
                    
                </div>


				<div class="colorLayer"></div>
				<div class="preview">
					<div class="imgs">
						<img class="activ" src="/Images/assets/${data.assetImage}" alt="img 1">
					</div>
					<div class="zoomControl"></div>
					<div class="closePreview"></div>
				</div>
			</div>

        `)

        var desiredValue = `${data.asset_type_code}`;
        var selectElement = document.getElementById("assetType");

        for (var i = 0; i < selectElement.options.length; i++) {
            if (selectElement.options[i].value === desiredValue) {
                selectElement.options[i].selected = true;
                break;
            }
        }

        var desiredValue2 = `${data.status}`;
        var selectElement2 = document.getElementById("status_type");

        for (var i = 0; i < selectElement2.options.length; i++) {
            if (selectElement2.options[i].value === desiredValue2) {
                selectElement2.options[i].selected = true;
                break;
            }
        }


        $('.attrib .option').click(function () {
            $(this).siblings().removeClass('activ');
            $(this).addClass('activ');
        })

        $('.zoomControl').click(function () {
            $(this).parents('.productCard').addClass('morph');
            $('body').addClass('noScroll');
        })

        $('.closePreview').click(function () {
            $(this).parents('.productCard').removeClass('morph');
            $('body').removeClass('noScroll');
        })

        $('.movControl').click(function () {
            let imgActiv = $(this).parents('.preview').find('.imgs img.activ');
            if ($(this).hasClass('left')) {
                imgActiv.index() == 0 ? $('.imgs img').last().addClass('activ') : $('.imgs img.activ').prev().addClass('activ');
            } else {
                imgActiv.index() == ($('.imgs img').length - 1) ? $('.imgs img').first().addClass('activ') : $('.imgs img.activ').next().addClass('activ');
            }
            imgActiv.removeClass('activ');
        })


    }









})

function ChooseImg(file) {
    var file = file.files[0]

    var reader = new FileReader();

    reader.readAsDataURL(file);

    reader.onload = function () {
        var img = document.getElementById("img_avatar");
        console.log(this.result)
        img.src = this.result;
    };
}