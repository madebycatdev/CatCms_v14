/*
	EuroCMS.NET
*/

function saveExclude() {

    var articlezonegroups = "";
    var articlezones = "";
    var articles = "";

    for (i = 0; i < $("#excluded_zonegroups").children().length; i++) {
        articlezonegroups += $($("#excluded_zonegroups").children()[i]).val() + ",";
    }
    if (articlezonegroups.indexOf(",") > -1) {
        articlezonegroups = articlezonegroups.substring(0, articlezonegroups.length - 1);
    }
    for (i = 0; i < $("#excluded_zones").children().length; i++) {
        articlezones += $($("#excluded_zones").children()[i]).val() + ",";
    }
    if (articlezones.indexOf(",") > -1) {
        articlezones = articlezones.substring(0, articlezones.length - 1);
    }
    for (i = 0; i < $("#excluded_articles").children().length; i++) {
        articles += $($("#excluded_articles").children()[i]).val() + ",";
    }
    if (articles.indexOf(",") > -1) {
        articles = articles.substring(0, articles.length - 1);
    }
    $("#h_excluded_zonegroups").val(articlezonegroups);
    $("#h_excluded_zones").val(articlezones);
    $("#h_excluded_articles").val(articles);
}

function addArrayExclude(objElement, arrayName) {
    fromObj = document.getElementById(objElement);
    var excludeObj = $(fromObj).val();
    var arr = excludeObj.split(",");
    for (var i = 0; i < arr.length; i++) {
        switch (objElement) {
            case "excluded_sites":
                if ($.inArray(arr[i], excludeSite) == "-1") {
                    excludeSite.push(arr[i]);
                }
                break;
            case "excluded_zonegroups":
                if ($.inArray(arr[i], excludeZGroup) == "-1") {
                    excludeZGroup.push(arr[i]);
                }
                break;
            case "excluded_zones":
                if ($.inArray(arr[i], excludeZone) == "-1") {
                    excludeZone.push(arr[i]);
                }
                break;
        }
    }
}

function approveArticleFilesRevision(article_idm, rev_idm) {
    $.ajax({
        url: "" + approveArticleFilesRevision_var + "/?ArticleId=" + article_idm + "&RevisionId=" + rev_idm,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            alert("Approved");
        },
        error: function (result) {
            alert("Failed");
        }
    });
    return false;
}

function discardArticleFilesRevision(article_idm, rev_idm) {
    if (confirm('Your revision will be discarded?')) {

        $.ajax({
            url: "" + discardArticleFilesRevision_var + "/?ArticleId=" + article_idm + '&RevisionId=' + rev_idm,
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                alert("Discarded");
            },
            error: function (result) {
                alert("Failed");
            }
        });
    }
    return false;
}

function deleteFile(article_idm, rev_idm, inId, cur_status) {

    if (confirm('Your file will be deleted?')) {

        $.ajax({
            url: "" + deleteFile_var + "/?RevisionId=" + rev_idm + '&ArticleId=' + article_idm + '&FileId=' + inId + '&RevisionStatus=' + cur_status,
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                alert("Deleted");
            },
            error: function (result) {
                alert("Failed");
            }
        });
    }
    return false;
}

function getSiteList(objElement) {
    addArrayExclude('excluded_sites', 'excludeSite');
    addArrayExclude('excluded_zonegroups', 'excludeZGroup');
    addArrayExclude('excluded_zones', 'excludeZone');

    fromObj = document.getElementById(objElement);
    $("#not_exclude_zGroup,#not_exclude_zone").html("");
    $(fromObj).html("");
    $.ajax({
        url: "" + getSiteList_var + "/?GroupID=0",
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var obj = eval(result);
            $.each(obj, function (key, item) {
                var str = item.site_id.toString();
                if ($.inArray(str, excludeSite) == "-1") {
                    $(fromObj).append("<option value=" + item.site_id + ">" + item.site_name + "</option>");
                }
                else {
                    //$("#exclude_site").append("<option value=" + item.site_id + ">" + item.site_name + "</option>");
                }
            });
        },
        error: function (responseText, textStatus, XMLHttpRequest) {
            alert(responseText.status + ": " + XMLHttpRequest);
        }
    });
}

/*-======== Site Load End ========-*/


/*-======== Zone Group Load Start ========-*/

function getZoneGroupList(site_ID) {
    if (site_ID == "#selectZone") {
        var objElement = site_ID;
    } else {
        var objElement = "#" + $(site_ID).parents("div.modal").attr("id");
    }

    $(objElement + " .zGroup").html("");
    $(objElement + " .zone").html("");
    $(objElement + " .article").html("");
    $(objElement + " .zGroup").html("<option value='0'>- Please Select -</option>");

    $.ajax({
        url: "" + getZoneGroupList_var + "?SiteID=" + $(site_ID).val() + "",
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var obj = eval(result);
            $.each(obj, function (key, item) {
                var str = item.zone_group_id.toString();
                if ($.inArray(str, excludeZGroup) == "-1") {
                    $(objElement + " .zGroup").append("<option value=" + item.zone_group_id + ">" + item.zone_group_name + "</option>");
                }
            });
        },
        error: function (responseText, textStatus, XMLHttpRequest) {
            alert(responseText.status + ": " + XMLHttpRequest);
        }
    });
}

/*-======== Zone Group Load End ========-*/

/*-======== Zone Load Start ========-*/

function getZoneList(zg_ID) {
    var objElement = $(zg_ID).parents("div.modal").attr("id");
    $("#" + objElement + " .zone").html("");
    $("#" + objElement + " .article").html("");
    $.ajax({
        url: "" + getZoneList_var + "?ZoneGroupID=" + $(zg_ID).val() + "",
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var obj = eval(result);

            $.each(obj, function (key, item) {
                var str = item.Name.toString();
                if ($.inArray(str, excludeZone) == "-1") {
                    $("#" + objElement + " .zone").append("<option value=" + item.Id + ">" + item.Name + "</option>");
                }
            });
        },
        error: function (responseText, textStatus, XMLHttpRequest) {
            alert(responseText.status + ": " + XMLHttpRequest);
        }
    });
}

function getZoneListIndex(zg_ID) {
    $("#ZoneID").html("");
    $.ajax({
        url: "" + getZoneList_var + "?ZoneGroupID=" + $("#" + zg_ID).val() + "",
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var obj = eval(result);
            $("#ZoneID").html("<option value='0'>All Zones</option>");
            $.each(obj, function (key, item) {

                $("#ZoneID").append("<option value=" + item.Id + ">" + item.Name + "</option>");
            });
        },
        error: function (responseText, textStatus, XMLHttpRequest) {
            alert(responseText.status + ": " + XMLHttpRequest);
        }
    });
}

/*-======== Zone Load End ========-*/

/*-======== Add List =======-*/

function addList(thisObj, e) {
    var pID = $("#" + $(e).parents("div.modal").attr("id") + " .article option:selected").val();
    var pName = $("#" + $(e).parents("div.modal").attr("id") + " .article option:selected").text();
    var obj = document.getElementById(thisObj);
    $(obj).html("<option value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");
    $(e).parents("div").find(".close").trigger("click");
}

/*-======== Add List =======-*/

/*-======== Get Article List Load Start ========-*/

function getArticleList(z_ID) {
    var objElement = $(z_ID).parents("div.modal").attr("id");
    $("#" + objElement + " .article").html("");
    $.ajax({
        url: "" + getArticleList_var + "?ZoneID=" + $("#" + objElement + " .zone").val() + "",
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var obj = eval(result);
            $.each(obj, function (key, item) {
                $("#" + objElement + " .article").append("<option value=" + item.zone_id + "-" + item.article_id + ">" + item.headline + "</option>");
            });
        },
        error: function (responseText, textStatus, XMLHttpRequest) {
            alert(responseText.status + ": " + XMLHttpRequest);
        }
    });
}

