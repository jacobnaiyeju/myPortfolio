var hostaddress="http://169.254.108.141/LRCMobileService/";
//hostaddress= "http://localhost/LRCMobileService/"

function storePatronID(){ 
//this function needs to store the patronID in localstorage when the user logs into the application 
    var patronID = localStorage.userName; 
    //alert(patronID);
    return patronID; 
} 

function getBookDetails(accessionNumber) {
	var title="";
	var status="";
	var itemtype="";
	$.ajax({
		type: "POST", //GET or POST
		url: hostaddress + "LRCMobileService.svc/Web/findBook?accessionNumber=" + accessionNumber, // Location of the service 
		processdata: false, 
        contentType: "application/json; charset=utf-8", // content type sent to server
        crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing?
        dataType: "json", //Expected data format from server
		success: function (data) {
			title = (JSON.stringify(data[i].Title));
			status = (JSON.stringify(data[i].Status));
			itemtype = (JSON.stringify(data[i].ItemType));
			/*alert("bookfcn");
			alert(title);
			alert(status);
			alert(itemtype);*/
			return {Title:title,Status:status,ItemType:itemtype};
		},
		error: function (response) {
			alert('Failed: ' + response.statusText);
			return {Title:title,Status:status,ItemType:itemtype};
		}
	});
}

function getPerDetails(accessionNumber) {
	var title="";
	var status="";
	var itemtype="";
	$.ajax({
		type: "POST", //GET or POST
		url: hostaddress + "LRCMobileService.svc/Web/findPeriodical/?accessionNumber=" + accessionNumber, // Location of the service 
		processdata: false,
		contentType: "application/json; charset=utf-8", //content type sent to server
		dataType: "json",
		success: function (data) {
			title = (JSON.stringify(data[i].Title));
			status = (JSON.stringify(data[i].Status));
			itemtype = (JSON.stringify(data[i].ItemType));
			/*alert(title);
			alert(status);
			alert(itemtype);
			alert("kfcn");*/
			return {Title:title,Status:status,ItemType:itemtype};
		},
		error: function (response) {
			alert('Failed: ' + response.statusText);
			return {Title:title,Status:status,ItemType:itemtype};
		}
	});
}

function getItemType(accessionNumber) {
	var accession = (accessionNumber);
	//alert("item" + typeof accession); 
	$.ajax({
		type: "POST", //GET or POST
		url: hostaddress + 'LRCMobileService.svc/Web/findItem/?id=' + accession, // Location of the service 
		processdata: false, 
        contentType: "application/json; charset=utf-8", // content type sent to server
        crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing?
        dataType: "json", //Expected data format from server
		success: function (data) {
			//alert(data);
			if(data.ItemType=='Book')
			{
				getBook(accession);
			}
			else if(data.ItemType=='Periodical')
			{
				getPeriodical(accession);
			}
			else if (data.ItemType=='Media') 
			{
				getMedia(accession);
			}
			else if (data.ItemType=='AVEquipment') 
			{
				getAV(accession);
			}
			else{
				//alert('notha');
			};
			
		},
		error: function (response) {
			alert('Failed: in itemtype' + response.statusText);
		}
	});
}

function getBook(accessionNumber) {
	var accession = (accessionNumber);
	//alert("item" + typeof accession); 
	$.ajax({
		type: "POST", //GET or POST
		url: hostaddress + 'LRCMobileService.svc/Web/findBook/?accessionNumber=' + accession, // Location of the service 
		processdata: false, 
        contentType: "application/json; charset=utf-8", // content type sent to server
        crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing?
        dataType: "json", //Expected data format from server
		success: function (data) {
			//alert(data);
			var result=JSON.stringify(data);
			var htmlSpray=createListViewHoldsDesc(data);
			var sprayLocation = "holddetailsResults";
			spraySearch(htmlSpray,sprayLocation);
			$('#'+result[0].ItemType).trigger('create');
		},
		error: function (response) {
			alert('Failed: in book itemtype' + response.statusText);
		}
	});
}

function getPeriodical(accessionNumber) {
	var accession = (accessionNumber);
	//alert("item" + typeof accession); 
	$.ajax({
		type: "POST", //GET or POST
		url: hostaddress + 'LRCMobileService.svc/Web/findPeriodical/?id=' + accession, // Location of the service 
		processdata: false, 
        contentType: "application/json; charset=utf-8", // content type sent to server
        crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing?
        dataType: "json", //Expected data format from server
		success: function (data) {
			//alert(data);
			var result=JSON.stringify(data);
			var htmlSpray=createListViewHoldsDesc(data);
			var sprayLocation = "holddetailsResults";
			spraySearch(htmlSpray,sprayLocation);
			$('#'+result[0].ItemType).trigger('create');
		},
		error: function (response) {
			alert('Failed: in per itemtype' + response.statusText);
		}
	});
}

