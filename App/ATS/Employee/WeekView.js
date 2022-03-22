function hidepopup(popupId) {
    obj = $find(popupId);
    obj.hidePopup();
    }

// This function should iterate through all the TextBoxes and get the values 
function UpdateSum(optDayNo, obj) 
{      
    var tBox;
    var sumTextBox;
    var sumPTextBox;
    var deli;
    var TimesheetTotalTextBox;
    // clear the sum variable 
    var sumMinutes = 0;
    var sumPercents = 0;
   var TimesheetTotalMinutes = 0;
   var dayNo ;
    dayNo = optDayNo
        // Iterate through all the TextBoxes
        tBox = document.getElementsByTagName("INPUT"); 
        for(i = 0; i < (tBox.length - 2) ; i++) 
        {                   
           if(tBox[i].type == "text") 
           {
              
              sumTimeTextbox = 'st' + dayNo;
              if(Right(tBox[i].name, sumTimeTextbox.length) == sumTimeTextbox) {
                sumTextBox =  tBox[i];              
              }
              
              TotalTimeTextBox = 'TT' + dayNo;
              if (Right(tBox[i].name, TotalTimeTextBox.length) == TotalTimeTextBox) 
              {
                // The Number function forces the JavaScript to recognizes the input as a number 
                  textValue = tBox[i].value;
                  if (textValue.indexOf(':') > 0) {
                      deli = ':';
                  }
                  if (deli == ':') {
                      minutesValue = (textValue.substring(0, textValue.indexOf(':')) - 0) * 60 +
                    (textValue.substring(textValue.indexOf(':') + 1, textValue.length) - 0)
                      sumMinutes += Number(minutesValue);
                  }
                  if (deli != ':') {
                      sumMinutes += Number(textValue.replace(',','.'));
                  }
                }

            if (tBox[i].name.indexOf("TT") > 0)
              {
                  totaltextValue = tBox[i].value;
                  if (totaltextValue.indexOf(':') > 0) {
                      deli = ':';
                  }
                  if (deli == ':') {
                      totalminutesValue = (totaltextValue.substring(0, totaltextValue.indexOf(':')) - 0) * 60 +
                    (totaltextValue.substring(totaltextValue.indexOf(':') + 1, totaltextValue.length) - 0)
                      TimesheetTotalMinutes += Number(totalminutesValue);
                  }
                  if (deli != ':') {
                      TimesheetTotalMinutes += Number(totaltextValue.replace(',','.'));
                  }
              }

            if (tBox[i].name.indexOf("txtTimesheetTotal") > 0) 
              {
              TimesheetTotalTextBox = tBox[i];
              }


            sumPercentTextbox = 'spc' + dayNo;
            if (Right(tBox[i].name, sumPercentTextbox.length) == sumPercentTextbox) {
            sumPTextBox = tBox[i];
            }

            TotalPercentTextBox = 'PC' + dayNo;
            if (Right(tBox[i].name, TotalPercentTextBox.length) == TotalPercentTextBox) {
            // The Number function forces the JavaScript to recognizes the input as a number 
            ptextValue = tBox[i].value;
            percentsVal = ptextValue
            sumPercents += Number(percentsVal);
            }
        }
    }

    if (deli == ':') {
        var h = Math.floor(sumMinutes / 60);
        var thb = Math.floor(TimesheetTotalMinutes / 60);
    }
    var pt = Math.floor(sumPercents);
    if (sumTextBox != null) {
        if (deli == ':') {
            sumTextBox.value = h + ':' + padl((sumMinutes - (h * 60)), 2, '0');
        }
        if (deli != ':') {
            sumTextBox.value = parseFloat(sumMinutes).toFixed(2);
        }
    }
    if (sumPTextBox != null) {
        sumPTextBox.value = pt + '%';
    }
    if (deli == ':') {
        TimesheetTotalTextBox.value = thb + ':' + padl((TimesheetTotalMinutes - (thb * 60)), 2, '0');
    }
    if (deli != ':') {
        TimesheetTotalTextBox.value = parseFloat(TimesheetTotalMinutes).toFixed(2);
    }
}
function padl(n, w, c) {
    n = String(n);
    while (n.length < w)
        n = c + n;
    return n;
}
function GetTimeFromMinutes(minutes, IsMinutes) {
    var h = Math.floor(minutes / 60);
    valueToReturn = padl(h,2,'0');
    if (IsMinutes == true) {
        valueToReturn = valueToReturn + ':' + padl((minutes - (h * 60)),2,'0');
    }
    return  valueToReturn
}
function GetTotalMinutesOfTime(timeValue) {
    if (timeValue == '') {
        return 0;
    }
    if (timeValue.substring(1,2) == '_') {
        timeValue = '0' + timeValue.substring(0,1) + timeValue.substring(2) 
    }
    timeValue = timeValue.replace('_','0')
    minutesValue = timeValue.substring(0,2) * 60 
    if (timeValue.indexOf(':') > 0) {
            minutesValue = parseInt(minutesValue) + Number(timeValue.substring(3,6))
    }
    if (timeValue.indexOf('PM') > 0) {
        if (timeValue.indexOf('12') != -1) {
            if (timeValue.indexOf('12') != 3) {
            minutesValue = minutesValue - (12 * 60); 
            }
        }
        minutesValue = minutesValue + (12 * 60);           
     }
     if (timeValue.indexOf('AM') > 0) {
        if (timeValue.indexOf('12') != -1) {
            if (timeValue.indexOf('12') != 3) {
            minutesValue = minutesValue - (12 * 60); 
            }
        }
     }
    return minutesValue ;
}
function Right(str, num)
{
     return str.substring(str.length-num);  // pull out right num
}    
function Left(str, n){
	if (n <= 0)
	    return "";
	else if (n > String(str).length)
	    return str;
	else
	    return String(str).substring(0,n);
}
function OnTimeChange(targetObject) {
    targetId = targetObject.id;
    dayNo = Right(targetObject.name,1)   
    StartTimeTextBox = 'S' + dayNo;
    EndTimeTextBox = 'E' + dayNo;
    if (targetId.indexOf("S") > 0 )  {
        StartTime = targetObject.value;
        otherId = targetId.replace("S","E") ;
        otherobj = window.document.getElementById(otherId);
        EndTime = otherobj.value;
        TotalTimeId = targetId.replace("S","TT") ;
    }
    else
    {
        EndTime = targetObject.value;
        otherId = targetId.replace("E","S") ; 
        otherobj = window.document.getElementById(otherId);
        StartTime = otherobj.value;
        TotalTimeId = targetId.replace("E","TT") ;
    }
    objEndTime = window.document.getElementById(TotalTimeId);
    minStartTime = GetTotalMinutesOfTime(StartTime);
    minEndTime = GetTotalMinutesOfTime(EndTime);
     if (minEndTime == 0) {
        return
     }
    if (minStartTime > minEndTime) {
        minEndTime = minEndTime + (24*60)
        }
    minTotalTime = minEndTime - minStartTime ;
    strTotalTime = GetTimeFromMinutes(minTotalTime, true);
    objEndTime.value = strTotalTime;
    UpdateSum(dayNo) ;
    }   
    
function replaceAll( str, from, to ) {
    var idx = str.indexOf( from );
    while ( idx > -1 ) {
        str = str.replace( from, to ); 
        idx = str.indexOf( from );
    }
    return str;
}   