/*-======== Get Article List Load End ========-*/

/*-======== Add Selected Article Start ========-*/

function addPageList(name, thisObj, formName) {
    var pID = $("#" + $(thisObj).parents("div.modal").attr("id") + " .article option:selected").val();
    var pName = $("#" + $(thisObj).parents("div.modal").attr("id") + " .article option:selected").text();
    switch (name) {
        case "homeArticle":
            $(formName + " #home_page_article").html("<option value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");
            break;
        case "errorArticle":
            $(formName + " #error_page_article").html("<option value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");
            break;
        case "defaultArticle":
            $(formName + " #default_article").html("<option value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");
            break;
        case "tagArticle":
            $(formName + " #tag_detail_article").html("<option value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");
            break;
        case "zoneArticle":
            pID = $("#" + $(thisObj).parents("div.modal").attr("id") + " .zone option:selected").val();
            pName = $("#" + $(thisObj).parents("div.modal").attr("id") + " .zone option:selected").text();
            $(formName + " #zones option").removeAttr("selected");
            $(formName + " #zones").append("<option selected='selected' value='" + pID + "'>" + pName + "</option>");
            $(formName + " #zones option:last-child").trigger("click");
            $("#az_names").append($("<input type='hidden' />").attr("name", "az_name_" + pID).val(pName));

            break;
        case "subZone":
            pID = $("#" + $(thisObj).parents("div.modal").attr("id") + " .zone option:selected").val();
            pName = $("#" + $(thisObj).parents("div.modal").attr("id") + " .zone option:selected").text();
            $(formName + " #navigation_zone_id").html("<option value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");
            $(formName + " #navigation_zone_id option").attr("selected", "selected");
            break;
        case "relatedArticle":
            $(formName + " #relateds").append("<option selected='selected' value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");
            break;
        case "redirectArticle":
            $(formName + " #redirect_article").append("<option selected='selected' value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");
            break;
        case "includeArticle":
            $(formName + " #include_articles").append("<option selected='selected' value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");
            break;
        case "excludeArticle":
            $(formName + " #exclude_articles").append("<option selected='selected' value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");
            break;
    }

    $("#" + $(thisObj).parents("div.modal").attr("id") + " .zone").html("");
    $("#" + $(thisObj).parents("div.modal").attr("id") + " .article").html("");
    $("#" + $(thisObj).parents("div.modal").attr("id") + " .zGroup").val('').attr('selected');
    $(thisObj).parents("div").find(".close").trigger("click");
}

/*-======== Add Selected Article End ========-*/

/*-======== Article Page Start ========-*/

function addLanguage(thisObj, closeObj) {

    var pID = $("#" + $(closeObj).parents("div.modal").attr("id") + " .article option:selected").val();
    var pName = $("#" + $(closeObj).parents("div.modal").attr("id") + " .article option:selected").text();
    $("#" + thisObj).append("<option selected='selected' value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");

    $(closeObj).parents("div").find(".close").trigger("click");
}

function openModalTwo(thisObj) {
    $("#selectArticle #saveClose").attr("onclick", "addLanguage('" + thisObj + "',this)");
    $("#selectArticle .zGroup option:eq(0)").attr("selected", "selected");
    $("#selectArticle .zone").html("");
    $("#selectArticle .article").html("");
}

function openModalThree(thisObj) {
    $("#selectArticle #saveClose, #selectZone #saveClose").attr("onclick", "addArticleType('" + $(thisObj).parent("div").find("select")[0].id + "',this)");
    $("#selectArticle .zone").html("");
    $("#selectArticle .article").html("");
}

function removeObj(inObj) {
    if ($("#" + inObj + ' option:selected').val() == "" || $("#" + inObj + ' option:selected').val() == undefined) {
        alert("Please select Zone");
    }
    else {
        $("#az_properties_" + $("#" + inObj + ' option:selected').val() + "").remove();
        $("#" + inObj + ' option:selected').remove();
        $("#" + inObj + ' option').attr('selected', 'selected');
        $("#zoneNames" + $("#" + inObj + ' option:selected').val()).remove();

    }
}

function gotoObj(type, id) {
    if (id != "") {
        window.open("/cms/" + type + "/Edit/" + id);
    } else {
        alert('Please select zone!');
    }
}

function addArticleType(thisObj, closeObj) {
    var pID = $("#selectArticle select.article option:selected, #selectZone select.zone option:selected").val();
    var pName = $("#selectArticle select.article option:selected, #selectZone select.zone option:selected").text();
    $("#" + thisObj).html("");

    $("#" + thisObj).html("<option value='" + pID + "'>" + pName + "</option>").removeAttr("disabled");

    $(closeObj).parents("div").find(".close").trigger("click");


    $("#article_type_detail").val(pID);
    $("#article_type_detail_text").val(pName);

}

function vsElement(inElement) {
    var target = document.getElementById(inElement);

    if (target.style.display == "block") {
        target.style.display = "none";
    }
    else {
        target.style.display = "block";
    }
}

//Article Alias Generate -- Uses Slugify jquery plugin
function generateAlias(a) {
    if ($("#headline").val() != '' || $("#headline").val() != null || $("#headline").val() != undefined) {
        (a.prev()).val(getSlug($("#headline").val()));
    }

}

/*-======== Article Page End ========-*/


/*-======== Edit Element/Objects Start ========-*/

function editObject(thisObj) {
    var attrHref = $(thisObj).attr("href");
    $.ajax({
        url: attrHref,
        type: "GET",
        success: function (result) {
            $(".modalElement").html(result);
            $('#modalOpen').live('click', function () { });
            $('#modalOpen').trigger('click');
        },
        error: function (responseText, textStatus, XMLHttpRequest) {
            alert(responseText.status + ": " + XMLHttpRequest);
        }
    });
}

/*-======== Edit Element/Objects End ========-*/

/*-======== Delete Element/Objects Start ========-*/

function deleteObject(thisObj) {
    var message = $(thisObj).attr("data-message");
    var answer = confirm(message);
    if (!answer) {
        return false;
    }
}

/*-======== Delete Element/Objects Start ========-*/

/*-======== Discard Changes Close Start ========-*/

function discardChanges(url) {
    if (confirm('Discard changes & close?')) {
        window.location.href = url;
    }
    return true;
}

/*-======== Discard Changes Close End ========-*/


/*-======== Content Html Editor Show/Edit/Update Start ========-*/

function showEditor(typeName) {
    switch (typeName) {
        case 'H':
            $("#type_T").hide();
            $("#type_H textarea").removeAttr("disabled");
            $("#type_T textarea").attr("disabled", "disabled");
            $("#type_H").show();
            var conText = $("#type_T textarea").val();

            CKEDITOR.instances.editor_html.setData(conText);
            CKEDITOR.instances.editor_html.destroy();
            CKEDITOR.replace('editor_html', {
                customConfig: '' + cmsPath + '/Content/plugins/ckeditor/config.js'
            });
            break;
        case 'T':
            $("#type_H").hide();
            $("#type_T textarea").removeAttr("disabled");
            $("#type_H textarea").attr("disabled", "disabled");
            var conText = CKEDITOR.instances.editor_html.getData();
            $("#type_T textarea").val(conText);
            $("#type_T").show();
            break;
    }
}
/*JHTML INIT*/
function editContent(cID, thisObj) {
    var editorName = $($(thisObj).attr("href")).find(".type_H textarea").attr("id");
    setTimeout(function () {
        jHtmlAreaInit(editorName);
    }, 10);
    return false;
}

/*-======== Content Html Editor Show/Edit/Update End ========-*/

/*-======== Html Content Function Start ========-*/

