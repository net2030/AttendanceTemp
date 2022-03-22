


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head><title>
	Letter of Guarantee Application
</title><link rel="stylesheet" type="text/css" media="all" href="scripts/jsdatepick-calendar/jsDatePick_ltr.min.css" />
    <script type="text/javascript" src="scripts/jsdatepick-calendar/jsDatePick.min.1.3.js"></script>

 


<script type="text/javascript">
        window.onload = function () {
            /* new JsDatePick({
                 useMode: 2,
                 target: "datepick2",
                 dateFormat: "%d-%M-%Y"
                 /*selectedDate:{				This is an example of what the full configuration offers.
                 day:5,						For full documentation about these settings please see the full version of the code.
                 month:9,
                 year:2006
                 },
                 yearsRange:[1978,2020],
                 limitToToday:false,
                 cellColorScheme:"beige",
                 dateFormat:"%m-%d-%Y",
                 imgPath:"img/",
                 weekStartDay:1 
 
             }); */


            setFieldsEN_Disabled();
            setFieldsAR_Enabled();

        };

        function expand(f) {
            f.size = f.size + 1;
        }

        function setOption1(funcNo)
        {

            // alert(document.getElementById('CheckBox5').checked);

            if (funcNo == "1") {
                document.getElementById('CheckBox5').checked = true;
            }

            if (funcNo == "2") {
                document.getElementById('CheckBox6').checked = true;
            }

            if (funcNo == "3") {
                document.getElementById('CheckBox1').checked = true;
            }

            if (funcNo == "4") {
                document.getElementById('CheckBox2').checked = true;
            }


            if (funcNo == "5") {
                document.getElementById('CheckBox7').checked = true;
                setFieldsEN_Disabled();
                setFieldsAR_Enabled();
                document.getElementById('TextBox2').focus();
            }

            if (funcNo == "6") {
                document.getElementById('CheckBox8').checked = true;
                setFieldsAR_Disabled();
                setFieldsEN_Enabled();
                document.getElementById('TextBox4').focus();
            }



            if (funcNo == "7") {
                document.getElementById('CheckBox3').checked = true;
                setFieldsEN_Disabled();
                setFieldsAR_Enabled();
                document.getElementById('TextBox2').focus();

            }

            if (funcNo == "8") {
                document.getElementById('CheckBox4').checked = true;
                setFieldsAR_Disabled(); 
                setFieldsEN_Enabled();
                document.getElementById('TextBox4').focus();
            }

            if (funcNo == "9") {
                document.getElementById('CheckBox14').checked = true;
                document.getElementById('TextBox16').value = '';
                document.getElementById('TextBox17').value = '';
               
            }

            if (funcNo == "10") {
                document.getElementById('CheckBox9').checked = true;
                document.getElementById('TextBox16').value = '';
                document.getElementById('TextBox17').value = '';
            }

            if (funcNo == "11") {
                document.getElementById('CheckBox15').checked = true;
                document.getElementById('TextBox16').value = '';
                document.getElementById('TextBox17').value = '';
            }

            if (funcNo == "12") {
                document.getElementById('CheckBox10').checked = true;
            }

            if (funcNo == "13") {
                document.getElementById('CheckBox16').checked = true;
                document.getElementById('TextBox16').value = '';
                document.getElementById('TextBox17').value = '';
            }

            if (funcNo == "14") {
                document.getElementById('CheckBox11').checked = true;
                document.getElementById('TextBox16').value = '';
                document.getElementById('TextBox17').value = '';
            }

            if (funcNo == "15") {
                document.getElementById('CheckBox17').checked = true;
                document.getElementById('TextBox16').value = '';
                document.getElementById('TextBox17').value = '';
            }

            if (funcNo == "16") {
                document.getElementById('CheckBox12').checked = true;
                document.getElementById('TextBox16').value = '';
                document.getElementById('TextBox17').value = '';
            }

            if (funcNo == "17") {
                document.getElementById('CheckBox18').checked = true;
            }

            if (funcNo == "18") {
                document.getElementById('CheckBox13').checked = true;
            }


            if (funcNo == "19") {
                document.getElementById('CheckBox21').checked = true;
            }


            if (funcNo == "20") {
                document.getElementById('CheckBox19').checked = true;
            }


            if (funcNo == "21") {
                document.getElementById('CheckBox22').checked = true;
            }


            if (funcNo == "22") {
                document.getElementById('CheckBox20').checked = true;
            }

            if (funcNo == "30") {
                document.getElementById('TextBox21').value = document.getElementById('TextBox20').value;
            }

            if (funcNo == "31") {
                document.getElementById('TextBox20').value = document.getElementById('TextBox21').value;
            }


            if (funcNo == "35") {
                document.getElementById('CheckBox13').checked = true;
                document.getElementById('CheckBox18').checked = true;
            }




        }


        function setFieldsAR_Enabled() {

            document.getElementById('TextBox2').value = '';
            document.getElementById('TextBox2').style.visibility = 'visible';

            document.getElementById('TextBox42').value = '';
            document.getElementById('TextBox42').style.visibility = 'visible';

            document.getElementById('TextBox43').value = '';
            document.getElementById('TextBox43').style.visibility = 'visible';

            document.getElementById('TextBox44').value = '';
            document.getElementById('TextBox44').style.visibility = 'visible';

            document.getElementById('TextBox11').value = '';
            document.getElementById('TextBox11').style.visibility = 'visible';

            document.getElementById('TextBox12').value = '';
            document.getElementById('TextBox12').style.visibility = 'visible';

            document.getElementById('TextBox17').value = '';
            document.getElementById('TextBox17').style.visibility = 'visible';

            document.getElementById('TextBox45').value = '';
            document.getElementById('TextBox45').style.visibility = 'visible';

            document.getElementById('TextBox46').value = '';
            document.getElementById('TextBox46').style.visibility = 'visible';

            document.getElementById('TextBox21').value = '';
            document.getElementById('TextBox21').style.visibility = 'visible';

            document.getElementById('TextBox47').value = '';
            document.getElementById('TextBox47').style.visibility = 'visible';

            document.getElementById('TextBox25').value = '';
            document.getElementById('TextBox25').style.visibility = 'visible';

            document.getElementById('TextBox26').value = '';
            document.getElementById('TextBox26').style.visibility = 'visible';

       
        }


        function setFieldsAR_Disabled() {

            document.getElementById('TextBox2').value = '';
            document.getElementById('TextBox2').style.visibility = 'hidden';
            ChangeSize(document.getElementById('TextBox2'));

            document.getElementById('TextBox42').value = '';
            document.getElementById('TextBox42').style.visibility = 'hidden';
            ChangeSize(document.getElementById('TextBox42'));
 

            document.getElementById('TextBox43').value = '';
            document.getElementById('TextBox43').style.visibility = 'hidden';
            ChangeSize(document.getElementById('TextBox43'));
     

            document.getElementById('TextBox44').value = '';
            document.getElementById('TextBox44').style.visibility = 'hidden';
            ChangeSize(document.getElementById('TextBox44'));
      

            document.getElementById('TextBox11').value = '';
            document.getElementById('TextBox11').style.visibility = 'hidden';
     

            document.getElementById('TextBox12').value = '';
            document.getElementById('TextBox12').style.visibility = 'hidden';
        

            document.getElementById('TextBox17').value = '';
            document.getElementById('TextBox17').style.visibility = 'hidden';

            document.getElementById('TextBox45').value = '';
            document.getElementById('TextBox45').style.visibility = 'hidden';

            document.getElementById('TextBox46').value = '';
            document.getElementById('TextBox46').style.visibility = 'hidden';
            ChangeSize(document.getElementById('TextBox46'));

            document.getElementById('TextBox21').value = '';
            document.getElementById('TextBox21').style.visibility = 'hidden';

            document.getElementById('TextBox47').value = '';
            document.getElementById('TextBox47').style.visibility = 'hidden';

            document.getElementById('TextBox25').value = '';
            document.getElementById('TextBox25').style.visibility = 'hidden';

            document.getElementById('TextBox26').value = '';
            document.getElementById('TextBox26').style.visibility = 'hidden';


       
        }

        function setFieldsEN_Disabled() {
            document.getElementById('TextBox4').value = '';
            document.getElementById('TextBox4').style.visibility = 'hidden';
            ChangeSize(document.getElementById('TextBox4')) ;

            document.getElementById('TextBox5').value = '';
            document.getElementById('TextBox5').style.visibility = 'hidden';
            ChangeSize(document.getElementById('TextBox5'));


            document.getElementById('TextBox8').value = '';
            document.getElementById('TextBox8').style.visibility = 'hidden';
            ChangeSize(document.getElementById('TextBox8'));


            document.getElementById('TextBox9').value = '';
            document.getElementById('TextBox9').style.visibility = 'hidden';
            ChangeSize(document.getElementById('TextBox9'));


            document.getElementById('TextBox10').value = '';
            document.getElementById('TextBox10').style.visibility = 'hidden';


            document.getElementById('TextBox13').value = '';
            document.getElementById('TextBox13').style.visibility = 'hidden';


            document.getElementById('TextBox16').value = '';
            document.getElementById('TextBox16').style.visibility = 'hidden';


            document.getElementById('TextBox18').value = '';
            document.getElementById('TextBox18').style.visibility = 'hidden';


            document.getElementById('TextBox19').value = '';
            document.getElementById('TextBox19').style.visibility = 'hidden';
            ChangeSize(document.getElementById('TextBox19'));


            document.getElementById('TextBox20').value = '';
            document.getElementById('TextBox20').style.visibility = 'hidden';




            document.getElementById('TextBox22').value = '';
            document.getElementById('TextBox22').style.visibility = 'hidden';



            document.getElementById('TextBox23').value = '';
            document.getElementById('TextBox23').style.visibility = 'hidden';


            document.getElementById('TextBox24').value = '';
            document.getElementById('TextBox24').style.visibility = 'hidden';


 

           

        }


        function setFieldsEN_Enabled() {
            document.getElementById('TextBox4').value = '';
            document.getElementById('TextBox4').style.visibility = 'visible';

            document.getElementById('TextBox5').value = '';
            document.getElementById('TextBox5').style.visibility = 'visible';


            document.getElementById('TextBox8').value = '';
            document.getElementById('TextBox8').style.visibility = 'visible';

            document.getElementById('TextBox9').value = '';
            document.getElementById('TextBox9').style.visibility = 'visible';

            document.getElementById('TextBox10').value = '';
            document.getElementById('TextBox10').style.visibility = 'visible';

            document.getElementById('TextBox13').value = '';
            document.getElementById('TextBox13').style.visibility = 'visible';

            document.getElementById('TextBox16').value = '';
            document.getElementById('TextBox16').style.visibility = 'visible';

            document.getElementById('TextBox18').value = '';
            document.getElementById('TextBox18').style.visibility = 'visible';

            document.getElementById('TextBox19').value = '';
            document.getElementById('TextBox19').style.visibility = 'visible';


            document.getElementById('TextBox20').value = '';
            document.getElementById('TextBox20').style.visibility = 'visible';



            document.getElementById('TextBox22').value = '';
            document.getElementById('TextBox22').style.visibility = 'visible';


            document.getElementById('TextBox23').value = '';
            document.getElementById('TextBox23').style.visibility = 'visible';

            document.getElementById('TextBox24').value = '';
            document.getElementById('TextBox24').style.visibility = 'visible';
 

        }

