var twoDates = true;

function ReloadSamepage() {

    window.location.replace(document.URL);
   
}

function getCookie(name) {
    var re = new RegExp(name + "=([^;]+)");
    var value = re.exec(document.cookie);
    return (value != null) ? unescape(value[1]) : null;
}

function AlignContent() {
  
    var lang = getCookie("CurrentLanguage");
    //alert(lang.indexOf("en"));
    if (lang && lang.indexOf("en") > -1) {
        document.getElementById("main").dir = "ltr";
        $("#shortcuts").css("right", "");
        $("#shortcuts").css("left", "0");
        $("#shortcuts").css("display", "block");

        $("#main").css("margin-left", "68px");
        $("#main").css("margin-right", "0px");

        $("#open-menu5").css("right", "10px");
        $("#open-menu5").css("left", '');
        $("#open-menu5").show();
      

        $("#open-menu").css("right", "");
        $("#open-menu").css("left", "0");
        $("#open-menu").show();

        $("#menu").css("right", "");
        $("#menu").css("left", "5px");
        document.getElementById("menu").dir = "ltr";


        $("#open-menu4").css("right", "");
        $("#open-menu4").css("left", "89px");
        $("#open-menu4").show();

        $("#open-menu22").css("right", "");
        $("#open-menu22").css("left", "213px");
        $("#open-menu22").show();

        $("#open-menu2").css("right", "");
        $("#open-menu2").css("left", "213px");
        $("#open-menu2").show();

        $(".menuLinks").css("text-align", "left");

        $("body").css("text-align", "left");


    }
    else {
        $("#shortcuts").css("right", "0");
        $("#shortcuts").css("left", "");
        $("#shortcuts").css("display", "block");

        $("#main").css("margin-left", "0px");
        $("#main").css("margin-right", "68px");

        $("#open-menu5").css("right", "");
        $("#open-menu5").css("left", '10px');
        $("#open-menu5").show();


        $("#open-menu").css("right", "0");
        $("#open-menu").css("left", "");
        $("#open-menu").show();

        $("#menu").css("right", "0");
        $("#menu").css("left", "");
        document.getElementById("menu").dir = "rtl";
      


        $("#open-menu4").css("right", "89px");
        $("#open-menu4").css("left", "");
        $("#open-menu4").show();

        $("#open-menu22").css("right", "213px");
        $("#open-menu22").css("left", "");
        $("#open-menu22").show();

        $("#open-menu2").css("right", "213px");
        $("#open-menu2").css("left", "");
        $("#open-menu2").show();

        $(".menuLinks").css("text-align", "right");

        $("body").css("text-align", "right");

    }

}

function ShowLoading() {

    updateProgress.style.display = "block";

}

function GetPeriod() {
    try {
       
        if(document.URL.indexOf("Vacation.aspx") <= -1)
        {
          
            return;
        }
        var sdat = document.getElementById(startdatefromsession).value;
        var edat = document.getElementById(enddatefromsession).value;


      
        
        $.ajax({
            type: "POST",
            url: "../GetPeriod.aspx/GetPeriodBetweenTwoDates",
            data: '{StartDate: ' + JSON.stringify(sdat) + ',EndDate: ' + JSON.stringify(edat) + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (parseInt(response.d) > 0) {
                    document.getElementById("txtvacationperiod").value = response.d;
                    document.getElementById("txtvacationperiod").style.backgroundColor = "white"
                    document.getElementById("txtvacationperiod").style.fontWeight = "900"
                }
                else if (parseInt(response.d) < 0) {
                    document.getElementById("txtvacationperiod").value = response.d;
                    document.getElementById("txtvacationperiod").style.backgroundColor = "#ff8080"
                    document.getElementById("txtvacationperiod").style.fontWeight = "900"
                }
                else {
                    $("#txtvacationperiod").parent().parent().hide()
                }
            },
            failure: function (response) {
                alert(response.d);
            }
        });

        return;
        var StartDate = new Date(convertToGreg(sdat));
        var EndDate = new Date(convertToGreg(edat));

        if ((EndDate.getTime() - StartDate.getTime()) / 1000 / 60 / 60 / 24 >= 0) {
            document.getElementById("txtvacationperiod").value = ((EndDate.getTime() - StartDate.getTime()) / 1000 / 60 / 60 / 24) + 1;
            document.getElementById("txtvacationperiod").style.backgroundColor = "white"
            document.getElementById("txtvacationperiod").style.fontWeight = "900"
        }
        else {
            document.getElementById("txtvacationperiod").value = (EndDate.getTime() - StartDate.getTime()) / 1000 / 60 / 60 / 24;
            document.getElementById("txtvacationperiod").style.backgroundColor = "#ff8080"
            document.getElementById("txtvacationperiod").style.fontWeight = "900"
        }
    
    }
    catch (err) {

    }
}