function removeTarget(inObj) {
    $(inObj).html("").attr("readonly");

    $("#article_type_detail").val("");
    $("#article_type_detail_text").val("");

    $("#article_type").val("0").trigger("change");
    $("#article_type option").removeAttr("selected");
    $("#article_type option:eq(0)").attr("selected", "selected");
}

function openModal(name, formName, thisObj) {
    $($(thisObj).attr("href") + " #saveClose").attr("onclick", "addPageList('" + name + "',this,'" + formName + "')");
    $(".zGroup option:eq(0)").attr("selected", "selected");
}

function openAcc(elementName) {
    $("div").each(function () {
        if ($(this).attr("title") == elementName) {
            $(this).slideToggle();
            $(this).find(".expand").trigger("click");
        }
    });
}

function showBlock(obj) {
    if (obj.value == "True") {
        $("#revision_status_block,#revision_flag_block, #revFlagLabel").show();
    }
    else {
        $("#revision_status_block,#revision_flag_block, #revFlagLabel").hide();
    }
}

function visibleObject(obj, hideObj) {
    var from = document.getElementsByClassName(hideObj)
    if (obj.value == "Y") {
        $(from).show();
    }
    else {
        $(from).hide();
    }
}

function selectionObj() {
    $("#zones option, #relateds option").attr("selected", "selected");
    $("#az_properties select option").attr("selected", "selected");
    $("#navigation_zone_id option").attr("selected", "selected");
    $("#channel_content option").attr("selected", "selected");
    $("#a_included_sites option").attr("selected", "selected");

    $("#article_type_detail0 option").attr("selected", "selected");
    $("#article_type_detail1 option").attr("selected", "selected");
    $("#article_type_detail2 option").attr("selected", "selected");
    $("#article_type_detail3 option").attr("selected", "selected");
    $("#article_type_detail4 option").attr("selected", "selected");
    $("#article_type_detail5 option").attr("selected", "selected");
    $("#article_type_detail6 option").attr("selected", "selected");
    $("#article_type_detail7 option").attr("selected", "selected");
    $("#article_type_detail8 option").attr("selected", "selected");
    $("#article_type_detail9 option").attr("selected", "selected");

    $("#article_type_detail").val($(".article_type:visible").val())
    $("#article_type_detail_text").val($(".article_type:visible").text() != null ? $(".article_type:visible").text() : $(".article_type:visible").val())
}

function modalVisible(obj) {
    var from = document.getElementById(obj)
    $(from).modal("hide");
}

function changeFtp(inId) {
    var ctrlF = $('#FtpChk' + inId).is(":checked");
    if (ctrlF == true) {
        $('#File' + inId).hide();
        $('#Ftp' + inId).show();
    } else {
        $('#File' + inId).show();
        $('#Ftp' + inId).hide();
    }
    return true;
}

/*-======== Html Content Function End ========-*/


/*-======== BreadCrumb/Sitemap Exclude Function Start ========-*/

function moveSelectedOptions(fromId, toId) {
    from = document.getElementById(fromId);
    to = document.getElementById(toId);

    // Move them over
    if (from.options.length < 1) { return; }
    for (var i = 0; i < from.options.length; i++) {
        var o = from.options[i];
        if (o.selected) {
            if (to.options.length < 1) { var index = 0; } else { var index = to.options.length; }
            to.options[index] = new Option(o.text, o.value, false, false);
            switch (fromId) {
                case "not_exclude_site":
                    excludeSite.push(o.value);
                    break;
                case "not_exclude_zGroup":
                    excludeZGroup.push(o.value);
                    break;
                case "not_exclude_zone":
                    excludeZone.push(o.value);
                    break;
            }
        }
    }
    // Delete them from original
    for (var i = (from.options.length - 1) ; i >= 0; i--) {
        var o = from.options[i];
        if (o.selected) {
            from.options[i] = null;
        }
    }
}

function showExcludeList() {
    if ($('#a_included_sites option').length < 1)
        alert('Please include least one site');
    else {
        $('#excluded_zonegroups').attr('size', $('#excluded_zonegroups option').length);
        $('#excluded_zones').attr('size', $('#excluded_zones option').length);
        $('#excluded_articles').attr('size', $('#excluded_articles option').length);

        $("#selectExclude").modal("show");
        openModal('defaultArticle', '#zCreate')
    }
}

function showIncludeList() {
    if ($("#inc_sites").children().length == 0) {
        var obje = $('#inc_sites');
        var oOption = document.createElement("OPTION");
        oOption.text = 'Please Select';
        oOption.value = '';
        $(obje).append(oOption);

        for (i = 0; i < $('#a_included_sites option').length; i++) {
            var oOption = document.createElement("OPTION");
            oOption.text = $('#a_included_sites option')[i].text;
            oOption.value = $('#a_included_sites option')[i].value;
            $(obje).append(oOption);
        }
        $('#not_excluded_zonegroups option').length = 0;
        $('#not_excluded_zones option').length = 0;
        $('#not_excluded_articles option').length = 0;


    }

    $("#addExclude").modal("show");
}



function reshowExcludeList() {
    $('#excluded_zonegroups').attr('size', $('#excluded_zonegroups option').length);
    $('#excluded_zones').attr('size', $('#excluded_zones option').length);
    $('#excluded_articles').attr('size', $('#excluded_articles option').length);
    $('plc').style.display = 'block';
    $('plp').style.display = 'none';
}


/*-======== BreadCrumb/Sitemap Exclude Function End ========-*/

/*-======== Article File List Function Start ========-*/

function formPost(element) {
    var obj = document.getElementById(element);
    $(obj).parents("form").submit();
}

/*-======== Article File List Function End ========-*/

function changePost(url, value) {
    window.location.href = url + "&ClsfId=" + value;
}

//global version
function hasOptions(obj) {
    if (obj != null && obj.options != null) { return true; }
    return false;
}

//global version
function sortSelect(obj) {
    var o = new Array();
    if (!hasOptions(obj)) { return; }
    for (var i = 0; i < obj.options.length; i++) {
        o[o.length] = new Option(obj.options[i].text, obj.options[i].value, obj.options[i].defaultSelected, obj.options[i].selected);
    }
    if (o.length == 0) { return; }
    o = o.sort(
		function (a, b) {
		    if ((a.text + "") < (b.text + "")) { return -1; }
		    if ((a.text + "") > (b.text + "")) { return 1; }
		    return 0;
		}
		);

    for (var i = 0; i < o.length; i++) {
        obj.options[i] = new Option(o[i].text, o[i].value, o[i].defaultSelected, o[i].selected);
    }
}

//global version
function copySelectedOptions(from, to) {
    //arguments from, to, sort destination, leave selected, must select source, reverse [X], display only (for RSS) 
    var options = new Object();
    var prefix_text = '[Z]';
    var prefix_value = 'Z';

    if ((arguments.length > 2) && (arguments[3] == true)) {
        var leaveSelected = true;
    } else {
        var leaveSelected = false;
    }
    if (hasOptions(to)) {
        for (var i = 0; i < to.options.length; i++) {
            options[to.options[i].value] = to.options[i].text;
        }
    }


    if (!hasOptions(from)) { return; }
    if ((arguments.length > 3) && (arguments[4] == true) && from.selectedIndex == -1) {
        alert('You need to select proper item to continue.');
        return;
    }

    if (arguments.length > 4 && arguments[5] == true) {
        prefix_text = '[X] ';
        prefix_value = 'X';
    }

    if (arguments.length > 5 && arguments[6] == true) {
        prefix_text = '[D] ';
        prefix_value = 'D';
    }

    for (var i = 0; i < from.options.length; i++) {
        var o = from.options[i];
        if (o.selected) {
            if (options[o.value] == null || options[o.value] == "undefined" || options[o.value] != o.text) {
                if (!hasOptions(to)) { var index = 0; } else { var index = to.options.length; }
                to.options[index] = new Option(prefix_text + " " + decodeURIComponent(o.text), prefix_value + o.value, false, false);
                if (leaveSelected) { to.options[index].selected = true }
                to.options[index].className = o.className;
            }
        }
    }
    if ((arguments.length < 3) || (arguments[2] == true)) {
        sortSelect(to);
    }
    from.selectedIndex = -1;
    if (!leaveSelected) { to.selectedIndex = -1 };

    $("#channel_content option:selected").removeAttr("selected")
}

