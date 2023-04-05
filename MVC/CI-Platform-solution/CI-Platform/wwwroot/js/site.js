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
    var countryId = $('#countryId').find(":selected").val();
    /* debugger;*/
    $.ajax({
        url: "/Home/GetCitys",
        method: "GET",
        data: {
            "countryId": countryId
        },
        success: function (data) {
            data = JSON.parse(data);
            $("#selectCityList").empty();
            document.getElementById("selectCityList").innerHTML += `
        <option value=${name}> City </option>
        `;
            data.forEach((name) => {
                document.getElementById("selectCityList").innerHTML += `
        <option value=${name.CityId} >${name.Name}</option>
        `;
            })
        }
        ,
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}



///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


function temp() {


   




    var checkedcntryvalues = [];
    var div1 = document.getElementById("countryId");
    var list = div1.getElementsByTagName("option");
    for (i = 0; i < list.length; i++) {
        if (list[i].selected) {
            checkedcntryvalues.push(list[i].value);
        }

    }
    console.log(checkedcntryvalues);


     var checkedvalues = [];
    var div = document.getElementById("selectCityList");
    var list = div.getElementsByTagName("option");
    for (i = 0; i < list.length; i++) {
        if (list[i].selected) {
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
  
    debugger

    $.ajax({
        type: "POST", // POST
        url: '/Platform/Filter',
        data: {
            'cityId': checkedvalues,
            'countryId': checkedcntryvalues,
            'themeId': checkedthemevalues,
            'skillId': checkedskillvalues,
            'search': search,
            'sort': sort
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {
            debugger
           
            $("#Filter").empty();
            console.log("grid Hii");
            $("#Filter").html(data);

            console.log(data.listView);

            //$("#grid-view").empty();
            //console.log("grid-view Hii");
            //$("#grid-view").html(data);
            //$("#list-view").empty();
            //console.log("list Hii");
            //$("#list-view").html(data);

        },
        error: function (e) {
            debugger
            console.log("Bye");
            alert('Error');
        },
    });
}



///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



function story(x) {




    var search = document.getElementById("searchb").value;
    console.log(search)

    


    debugger

    $.ajax({
        type: "POST", // POST
        url: '/Story/StoryFilter',
        data: {

            'search': search,
            'PageIndex' : x ,
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {
            debugger


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

function fav(x) {

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
             debugger
            if (missions == "false") {

                $('#favMission').removeClass();
                $('#favMission').addClass("bi bi-heart");
                document.getElementById("favMissionText").innerHTML ="Add to Favourite";
                alert('Mission Removed from Favorites.');
            }
            
            else {
                $('#favMission').removeClass();
                $('#favMission').addClass("bi bi-heart-fill text-danger");
                document.getElementById("favMissionText").innerHTML = "Remove from Favourite";
                alert('Mission Added to Favorites.');
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


//$.ajax({
//    url: "/Rating/Submit",
//    type: "POST",
//    data: { rating: value },
//    success: function (result) {
//        console.log("Rating submitted successfully");
//        // Display a success message to the user
//    },
//    error: function () {
//        console.log("Error submitting rating");
//        // Display an error message to the user
//    }
//});





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
           
         
          
            alert('Comment Added successfully.');
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

        }
        ,
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
    debugger
    $.ajax({

        url: '/Platform/Volunteering_Mission',
        method: "POST",
        data: {
            'mid': x,
            'pageIndex': y
        },
        success: function (data) {
            debugger

            console.log("Pagination");
            $("body").html(data);


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

    /* debugger;*/
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
        }
        ,
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