</script>

<script type="text/javascript">
    function ChangeSize(obj) {
        var tBox = obj;
        tBox.style['width'] = ((tBox.value.length) * 8) + 'px';
    }

    function ChangeSizeAr(obj) {
        var tBox = obj;
        tBox.style['width'] = ((tBox.value.length) * 5) + 'px';
    }
</script>

<script language="javascript">
    function printDiv(divName) {
        var printContents = document.getElementById(divName).innerHTML;

        var mp = document.getElementById('mainPic').innerHTML;


        document.body.innerHTML = printContents + mp;
          

        window.print();

        var originalContents = document.body.innerHTML;
        document.body.innerHTML = originalContents;
    }

 
    function numbersonly(e){
        var unicode=e.charCode? e.charCode : e.keyCode
        if (unicode!=8){ //if the key isn't the backspace key (which we should allow)
            if (unicode<48||unicode>57) //if not a number
                return false //disable key press
        }
    }


    function imposeMaxLength(Event, Object, MaxLen) {
        return (Object.value.length <= MaxLen) || (Event.keyCode == 8 || Event.keyCode == 46 || (Event.keyCode >= 35 && Event.keyCode <= 40))
    }

    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {

            return false;



        }

        return true; 
    }


    function autoTab(currField , field) {

        if (!isNaN(document.getElementById(currField).value)) {
            document.getElementById(field).focus();
        }

    }



    function chkNumber(evt, field, val,fieldthis) {

        if (isNumberKey(evt)) {
            //alert(val);
            // document.getElementById(fieldthis).value = val;
           
        }



    }


    function isValidDate(dateString) {

        if (dateString != '') {


            // First check for the pattern
            if (!/^\d{2}\/\d{2}\/\d{4}$/.test(dateString))
                return false;

            // Parse the date parts to integers
            var parts = dateString.split("/");
            var month = parseInt(parts[1], 10);
            var day = parseInt(parts[0], 10);
            var year = parseInt(parts[2], 10);



            // Check the ranges of month and year
            if (year < 1000 || year > 3000 || month == 0 || month > 12)
                return false;

            var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

            // Adjust for leap years
            if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
                monthLength[1] = 29;

            // Check the range of the day
            return day > 0 && day <= monthLength[month - 1];
        }
        else
            return true; 
    };


    function Validate() {


        var name = document.getElementById('TextBox1').value;
        var dt = document.getElementById('datepick2').value;

        if (name == "") {

            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {
                alert('الرجاء إدخال إسم الفرع');
            }
            else
            {
                alert('Please enter branch name');
            }

            document.getElementById('TextBox1').focus();
            return false;
        }


        if (dt == "") {

            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {
                alert(' الرجاء إدخال التاريخ');
            }
            else
            {
                alert('Please enter the date');
            }
            document.getElementById('datepick2').focus();
            return false;
        }

       


        if (document.getElementById('TextBox4').value == "" && document.getElementById('TextBox2').value == "") {

            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {
                alert('الرجاء إدخال خانة بالنيابه عن');
                document.getElementById('TextBox2').focus();
            }
            else
            {
                alert('Please enter On behalf of field');
                document.getElementById('TextBox4').focus();
            }
           
            return false;
        }


   


        if (document.getElementById('TextBox8').value == "" && document.getElementById('TextBox43').value=="") {

            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {
                alert(' الرجاء إدخال إسم المستفيد');
                document.getElementById('TextBox43').focus();
            }
            else
            {
                alert('Please enter Beneficiary Name');
                document.getElementById('TextBox8').focus();
            }
           
            return false;
        }



        if (document.getElementById('TextBox9').value == "" && document.getElementById('TextBox44').value=="") {
            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {
                alert(' الرجاء إدخال عنوان المستفيد');
                document.getElementById('TextBox44').focus();
            }
            else
            {
                alert('Please enter Beneficiary address field');
                document.getElementById('TextBox9').focus();
            }
           
            return false;
        }



        if (document.getElementById('TextBox15').value == "") {

            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {
                alert(' الرجاء إدخال رقم الفاكس للمستفيد');
            }
            else
            {
                alert('Please enter Beneficiary fax number');
            }
            document.getElementById('TextBox15').focus();
            return false;
        }



        if (document.getElementById('TextBox18').value == "" && document.getElementById('TextBox45').value=="") {
            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {
                alert(' الرجاء ادخال المبلغ بالارقام');
                document.getElementById('TextBox45').focus();
            }
            else
            {
                alert('Please enter Amount In figures');
                document.getElementById('TextBox18').focus();
            }
            
            return false;
        }



        if (document.getElementById('TextBox19').value == "" && document.getElementById('TextBox46').value=="") {

            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {
                alert(' الرجاء ادخال المبلغ بالاحرف');
                document.getElementById('TextBox46').focus();
            }
            else
            {
                alert('Please enter Amount In words ');
                document.getElementById('TextBox19').focus();
            }
          
            return false;
        }



        if (document.getElementById('TextBox20').value == "" && document.getElementById('TextBox21').value == "") {

            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {

                alert(' الرجاء ادخال النسبة المئوية لقيمة العقد');
                document.getElementById('TextBox21').focus();
                document.getElementById('TextBox21').focus();
            }
            else
            {
                alert('Please enter percentage of contract value');
                document.getElementById('TextBox20').focus();
            }
           
            return false;
        }





        if (document.getElementById('TextBox20').value > 100 ) {
            
            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {
                alert('   الرجاء ادخال النسبة المئوية لقيمة العقد بشكل صحيح');
            }
            else
            {
                alert('Please enter correct percentage of contract value');
            }
            document.getElementById('TextBox20').focus();
            return false;
        }




        if (document.getElementById('TextBox21').value > 100 ) {
            
            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {
                alert('   الرجاء ادخال النسبة المئوية لقيمة العقد بشكل صحيح');
            }
            else
            {
                alert('Please enter correct percentage of contract value');
            }
            document.getElementById('TextBox21').focus();
            return false;
        }




         

      

        if (document.getElementById('TextBox22').value == "" && document.getElementById('TextBox47').value=="") {
            
            if ( document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true )
            {
                alert(' الرجاء ادخال غرض الضمان ');
                document.getElementById('TextBox47').focus();
            }
            else
            {
                alert('Please enter Pourpose of Project');
                document.getElementById('TextBox22').focus();
            }
          
            return false;
        }


        if (document.getElementById('TextBox24').value == "" && document.getElementById('TextBox26').value == "") {

            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert(' الرجاء إدخال تاريخ إنتهاء الصلاحية ');
                document.getElementById('TextBox26').focus();
            }
            else {
                alert('Please enter validate until field');
                document.getElementById('TextBox24').focus();
            }

            return false;
        }

        if (! isValidDate(document.getElementById('TextBox26').value)) {

            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('  dd/mm/yyyy  الرجاء إدخال تاريخ إنتهاء الصلاحية ');
            }
            else {
                alert('Please enter validate until field with format dd/mm/yyyy');
            }
            document.getElementById('TextBox26').focus();
            return false;

        }


        if (!isValidDate(document.getElementById('TextBox25').value)) {

            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('  dd/mm/yyyy   الرجاء إدخال تاريخ الصلاحيه من ');
            }
            else {
                alert('Please enter validate until field with format dd/mm/yyyy');
            }
            document.getElementById('TextBox25').focus();
            return false;

        }

        if (!isValidDate(document.getElementById('TextBox23').value)) {

            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('  dd/mm/yyyy   الرجاء إدخال تاريخ الصلاحيه من ');
            }
            else {
                alert('Please enter validate from field with format dd/mm/yyyy');
            }
            document.getElementById('TextBox23').focus();
            return false;

        }

        if (!isValidDate(document.getElementById('TextBox24').value)) {

            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('  dd/mm/yyyy   الرجاء إدخال تاريخ الصلاحيه من ');
            }
            else {
                alert('Please enter validate until field with format dd/mm/yyyy');
            }
            document.getElementById('TextBox24').focus();
            return false;

        }


        if (document.getElementById('TextBox29').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox29').focus();
            return false;
        }

 
        if (document.getElementById('TextBox30').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox30').focus();
            return false;
        }


        if (document.getElementById('TextBox31').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox31').focus();
            return false;
        }

        if (document.getElementById('TextBox32').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox32').focus();
            return false;
        }

        if (document.getElementById('TextBox33').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox33').focus();
            return false;
        }

        if (document.getElementById('TextBox34').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox34').focus();
            return false;
        }

        if (document.getElementById('TextBox35').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox35').focus();
            return false;
        }

        if (document.getElementById('TextBox36').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox36').focus();
            return false;
        }

        if (document.getElementById('TextBox37').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox37').focus();
            return false;
        }

        if (document.getElementById('TextBox38').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox38').focus();
            return false;
        }

        if (document.getElementById('TextBox39').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox39').focus();
            return false;
        }

        if (document.getElementById('TextBox40').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox40').focus();
            return false;
        }

        if (document.getElementById('TextBox41').value == "") {
            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء ادخال رقم الحساب بشكل صحيح');
            }
            else {
                alert('Please enter correct account number');
            }
            document.getElementById('TextBox41').focus();
            return false;
        }



        if (document.getElementById('TextBox27').value == "") {

            if (document.getElementById('CheckBox3').checked == true || document.getElementById('CheckBox7').checked == true) {
                alert('الرجاء إدخال إسم طالب الإصدار');
            }
            else {
                alert('Please enter name of the customer');
            }

            document.getElementById('TextBox27').focus();
            return false;
        }

 

        // document.getElementById('prt').style.visibility = 'hidden';

      
        document.getElementById('mainPic').style.visibility = 'visible';
 
        printDiv('mainDiv');
        return true;

    }
      