//global version
function removeSelectedOptions(from) {
    if (!hasOptions(from)) { return; }
    if (from.type == "select-one" && from.selectedIndex != -1) {
        from.options[from.selectedIndex] = null;
    }
    else {
        for (var i = (from.options.length - 1) ; i >= 0; i--) {
            var o = from.options[i];
            if (o.selected) {
                from.options[i] = null;
            }
        }
    }
    from.selectedIndex = -1;
}

//global version
function colorizeSelect(to) {
    if (!hasOptions(to)) { return; }
    for (var i = (to.options.length - 1) ; i >= 0; i--) {
        var o = to.options[i].value.substring(0, 1);
        if (o != '') {
            to.options[i].className = 'perm_' + o;
        }
    }
}
/*-======== RSSFeed Function Start ========-*/
function addZone() {
    var search_zone = document.getElementById('search_zone');
    var channel_zones = document.getElementById('channel_content');
    copySelectedOptions(search_zone, channel_zones, true, true);
    colorizeSelect(channel_zones);
    return true;
}

function addExcludeZone() {
    var search_zone = document.getElementById('search_zone');
    var channel_zones = document.getElementById('channel_content');
    copySelectedOptions(search_zone, channel_zones, true, true, false, true);
    colorizeSelect(channel_zones);
    return true;
}

function addDisplay() {
    var search_zone = document.getElementById('search_zone');
    var channel_zones = document.getElementById('channel_content');
    copySelectedOptions(search_zone, channel_zones, true, true, false, false, true);
    colorizeSelect(channel_zones);
    return true;
}

function removeAZL(inSel) {
    var channel_zones = document.getElementById(inSel);
    removeSelectedOptions(channel_zones);
    return true;
}

function getSiteAndZoneGroups(obj) {
    var objElement = $(obj);
    $.ajax({
        url: "" + getSiteAndZoneGroups_var + "",
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var obj = eval(result);
            $.each(obj, function (key, item) {
                $(objElement).append("<option class='perm_S' value='S" + item.site_id + "'>" + item.site_name + "</option>");
                for (var i = 0; i < item.zone_groups.length; i++) {
                    $(objElement).append("<option class='perm_G' value='G" + item.zone_groups[i].zone_group_id + "'>" + item.zone_groups[i].zone_group_name + "</option>");
                }
            });
        },
        error: function (responseText, textStatus, XMLHttpRequest) {
            alert(responseText.status + ": " + XMLHttpRequest);
        }
    });
}

function getZoneListRSS(inElement, inObject) {
    $(inElement).html("");
    if (document.getElementById(inObject).value == "") {
        getSiteAndZoneGroups("#search_zone");
    }
    else {
        $.ajax({
            url: "" + getZoneList_var + "?ZoneGroupID=" + document.getElementById(inObject).value + "",
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                var obj = eval(result);
                $.each(obj, function (key, item) {
                    $(inElement).append("<option value=" + item.Id + ">" + item.Name + "</option>");
                });
            },
            error: function (responseText, textStatus, XMLHttpRequest) {
                alert(responseText.status + ": " + XMLHttpRequest);
            }
        });
    }
}

/*-======== RSSFeed Function End ========-*/

/*-======== Add Xml Data Function Start ========-*/

function addXmlData(obj) {
    $('html,body').animate({ scrollTop: $(obj).offset().top }, 1000);
    var objIndex = $(".xmlItem").length + 1;
    if (objIndex <= 20) {
        $(".xmlWrap .xmlItem:eq(0)").clone().appendTo(".xmlWrap").attr("id", "xmlData_" + objIndex);
        $("#xmlData_" + objIndex + " .dataName").html("XML Data " + objIndex + ":" + " <a class='mini btn red' href='#' onclick='return removeData(this),false'><i class='icon-remove'></i> Remove Item</a>");
        $("#xmlData_" + objIndex + " label[for=name_1]").attr("for", "name_" + objIndex);
        $("#xmlData_" + objIndex + " #name_1").attr("id", "name_" + objIndex).attr("name", "name_" + objIndex);
        $("#xmlData_" + objIndex + " #afiles_1").attr("id", "afiles_" + objIndex).attr("name", "afiles_" + objIndex).removeAttr("checked");
        $("#xmlData_" + objIndex + " label[for=attribute_1]").attr("for", "attribute_" + objIndex);
        $("#xmlData_" + objIndex + " #attribute_1").attr("id", "attribute_" + objIndex).attr("name", "attribute_" + objIndex);
        $("#xmlData_" + objIndex + " label[for=value_1]").attr("for", "value_" + objIndex);
        $("#xmlData_" + objIndex + " #value_1").attr("id", "value_" + objIndex).attr("name", "value_" + objIndex);
        $("#xmlData_" + objIndex + " #cdata_1").attr("id", "cdata_" + objIndex).attr("name", "cdata_" + objIndex).removeAttr("checked");

        $("#xmlData_" + objIndex + " #name_" + objIndex).val("");
        $("#xmlData_" + objIndex + " #attribute_" + objIndex).val("");
        $("#xmlData_" + objIndex + " #value_" + objIndex).val("");

        $("#xmlData_" + objIndex + " .count").html(objIndex);
    }
    if (objIndex == 20) {
        $(obj).hide();
    }
}

function removeData(obj) {
    $(obj).parents(".xmlItem").remove();
    $(".xmlItem").each(function (i) {
        count = i + 1;
        if (count == 1) {
            $(this).find(".dataName").html("XML Data " + count);
        } else {
            $(this).find(".dataName").html("XML Data " + count + ":" + " <a class='mini btn red' href='#' onclick='return removeData(this),false'><i class='icon-remove'></i> Remove Item</a>");
        }

        $(this).find(".dataXmlName").attr("for", "name_" + count);
        $(this).find(".dataNameInput").attr("id", "name_" + count).attr("name", "name_" + count);
        $(this).find(".dataNameCheck").attr("id", "afiles_" + count);
        $(this).find(".dataAttr").attr("for", "attribute_" + count);
        $(this).find(".dataAttrInput").attr("id", "attribute_" + count).attr("name", "attribute_" + count);
        $(this).find(".dataValue").attr("for", "value_" + count);
        $(this).find(".dataValueInput").attr("id", "value_" + count).attr("name", "value_" + count);
        $(this).find(".dataValueCheck").attr("id", "cdata_" + count);
        $(this).find(".count").html(count);

    });
}

/*-======== Add Xml Data Function End ========-*/

/*-======== Add File Type Functio Start ========-*/