function CompareDate(oSrc, args) {
    try{
        var ParentDiv = document.getElementById("C_C_C_RadPageView1");
        if (ParentDiv && ParentDiv.className == "rmpHiddenView")
            ParentDiv = document.getElementById("C_C_C_RadPageView2");

        var SDate;
        var EDate;
        var dats;


        if (ParentDiv)
            dats = ParentDiv.getElementsByClassName("textboxnew is-calendarsPicker");
        else
            dats = document.getElementsByClassName("textboxnew is-calendarsPicker");
  
        //alert(ParentDiv.id);

        for (var i = 0; i < dats.length; i++) {
            if (dats[i].id.indexOf("Beg") > -1 || dats[i].id.indexOf("Start") > -1 || dats[i].id.indexOf("Effective") > -1) {
                SDate = document.getElementById(dats[i].id).value;
            }
            else if (dats[i].id.indexOf("End") > -1 || dats[i].id.indexOf("Expire") > -1) {
                EDate = document.getElementById(dats[i].id).value;
            }

        }

        //alert(SDate);
        //alert(EDate);

        var StartDate = new Date(convertToGreg(SDate));
        var EndDate = new Date(convertToGreg(EDate));
  

        //args.IsValid = true;
        //return;

        args.IsValid = (StartDate.getTime() <= EndDate.getTime());
    }
    catch (err) {
        args.IsValid = true;
    }
}

function CompareDatelocal(oSrc, args) {
   
    try {
        var vacactionperiod = document.getElementById("txtvacationperiod").value;
        args.IsValid = (parseInt(vacactionperiod) > 0);
    }
    catch (err) {
        args.IsValid = true;
    }
}

function convertToGreg(dat) {

    var split = dat.split('/');
    var yy = split[0];
    var mm = split[1];
    var dd = split[2];
    if (yy > 1900)
        return dat
    d = parseInt(dd)
    m = parseInt(mm)
    y = parseInt(yy)

    var jd = intPart((11 * y + 3) / 30) + 354 * y + 30 * m - intPart((m - 1) / 2) + d + 1948440 - 385

    if (jd > 2299160) {
        l = jd + 68569
        n = intPart((4 * l) / 146097)
        l = l - intPart((146097 * n + 3) / 4)
        i = intPart((4000 * (l + 1)) / 1461001)
        l = l - intPart((1461 * i) / 4) + 31
        j = intPart((80 * l) / 2447)
        d = l - intPart((2447 * j) / 80)
        l = intPart(j / 11)
        m = j + 2 - 12 * l
        y = 100 * (n - 49) + i + l
    }
    else {
        j = jd + 1402
        k = intPart((j - 1) / 1461)
        l = j - 1461 * k
        n = intPart((l - 1) / 365) - intPart(l / 1461)
        i = l - 365 * n + 30
        j = intPart((80 * i) / 2447)
        d = i - intPart((2447 * j) / 80)
        i = intPart(j / 11)
        m = j + 2 - 12 * i
        y = 4 * k + n + i - 4716
    }

    return y + "/" + m + "/" + d
}

function intPart(floatNum) {
    if (floatNum < -0.0000001) {
        return Math.ceil(floatNum - 0.0000001)
    }
    return Math.floor(floatNum + 0.0000001)
}



//function checkBalance(oSrc, args) {



//    args.IsValid = true;


//}

//alert('')