function getMedia(accessionNumber) {
	var accession = (accessionNumber);
	//alert("item" + typeof accession); 
	$.ajax({
		type: "POST", //GET or POST
		url: hostaddress + 'LRCMobileService.svc/Web/findMedia/?id=' + accession, // Location of the service 
		processdata: false, 
        contentType: "application/json; charset=utf-8", // content type sent to server
        crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing?
        dataType: "json", //Expected data format from server
		success: function (data) {
			//alert(data);
			var result=JSON.stringify(data);
			var htmlSpray=createListViewHoldsDesc(data);
			var sprayLocation = "holddetailsResults";
			spraySearch(htmlSpray,sprayLocation);
			$('#'+result[0].ItemType).trigger('create');
		},
		error: function (response) {
			alert('Failed: in med itemtype' + response.statusText);
		}
	});
}

function getAV(accessionNumber) {
	var accession = (accessionNumber);
	//alert("item" + typeof accession); 
	$.ajax({
		type: "POST", //GET or POST
		url: hostaddress + 'LRCMobileService.svc/Web/findAV/?id=' + accession, // Location of the service 
		processdata: false, 
        contentType: "application/json; charset=utf-8", // content type sent to server
        crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing?
        dataType: "json", //Expected data format from server
		success: function (data) {
			var result=JSON.stringify(data);
			var htmlSpray=createListViewHoldsDesc(data);
			//alert (htmlSpray);
			var sprayLocation = "holddetailsResults";
			spraySearch(htmlSpray,sprayLocation);
			$('#'+result.AccessionNumber).trigger('create');
		},
		error: function (response) {
			alert('Failed: in av itemtype' + response.statusText);
		}
	});
}

function createNotificationListView(data, title, status, itemtype){
	strHtml='<ul data-role="listview"  data-inset="true" id="'+ itemtype +'" >';
	
	for(var i=0;i<data.length;i++)
	{
		var date = moment(data[i].Date);
		strHtml+='<li data-theme="a" id="'+JSON.stringify(data[i].AccessionNumber)+'"><a data-theme="a" data-role="button"><h3>'+ title
		+'</h3><p>'+ date +'</p>'
		+'<p>'+ status +'</p>'
		+'</a></li>';
	}
	strHtml+="</ul>";
	
	return strHtml;
}


function searchPeriodicals(search) {
var Data = search;
    $.ajax({
    	
        type: "POST", //GET or POST or PUT or DELETE verb
        url: hostaddress + "LRCMobileService.svc/Web/getPeriodicals/" + Data, // Location of the service
        processdata: false, 
        contentType: "application/json; charset=utf-8", // content type sent to server
        crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing?
        dataType: "json", //Expected data format from server
		success: function (data) {//On Successful service call
			var result=JSON.stringify(data);
			var htmlSpray=createListView(data);
			var sprayLocation = "searchResults";
			spraySearch(htmlSpray,sprayLocation);
			$('#'+result[0].ItemType).trigger('create');
			$.mobile.changePage('#pageSearch');
       	},
		error: function (response) {
            alert('Failed: ' + response.statusText);
        }// When Service call fails
   });
	//
}

function searchBooks(search) {
var Data = search;
    $.ajax({
    	
        type: "POST", //GET or POST or PUT or DELETE verb
        url: hostaddress + "LRCMobileService.svc/Web/getBooks/" + Data, // Location of the service
        processdata: false, 
        contentType: "application/json; charset=utf-8", // content type sent to server
        crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing?
        dataType: "json", //Expected data format from server
		success: function (data) {//On Successful service call
			var result=JSON.stringify(data);
			var htmlSpray=createListView(data);
			var sprayLocation = "searchResults";
			spraySearch(htmlSpray,sprayLocation);
			$('#'+result[0].ItemType).trigger('create').listview('refresh');
			$.mobile.changePage('#pageSearch');
       	},
		error: function (response) {
            alert('Failed: ' + response.statusText);
        }// When Service call fails
   });
	//
}

function createListView(data){
	strHtml='<ul data-role="listview"  data-inset="true" id="'+JSON.stringify(data[0].ItemType)+'" >';
	for(var i=0;i<data.length;i++)
	{
		strHtml+='<li data-theme="a" id="'+JSON.stringify(data[i].AccessionNumber)+'"><a data-theme="a" data-role="button" onclick = mobilePlaceHolds('+(Number(data[i].AccessionNumber))+')><h3>'+data[i].Title
		+'</h3><p>'+JSON.stringify(data[i].ItemType)+'</p>'
		+'<p>'+JSON.stringify(data[i].Status)+'</p>'
		+'</a></li>';
	}
	strHtml+="</ul>";
	
	return strHtml;
}