function addFileType(obj) {
    $('html,body').animate({ scrollTop: $(obj).offset().top }, 1000);
    var objIndex = $(".fileTypeItem").length + 1;
    if (objIndex <= 10) {
        $(".fileWrap .fileTypeItem:eq(0)").clone().appendTo(".fileWrap").attr("id", "fileType_" + objIndex);
        $("#fileType_" + objIndex + " .dataFileName").html("File Type " + objIndex + ":" + " <a class='mini btn red' href='#' onclick='return removeDataFile(this),false'><i class='icon-remove'></i> Remove Item</a>");
        $("#fileType_" + objIndex + " label[for=filename_1]").attr("for", "filename_" + objIndex)
        $("#fileType_" + objIndex + " #filename_1").attr("id", "filename_" + objIndex).attr("name", "filename_" + objIndex);
        $("#fileType_" + objIndex + " label[for=fileextension_1]").attr("for", "fileextension_" + objIndex)
        $("#fileType_" + objIndex + " #fileextension_1").attr("id", "fileextension_" + objIndex).attr("name", "fileextension_" + objIndex);
        $("#fileType_" + objIndex + " label[for=filesize_1]").attr("for", "filesize_" + objIndex)
        $("#fileType_" + objIndex + " #filesize_1").attr("id", "filesize_" + objIndex).attr("name", "filesize_" + objIndex);
        $("#fileType_" + objIndex + " label[for=filewh_1]").attr("for", "filewh_" + objIndex)
        $("#fileType_" + objIndex + " #filewh_1").attr("id", "filewh_" + objIndex).attr("name", "filewh_" + objIndex);

        $("#fileType_" + objIndex + " #filename_" + objIndex).val("");
        $("#fileType_" + objIndex + " #fileextension_" + objIndex).val("");
        $("#fileType_" + objIndex + " #filesize_" + objIndex)
        $("#fileType_" + objIndex + " #file-wh_" + objIndex).val("");

        $("#fileType_" + objIndex + " .count").html(objIndex);

    }
    if (objIndex == 10) {
        $(obj).hide();
    }
}

function removeDataFile(obj) {
    $(obj).parents(".fileTypeItem").remove();
    $(".fileTypeItem").each(function (i) {
        count = i + 1;
        if (count == 1) {
            $(this).find(".dataFileName").html("File Type " + count);
        } else {
            $(this).find(".dataFileName").html("File Type " + count + ":" + " <a class='mini btn red' href='#' onclick='return removeDataFile(this),false'><i class='icon-remove'></i> Remove Item</a>");
        }

        $(this).find(".dataName").attr("for", "filename_" + count);
        $(this).find(".dataNameInput").attr("id", "filename_" + count).attr("name", "filename_" + count);
        $(this).find(".dataExtension").attr("for", "fileextension_" + count);
        $(this).find(".dataExtensionInput").attr("id", "fileextension_" + count).attr("name", "fileextension_" + count);
        $(this).find(".dataSize").attr("for", "filesize_" + count);
        $(this).find(".dataSizeInput").attr("id", "filesize_" + count).attr("name", "filesize_" + count);
        $(this).find(".dataFileWh").attr("for", "filewh_" + count);
        $(this).find(".dataFileWhInput").attr("id", "filewh_" + count).attr("name", "filewh_" + count);
        $(this).find(".count").html(count);
    });
}

/*-======== Add File Type Functio End ========-*/

/*-======== Add Refresh Function Start ========-*/

function refreshContent(obj) {
    if (!(CKEDITOR.instances[obj])) {
        var editor = CKEDITOR.replace(obj, {
            filebrowserBrowseUrl: '' + cmsPath + '/tool/ckfinder',
            filebrowserImageBrowseUrl: '' + cmsPath + '/tool/ckfinder',
            filebrowserFlashBrowseUrl: '' + cmsPath + '/tool/ckfinder',
            filebrowserUploadUrl: '' + cmsPath + '/Content/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload',
            filebrowserImageUploadUrl: '' + cmsPath + '/Content/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload',
            filebrowserFlashUploadUrl: '' + cmsPath + '/Content/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload',
            height: "300px"
        });
        CKFinder.setupCKEditor(editor, '' + cmsPath + '/Content/plugins/ckfinder/');
    }
}

/*-======== Add Refresh Function End ========-*/

/*-======== Reload Cache Function Start ========-*/

function doubler(inS) {
    var strR = inS.toString();
    if (strR.length == 1) { strR = '0' + strR; }
    return strR;
}

function reloadCache() {
    $.ajax({
        type: "POST",
        url: reloadCache_var,
        success: function (res) {
            if (res == "OK") {
                var now = new Date();
                var rightnow = doubler(now.getHours()) + ':' + doubler(now.getMinutes()) + ':' + doubler(now.getSeconds());
                $(".cacheMessages").html("<strong>CMS Cache Updated @</strong>" + rightnow + " (your client time)").show();
            }
        }
    });
}

/*-======== Reload Cache Function End ========-*/

/*-======== Classifacation Element Function Start ========-*/

function showElement(element, number) {
    if ($(element).val() == "c") {
        $("#custom" + number + "_Column").css("display", "inline-block");
    } else {
        $("#custom" + number + "_Column").hide();
    }
}

function showComboEditor(inCID, inVal) {
    $('#combo_editor_text').val(inVal);
    $('#combo_column_no').val(inCID);
    return true;
}

function saveComboValues(thisObj) {
    $("#" + $('#combo_column_no').val() + '_hidden').val($('#combo_editor_text').val());
    $('#combo_editor_text').val("");
    $('#combo_column_no').val("");
    $(thisObj).parents("div").find(".close").trigger("click");
    return true;
}

function showDiv(thisObj) {
    if ($(thisObj).is(":checked")) {
        $('#divFileTypes').show();
    }
    else {
        $('#divFileTypes').hide();
    }
}

/*-======== Classifacation Element Function End ========-*/

/*-======== Portlet Properties Function Start ========-*/

function MoveSelectOptionUp(inSelect) {
    var selectList = $("#" + inSelect);
    var selectOptions = selectList.find('option');
    for (var i = 1; i < selectOptions.length; i++) {
        var opt = selectOptions[i];
        if (opt.selected) {
            $(selectList).find(opt).remove();
            order = i - 1;
            $(selectList).find('option:eq(' + order + ')').before(opt);
            //selectList.insertBefore(opt, selectOptions[i - 1]);
        }
    }
}

function MoveSelectOptionDown(inSelect) {
    var selectList = $("#" + inSelect);
    var selectOptions = selectList.find('option');
    for (var i = selectOptions.length - 2; i >= 0; i--) {
        var opt = selectOptions[i];
        if (opt.selected) {
            if (i == selectOptions.length - 2) {
                opt = selectOptions[i + 1];
                $(selectList).find(opt).remove();
                $(selectList).find('option:eq(' + i + ')').before(opt);
            } else {
                $(selectList).find(opt).remove();
                order = i + 1;
                $(selectList).find('option:eq(' + order + ')').before(opt);
            }
        }
    }
}

/*-======== Portlet Properties Function End ========-*/

/*-======== Zone,Article Remove Selected Start ========-*/

function removeSelected(thisObj) {
    $(thisObj).find("option").change(function () {
        var count = $(thisObj).find('option:selected').length;
        if (count > 1) {
            $(this).removeAttr('selected');
        }
    });
}

/*-======== Zone,Article Remove Selected End ========-*/

/*-======== Portlet, Menu Properties Function Start ========-*/

function ckeditorInit(textArea) {
    if (textArea == undefined) {
        textArea = "article_1"
    }
    var editor = CKEDITOR.replace(textArea, {
        filebrowserBrowseUrl: '' + cmsPath + '/tool/ckfinder',
        filebrowserImageBrowseUrl: '' + cmsPath + '/tool/ckfinder',
        filebrowserFlashBrowseUrl: '' + cmsPath + '/tool/ckfinder',
        filebrowserUploadUrl: '' + cmsPath + '/Content/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload',
        filebrowserImageUploadUrl: '' + cmsPath + '/Content/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload',
        filebrowserFlashUploadUrl: '' + cmsPath + '/Content/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload',
        height: "300px"
    });
    CKFinder.setupCKEditor(editor, '' + cmsPath + '/Content/plugins/ckfinder/');
}


function showHide(thisObj) {
    //$("table tr").hide();
    $(thisObj).parents('tr').next().slideToggle();
    $(thisObj).find("i").toggleClass("m-icon-swapdown");
    $(thisObj).find("i").toggleClass("m-icon-swapup");

    return false;
}