</script>

<style type="text/css">
        
        @media print{
  body{ background-color:#FFFFFF; background-image:none; color:#000000 }
  #ad{ display:none;}
  #leftbar{ display:none;}
  #contentarea{ width:100%;}
}

    
         
       .darkrow 
     {
  
   border:1px solid black; 
   border-left-color:#ffffff;
   border-top-color:#ffffff;
   border-right-color:#ffffff;
  
 }

        
        
        .0d
       {
	 color: black; border-width:1px; 
        }
 

        
        .style1
        {
              
                      width: 100%;
           
        }
        
        
       
          

.txt_input
{        

 border-bottom-style:dotted;
 border-bottom-color:#E1E1E1;
 border-left-style:none;
 border-left-color:#ffffff;
 border-right-color:#ffffff;
 border-top-color:#ffffff;
 outline-offset: 0;
 background-color:transparent;
 border-bottom-style:dotted;

            border-left-style: solid;
            border-right-style: solid;
            border-top-style: solid;
        }

.lbl_input
{
    font-size:small;
    font-family:Tahoma;
            text-align: left;
            padding-left:1px;
            padding-right:1px;
            color="#3F3F3F";
             
           
        }
        
        
        .lbl_inputB
{
    font-size:small;
    font-family:Tahoma;
    font-weight:bolder; 
            text-align: left;
            padding-left:1px;
            padding-right:1px;
            color="#3F3F3F";
             
           
        }

        .style2
        {
            height: 30px;
            text-align: left;
        }

        .style3
        {
            height: 30px;
            width: 149px;
            text-align: left;
        }
        .style4
        {
            width: 149px;
            text-align: left;
        }

        .style5
        {
            width: 10px;
        }

        .style6
        {
           
            width: 100%;
            border: 1px solid #000000;
            border-collapse:collapse;
            border-color:#000000;
        
            
        }
        
         .style6  td, th
                {
                    border:1px solid black;
            text-align: center;
        } 
                
       .styleAppNo
        {
           
            width: 380px;
            border: 1px solid #000000;
            border-collapse:collapse;
            border-color:#000000;
        
            
        }
        
         .styleAppNo  td, th
                {
                    border:1px solid black;
                } 
                
                
        .styleBig
        {
           
            width: 380px;
            border: 1px solid #000000;
            border-collapse:collapse;
            border-color:#000000;
            .th 
            {border:1px solid black;}
            .td
            {border:1px solid black;} 
            
        }
        
        
                       
                
                
        </style>
</head>
<body style="margin-left=0;margin-top=0" >
    <form method="post" action="GuaranteeApps.aspx" id="form1">
<div class="aspNetHidden">
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwULLTE4MDgzMjEyMTcPZBYCAgMPZBYCAhEPDxYCHgRUZXh0BQsgRzE1UTM4Mzg5OWRkGAEFHl9fQ29udHJvbHNSZXF1aXJlUG9zdEJhY2tLZXlfXxYkBQlDaGVja0JveDEFCUNoZWNrQm94MgUJQ2hlY2tCb3gyBQlDaGVja0JveDYFCUNoZWNrQm94NgUJQ2hlY2tCb3g1BQlDaGVja0JveDMFCUNoZWNrQm94NAUJQ2hlY2tCb3g0BQlDaGVja0JveDcFCUNoZWNrQm94OAUJQ2hlY2tCb3g4BQlDaGVja0JveDkFCkNoZWNrQm94MTQFCkNoZWNrQm94MTAFCkNoZWNrQm94MTAFCkNoZWNrQm94MTUFCkNoZWNrQm94MTUFCkNoZWNrQm94MTEFCkNoZWNrQm94MTEFCkNoZWNrQm94MTYFCkNoZWNrQm94MTYFCkNoZWNrQm94MTIFCkNoZWNrQm94MTIFCkNoZWNrQm94MTcFCkNoZWNrQm94MTcFCkNoZWNrQm94MTMFCkNoZWNrQm94MTMFCkNoZWNrQm94MTgFCkNoZWNrQm94MTgFCkNoZWNrQm94MTkFCkNoZWNrQm94MjEFCkNoZWNrQm94MjAFCkNoZWNrQm94MjAFCkNoZWNrQm94MjIFCkNoZWNrQm94MjJmi+V4auvTf8HBszM6A/m9TG5WHSsvUZIipaw3I2E9hA==" />
</div>

<div class="aspNetHidden">

	<input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION" value="/wEdAEdBe44x+IZ6p46S/F7sL8/kESCFkFW/RuhzY1oLb/NUVHrJFO7tR/ln1WVEsF9j7rzuG/voEfy+7uIzi2Rvx6fVtXt6jCz1YfxYlnXsI0NdZ8N4rcplfaXiyPTvG+1ls5UbFgjMf8peAkxtoTxn5PIsUHrMvyHQIEVBJQH/5z+hV+28gY/MCPOQ9CcfLLX6ES39cfRwOptQV7pXQ3HjlPO8Y/eSSXooNGNzgMwKdZUrR1osXBLravDOIGFpXNkp6HtrF7gV+isKZD+YuU/zxdxzHadc/p2FmfqYq2ZMY0d3c41AodCUr7891sIvnWuWSKE1IKIVpPQ2NKsreyj8eVCruqPfnWPjLhGGtOfEKkH/VQ2SAovm8CtCMEJB6jivrmLz82P7qR1Xkm/inOg2weTmMBnQ33GeWWnGL9BVdUiPk1fWg1Gosr+sjKHWm4an7+d5CT7tcVcBFuAatiTkNzTkg8RCtHHRoQ7HLsnOhWQrupi0scUsaHiJ03c1Nukdz1dS1t+2VMc64miEaU+8B6+etFkthzWFWdRJPy8Ul6GtIGBCxjCEFCLc0Ue0cKgflxgZ1RhfQYZVD4u7CVlPPKYjFZgb/iaI6y4NsvFGB6DhaWgrZIHrZ6gCzYSxS8U/Ac9w2kNqu7ANQjGEMqTR3Fbl7QGv48/JY3XM/B4GobLsWfvT8T/Xwcsr6GhK7RorxgI2hnOPTDrr1MF+Pm5Np/PF97RNZlv36jLpfWYMySwnHe8PL2njdqV0To/mLefiJvqgUrmpqJ3woFManEPIpPlVLwMBgHohBQH9piYO8MIKeiwBZWc40kkLholgdBWG8TdooyYkRX9jDKkvfugmK+GfTKl2kpTASRNrm8io02OERFqhXbmlseSoO9xeJRo5eGUVa9/Ktx/evtVdVqqhzr7WY1i4KWmQmBD0TCynrXLSmiJz7Nfp3DDoJ70dDsZVMJ3nzYvTNI0GP0ijR21p7qbEA/UVUC5PmbZNMMsLnxVqIOEEYV7auT3jkaytrG9rK5Q7rerd4Nci8zZbnnNqM9W5qsy7Jaj/PvVCNqwCCg4Cyu6Pu201O6RcN2yNNdurDOS9HQJiWqPip4/0HJ4j3cX6Y/1m69WxYKg2XEmD2sOgAm/zfdY2/3c3fpRFPxfLPrBwhNaJa/HNFFVi1dnCFLSczalYSrUYT4zUHfZBu63J8s06XEAxA11rQRCj29MHxw2H/UhV0/5gQ4+s8iMrM9C2/7EpSI0HLi5QlUZVVldhL6zrBV4jf+51IH4+DIWu9T9qJeYaTknBnThfnFtm+xGNkIp/ruq3qwqtbzDUEq+qajzw4JMc4jo07islWIGx8TSYOqW/4Tm5Q3/XYJ7uqZpZwiKpuxlHMnQosQ9D6HrO3dWWiN455dGcCfq6N1zRusR2xub66Prd6cK+7Ne+zLQfE9YPgUvZxecskUiMRcF080glR6I9DzHUsMM2/BciqaGUf61nbrO5w1m5QE6YdrXHXXWfKItjcXUrgqvlKFDEWLltnVsIbYny0vT166oQeh9JnuOG9IAYWXNOKqlh57CD" />
</div>
    <div id="mainDiv">
    
        <table cellpadding="0" cellspacing="0"  style="width:774px"  align=center>
            <tr>
                <td>
                
                    <table class="style1">
                        <tr>
                            <td>
                                <table class="style1">
                                    <tr>
                                        <td style="text-align: center">
                                            <img src="img/logo.png" style="height: 61px"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                             <img src="img/title2.png" style="width: 900px; height: 30px"> </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td valign=top>
                                                        <table class="style1">
                                                            <tr>
                                                                <td>
                                                                    <span id="Label1" class="lbl_input">Branch</span>
                                                                    :<input name="TextBox1" type="text" id="TextBox1" class="txt_input" autocomplete="off" style="width:289px;text-align:center" />
                                                                    <span id="Label3" class="lbl_input">فرع </span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span id="Label2" class="lbl_input">Date:</span>
                                                                  
                                                                    <input name="datepick2" type="text" id="datepick2" class="txt_input" readonly="" style="text-align:center" value="28/12/2015" />
                                                                    <span id="Label4" class="lbl_input">التاريخ</span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign=top align=right >
                                                         <table class="styleAppNo"   cellpadding=0 cellspacing=0   >
                                                             <tr>
                                                                 <td class="style3">
                                                                     <span id="Label5" class="lbl_input"> Applicattion Ref. No.</span>
                                                                 </td>
                                                                 <td class="style2">
                                                                     <span id="Label6" class="lbl_input"> For Bank use only</span>
                                                                 </td>
                                                             </tr>
                                                             <tr>
                                                                 <td class="style4">
                                                                    &nbsp; <span id="Label8" class="lbl_input"> G15Q383899</span>
                                                                 </td>
                                                                 <td style="text-align: left">
                                                                     <span id="Label7" class="lbl_input"> L/G NO.</span>
                                                                    <input name="TextBox3" type="text" id="TextBox3" class="txt_input" autocomplete="off" style="width:150px;" />
                                                                 </td>
                                                             </tr>
                                                         </table>
                                                         </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                             <table cellpadding="0" cellspacing="0" width=100%  >
                                                 <tr>
                                                     <td>
                                                         &nbsp;&nbsp;</td>
                                                     <td style="text-align: right">
                                                         &nbsp;</td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         <span id="Label9" class="lbl_input">Kindly issue an Irrevocable Letter of Guarantee for our account </span>
                                                     </td>
                                                     <td style="text-align: right">
                                                         <span id="Label14" class="lbl_input">نرجو أن تصدروا لحسابنا وعلى كامل مسؤوليتنا خطاب ضمان غير قابل للنقض </span>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         <span id="Label10" class="lbl_input">and at our full responsibility by</span>
                                                         <span class="lbl_input"><input id="CheckBox1" type="radio" name="bymailEn" value="CheckBox1" checked="checked" onclick="setOption1(&#39;1&#39;);" /><label for="CheckBox1">Mail</label></span>
&nbsp;<span class="lbl_input"><input id="CheckBox2" type="radio" name="bymailEn" value="CheckBox2" onclick="setOption1(&#39;2&#39;);" /><label for="CheckBox2">Swift</label></span>
                                                     </td>
                                                     <td style="text-align: right">
                                                         <table class="style1">
                                                             <tr>
                                                                 <td>
                                                                     &nbsp;</td>
                                                                 <td>
                                                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                                     &nbsp;</td>
                                                                 <td>
                                                                     &nbsp;</td>
                                                                 <td>
                                                                     <span id="Label19" class="lbl_input" dir="rtl"> باللغة</span>
                                                                 </td>
                                                                 <td>
                                                                     <span class="lbl_input" dir="rtl"><input id="CheckBox6" type="radio" name="bymailAr" value="CheckBox6" onclick="setOption1(&#39;4&#39;);" /><label for="CheckBox6">السويفت</label></span>
                                                                 </td>
                                                                 <td>
                                                         <span class="lbl_input" dir="rtl"><input id="CheckBox5" type="radio" name="bymailAr" value="CheckBox5" checked="checked" onclick="setOption1(&#39;3&#39;);" /><label for="CheckBox5">البريد</label></span>
                                                                 </td>
                                                                 <td>
                                                         <span id="Label18" class="lbl_input" dir="rtl">وغير مشروط بواسطة</span>
                                                                 </td>
                                                             </tr>
                                                         </table>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         <span id="Label11" class="lbl_input">in</span>
                                                         &nbsp;<span class="lbl_input"><input id="CheckBox3" type="radio" name="bylangEn" value="CheckBox3" checked="checked" onclick="setOption1(&#39;5&#39;);" /><label for="CheckBox3">Arabic</label></span>
&nbsp;<span class="lbl_input"><input id="CheckBox4" type="radio" name="bylangEn" value="CheckBox4" onclick="setOption1(&#39;6&#39;);" /><label for="CheckBox4">English</label></span>
&nbsp;<span id="Label12" class="lbl_input">Language  as details given below </span>
                                                         </td>
                                                     <td style="text-align: right">
                                                         <span class="lbl_input" dir="rtl"><input id="CheckBox7" type="radio" name="bylangAR" value="CheckBox7" checked="checked" onclick="setOption1(&#39;7&#39;);" /><label for="CheckBox7">العربية</label></span>
                                                         <span class="lbl_input" dir="rtl"><input id="CheckBox8" type="radio" name="bylangAR" value="CheckBox8" onclick="setOption1(&#39;8&#39;);" /><label for="CheckBox8">الانجليزية</label></span>
                                                         &nbsp;<span id="Label16" class="lbl_input" dir="rtl"> وحسب التفاصيل الموضحة أدناه</span>
                                                     </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                         <span id="Label13" class="lbl_input">(Instructions marked (X) to be followed):</span>
                                                         </td>
                                                     <td style="text-align: right">
                                                         <span id="Label17" class="lbl_input" dir="rtl">ضع علامة (x) على المربع المطلوب:</span>
                                                         </td>
                                                 </tr>
                                             </table>
                                             </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="border:1px solid black;"   cellpadding=0  cellspacing=0   >
                                    <tr>
                                        <td class=darkrow  >
                                             <table cellpadding="0" cellspacing="0" class="style1">
                                                 
                                                 <tr >
                                                     <td>
                                                                    <span id="Label21" class="lbl_input">On behalf of: </span>
                                                                    </td>
                                                     <td >
                                                     <table border=0 width="100%"><tr><td>
                                                        <input name="TextBox4" type="text" maxlength="90" id="TextBox4" autocomplete="off" onkeypress="ChangeSize(this);" onkeyup="ChangeSize(this);" class="txt_input" style="width:5px;text-align:center" />

                                                                    </td>

                                                        <td  dir=rtl>
                                                         <input name="TextBox2" type="text" maxlength="120" id="TextBox2" autocomplete="off" onkeypress="ChangeSizeAr(this);" onkeyup="ChangeSizeAr(this);" class="txt_input" style="width:10px;text-align:center" />
                                                                    </td>
                                                                    </tr></table>


                                                     <td align="right">
                                                                    <span id="Label22" class="lbl_input" dir="rtl">بالنيابة عن:</span>
                                                                </td>
                                                 </tr>
                                                 <tr >
                                                     <td >
                                                                    <span id="Label23" class="lbl_input">Address: </span>
                                                                    </td>
                                                     <td >
                                                      <table border=0 width="100%"><tr><td>
                                                         <input name="TextBox5" type="text" maxlength="90" id="TextBox5" autocomplete="off" onkeypress="ChangeSize(this);" onkeyup="ChangeSize(this);" class="txt_input" style="width:5px;text-align:center" />
                                                                    </td>

                                                                    <td dir=rtl>
                                                         <input name="TextBox42" type="text" maxlength="120" id="TextBox42" autocomplete="off" onkeypress="ChangeSizeAr(this);" onkeyup="ChangeSizeAr(this);" class="txt_input" style="width:10px;text-align:center" />
                                                                    </td></tr></table>


                                                     <td  dir=rtl>
                                                                    <span id="Label24" class="lbl_input" dir="rtl">العنوان : </span>
                                                                </td>
                                                 </tr>
                                                 <tr>
                                                     <td>
                                                                    <span id="Label25" class="lbl_input">Telephone :</span>
                                                                    </td>
                                                     <td>
                                                         <table class="style1">
                                                             <tr>
                                                                 <td align="left">
                                                                     <input name="TextBox6" type="text" maxlength="20" id="TextBox6" class="txt_input" autocomplete="off" style="width:249px;text-align:center" />
                                                                    <span id="Label27" class="lbl_input">رقم الهاتف</span>
                                                                 </td>
                                                                 <td>
                                                                     &nbsp;</td>
                                                                 <td align="right">
                                                                     &nbsp;</td>
                                                                 <td align="right">
                                                                    <span id="Label28" class="lbl_input">Fax</span>
                                                                &nbsp;<input name="TextBox7" type="text" maxlength="20" id="TextBox7" class="txt_input" autocomplete="off" style="width:247px;text-align:center" />
                                                                    </td>
                                                             </tr>
                                                         </table>
                                                     </td>
                                                     <td style="direction: rtl">
                                                                    <span id="Label26" class="lbl_input" dir="rtl">رقم الفاكس</span>
                                                                </td>
                                                 </tr>
                                             </table>
                                             </td>
                                    </tr>
                                    <tr>
                                        <td  class=darkrow  >
                                            <table class="style1">
                                                <tr  >
                                                    <td >
                                                                    <span id="Label29" class="lbl_input">Beneficiary name:</span>
                                                                    </td>
                                                    <td width="100%">
                                                      <table border=0 width="100%"><tr><td>
                                                         <input name="TextBox8" type="text" maxlength="90" id="TextBox8" autocomplete="off" onkeypress="ChangeSize(this);" onkeyup="ChangeSize(this);" class="txt_input" style="width:5px;text-align:center" />
                                                                    </td>

                                                                    <td dir=rtl>
                                                         <input name="TextBox43" type="text" maxlength="120" id="TextBox43" autocomplete="off" onkeypress="ChangeSizeAr(this);" onkeyup="ChangeSizeAr(this);" class="txt_input" style="width:10px;text-align:center" />
                                                                    </td></tr></table>


                                                    <td align="right">
                                                                    <span id="Label33" class="lbl_input" dir="rtl">إسم المستفيد</span>
                                                                </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table class="style1">
                                                            <tr>
                                                                <td>
                                                                    <span id="Label34" class="lbl_input">Address:</span>
                                                                    </td>
                                                                <td width="100%">
                                                              <table border=0 width="100%"><tr><td>    
                                                         <input name="TextBox9" type="text" maxlength="90" id="TextBox9" autocomplete="off" onkeypress="ChangeSize(this);" onkeyup="ChangeSize(this);" class="txt_input" style="width:5px;text-align:center" />
                                                                    </td>

                                                                    <td dir=rtl>    
                                                         <input name="TextBox44" type="text" maxlength="120" id="TextBox44" autocomplete="off" onkeypress="ChangeSizeAr(this);" onkeyup="ChangeSizeAr(this);" class="txt_input" style="width:10px;text-align:center" />
                                                                    </td></tr></table>


                                                                <td  style="direction: rtl">
                                                                    <span id="Label35" class="lbl_input">العنوان:</span>
                                                                    </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table class="style1">
                                                            <tr>
                                                                <td>
                                                                    <span id="Label36" class="lbl_input">P.O. BOX :</span>
                                                                    </td>
                                                                <td>
                                                                    <input name="TextBox10" type="text" maxlength="15" id="TextBox10" class="txt_input" autocomplete="off" style="width:130px;text-align:center" />
                                                                    </td>
                                                                <td>
                                                                    <span id="Label37" class="lbl_input">Postal Code :</span>
                                                                    </td>
                                                                <td>
                                                                    <input name="TextBox13" type="text" maxlength="10" id="TextBox13" class="txt_input" autocomplete="off" style="width:120px;text-align:center" />
                                                                    </td>
                                                                <td class="style5">
                                                                    </td>
                                                                <td align="right">
                                                                    <input name="TextBox12" type="text" maxlength="10" id="TextBox12" class="txt_input" autocomplete="off" style="width:120px;text-align:center" />
                                                                    </td>
                                                                <td align="right">
                                                                    <span id="Label39" class="lbl_input" dir="rtl">الرمز البريدي :</span>
                                                                    </td>
                                                                <td align="right">
                                                                    <input name="TextBox11" type="text" maxlength="15" id="TextBox11" class="txt_input" autocomplete="off" style="width:130px;text-align:center" />
                                                                    </td>
                                                                <td dir=rtl">
                                                                    <span id="Label38" class="lbl_input" dir="rtl">صندوق البريد:</span>
                                                                    </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                                    <table class="style1">
                                                                        <tr>
                                                                            <td>
                                                                    <span id="Label40" class="lbl_input">Telephone :</span>
                                                                            </td>
                                                                            <td>
                                                                     <input name="TextBox14" type="text" maxlength="20" id="TextBox14" class="txt_input" autocomplete="off" style="width:249px;text-align:center" />
                                                                    <span id="Label41" class="lbl_input">رقم الهاتف</span>
                                                                            </td>
                                                                            <td>
                                                                                </td>
                                                                            <td align="right">
                                                                    <span id="Label43" class="lbl_input">Fax</span>
                                                                                <input name="TextBox15" type="text" maxlength="20" id="TextBox15" class="txt_input" autocomplete="off" style="width:247px;text-align:center" />
                                                                            </td>
                                                                            <td align="right">
                                                                    <span id="Label42" class="lbl_input" dir="rtl">رقم الفاكس</span>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    </td>
                                                </tr>
                                            </table>
                                           </td>
                                    </tr>
                                    <tr>
                                        <td  class=darkrow >
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                 
                                                <tr>
                                                    <td>
                                                        &nbsp;<span id="Label44" style="font-weight: 700; text-decoration: underline; font-size: large;">  Type of Guarantee</span>
                                                    </td>
                                                    <td style="text-align: right" dir=rtl>
                                                        &nbsp;<span id="Label45" style="font-weight: 700; text-decoration: underline; font-size: large;">نوع الضمان:  </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="lbl_input"><input id="CheckBox9" type="radio" name="InshuranceEn" value="CheckBox9" checked="checked" onclick="setOption1(&#39;9&#39;);" /><label for="CheckBox9">Preliminary Guarantee/Bid Bond</label></span>
                                                    </td>
                                                    <td dir=rtl>
                                                        <span class="lbl_input"><input id="CheckBox14" type="radio" name="InshuranceAR" value="CheckBox14" checked="checked" onclick="setOption1(&#39;10&#39;);" /><label for="CheckBox14">التأمين المؤقت / ابتدائي</label></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="lbl_input"><input id="CheckBox10" type="radio" name="InshuranceEn" value="CheckBox10" onclick="setOption1(&#39;11&#39;);" /><label for="CheckBox10">Final Deposit Guarantee/Performance Bond</label></span>
                                                    </td>
                                                    <td dir=rtl>
                                                        <span class="lbl_input"><input id="CheckBox15" type="radio" name="InshuranceAR" value="CheckBox15" onclick="setOption1(&#39;12&#39;);" /><label for="CheckBox15">التأمين النهائي / حسن تنفيذ</label></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="lbl_input"><input id="CheckBox11" type="radio" name="InshuranceEn" value="CheckBox11" onclick="setOption1(&#39;13&#39;);" /><label for="CheckBox11">Paymnet Guarantee</label></span>
                                                    </td>
                                                    <td dir=rtl>
                                                        <span class="lbl_input"><input id="CheckBox16" type="radio" name="InshuranceAR" value="CheckBox16" onclick="setOption1(&#39;14&#39;);" /><label for="CheckBox16">مالي</label></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="lbl_input"><input id="CheckBox12" type="radio" name="InshuranceEn" value="CheckBox12" onclick="setOption1(&#39;15&#39;);" /><label for="CheckBox12">Advance Payment Guarantee</label></span>
                                                    </td>
                                                    <td dir=rtl>
                                                        <span class="lbl_input"><input id="CheckBox17" type="radio" name="InshuranceAR" value="CheckBox17" onclick="setOption1(&#39;16&#39;);" /><label for="CheckBox17">دفعة مقدمه</label></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="lbl_input"><input id="CheckBox13" type="radio" name="InshuranceEn" value="CheckBox13" onclick="setOption1(&#39;17&#39;);" /><label for="CheckBox13">Other (PLease Specify)</label></span>
                                                    </td>
                                                    <td dir=rtl>
                                                        <span class="lbl_input"><input id="CheckBox18" type="radio" name="InshuranceAR" value="CheckBox18" onclick="setOption1(&#39;18&#39;);" /><label for="CheckBox18">أنواع أخرى</label></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                                                <input name="TextBox16" type="text" maxlength="45" id="TextBox16" class="txt_input" autocomplete="off" onkeypress="setOption1(&#39;35&#39;);" style="width:380px;" />
                                                                            </td>
                                                    <td style="direction: rtl">
                                                                                <input name="TextBox17" type="text" maxlength="45" id="TextBox17" class="txt_input" autocomplete="off" onkeypress="setOption1(&#39;35&#39;);" style="width:332px;" />
                                                                            </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="direction: ltr">
                                            <table class="style1">
                                                <tr>
                                                    <td>
                                                        <span id="Label46" class="lbl_input">Amount in figures:</span>
                                                    </td>
                                                    <td >
                                                     <table border=0 width="100%"><tr><td>   
                                                         <input name="TextBox18" type="text" maxlength="25" id="TextBox18" class="txt_input" autocomplete="off" style="width:320px;" />
                                                                    </td>
                                                                    <td dir=rtl>   
                                                         <input name="TextBox45" type="text" maxlength="25" id="TextBox45" class="txt_input" autocomplete="off" style="width:320px;" />
                                                                    </td></tr></table>

                                                    <td dir=rtl>
                                                        <span id="Label49" class="lbl_input">المبلغ بالأرقام:</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span id="Label47" class="lbl_input">In words:</span>
                                                    </td>
                                                    <td >
                                                    <table border=0 width="100%"><tr><td>   
                                                         <input name="TextBox19" type="text" maxlength="90" id="TextBox19" autocomplete="off" onkeypress="ChangeSize(this);" onkeyup="ChangeSize(this);" class="txt_input" style="width:5px;text-align:center" />
                                                                    </td>
                                                                    <td dir=rtl>   
                                                         <input name="TextBox46" type="text" maxlength="150" id="TextBox46" autocomplete="off" onkeypress="ChangeSizeAr(this);" onkeyup="ChangeSizeAr(this);" class="txt_input" style="width:10px;text-align:center" />
                                                                    </td></tr></table>


                                                    <td dir=rtl>
                                                        <span id="Label50" class="lbl_input">بالحروف:</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <table cellpadding="0" cellspacing="0" class="style1">
                                                            <tr>
                                                                <td>
                                                        <span id="Label51" class="lbl_input"> Being</span>
                                                                </td>
                                                                <td>
                                                                                <input name="TextBox20" type="text" maxlength="3" id="TextBox20" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="setOption1(&#39;30&#39;)" style="width:212px;text-align:center" />
                                                                            <span id="Label52" class="lbl_input">% of contract value.</span>
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td dir=RTL>
                                                                                <input name="TextBox21" type="text" maxlength="3" id="TextBox21" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="setOption1(&#39;31&#39;)" style="width:212px;text-align:center" />
                                                                            <span id="Label54" class="lbl_input">% من قيمة العقد.</span>
                                                                     </td>
                                                                <td dir=rtl>
                                                                    <span id="Label53" class="lbl_input">يمثل</span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class=darkrow >
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td>
                                                        <span id="Label55" class="lbl_input">Purpose (Project) :</span>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td dir=rtl>
                                                        <span id="Label56" class="lbl_input">غرض الضمان (اسم المشروع)</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align=center>
                                                     <table border=0 width="100%"><tr><td>   
                                                         <textarea name="TextBox22" rows="3" cols="20" id="TextBox22" class="txt_input" autocomplete="off" onkeypress="return imposeMaxLength(event, this, 150);" style="width:420px;OVERFLOW:hidden">
</textarea>
                                                                    </td>
                                                                    <td dir=rtl>   
                                                         <textarea name="TextBox47" rows="3" cols="20" id="TextBox47" class="txt_input" autocomplete="off" onkeypress="return imposeMaxLength(event, this, 150);" style="width:420px;OVERFLOW:hidden">
</textarea>
                                                                    </td></tr>
                                                                    
                                                                   
                                                                    
                                                                    </table>


                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class=darkrow >
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td>
                                                        <span id="Label57" class="lbl_input">Valid from:</span>
                                                    </td>
                                                    <td>
                                                                                <input name="TextBox23" type="text" maxlength="20" id="TextBox23" class="txt_input" autocomplete="off" style="width:160px;text-align:center" />
                                                                            </td>
                                                    <td>
                                                        <span id="Label58" class="lbl_input">Until:</span>
                                                    </td>
                                                    <td>
                                                                                <input name="TextBox24" type="text" maxlength="20" id="TextBox24" class="txt_input" autocomplete="off" style="width:160px;text-align:center" />
                                                                            </td>
                                                    <td>
                                                       &nbsp;</td>
                                                    <td style="direction: rtl">
                                                                                <input name="TextBox26" type="text" maxlength="20" id="TextBox26" class="txt_input" autocomplete="off" style="width:160px;text-align:center" />
                                                                            </td>
                                                    <td style="direction: rtl">
                                                         <span id="Label60" class="lbl_input">لغاية:</span>
                                                         </td>
                                                    <td dir=rtl>
                                                                                <input name="TextBox25" type="text" maxlength="20" id="TextBox25" class="txt_input" autocomplete="off" style="width:160px;text-align:center" />
                                                         </td>
                                                    <td dir=rtl>
                                                         <span id="Label59" class="lbl_input">الصلاحية من:</span>
                                                         </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class=darkrow >
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td>
                                                        <span id="Label61" style="font-weight: 700; font-size: large;">Text of guarantee to be issued</span>
                                                    </td>
                                                    <td dir=rtl>
                                                        <span id="Label62" style="font-weight: 700; font-size: large;">نص الضمان المطلوب إصداره</span>
                                                         </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="lbl_input"><input id="CheckBox19" type="radio" name="byTextEn" value="CheckBox19" checked="checked" onclick="setOption1(&#39;19&#39;);" /><label for="CheckBox19">As per Saudi Arabian Monetary Agency's approved text</label></span>
                                                    </td>
                                                    <td dir=rtl>
                                                        <span class="lbl_input"><input id="CheckBox21" type="radio" name="byTextAr" value="CheckBox21" checked="checked" onclick="setOption1(&#39;20&#39;);" /><label for="CheckBox21">حسب النص المعتمد لمؤسسة النقد العربي السعودي</label></span>
                                                         </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="lbl_input"><input id="CheckBox20" type="radio" name="byTextEn" value="CheckBox20" onclick="setOption1(&#39;21&#39;);" /><label for="CheckBox20">As per attached text (Subject to your approval)</label></span>
                                                    </td>
                                                    <td dir=rtl>
                                                        <span class="lbl_input"><input id="CheckBox22" type="radio" name="byTextAr" value="CheckBox22" onclick="setOption1(&#39;22&#39;);" /><label for="CheckBox22">حسب النص المرفق (خاضع لموافقتكم)</label></span>
                                                         </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="direction: ltr"  class=darkrow >
                                            <table class="style1">
                                                <tr>
                                                    <td>
                                                        <span id="Label63" class="lbl_inputB">We authorize you to debit our A/C with you for margin, </span>
                                                        <span id="Label64" class="lbl_inputB">commissions, swift, courier service, or any other expenses </span>
                                                        <span id="Label65" class="lbl_inputB">incurred by you under this L/G without referring to us.</span>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;</td>
                                                    <td style="text-align: right" dir=rtl>
                                                         <span id="Label68" class="lbl_inputB">نفوضكم بالقيد على حسابنا لديكم قيمة التأمين النقدي والعمولات وأجور البريد</span>
                                                          <span id="Label69" class="lbl_inputB">وأجور السويفت وأي مصاريف تنشأ عن قيامكم بإصدار الضمان أعلاه وبدون </span>
                                                          <span id="Label70" class="lbl_inputB">الرجوع الينا.</span>
                                                         </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span id="Label66" class="lbl_inputB">We declare that we have read, understood, and agree with </span>
                                                        <span id="Label67" class="lbl_inputB">the  general Terms & Conditions related to the issuance of the L/G. </span>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td dir=rtl>
                                                          <span id="Label71" class="lbl_inputB">نقر بأننا قرأنا ووافقنا على جميع الشروط العامة الواردة على ظهر هذا النموذج </span>
                                                          <span id="Label72" class="lbl_inputB">والخاصة بإصدار الضمان. </span>
                                                         </td>
                                                </tr>
                                                </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td>
                                                        <span id="Label73" class="lbl_input">Account number :</span>
                                                    </td>
                                                    <td>
                                                        <table class="style1">
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" class="style6" >
                                                                        <tr>
                                                                            <td>
                                                                                <input name="TextBox29" type="text" maxlength="1" id="TextBox29" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox29&#39;,&#39;TextBox30&#39;);" style="width:30px;" />
                                                                            </td>
                                                                            <td>
                                                                                <input name="TextBox30" type="text" maxlength="1" id="TextBox30" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox30&#39;,&#39;TextBox31&#39;);" style="width:30px;" />
                                                                            </td>
                                                                            <td>
                                                                                <input name="TextBox31" type="text" maxlength="1" id="TextBox31" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox31&#39;,&#39;TextBox32&#39;);" style="width:30px;" />
                                                                            </td>
                                                                            <td>
                                                                                <input name="TextBox32" type="text" maxlength="1" id="TextBox32" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox32&#39;,&#39;TextBox33&#39;);" style="width:30px;" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <table cellpadding="0" class="style6">
                                                                        <tr>
                                                                            <td>
                                                                                <input name="TextBox33" type="text" maxlength="1" id="TextBox33" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox33&#39;,&#39;TextBox34&#39;);" style="width:30px;" />
                                                                            </td>
                                                                            <td>
                                                                                <input name="TextBox34" type="text" maxlength="1" id="TextBox34" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox34&#39;,&#39;TextBox35&#39;);" style="width:30px;" />
                                                                            </td>
                                                                            <td>
                                                                                <input name="TextBox35" type="text" maxlength="1" id="TextBox35" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox35&#39;,&#39;TextBox36&#39;);" style="width:30px;" />
                                                                            </td>
                                                                            <td>
                                                                                <input name="TextBox36" type="text" maxlength="1" id="TextBox36" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox36&#39;,&#39;TextBox38&#39;);" style="width:30px;" />
                                                                            </td>
                                                                            <td>
                                                                                <input name="TextBox38" type="text" maxlength="1" id="TextBox38" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox38&#39;,&#39;TextBox37&#39;);" style="width:30px;" />
                                                                            </td>
                                                                            <td>
                                                                                <input name="TextBox37" type="text" maxlength="1" id="TextBox37" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox37&#39;,&#39;TextBox39&#39;);" style="width:30px;" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <table cellspacing="0" class="style6">
                                                                        <tr>
                                                                            <td>
                                                                                <input name="TextBox39" type="text" maxlength="1" id="TextBox39" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox39&#39;,&#39;TextBox40&#39;);" style="width:30px;" />
                                                                            </td>
                                                                            <td>
                                                                                <input name="TextBox40" type="text" maxlength="1" id="TextBox40" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" onkeyup="autoTab(&#39;TextBox40&#39;,&#39;TextBox41&#39;);" style="width:30px;" />
                                                                            </td>
                                                                            <td>
                                                                                <input name="TextBox41" type="text" maxlength="1" id="TextBox41" class="txt_input" autocomplete="off" onkeypress="return numbersonly(event);" style="width:30px;" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td dir=rtl>
                                                          <span id="Label76" class="lbl_input">رقم الحساب:</span>
                                                         </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span id="Label74" class="lbl_input">Name of the customer :</span>
                                                    </td>
                                                    <td style="text-align: center">
                                                         <input name="TextBox27" type="text" maxlength="60" id="TextBox27" class="txt_input" autocomplete="off" style="width:450px;text-align:center" />
                                                                    </td>
                                                    <td dir=rtl>
                                                          <span id="Label77" class="lbl_input">إسم طالب الإصدار:</span>
                                                         </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span id="Label75" class="lbl_input" style="display:inline-block;width:180px;">Signature(s) & official Stamp</span>
                                                    </td>
                                                    <td style="text-align: center">
                                                         <input name="TextBox28" type="text" maxlength="60" id="TextBox28" class="txt_input" autocomplete="off" style="width:450px;text-align:center" />
                                                                    </td>
                                                    <td dir=rtl>
                                                          <span id="Label78" class="lbl_input" style="display:inline-block;width:190px;">التوقيع وختم الشركة/ المؤسسة:</span>
                                                         </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;&nbsp;</td>
                                                    <td style="text-align: center">
                                                      
                                                    </td>
                                                    <td dir=rtl>
                                                          &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                    </table>
                
                </td>
            </tr>
        </table>
    
    </div>
    <center>

      <div id="mainPic" style="display:none;"><center><img src="images/Guarantee.jpg" width="920px"    /></center></div>

    <input type=button value="  Print  " style="width: 154px"  onclick="printDiv('mainDiv');"  >
</center>
    </form>
</body>
</html>