function spraySearch(strHtml,strId){
	$('#'+strId).html("");
	$('#'+strId).append(strHtml);
}
function spraySearchAppend(strHtml,strId){
	$('#'+strId).append(strHtml);
	$('#LoansNotify').trigger('create').listview('refresh'); 
}

function getStructureSize() {
    $.ajax({
        type: "GET", //GET or POST or PUT or DELETE verb
        url: hostaddress + "LRCMobileService.svc/Web/mobileGetIconContent", // Location of the service
        processdata: false, 
        contentType: "application/json; charset=utf-8", // content type sent to server
        crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing?
        dataType: "json", //Expected data format from server
		success: function (data) {//On Successful service call
            var totalIconContent = Number(JSON.stringify(data.length));
			sprayStructureIconContent(totalIconContent);
			//alert(JSON.stringify(data[2].Content));
			for (var i=0;i<data.length;i++)
				{
					//var content="";
					//content = (data[i].Content);
					//alert(typeof content);
					//alert(data[i]);
					content = JSON.stringify(data[i].Content);
					var newContent=parseContent(content);
					//alert("after stringify"+typeof content);
					
					var icon = data[i].IconName;
					
					//alert('Success: ' + data[0].placeInt);
					var placeNumber = Number(data[i].placeInt);
					putIconContent(icon, newContent, placeNumber);						
				}
				$('#listForIcons').trigger('create');
				
       	},
		error: function (response) {
            alert('Failed: ' + response.statusText);
        }// When Service call fails
   });
	//
}

function sprayStructureIconContent(totalIconContent){	
	var total = totalIconContent;
	var htmlString1 = "";
	var htmlString2 = "";
	var totalHtmlStr1 = "";
	var totalHtmlStr2="";
	for (var i=0; i<total ;i++)
	{
		htmlString1 = '<li data-theme="a" id = "IC'+(i+1) 
						+'" ></li>';
		
		totalHtmlStr1 += htmlString1;
		
	}
	
	sprayIconInList(totalHtmlStr1);
	$('#listForIcons').listview('refresh');
}


function sprayIconInList(totalHtmlStr1)
{
	$("#listForIcons").html(totalHtmlStr1);

}


function putIconContent(icon, content, placeNumber)
{
	var strcon=content;  
	var serverPath =hostaddress + "/Icon";
	var htmlString = '<a data-theme="a" data-role="button" href="#pageContent" onclick= sprayContent('+strcon+') >'
					+ '<img src="'+serverPath + icon
					+ '" alt="Image not displayed" width="70" height="60" '
					+'/>'
					+'</a>';

	sprayInUnorderedList(htmlString,placeNumber);
	$("#listForIcons").listview('refresh');
}

function parseContent(content)
{
	var strcon=content.split(" ");
	var newStr="";
	for (var i=0; i<strcon.length;i++){
		newStr+=strcon[i]+"&#32";
	}
	return newStr;
}


function sprayInUnorderedList(htmlString,placeNumber)
{
	$('#IC'+placeNumber).html(htmlString);
}

function sprayContent(stuff){
		$('#contentPages').html(stuff);
}


function handleSignIn() {
	var userName=GetSignInDetails().userName;
	var password=GetSignInDetails().password;
    $.ajax({
        type: "POST", //GET or POST or PUT or DELETE verb
        url: hostaddress + "LRCMobileService.svc/Web/authenticate/?user="+userName+"&pass="+password, // Location of the service
        //url: hostaddress + "/Web/authenticate/?user=" + details.userName + "&pass=" + details.password, // Location of the service
        processdata: false, 
        contentType: "application/json; charset=utf-8", // content type sent to server
        crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing?
        dataType: "json", //Expected data format from server
		success: function (data) {//On Successful service call
            //alert('success' + JSON.stringify(data));
            var authenticate = {valid:data.authenticatePatronResult.passValidate,patronId:data.authenticatePatronResult.patronId};
            resultSignInAuthentication(authenticate);			
       	},
		error: function (response) {
            alert('Failed: ' + response.statusText);
        }// When Service call fails
   });
	//
}

function getHolds(){ 
    var patronID = storePatronID(); 
    
    $.ajax({ 
        type: "POST", //GET or POST or PUT or DELETE verb 
        url: hostaddress + "LRCMobileService.svc/Web/getPatronHolds/?patronid=" + patronID, // Location of the service 
        processdata: false,  
        async: true,
        contentType: "application/json; charset=utf-8", // content type sent to server 
        //crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing? 
        dataType: "json", //Expected data format from server 
        success: function (data) {//On Successful service call 
            var strHtml=createHoldsListView(data,"HoldsUlList");
            spraySearch(strHtml,"holdsResults");
            $('#HoldsUlList').trigger('create').listview('refresh');
            
			//storePatronID();
       }, 
        error: function (response) { 
            alert('Failed: ' + response.statusText); 
        }// When Service call fails 
   });   
};