function modelessDialogShow(url, width, height) {
    var left = screen.availWidth / 2 - width / 2;
    var top = screen.availHeight / 2 - height / 2;
    window.open(url, "", "dependent=yes,width=" + width + "px,height=" + height + ",left=" + left + ",top=" + top);
}

var parentObj;

function applyPortlet(e) {
    var s_Element = $(parentObj).attr("alt");

    var includeArr = [];
    var excludeArr = [];

    $("#include_articles option").each(function () {
        includeArr.push($(this).val());
    });

    $("#exclude_articles option").each(function () {
        var excludeSplit = $(this).val().split("-");
        excludeArr.push(excludeSplit[1]);

    });

    p_IncludeArr = includeArr.join(",");
    $("#inpInclude").val(p_IncludeArr);
    p_ExcludeeArr = excludeArr.join(",");
    $("#inpExclude").val(p_ExcludeeArr);

    var p_ZoneId = $("#zone_id option:selected").val();
    var p_ItemCount = $("#inpItemCount option:selected").val();
    var p_ItemOrder = $("#inpOrderID option:selected").val();
    var p_MenuDepth = $("#menu_depth option:selected").val();
    var p_SitemapType = $("#SitemapType option:selected").val();
    var p_Header = $("#inpHeader").val();
    var p_ClassName = $("#inpClass").val();
    var p_ContainerTag = $("#inpContainer").val();
    var p_Include = $("#inpInclude").val();
    var p_Exclude = $("#inpExclude").val();
    var p_Seperator = $("#inpSeperator").val();
    var p_PrevNextCaption = $("#inpPrevNext").val();
    var p_PagerHeader = $("#inpPagerHeader").val();
    var p_PagerCount = $("#inpPagerCount option:selected").val();
    var p_PagerLocation = $("#inpPagerLocation option:selected").val();
    var p_ClassPager = $("#inpPagerClass").val();

    var p_MenuNotSelectedCls = $("#menu_not_selected_item_class").val();
    var p_MenuSelectedCls = $("#menu_selected_item_class").val();
    var p_MenuEliminateSingle = $("#eliminate_single_items").is(":checked") == true ? "true" : "";
    var p_MenuRemoveOnclick = $("#remove_onlick_function").is(":checked") == true ? "true" : "";
    var p_MenuContainerTagId = $("#menu_container_tag_id").val();
    var p_MenuPosition = $("#inpVH option:selected").val();

    if (s_Element.substr(0, 10) == "##portlet_") {
        var p_Ids = p_ZoneId + "_" + p_ItemCount + "_" + p_ItemOrder;
        var p_ExcludeSelf = $("#inpExSelf").is(":checked") == true ? "1" : "";

        $(parentObj).attr("cid", p_Ids);
        $(parentObj).attr("Excludeself", p_ExcludeSelf);
        $(parentObj).attr("PortletID", s_Element.substr(10, 10).replace("#", "").replace("#", ""));
        $(parentObj).attr("Zone", p_ZoneId);
        $(parentObj).attr("ItemCount", p_ItemCount);
        $(parentObj).attr("ItemOrdering", p_ItemOrder);
        $(parentObj).attr("Header", p_Header);
        $(parentObj).attr("ClassName", p_ClassName);
        $(parentObj).attr("ContainerTag", p_ContainerTag);
        $(parentObj).attr("DisplayArticles", p_Include);
        $(parentObj).attr("ExternalArticles", p_Exclude);
        $(parentObj).attr("PagerClassName", p_ClassPager);
        $(parentObj).attr("PagerCount", p_PagerCount);
        $(parentObj).attr("PagerPosition", p_PagerLocation);
        $(parentObj).attr("PagerHeader", p_PagerHeader);
        $(parentObj).attr("PagerCaptions", p_PrevNextCaption);
        $(parentObj).attr("PagerSeperator", p_Seperator);

    } else if (s_Element.substr(0, 10) == "##sitemap_") {
        var p_Ids = p_ZoneId + "_" + p_MenuDepth + "_" + p_ItemOrder + "_" + p_SitemapType;
        var sitemap_ExcludeArticleIds = "";
        var sitemap_ExcludeZoneIds = "";
        sitemap_ExcludeArticleIds = $("#inpExcludeArticleIds").val();
        sitemap_ExcludeZoneIds = $("#inpExcludeZoneIds").val();

        $(parentObj).attr("cid", p_Ids);
        $(parentObj).attr("SitemapType", p_SitemapType);
        $(parentObj).attr("Zone", p_ZoneId);
        $(parentObj).attr("MenuDepth", p_MenuDepth);
        $(parentObj).attr("ItemOrdering", p_ItemOrder);
        $(parentObj).attr("ClassName", p_ClassName);
        $(parentObj).attr("ContainerTag", p_ContainerTag);
        $(parentObj).attr("ExcludeArticleIds", sitemap_ExcludeArticleIds);
        $(parentObj).attr("ExcludeZoneIds", sitemap_ExcludeZoneIds);

    } else if (s_Element.substr(0, 7) == "##menu_") {
        var p_Ids = p_ZoneId + "_" + p_MenuDepth + "_" + p_ItemOrder + "_" + p_MenuPosition;

        $(parentObj).attr("cid", p_Ids);
        $(parentObj).attr("MenuType", "menu");
        $(parentObj).attr("Zone", p_ZoneId);
        $(parentObj).attr("MenuDepth", p_MenuDepth);
        $(parentObj).attr("ItemOrdering", p_ItemOrder);
        $(parentObj).attr("ClassName", p_ClassName);
        $(parentObj).attr("ContainerTag", p_ContainerTag);
        $(parentObj).attr("DisplayArticles", p_Include);
        $(parentObj).attr("ExternalArticles", p_Exclude);
        $(parentObj).attr("Position", p_MenuPosition);
        $(parentObj).attr("ContainerTagId", p_MenuContainerTagId);
        $(parentObj).attr("EliminateSignleItems", p_MenuEliminateSingle);
        $(parentObj).attr("EliminateOnclikFunction", p_MenuRemoveOnclick);
        $(parentObj).attr("SelectedItemClass", p_MenuSelectedCls);
        $(parentObj).attr("NotSelectedItemClass", p_MenuNotSelectedCls);
    }
    $(e).parents("div").find(".close").trigger("click");

    return false;
}
//JHTMLAREA
var myVar;
function jHtmlAreaInit(txtArea) {
    $('#' + txtArea).htmlarea({
        toolbar: [
            ["html"],
            [{
                css: 'strong',
                text: 'Strong',
                action: function (btn) {
                    var selectedNode = this.getSelection().anchorNode.parentNode.localName;

                    var selectedText = this.getSelectedHTML();
                    if (selectedText != "") {
                        if (selectedNode == "strong") {
                            this.removeFormat("strong")
                        } else {
                            this.pasteHTML("<strong>" + selectedText + "</strong>");
                        }
                    }
                }
            },
            "italic", "underline", "strikethrough", "|", "subscript", "superscript", "|", "forecolor"],
            ["increasefontsize", "decreasefontsize"],
            ["orderedlist", "unorderedlist"],
            ["indent", "outdent"],
            ["justifyleft", "justifycenter", "justifyright"],
            ["p", "h1", "h2", "h3", "h4", "h5", "h6"],
            ["link", "unlink", "|", "image"],
            [{
                css: 'removeFormatting',
                text: 'Remove Format',
                action: function (btn) {
                    myVar = this;
                    var regex = /(<([^>]+)>)/ig;
                    var selectedText = this;
                    if (selectedText != "") {
                        this.html(selectedText.html().replace(regex, ""));
                    }
                }
            }],
            [
                {
                    css: 'add-custombutton',
                    text: 'Add Objects',
                    action: function (btn) {
                        var result = this.getSelection().anchorNode.childNodes;
                        modelessDialogShow(addObjects_var, "450", "450");
                    }
                }
            ],
            [
                {
                    css: 'edit-custombutton',
                    text: 'Edit Objects',
                    action: function (btn) {
                        var selectObj = this.getSelection();

                        if (selectObj.anchorNode.tagName == "CONTENT") {
                            var parent = selectObj.anchorNode;
                            parentObj = $(parent).context.parentNode;
                            parentObj = eval(parentObj);
                            var s_Element = $(parentObj).attr("alt");

                            if (s_Element.substr(0, 10) == "##portlet_") {
                                var s_Id = $(parentObj).attr("cid");
                                if (s_Id != undefined) {
                                    var s_Split = s_Id.split("_");
                                    var s_ZoneId = s_Split[0];
                                    var s_ItemCount = s_Split[1];
                                    var s_ItemOrder = s_Split[2];
                                } else {
                                    var s_ZoneId = 0
                                    var s_ItemCount = 0
                                    var s_ItemOrder = 0
                                }

                                var s_Class = $(parentObj).attr("ClassName") != undefined ? $(parentObj).attr("ClassName") : "";
                                var s_Lang = $(parentObj).attr("ContainerTag") != undefined ? $(parentObj).attr("ContainerTag") : "";
                                var s_Include = $(parentObj).attr("DisplayArticles") != undefined ? $(parentObj).attr("DisplayArticles") : "";
                                var s_Exclude = $(parentObj).attr("ExternalArticles") != undefined ? $(parentObj).attr("ExternalArticles") : "";

                                var s_Header = $(parentObj).attr("Header") != undefined ? $(parentObj).attr("Header") : "";
                                var s_Seperator = $(parentObj).attr("PagerSeperator") != undefined ? $(parentObj).attr("PagerSeperator") : "";
                                var s_PrevNextCaption = $(parentObj).attr("PagerCaptions") != undefined ? $(parentObj).attr("PagerCaptions") : "";
                                var s_PagerHeader = $(parentObj).attr("PagerHeader") != undefined ? $(parentObj).attr("PagerHeader") : "";
                                var s_PagerCount = $(parentObj).attr("PagerCount") != undefined ? $(parentObj).attr("PagerCount") : "";
                                var s_PagerLocation = $(parentObj).attr("PagerPosition") != undefined ? $(parentObj).attr("PagerPosition") : "";
                                var s_ClassPager = $(parentObj).attr("PagerClassName") != undefined ? $(parentObj).attr("PagerClassName") : "";
                                var s_ExcludeSelf = $(parentObj).attr("ExcludeSelf") != undefined ? $(parentObj).attr("ExcludeSelf") : "";

                                $('body').modalmanager('loading');
                                $.ajax({
                                    "url": portletProp_var,
                                    "type": "POST",
                                    "data": "zone_id=" + s_ZoneId + "&item_count=" + s_ItemCount + "&item_ordering=" + s_ItemOrder + "&portlet_header=" + s_Header + "&container_tag=" + s_Lang + "&include_articles=" + s_Include + "&exclude_articles=" + s_Exclude + "&pager_class=" + s_ClassPager + "&exclude_self=" + s_ExcludeSelf + "&pager_count=" + s_PagerCount + "&pager_position=" + s_PagerLocation + "&pager_header=" + s_PagerHeader + "&prev_next_caption=" + s_PrevNextCaption + "&item_seperator=" + s_Seperator + "&class_name=" + s_Class + "",
                                    "success": function (res) {
                                        $("#ajax-modal").html(res);
                                        $('.modal-body .switch').bootstrapSwitch();
                                        initSwitch('.modal-body');
                                        $("#ajax-modal").modal().on("hidden", function () {
                                            $("#ajax-modal").empty();
                                        });
                                    }
                                });
                            } else if (s_Element.substr(0, 10) == "##sitemap_") {
                                var s_Id = $(parentObj).attr("cid");
                                var s_ExcludeArticleIds = "";
                                var s_ExcludeZoneIds = "";
                                if (s_Id != undefined) {
                                    var s_Split = s_Id.split("_");
                                    var s_ZoneId = s_Split[0];
                                    var s_MenuDepth = s_Split[1];
                                    var s_ItemOrder = s_Split[2];
                                    var s_SiteMapType = s_Split[3];
                                    s_ExcludeArticleIds = $(parentObj).attr("ExcludeArticleIds") != undefined ? $(parentObj).attr("ExcludeArticleIds") : "";
                                    s_ExcludeZoneIds = $(parentObj).attr("ExcludeZoneIds") != undefined ? $(parentObj).attr("ExcludeZoneIds") : "";
                                } else {
                                    var s_ZoneId = 0
                                    var s_MenuDepth = 0
                                    var s_ItemOrder = 0
                                    var s_SiteMapType = 0
                                }

                                var s_Class = $(parentObj).attr("ClassName") != undefined ? $(parentObj).attr("ClassName") : "";
                                var s_Lang = $(parentObj).attr("ContainerTag") != undefined ? $(parentObj).attr("ContainerTag") : "";

                                $('body').modalmanager('loading');
                                $.ajax({
                                    "url": sitemapProp_var,
                                    "type": "POST",
                                    "data": "zone_id=" + s_ZoneId + "&menu_depth=" + s_MenuDepth + "&item_ordering=" + s_ItemOrder + "&class_name=" + s_Class + "&container_tag=" + s_Lang + "&sitemap_type=" + s_SiteMapType + "&exclude_article_ids=" + s_ExcludeArticleIds + "&exclude_zone_ids=" + s_ExcludeZoneIds + "",
                                    "success": function (res) {
                                        $("#ajax-modal").html(res);
                                        $("#ajax-modal").modal().on("hidden", function () {
                                            $("#ajax-modal").empty();
                                        });
                                    }
                                });
                            }
                            else if (s_Element.substr(0, 7) == "##menu_") {
                                var s_Id = $(parentObj).attr("cid");
                                if (s_Id != undefined) {
                                    var s_Split = s_Id.split("_");
                                    var s_ZoneId = s_Split[0];
                                    var s_MenuDepth = s_Split[1];
                                    var s_ItemOrder = s_Split[2];
                                    var s_Position = s_Split[3];
                                } else {
                                    var s_ZoneId = 0
                                    var s_MenuDepth = 0
                                    var s_ItemOrder = 0
                                    var s_Position = "v"
                                }

                                var s_Class = $(parentObj).attr("ClassName") != undefined ? $(parentObj).attr("ClassName") : "";
                                var s_Lang = $(parentObj).attr("ContainerTag") != undefined ? $(parentObj).attr("ContainerTag") : "";
                                var s_Include = $(parentObj).attr("DisplayArticles") != undefined ? $(parentObj).attr("DisplayArticles") : "";
                                var s_Exclude = $(parentObj).attr("ExternalArticles") != undefined ? $(parentObj).attr("ExternalArticles") : "";
                                var s_MenuNotSelectedCls = $(parentObj).attr("NotSelectedItemClass") != undefined ? $(parentObj).attr("NotSelectedItemClass") : "";
                                var s_MenuSelectedCls = $(parentObj).attr("SelectedItemClass") != undefined ? $(parentObj).attr("SelectedItemClass") : "";
                                var s_MenuOnClickFunction = $(parentObj).attr("EliminateOnclikFunction") != undefined ? $(parentObj).attr("EliminateOnclikFunction") : "";
                                var s_MenuSingleItem = $(parentObj).attr("EliminateSignleItems") != undefined ? $(parentObj).attr("EliminateSignleItems") : "";
                                var s_MenuContainerTagId = $(parentObj).attr("ContainerTagId") != undefined ? $(parentObj).attr("ContainerTagId") : "";

                                $('body').modalmanager('loading');
                                $.ajax({
                                    "url": menuProp_var,
                                    "type": "POST",
                                    "data": "zone_id=" + s_ZoneId + "&menu_depth=" + s_MenuDepth + "&item_ordering=" + s_ItemOrder + "&class_name=" + s_Class + "&container_tag=" + s_Lang + "&include_articles=" + s_Include + "&exclude_articles=" + s_Exclude + "&position=" + s_Position + "&container_tag_id=" + s_MenuContainerTagId + "&eliminate_single=" + s_MenuSingleItem + "&remove_onclick_function=" + s_MenuOnClickFunction + "&selected_item_class=" + s_MenuSelectedCls + "&not_selected_item_class=" + s_MenuNotSelectedCls + "",
                                    "success": function (res) {
                                        $("#ajax-modal").html(res);
                                        $("#ajax-modal").modal().on("hidden", function () {
                                            $("#ajax-modal").empty();
                                        });
                                    }
                                });
                            }
                        } else {
                            alert("This is not a portlet, menu, tag, sitemap or plugin item.");
                        }
                    }
                }
            ]
        ]
    });
}

