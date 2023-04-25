// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.



//function DisableBackButton() {
//    window.history.forward();
//}
//DisableBackButton();
//window.onload = DisableBackButton;
//window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton(); }
//window.onunload = function () { void (0); };


function GetCity() {
    debugger
    var countryIds = [];
    var countrydiv = document.getElementById("countryId");
    var list = countrydiv.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            countryIds.push(list[i].value);
        }
    }
    console.log("countryids:" + countryIds);
    /* var countryId = $('#countryId').find(":selected").val();*/
    debugger
    $.ajax({
        url: "/Home/GetCitys",
        method: "POST",
        data: {
            'countryId': countryIds
        },
        success: function (data) {


            $("#selectCityList").empty();
            //    document.getElementById("selectCityList").innerHTML += `
            //<option value=${name}> City </option>
            //`;

            document.getElementById("selectCityList").innerHTML += `
          <ul class="dropdown-menu">
        `;
            data = JSON.parse(data);

            data.forEach((name) => {
                //        document.getElementById("selectCityList").innerHTML += `
                //<option value=${name.CityId} >${name.Name}</option>
                //`;

                document.getElementById("selectCityList").innerHTML += `
                     <li><input type="checkbox" class="city_${name.CityId}" name="city" id="${name.Name}" value="${name.CityId}" />${name.Name}</li>
                    `;
            })
            document.getElementById("selectCityList").innerHTML += `
          </ul>
        `;
        }
        ,
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function GetProfileCity(x) {
 /*   debugger*/
    if (x != undefined) {
        var countryId = $('#countryId' + x).find(":selected").val();
        console.log($('#countryId' + x));
        var cityDivId = "selectCityList" + x;
    }
    else {
        var countryId = $('#countryId').find(":selected").val();
        var cityDivId = "selectCityList";
    }
    /*debugger*/
    $.ajax({
        url: "/Home/GetCitys",
        method: "POST",
        data: {
            'countryId': countryId
        },
        success: function (data) {

            $("#" + cityDivId).empty();
            document.getElementById(cityDivId).innerHTML += `
            <option value=${name}> City </option>
            `;
            data = JSON.parse(data);

            data.forEach((name) => {
                document.getElementById(cityDivId).innerHTML += `
                <option value=${name.CityId} >${name.Name}</option>
                `;

            });
        }
        ,
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function temp(x) {
    var checkedcntryvalues = [];
    var div1 = document.getElementById("countryId");
    var list = div1.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedcntryvalues.push(list[i].value);
        }
    }
    console.log(checkedcntryvalues);

    var checkedvalues = [];
    var div = document.getElementById("selectCityList");
    var list = div.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedvalues.push(list[i].value);
        }
    }
    console.log(checkedvalues);

    var checkedthemevalues = [];
    var div2 = document.getElementById("theme");
    var list = div2.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedthemevalues.push(list[i].value);
        }
    }
    console.log(checkedthemevalues);

    var checkedskillvalues = [];
    var div3 = document.getElementById("skill");
    var list = div3.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedskillvalues.push(list[i].value);
        }
    }
    console.log(checkedskillvalues);

    var search = document.getElementById("searchb").value;
    console.log(search);

    var sort = document.getElementById("sort").value;
    console.log(sort);

    //var listView = document.getElementById("list-view");
    //var gridView = document.getElementById("grid-view");

    $.ajax({
        type: "POST", // POST
        url: '/Platform/Filter',
        data: {
            'cityId': checkedvalues,
            'countryId': checkedcntryvalues,
            'themeId': checkedthemevalues,
            'skillId': checkedskillvalues,
            'search': search,
            'sort': sort,
            'PageIndex': x,
        },

        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {
          /*  debugger*/
            var listDisplay = document.getElementById("list-view").style.display;
            var gridDisplay = document.getElementById("grid-view").style.display;
            console.log("Grid" + gridDisplay, "List" + listDisplay);
            $("#Filter").empty();
            console.log("grid Hii");
            var filterObject = document.createElement('div');
            filterObject.innerHTML = data;
            filterObject.querySelector("#list-view").style.display = listDisplay;
            filterObject.querySelector("#grid-view").style.display = gridDisplay;
            $("#Filter").html(filterObject);
        },
        error: function (e) {
            debugger
            console.log("Bye");
            alert('Error');
        },
    });
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function filterBadges() {
    console.log(this);
    $("#filter-button").empty();
    $('input[name="country"]:checked').each(function () {
        console.log("..Filter Badges..)Country:)" + this.className);
        document.getElementById("filter-button").innerHTML += `
<button class="filter rounded-pill border" id="${this.value}" >
<div style="width:max-content">${this.id} <i onclick="removeFilter(${this.value},'country')" class="bi bi-x"></i></div>
</button>
`
    });
    debugger
    $('input[name="city"]:checked').each(function () {
        debugger
        console.log("..Filter Badges..)City:)" + this.className);

        document.getElementById("filter-button").innerHTML += `
<button class="filter rounded-pill border" id="${this.value}" >
<div style="width:max-content">${this.id} <i onclick="removeFilter(${this.value},'city')" class="bi bi-x"></i></div>
</button>
`
    });
    debugger
    $('input[name="theme"]:checked').each(function () {

        document.getElementById("filter-button").innerHTML += `
<button class="filter rounded-pill border" id="${this.value}">
<div style="width:max-content">${this.id} <i onclick="removeFilter(${this.value},'theme')" class="bi bi-x"></i></div>
</button>
`
    });

    $('input[name="skill"]:checked').each(function () {

        document.getElementById("filter-button").innerHTML += `
<button class="filter rounded-pill border" id="${this.value}">
<div style="width:max-content">${this.id} <i onclick="removeFilter(${this.value},'skill')" class="bi bi-x"></i></div>
</button>
`
    });

    document.getElementById("filter-button").innerHTML += `
<button class="clearall p-0 rounded-pill border" onclick="clearAll()">Clear all</button>
`
}