function getLoans(){ 
    var patronID = storePatronID(); 
    
    $.ajax({ 
        type: "POST", //GET or POST or PUT or DELETE verb 
        url: hostaddress + "LRCMobileService.svc/Web/retrievePatronLoans/?id=" + patronID, // Location of the service 
        processdata: false,  
        async: true,
        contentType: "application/json; charset=utf-8", // content type sent to server 
        //crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing? 
        dataType: "json", //Expected data format from server 
        success: function (data) {//On Successful service call 
           //alert(data);
           var strHtml='<ul data-role="listview"  data-inset="true" id="LoansRetrieve" >';
            strHtml+=createLoansListView(data,"LoansRetrieve");
            strHtml+='</ul>';
            spraySearch(strHtml,"holdsResults");
            $('#LoansRetrieve').trigger('create').listview('refresh');   
       }, 
        error: function (response) { 
            alert('Failed: ' + response.statusText); 
        }// When Service call fails 
   });   
};  

function createNotifications(){ 
    var patronID = storePatronID(); 
    
    $.ajax({ 
        type: "POST", //GET or POST or PUT or DELETE verb 
        url: hostaddress + "LRCMobileService.svc/Web/retrievePatronLoans/?id=" + patronID, // Location of the service 
        processdata: false,  
        async: true,
        contentType: "application/json; charset=utf-8", // content type sent to server 
        //crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing? 
        dataType: "json", //Expected data format from server 
        success: function (data) {//On Successful service call 
        	var newDataList=new Array();
        	var today = moment().toArray();
        	var duedate = null;
            for (var i =0; i<data.length; i++)
            {
            	duedate = moment(data[i].DueDate).toArray();
            	
            	if (today[0] > duedate[0])
            	{
            		newDataList[i]=data[i];
            	}
            	else if(today[1]>duedate[1])
            	{
            		newDataList[i]=data[i];
            	}
            	else if(today[2]+1 >= duedate[2])
            	{
            		newDataList[i]=data[i];
            	}
            	else
            	{
            	}
            }
            
            var strHtml='<ul data-role="listview"  data-inset="true" id="LoansNotify" >';
            strHtml+=createLoansListView(newDataList,"LoansNotify");
  
            spraySearch(strHtml,"notificationResults");
            //$('#LoansNotify').trigger('create').listview('refresh');  
            /*
            var strHtml=createLoansListView(data);
            spraySearch(strHtml,"holdsResults");
            $('#LoansUL').trigger('create').listview('refresh');   
            */
              }, 
        error: function (response) { 
            alert('Failed: ' + response.statusText); 
        }// When Service call fails 
   });   
};  

function createRecallNotifications(){ 
    var patronID = storePatronID(); 
    
    $.ajax({ 
        type: "GET", //GET or POST or PUT or DELETE verb 
        url: hostaddress + "LRCMobileService.svc/Web/getPatronRecalls/?patron=" + patronID, // Location of the service 
        processdata: false,  
        async: true,
        contentType: "application/json; charset=utf-8", // content type sent to server 
        //crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing? 
        dataType: "json", //Expected data format from server 
        success: function (data) {//On Successful service call 	}
        
            var strHtml1=createLoansListView2(data.getPatronRecallsResult,"LoansNotify");
            spraySearchAppend(strHtml1,"notificationResults");
            $('#LoansNotify').trigger('create').listview('refresh');  
            
              }, 
        error: function (response) { 
            alert('Failed: INside recall' + response.statusText); 
        }// When Service call fails 
   });   
};  

function mobilePlaceHolds(accessionNumber){ 
	
	var choice=confirm("You are about to place a hold on this item");
	if(choice ==true)
	{
		if (localStorage['userName'] !="")
	{
		var patronID = Number(storePatronID()); 
   		//s alert(accessionNumber);
    	var accession = Number(accessionNumber);
   		// alert(accession);
    	$.ajax({ 
        	type: "POST", //GET or POST or PUT or DELETE verb 
        	url: hostaddress + "LRCMobileService.svc/Web/mobPlaceHold/?accession=" + accession+"&patron=" + patronID, // Location of the service 
        	processdata: false,  
        	async: true,
        	contentType: "application/json; charset=utf-8", // content type sent to server 
        	//crossDomain: true, //not needed as it doesnt seem to be needed to force the request to be cross domain needs testing? 
        	dataType: "json", //Expected data format from server 
        	success: function (data) {//On Successful service call 
            	alert("Hold placed, check account details");
           		// alert(JSON.stringify(data));
           
       		}, 
        	error: function (response) { 
            	alert('Error placing hold' + response.statusText); 
        	}// When Service call fails 
   		});   
	}
	else
	{
		alert("Cannot place holds without signing in");
	}
	}
	
} 