function insertObject(sHTML) {
    var opener = window.opener;
    if (opener) {
        var elem = opener.$(".editorContent > div.active textarea");
        if (elem) {
            var val = elem.value;
            $(elem).htmlarea('pasteHTML', sHTML);
        }
    }
}

function iP(id, text) {
    insertObject("<cms:portlet src='/cms/Content/img/icon_article.gif' title='" + text + "' alt='##portlet_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_article.gif' /></content></cms:portlet>");
    self.close();
    return true;
}

function iM(id, text) {
    insertObject("<cms:menu title='" + text + "' alt='##menu_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_blank.gif' /></content></cms:menu>");
    self.close();
    return true;
}

function iPl(id, text) {
    insertObject("<cms:plugin title='" + text + "' alt='##plugin_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_article.gif' /></content></cms:plugin>");
    self.close();
    return true;
}

function iSM(id, text) {
    insertObject("<cms:sitemap title='" + text + "' alt='##sitemap_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_blank.gif' /></content></cms:sitemap>");
    self.close();
    return true;
}

function iTag(id, text) {
    insertObject("<cms:tag title='" + text + "' alt='##tag_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_blank.gif' /></content></cms:tag>");
    self.close();
    return true;
}

function iB(id, text) {
    insertObject("<cms:breadcrumb title='" + text + "' alt='##breadcrumb_" + id + "##' breadcrumbid='" + id + "' runat='server'><content><img class='' src='/cms/Content/img/icon_article.gif' /></content></cms:breadcrumb>");
    self.close();
    return true;
}

