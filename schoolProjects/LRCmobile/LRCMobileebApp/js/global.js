/*
Title: global.js
Purpose: rules and error messages for site/mobile app
Author: AJA
Last updated: 2014-04-09
*/


$(document).ready(function(){
	//localStorage['userName']="";
	//mobilePlaceHolds();
	$("#btnSignIn").tap(function(){
		handleSignIn();
	});
	$('#signOut').tap(function(){
		handleSignOut();
		$.mobile.changepage('#signInPage');
		location.reload(true);
		return false;
	});
	$("#holds").on('pageshow',function(){
		$("#LoansNotify").trigger('create').listview('refresh');
	});
	$("#btnback").tap(function(){
		$('#home').trigger('pagecreate');
	});
	getStructureSize();
	//getDueDates();
	//getHolds();
	$("#listForIcons").listview('refresh');
	$('#btnSearchStr').on('tap',function(){
		$('#searchResults').html("");
		var search=$('#searchinput1').val();
		if($('#searchBooks').is(':checked')){
			searchBooks(search);
			
		}
		else
		{
			searchPeriodicals(search);
		}
		$('ul').listview('create').listview('refresh');
		return false;
	});
	}
		
);