function removeFilter(checkboxId, type) {
    console.log("rm" + checkboxId);
    if (type == 'country') {
        $(".country_" + checkboxId).prop("checked", false);
        GetCity();
    }
    if (type == 'city') {
        $(".city_" + checkboxId).prop("checked", false);
    }
    if (type == 'theme') {
        $(".theme_" + checkboxId).prop("checked", false);
    }
    if (type == 'skill') {
        $(".skill_" + checkboxId).prop("checked", false);
    }

    temp();
    filterBadges();

}
function clearAll() {

    $('input[name="country"]:checked').each(function () {

        $("." + this.className).prop("checked", false);
    });
    $('input[name="city"]:checked').each(function () {

        $("." + this.className).prop("checked", false);
    });
    $('input[name="theme"]:checked').each(function () {

        $("." + this.className).prop("checked", false);
    });
    $('input[name="skill"]:checked').each(function () {

        $("." + this.className).prop("checked", false);
    });

    filterBadges();
    temp();
    $(".clearall").addClass("d-none");
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function story(x) {
    var search = document.getElementById("searchb").value;
    console.log(search);
    $.ajax({
        type: "POST", // POST
        url: '/Story/StoryFilter',
        data: {
            'search': search,
            'PageIndex': x,
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {
        /*    debugger*/
            console.log(data);
            $("#StoryFilter").empty();
            $("#StoryFilter").html(data);
            //$("#StoriesId").empty();
            //console.log("Filtered Story");
            //$("#StoriesId").html(data);
            var paginatoinPage = document.getElementById("paginationPage").style.display = 'none';
            var paginationFilter = document.getElementById("paginationFilter").style.display = 'block';
        },
        error: function (e) {
            debugger
            console.log("Bye");
            alert('Error');
        },
    });
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function fav(x, y) {
    var crd = document.getElementById("card");
    /*    var missionId = document.getElementsByClassName("mission_id").value;*/
    var missionId = x;
    $.ajax({
        type: "POST", // POST
        url: '/Platform/AddFav',
        data: {
            'MissionId': missionId
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (missions) {
      /*      debugger*/
            if (missions == "false") {
                if (y == "landingPage") {
                    $('#' + x).removeClass("bi-suit-heart-fill text-danger");
                    $('#' + x).addClass("bi-suit-heart text-light");
                }
                else {
                    $('#favMission').removeClass();
                    $('#favMission').addClass("bi bi-heart");
                    document.getElementById("favMissionText").innerHTML = "Add to Favourite";
                }
                toastr.success('Mission Removed from Favorites.');
            }
            else {
                if (y == "landingPage") {
                    $('#' + x).removeClass("bi-suit-heart text-light");
                    $('#' + x).addClass("bi-suit-heart-fill text-danger");
                }
                else {
                    $('#favMission').removeClass();
                    $('#favMission').addClass("bi bi-heart-fill text-danger");
                    document.getElementById("favMissionText").innerHTML = "Remove from Favourite";
                }
                toastr.success('Mission Added to Favorites.');
            }
            //if (missions == true) {
            //    $('#favMission').removeClass();
            //    $('#favMission').addClass("bi bi-heart-fill text-danger");
            // /*   $('#favMission').css("color", "red");*/
            //}
            //else {
            //   /* $('#favMission').css("color", "black");*/
            //    $('#favMission').removeClass();
            //    $('#favMission').addClass("bi bi-heart");
            //}
            console.log("Added ");
        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function opengrid() {
    console.log("grid-view");
    var div1 = document.getElementById("list-view");
    var div2 = document.getElementById("grid-view");
    div1.style.display = 'none';
    div2.style.display = 'block';
    console.log("Done");
}
function openlist() {
    console.log("list-view");
    var div1 = document.getElementById("list-view");
    var div2 = document.getElementById("grid-view");
    div1.style.display = 'block';
    div2.style.display = 'none';
    console.log("Done");
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function comment(x) {
    var comnt = $("#commentDescription").val();
    var missionid = x;
    console.log(comnt);
    $.ajax({
        url: "/Platform/AddComment",
        type: "POST", // POST
        data: {
            'obj': missionid,
            'comnt': comnt
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {
            toastr.success('Comment Added successfully.');
            //$("#commentDescription").val(null);
            //$("#comment").;
            //console.log("Added ");
        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function recommandToCoWorker(x) {

    var toUserId = [];
    var recommand = document.getElementById("recommand");
    var list = recommand.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            toUserId.push(list[i].value);
        }

    }
    var Missiond = x;
    /* debugger;*/
    $.ajax({
        url: "/Platform/RecommandToCoWorker",
        method: "Post",
        data: {
            "toUserId": toUserId,
            "mid": Missiond
        },
        success: function (data) {
            console.log(toUserId);
            toastr.success('E-mail Sent Successfully!')
        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function applyMission(missionId) {
    debugger
    $.ajax({
        url: '/Platform/applyMission',
        method: "POST",
        data: {
            'missionId': missionId,
        },
        success: function (missions) {
            debugger
            if (missions == true) {
                console.log("done");
                //$('#applyMission').prop('disabled', true);
                //$('#applyMission').text("Applied");
                //$('#applyMission').css("color", "red");
                //document.getElementById("pop-up").innerHTML += `Applied Successfully...`;
                $('#applyMission').empty();
                $('#applyMission').text("Requested");
                document.getElementById("pop-up").innerHTML += `Applied Successfully...`;
            }
        },
        error: function (request, error) {
            console.log("function not working");
            document.getElementById("pop-up").innerHTML += `You've already Applied...`;
            alert('Error');
        },
    });
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function PageIndex(x, y) {
    console.log(x, y);
    $.ajax({
        url: '/Platform/recentVolunteers',
        method: "POST",
        data: {
            'mid': x,
            'pageIndex': y
        },
        success: function (data) {
            debugger
            console.log(data);
            console.log("Pagination");
            $("#recentVolunter").html(data);
        },
        error: function (request, error) {
            console.log("Error in Pagination");
            alert('Error in Pagination');
        },
    });
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function showStory(x) {

    var displayimg = x;
    var text = document.getElementById("text");
    var image = document.getElementById(x).src;

    var storyText = document.getElementById("storyText");
    storyText = text;
    document.getElementById("baner").style.backgroundImage = 'url(' + image + ')';

}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function recommandStory(x) {
    var toUserId = [];
    var recommand = document.getElementById("recommand");
    var list = recommand.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            toUserId.push(list[i].value);
        }
    }
    var StoryId = x;
    $.ajax({
        url: "/Story/RecommandToCoWorker",
        method: "Post",
        data: {
            "toUserId": toUserId,
            "sid": StoryId,
        },
        success: function (data) {
            console.log(toUserId);
            toastr.success('E-mial sent successfully!');
        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function StoryPage(x) {
    $.ajax({
        url: '/Story/StoryListing',
        method: "GET",
        data: {
            'PageIndex': x
        },
        success: function (data) {
            console.log("Pagination");
            $("body").html(data);
        },
        error: function (request, error) {
            console.log("Error in Pagination");
            alert('Error in Pagination');
        },
    });
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function ContactUs() {
    var Name = document.querySelector('#Name').value;
    var Email = document.querySelector('#Email').value;
    var Subject = document.querySelector('#Subject').value;
    var Message = document.querySelector('#Message').value;
    console.log(Name, Email, Subject, Message);
    /* debugger;*/
    $.ajax({
        url: "/Profile/ContactUs",
        method: "Post",
        data: {
            "Name": Name,
            "Email": Email,
            "Subject": Subject,
            "Message": Message,
        },
        success: function (data) {
            console.log(data);
            if (data)
                toastr.success('E-mial sent successfully!');
        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function getActivity(sheet, x) {
    console.log("TimeSheet!!!!!!!!");
    if (x > 0) {
        $.ajax({
            url: "/Profile/getActivity",
            method: "Post",
            data: {
                "tid": x,
            },
            success: function (data) {
                console.log(data);
           /*     debugger*/
                if (sheet == 1) {
                    $("#TimesheetTime").empty();
                    $("#TimesheetTime").html(data);
                }
                if (sheet == 2) {
                    $("#TimeSheetModalGoal").empty();
                    $("#TimeSheetModalGoal").html(data);
                }
            },
            error: function (e) {
                console.log("Bye");
                alert('Error');
            },
        });
    }

    else {
        const timeForm = document.querySelector('#timesheetform');
        const goalForm = document.querySelector('#goalsheetform');

        if (timeForm != undefined) {
            timeForm.querySelectorAll('.form-control').forEach((element, index) => {
                element.value = "";
                console.log(element.value);
            });
        }


        if (goalForm != undefined) {
            goalForm.querySelectorAll('.form-control').forEach((element, index) => {
                element.value = "";
                console.log(element.value);
            });
        }
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function AdminSearch(x, y) {
    var search = document.getElementById("search" + x).value;
    console.log(search);
    $.ajax({
        url: "/Admin/SearchAdmin",
        method: "Post",
        data: {
            "obj": search,
            "page": x,
            "pageIndex": y,
        },
        success: function (data) {
            console.log(data);
            $("#cmspage" + x).empty();
            $("#cmspage" + x).html(data);
            //var htmlObject = document.createElement('div');
            //htmlObject.innerHTML = data;
            //var abc = htmlObject.querySelector("#add");
            //console.log(abc);
        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function getData(x, y, id) {
    var page = document.getElementById(x);
    var addForm = page.querySelector("#edit");
  /*  debugger*/
    $.ajax({
        url: "/Admin/EditForm",
        method: "Post",
        data:
        {
            "id": id,
            "page": y,
        },
        success: function (data) {
       /*     debugger*/
            console.log(data);
            var htmlObject = document.createElement('div');
            htmlObject.innerHTML = data;
            var abc = htmlObject.querySelector("#edit");
            abc.style.display = "block";
            if (y == 3) {
                var missionSkill = abc.querySelector("#photos");
                console.log(missionSkill);
                var ck = [];
                missionSkill.querySelectorAll(".Dimg").forEach((element) => {
                    ck.push(element.value);
                });
                console.log(ck);
            }
            console.log(addForm);
            /*page.remove(addForm);*/
            console.log(abc);
            /*  page.addForm(abc);*/
            addForm.replaceWith(abc);
            if (y == 2) {
                var cmsDescription = document.getElementById("cms2");
                CKEDITOR.replace(cmsDescription);
            }
            if (y == 3) {
                var abc = document.getElementById("mission2");
                CKEDITOR.replace(abc);
            }
            if (y == 1) {
                $("#editInput").on('change', function () {
                    readURL1(this);
                });
            }
            if (y == 3) {

                const inputDiv = document.querySelector(".input-div")
                const input = document.querySelector("#imageupload")
                const output = document.querySelector("#preview")
                let imagesArray = []

                input.addEventListener("change", () => {

                    const files = input.files
                    for (let i = 0; i < files.length; i++) {
                        imagesArray.push(files[i])
                    }
                    console.log(files);
                    displayImages()

                })

                inputDiv.addEventListener("drop", () => {
                    e.preventDefault()
                    const files = e.dataTransfer.files
                    for (let i = 0; i < files.length; i++) {
                        if (!files[i].type.match("image")) continue;

                        if (imagesArray.every(image => image.name !== files[i].name))
                            imagesArray.push(files[i])
                    }
                    console.log(files);
                    displayImages()
                })
                function displayImages() {

                    let images = ""
                    imagesArray.forEach((image, index) => {
                        images += `<div class="image storyimages">
                                         <img src="${URL.createObjectURL(image)}" alt="image">
                                         <span onclick="deleteImg(${index})">&times;</span>
                                       </div>`
                    })
                    output.innerHTML = images
                }

                function deleteImg(index) {
                    console.log("Hii");
                    imagesArray.splice(index, 1)
                    displayImages()
                }

               
               
             
             /*   debugger*/
                if (ck.length > 0) {

                    function toDataUrl(url, callback) {
                        console.log(url);
                        var newUrl = url;
                        var xhr = new XMLHttpRequest();
                        xhr.onload = function () {
                            callback(xhr.response);

                        };
                        xhr.open('GET', newUrl);
                        xhr.responseType = 'blob';
                        xhr.send();
                    }
                    const dT = new DataTransfer();
                    let image;
                    let images = ""
                    let returnImage = ""
                    ck.forEach((img, index) => {
                        returnImage = img;
                        img = "/img/" + img;
                        toDataUrl(img, function (x) {
                            image = x;
                            dT.items.add(new File([image], returnImage, {
                                type: "image/png"
                            }));
                            imagesArray.push(new File([image], returnImage, {
                                type: "image/png"
                            }));
                            document.querySelector('#imageupload').files = dT.files;
                        });


                        images += `<div class="image">
                                                <img src="${img}" alt="image">
                                                <span onclick="deleteImg(${index})">&times;</span>
                                              </div>`

                    });
                    
                    output.innerHTML = images;
                   
                }
                else {
                    output.innerHTML = "No Images Are Choosen";
                }
            }
         /*   debugger*/
        },
        error: function (e) {
            debugger
            console.log("Bye");
            alert('Error');
        },
    });
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function approval(x, y, z) {
    $.ajax({
        url: "/Admin/Approval",
        method: "Post",
        data:
        {
            "id": x,
            "status": y,
            "page": z,
        },
        success: function (data) {
         /*   debugger*/
            if (data) {
                document.getElementById("A" + z + "(" + x).disabled = true;
                document.getElementById("D" + z + "(" + x).disabled = false;
                toastr.success('Approved successfully!');
            }
            else {
                document.getElementById("A" + z + "(" + x).disabled = false;
                document.getElementById("D" + z + "(" + x).disabled = true;
                toastr.error('Decline successfully!');
            }
          /*  debugger*/
        },
        error: function (e) {
            debugger
            console.log("Bye");
            alert('Error');
        },
    });
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

$('#myForm').submit(function (event) {
    event.preventDefault();
    var formData = $(this).serialize(); // serialize form data
    console.log(formData);
    $.ajax({
        url: "/Admin/AddCms", // form action URL
        type: $(this).attr('method'), // form method (post/get)
        data: formData, // form data
        success: function (result) {
            // handle success response here
            alert('Form Submitted');
        },
        error: function (e) {
            debugger
            console.log("Bye");
            alert('Error');
        },
    });
});

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


function StoryPreview(id) {

    $.ajax({
        url: "/Story/StoryDetail",
        method: "Post",
        data:
        {
            "sid": id,
        },
        success: function (data) {

            console.log(data);
            //var page = document.getElementById("nav-story");
            //var content = page.getElementById("pageContent");
            //content.style.display = "none";
            $("#cmspage5").empty();
            $("#cmspage5").html(data);
            debugger
            //var button1 = document.createElement("button");

            ////var btn = $('<button/>', {
            ////    text: 'Click me!',
            ////    /*class: 'my-button',*/
            ////    click: AdminSearch(5),
            ////});
            //$("#cmspage5").append(button1);
            //button1.innerHTML = "Do Something";
            //button1.onclick = AdminSearch(5);
            //button1.id = "bhbch";

        },
        error: function (e) {
            debugger
            console.log("Bye");
            alert('Error');
        },
    });
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//console.log("Edit form profile");
//var readURL1 = function (input) {
//    if (input.files && input.files[0]) {
//        var reader1 = new FileReader();
//        reader1.onload = function (e) {
//            $('.editProfile').attr('src', e.target.result);
//            console.log(e.target.result);
//        }

//        reader1.readAsDataURL(input.files[0]);
//    }
//}
//$("#editInput").on('change', function () {
//    console.log("EditForm");
//    console.log(this);
//    readURL1(this);

//});