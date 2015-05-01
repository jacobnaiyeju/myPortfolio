//sign in functions
function GetSignInDetails(){
	var UserName=$('#signInUserName').val();
	var Password=$('#signInPassword').val();
	var details={userName:UserName,password:Password};
	return details;
}
function resultSignInAuthentication(authenticate){
	if(authenticate.valid==true)
	{
		handleSuccessSignIn();
		localStorage['userName']=authenticate.patronId;
	}
	else
	{
		handleFailedSignIn();
	}
}
function handleSuccessSignIn(){
	var addNavTags='<h2>LRC Mobile Application</h2>'
    +'<div class="navbars" data-role="navbar" data-iconpos="left">'
    +'<a href="#home" data-theme="a" data-icon="home" rel="external">Home</a>'
    +'<a href="#notifications" data-theme="a" data-icon="grid" rel="external">Notifications</a>'
    +'<a href="#account" data-theme="a" data-icon="gear" rel="external">Account Details</a>'
    +'</div>'
    +'<hr></hr>'
	+'<div id="pageContentNav" style="float:right;">'
	+'<a data-theme="a" data-role="button" onclick = handleSignOut() >Sign-Out<a>'
	+'</div>';
	
	$("#holddetailsHeader").html(addNavTags).trigger('create');
	$("#accountHeader").html(addNavTags).trigger('create');
	$("#notificationsHeader").html(addNavTags).trigger('create');
	$('#pageSearchHeader').html(addNavTags).trigger('create');
	$('#pageContentHeader').html(addNavTags).trigger('create');
	$('#homeHeader').html(addNavTags).trigger('create');
	$('#holdsHeader').html(addNavTags).trigger('create');
	$('#contactHeader').html(addNavTags).trigger('create');
	$('#articlesHeader').html(addNavTags).trigger('create');
	$('#home').trigger('pagecreate');
	createNotifications();
	createRecallNotifications();
}
function handleFailedSignIn(){
	alert('failed Login');
	$('#signInUserName').val("");
	$('#signInPassword').val("");
}
//end sign in functions

function handleSignOut(){
	var addNavbarHome='<div id="pageContentNav" style="float:right;">'
	+'<a data-role="button" href="#signInPage" data-theme="a" rel="external">Sign-In</a>'
	+'</div>';
	var addNavTags='<div id="pageContentNav" style="float:right;" >'
	+'<button data-theme="a" id="btnGoSignIn" onclick = handleGoSignIn() >Sign-In</a>'
	+'</div>';
	
	$("#holddetailsHeader").html(addNavTags).trigger('create');
	$("#accountHeader").html(addNavTags).trigger('create');
	$("#notificationsHeader").html(addNavTags).trigger('create');
	$('#pageSearchHeader').html(addNavTags).trigger('create');
	$('#pageContentHeader').html(addNavTags).trigger('create');
	$('#homeHeader').html(addNavbarHome).trigger('create');
	$('#holdsHeader').html(addNavTags).trigger('create');
	$('#contactHeader').html(addNavTags).trigger('create');
	$('#articlesHeader').html(addNavTags).trigger('create');
	refreshPage();
	
	//commented out breaks a lot of other stuff
	//localStorage['userName']="";
	
}
function refreshPage(){
	$.mobile.changePage(
		window.location.href,{
			allowSamePageTransition:true,
			transition:'none',
			showLoadMsg:false,
			reloadPage:true
		}
	);
	document.location.href='index.html';
}
function handleGoSignIn()
{
	$.mobile.changepage('#signInPage');
	refreshPage();
}
function createHoldsListView(data,id){
	strHtml='<ul data-role="listview"  data-inset="true" id="'+id+'" >';
	for(var i=0;i<data.length;i++)
	{
		
	date = moment(data[i].Date);
	date = JSON.stringify(date);
	date = date.substr(1,10);
		strHtml+='<li data-theme="a" id="'+id+JSON.stringify(data[i].ItemAccessionNumber)+'"><a data-theme="a" href="#holddetails" data-role="button" onclick = viewHold('+Number(data[i].ItemAccessionNumber)+') ><h3>Hold: Accession Number'+Number(data[i].ItemAccessionNumber)+'</h3>'
		+'<p>'+ date +'</p>'
		+'</a></li>';
	}
	strHtml+="</ul>";
	
	return strHtml;
}
function createLoansListView(data,id){
		
		var strHtml="";
	for(var i=0;i<data.length;i++)
	{
		if (data[i] != undefined)
		{
			//alert(i +typeof data[i]);
            	
			date = moment(data[i].DueDate);
			//alert(i + typeof date);
			date = JSON.stringify(date);
			date = date.substr(1,10);
		
		strHtml+='<li data-theme="a" id="'+id+JSON.stringify(data[i].ItemAccessionNumber)+'"><a data-theme="a" href="#holddetails" data-role="button" onclick = viewHold('+Number(data[i].ItemAccessionNumber)+') ><h3>Loan: Accession Number'+Number(data[i].ItemAccessionNumber)+'</h3>'
			+'<p>'+ date +'</p>'
			+'</a></li>';
		
		}
			
	}
	
	
	return strHtml;
}
function createLoansListView2(data,id){
		var strHtml='';
	for(var i=0;i<data.length;i++)
	{
			date = moment(data[i].DueDate);
			//alert(i + typeof date);
			date = JSON.stringify(date);
			date = date.substr(1,10);
		
		strHtml+='<li data-theme="d" id="'+id+JSON.stringify(data[i].ItemAccessionNumber)+'"><a data-theme="d" href="#holddetails" data-role="button" onclick = viewHold('+Number(data[i].ItemAccessionNumber)+') ><h3 style="color:red">Loan: Accession Number'+Number(data[i].ItemAccessionNumber)+'</h3>'
			+'<p style="color:red;">'+ date +'</p>'
			+'</a></li>';
		
	}
	
	
	return strHtml;
}

function viewHold(value){
	getItemType(value);
}

function createListViewHoldsDesc(data){
	strHtml='<H3>Title</H3>'
	+'<H3>'+JSON.stringify(data.Title)+'</H3><hr></hr><br />'
	+'<H3>ItemType</H3>'
	+'<H3>'+JSON.stringify(data.ItemType)+'</H3><hr></hr><br />'
	+'<H3>Status</H3>'
	+'<H3>'+JSON.stringify(data.Status)+'</H3><hr></hr><br />'
	+'<br />';

	return strHtml;
}
function clearfield(){
	$('#searchinput1').val("");
}