function checkBalance(oSrc, args) {

    //////////////////////
    var gvs
    var gv

    var tbl_row
    var typ_Cell
    var caryFc_Cell

    var ddls
    var ddl
    var ddlTxt
    var ddlValue

    var typ
    var blnc
    var caryf

    //////////////////////

    //args.IsValid = true;
    //return;
    try{
        gvs = document.getElementsByClassName("xGridView")

        for (var i = 0; i < gvs.length; i++)
            if (gvs[i].id.indexOf("gvBalance") > -1) {
                gv = document.getElementById(gvs[i].id);
            }

        if (!gv) {
            args.IsValid = true;
            return;
        }

        ddls = document.getElementsByTagName("select")

        for (var i = 0; i < ddls.length; i++)
            if (ddls[i].id.indexOf("Type") > -1) {
                ddl = document.getElementById(ddls[i].id);
            }

        ddlTxt = ddl.options[ddl.selectedIndex].text;
       
        for (var i = 1; i < gv.rows.length; i++) {

            tbl_row = gv.rows[i];
            typ_Cell = tbl_row.cells[0];
            blnc_Cell = tbl_row.cells[1];
            caryFc_Cell = tbl_row.cells[3];

            typ = typ_Cell.innerHTML.toString();
            blnc = blnc_Cell.innerHTML.toString();
            caryf = caryFc_Cell.innerHTML.toString();

        }

        ddlTxt = ddlTxt.trim();
        typ = typ.trim();
        
        if (ddlTxt.indexOf(typ) > -1) {
            args.IsValid = GetDateDiff() < (parseInt(blnc) + parseInt(caryf));
        }
        else
            args.IsValid = true;
    }
    catch (err) {
        args.IsValid = true;
    }
}

function checklimits(oSrc, args) {
    args.IsValid = true;
    return;
    try {

        var ddls
        var ddl
        var ddlTxt
        var ddlValue

        ddls = document.getElementsByTagName("select")
      

        for (var i = 0; i < ddls.length; i++)
            if (ddls[i].id.indexOf("Type") > -1) {
                ddl = document.getElementById(ddls[i].id);
            }

        ddlValue = ddl.options[ddl.selectedIndex].value;
        
        if (ddlValue != 1) {
            args.IsValid = true;
            return;
        }

        var peripd = GetDateDiff()
   
        if (peripd && peripd!=0 && (peripd < 30 || peripd > 60)) {
            args.IsValid = false;
            return;
        }
        else {
            args.IsValid = true;
            return;
        }
    }
    catch (err) {
        args.IsValid = true;
    }
}

function GetDateDiff() {
        try{
       
            var ParentDiv = document.getElementById("C_C_C_RadPageView1");
            if (ParentDiv && ParentDiv.className == "rmpHiddenView")
                ParentDiv = document.getElementById("C_C_C_RadPageView2");

            var SDate;
            var EDate;
            var dats;
            var times;
            
            if (ParentDiv) {
                dats = ParentDiv.getElementsByClassName("textboxnew is-calendarsPicker");
                times = ParentDiv.getElementsByClassName("riTextBox riEnabled");
            }
            else {
                dats = document.getElementsByClassName("textboxnew is-calendarsPicker");
                times = document.getElementsByClassName("riTextBox riEnabled");
            }
          
            var IsGatepass = false;

            for (var i = 0; i < dats.length; i++) {
                if (dats[i].id.indexOf("Beg") > -1 || dats[i].id.indexOf("Start") > -1 || dats[i].id.indexOf("Effective") > -1) {
                    SDate = document.getElementById(dats[i].id).value;
                }
                else if (dats[i].id.indexOf("End") > -1 || dats[i].id.indexOf("Expire") > -1) {
                    EDate = document.getElementById(dats[i].id).value;
                }
                if (dats[i].id.indexOf("Gatepass") > -1) 
                    IsGatepass = true;
            }

            if (IsGatepass) {

                for (var i = 0; i < times.length; i++) {

                    if (times[i].id.indexOf("BegTime") > -1) {
                        SDate = document.getElementById(times[i].id).value;

                    }
                    else if (times[i].id.indexOf("EndTime") > -1) {
                        EDate = document.getElementById(times[i].id).value;

                    }

                }

                SDate = SDate.replace("ص", "AM")
                SDate = SDate.replace("م", "PM")

                EDate = EDate.replace("ص", "AM")
                EDate = EDate.replace("م", "PM")


                var timeStart = new Date("01/01/2015 " + SDate).getTime();
                var timeEnd = new Date("01/01/2015 " + EDate).getTime();
                return ((timeEnd - timeStart) / 1000 / 60 -1);
            }

            else {

                var StartDate = new Date(convertToGreg(SDate));
                var EndDate = new Date(convertToGreg(EDate));
                return ((EndDate.getTime() - StartDate.getTime()) / 1000 / 60 / 60 / 24);
            }
        }

    catch (err) {
       
            return 0;
        }
  

}