function iS(id, text) {
    //insertObject("<cms:splash src='/cms/Content/img/icon_article.gif' title='" + text + "' alt='##splash_" + id + "##' runat='server'><content><img class='' src='/cms/Content/img/icon_article.gif' /></content></cms:splash>");
    insertObject("##splash_" + id + "##");
    self.close();
    return true;
}

/*-======== Portlet, Menu Properties Function End ========-*/



/* get alias for site, zone group, zone */
function getAlias(name, obj, isNameInput, url) {

    var currentObjParentID = "";
    if (($('.getAlias').val() === "") || (isNameInput === true)) {
        switch (url) {
            case "site": var url = '/cms/Site/CheckAlias'; currentObjParentID = $('#DomainId :selected').val(); break;
            case "language": var url = '/cms/Language/CheckAlias'; break;
            case "zonegroup": var url = '/cms/ZoneGroup/CheckAlias'; currentObjParentID = $('#site_id :selected').val(); break;
            case "zone": var url = '/cms/Zone/CheckAlias'; currentObjParentID = $('#zone_group_id :selected').val(); break;
            case "tag": var url = '/cms/Tag/CheckAlias'; currentObjParentID = $('#site_id :selected').val(); break;
            default: var url = '';
        }
        var currentObjID = $('#CurrentObjID').val();



        if (obj.value == "") {
            obj = name;
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: { 'text': obj.value, 'id': currentObjID, 'parentId': currentObjParentID },
            dataType: 'json',
            beforeSend: function () {
                $('.getAlias').addClass('spinner');
                $('button[type="submit"]').attr('disabled', 'disabled');
            },
            success: function (data) {
                $('.getAlias').removeClass('spinner');
                if (data != "_#NOK#_") {
                    $('.getAlias').parents('.control-group').removeClass('error');
                    $('.getAlias').val(data);
                    $('button[type="submit"]').removeAttr('disabled');
                } else {
                    $('.getAlias').parents('.control-group').addClass('error');
                    $('.getAlias').val('Server error... Try again...');
                    $('button[type="submit"]').attr('disabled', 'disabled');
                }
            },
            error: function () {
                $('.getAlias').removeClass('spinner');
                $('.getAlias').parents('.control-group').addClass('error');
                $('.getAlias').val('Server error... Try again...');
                $('button[type="submit"]').attr('disabled', 'disabled');
            }
        });
    }
}

function selAllRelArt(obj) {
    $('#relateds option').attr('selected', 'selected');
    //$('#aCreate').submit();

}

/*========== Create User =================================*/
function GeneratePassword(numLc, numUc, numDigits, numSpecial) {
    numLc = numLc || 4;
    numUc = numUc || 4;
    numDigits = numDigits || 4;
    numSpecial = numSpecial || 2;


    var lcLetters = 'abcdefghijklmnopqrstuvwxyz';
    var ucLetters = lcLetters.toUpperCase();
    var numbers = '0123456789';
    var special = '!?=#*$@+-.';

    var getRand = function (values) {
        return values.charAt(Math.floor(Math.random() * values.length));
    }

    //+ Jonas Raoni Soares Silva
    //@ http://jsfromhell.com/array/shuffle [v1.0]
    function shuffle(o) { //v1.0
        for (var j, x, i = o.length; i; j = Math.floor(Math.random() * i), x = o[--i], o[i] = o[j], o[j] = x);
        return o;
    };

    var pass = [];
    for (var i = 0; i < numLc; ++i) { pass.push(getRand(lcLetters)) }
    for (var i = 0; i < numUc; ++i) { pass.push(getRand(ucLetters)) }
    for (var i = 0; i < numDigits; ++i) { pass.push(getRand(numbers)) }
    for (var i = 0; i < numSpecial; ++i) { pass.push(getRand(special)) }

    var finalPass = shuffle(pass).join('');

    $('#generatedPassword').html(finalPass);
    $('input[name=password]').val(finalPass);
    $('input[name=confirmPassword]').val(finalPass);
}
/*========== Create User =================================*/

/*========== FORM VALIDATION =================================*/
function FormValidation(submitHandlerFunction) {

    // for more info visit the official plugin documentation: 
    // http://docs.jquery.com/Plugins/Validation


    var form = $('form.form-horizontal');
    var error = $('.alert-error', form);
    var success = $('.alert-success', form);
    $.validator.messages.required = '';

    form.each(function () {
        $(this).validate({
            //errorElement: 'span', //default input error message container
            //errorClass: 'help-inline', // default input error message class
            focusInvalid: true, // do not focus the last invalid input
            ignore: "",



            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.help-inline').removeClass('ok'); // display OK icon
                $(element)
                    .closest('.control-group').removeClass('success').addClass('error'); // set error class to the control group
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element)
                    .closest('.control-group').removeClass('error'); // set error class to the control group
            },
        });


        //apply validation on chosen dropdown value change, this only needed for chosen dropdown integration.
        $('.chosen, .chosen-select, .chosen-with-diselect', form).change(function () {
            form.validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
        });

    })
}
/*========== FORM VALIDATION =================================*/