function gmod(n, m) {
    return ((n % m) + m) % m;
}

function kuwaiticalendar(adjust, GDate) {
    var today = new Date(GDate);

    if (adjust) {
        adjustmili = 1000 * 60 * 60 * 24 * adjust;
        todaymili = today.getTime() + adjustmili;
        today = new Date(todaymili);
    }



    day = today.getDate();
    month = today.getMonth();
    year = today.getFullYear();


    m = month + 1;
    y = year;
    if (m < 3) {
        y -= 1;
        m += 12;
    }

    a = Math.floor(y / 100.);
    b = 2 - a + Math.floor(a / 4.);
    if (y < 1583) b = 0;
    if (y == 1582) {
        if (m > 10) b = -10;
        if (m == 10) {
            b = 0;
            if (day > 4) b = -10;
        }
    }

    jd = Math.floor(365.25 * (y + 4716)) + Math.floor(30.6001 * (m + 1)) + day + b - 1524;

    b = 0;
    if (jd > 2299160) {
        a = Math.floor((jd - 1867216.25) / 36524.25);
        b = 1 + a - Math.floor(a / 4.);
    }
    bb = jd + b + 1524;
    cc = Math.floor((bb - 122.1) / 365.25);
    dd = Math.floor(365.25 * cc);
    ee = Math.floor((bb - dd) / 30.6001);
    day = (bb - dd) - Math.floor(30.6001 * ee);
    month = ee - 1;
    if (ee > 13) {
        cc += 1;
        month = ee - 13;
    }
    year = cc - 4716;


    wd = gmod(jd + 1, 7) + 1;

    iyear = 10631. / 30.;
    epochastro = 1948084;
    epochcivil = 1948085;

    shift1 = 8.01 / 60.;

    z = jd - epochastro;
    cyc = Math.floor(z / 10631.);
    z = z - 10631 * cyc;
    j = Math.floor((z - shift1) / iyear);
    iy = 30 * cyc + j;
    z = z - Math.floor(j * iyear + shift1);
    im = Math.floor((z + 28.5001) / 29.5);
    if (im == 13) im = 12;
    id = z - Math.floor(29.5001 * im - 29);

    var myRes = new Array(8);

    myRes[0] = day; //calculated day (CE)
    myRes[1] = month - 1; //calculated month (CE)
    myRes[2] = year; //calculated year (CE)
    myRes[3] = jd - 1; //julian day number
    myRes[4] = wd - 1; //weekday number
    myRes[5] = id; //islamic date
    myRes[6] = im - 1; //islamic month
    myRes[7] = iy; //islamic year

    return myRes;
}
function writeIslamicDate(adjustment, GDate) {
    var wdNames = new Array("Ahad", "Ithnin", "Thulatha", "Arbaa", "Khams", "Jumuah", "Sabt");
    var iMonthNames = new Array("Muharram", "Safar", "Rabi'ul Awwal", "Rabi'ul Akhir",
    "Jumadal Ula", "Jumadal Akhira", "Rajab", "Sha'ban",
    "Ramadan", "Shawwal", "Dhul Qa'ada", "Dhul Hijja");
    var iDate = kuwaiticalendar(adjustment, GDate);
    var outputIslamicDate = iDate[7] + "/" + parseInt(iDate[6] + 1) + "/" + iDate[5];
    //var outputIslamicDate = wdNames[iDate[4]] + ", " + iDate[5] + " " + iMonthNames[iDate[6]] + " " + iDate[7] + " AH";
    return outputIslamicDate;
}
