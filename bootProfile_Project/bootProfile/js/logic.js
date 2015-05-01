function getLeagues(){
	$("#bP_leagueContent").html("");
	var url='http://localhost:8000/bootProfile/phpScripts/leagues.php';
	$("#bP_leagueContent").html("");
	$.getJSON(url,function(data){
	$.each(data.leagues, function(i,league){
	var leagueCodeStr=String(league.leagueCode);
	var buildLeagues =
	'<div class="bP_league_container" onclick=getDivisions("'+leagueCodeStr+'") >'
	+'<img class="divImage" src="http://localhost:8000/bootProfile/'+league.leagueLogo+'" />'
	+'<div class="divName"><p>'+league.leagueName+'</p></div>'
	+'</div>' ;
	$("#bP_leagueContent").append(buildLeagues);
	});
	});
	$("#bP_leagueContent_backButton_holder").html("");
}

function getDivisions(leagueCodeStr){
	$("#bP_leagueContent").html("");
	var url='http://localhost:8000/bootProfile/phpScripts/divisions.php';
	var param={leagueCode:leagueCodeStr};
	$("#bP_leagueContent").html("");
	$.getJSON(url,param,function(data){
	$.each(data.divisions, function(i,division){
	var divisionCodeStr = division.divisionCode;
	var buildDivisions =
	'<div class="bP_division_container" onclick=getTeams("'+divisionCodeStr+'","'+leagueCodeStr+'") >'
	+'<img class="divImage" src="http://localhost:8000/bootProfile/'+division.divisionLogo+'" />'
	+'<div class="divName"><p>'+division.divisionName+'</p></div>'
	+'</div>' ;
	$("#bP_leagueContent").append(buildDivisions);
	});
	});
	var backBtn =
	'<button onclick=getLeagues() >'
	+' Go Back'
	+'</button>';
	$("#bP_leagueContent_backButton_holder").html(backBtn);
}

function getTeams(divisionCodeStr,leagueCodeStr){
	$("#bP_leagueContent").html("");
	var url='http://localhost:8000/bootProfile/phpScripts/teams.php';
	var param={divisionCode:divisionCodeStr};
	$("#bP_leagueContent").html("");
	$.getJSON(url,param,function(data){
	$.each(data.teams, function(i,team){
	var teamCodeStr = team.teamCode;
	var buildTeams =
	'<div class="bP_team_container" onclick=createTeamQuery("'+teamCodeStr+'") >'
	+'<img class="divImage" src="http://localhost:8000/bootProfile/'+team.teamLogo+'" />'
	+'<div class="divName"><p>'+team.teamName+'</p></div>'
	+'</div>' ;
	$("#bP_leagueContent").append(buildTeams);
	});
	});
	var backBtn =
	'<button onclick=getDivisions("'+leagueCodeStr+'") >'
	+' Go Back'
	+'</button>';
	$("#bP_leagueContent_backButton_holder").html(backBtn);
}

function createTeamQuery(teamCodeStr){
	var QueryStr='';
	var url='http://localhost:8000/bootProfile/phpScripts/teams.php';
	var param={teamCode:teamCodeStr};
	$.getJSON(url,param,function(data){
	$.each(data.teams, function(i,team){
	QueryStr='?teamCode='+ team.teamCode +'&managerCode='+ team.managerCode ;
	window.location.href='http://localhost:8000/bootProfile/team.html'+QueryStr;
	});
	});
}

function getTeamPlayers(teamCodeStr){
	$("#bP_leagueContent").html("");
	$("#bP_teamPlayers_holder").html("");
	var url='http://localhost:8000/bootProfile/phpScripts/players.php';
	var param={teamCode:teamCodeStr};
	$("#bP_teamPlayers_holder").html("");
	$.getJSON(url,param,function(data){
	$.each(data.players, function(i,player){
	var playerCodeStr = player.playerCode;
	var buildPlayers =
	'<div class="bP_player_container" onclick=getPlayerStat("'+playerCodeStr+'") >'
	+'<img class="divImage" src="http://localhost:8000/bootProfile/'+player.playerImage+'" />'
	+'<div class="divName"><p>'+player.playerName+'</p></div>'
	+'</div>' ;
	$("#bP_teamPlayers_holder").append(buildPlayers);
	});
	if(data.players.length<25)
	{
		var i=25-data.players.length;
		for(var count=0;count<i;count++){
			var buildPlayers =
				'<div class="bP_player_container" >'
				+'<img class="divImage" src="http://localhost:8000/bootProfile/'+'default/playerImage/default.png'+'" />'
				+'<div class="divName"><p>'+'player'+'</p></div>'
				+'</div>' ;
			$("#bP_teamPlayers_holder").append(buildPlayers);
		}
	}
	});
}

function getTeamInfo(){
	var result=[];
	var result1="";
	var url=window.location.href;
	var QueryStr=url.split('?');
	if(QueryStr[1].length>0){
		var QueryStrKeyValues=QueryStr[1].split('&');
		if(QueryStrKeyValues.length == 2){
		for(var i=0;i<QueryStrKeyValues.length;i++)
		{
			var data=QueryStrKeyValues[i].split('=');
			result[i]=data[1];
		}	
		return result;
		}
		else{
			var data=QueryStr[1].split('=');
			result1=data[1];
			return result1;
		}
	}	
}

function getTeamDetails(){
	var buildTeamHeader='';
	var url='http://localhost:8000/bootProfile/phpScripts/teams.php';
	var url1='http://localhost:8000/bootProfile/phpScripts/managers.php';
	var url2='http://localhost:8000/bootProfile/phpScripts/achievements.php'
	var param={teamCode:getTeamInfo()[0]};
	$.getJSON(url,param,function(data){
		buildTeamHeader+=
			'<div class="headerImageContainer" >'
			+'<img class="headerImage" src="'+data.teams[0].teamLogo+'" />'
			+'</div>'
			+'<div class="headerTitleContainer divName CusdivName">'
			+'<p>'+data.teams[0].teamName+'</p>'
			+'</div>';
			var param1={managerCode:data.teams[0].managerCode};
			$.getJSON(url1,param1,function(data){
				buildTeamHeader+=
				'<div class="headerImageContainer">'
				+'<img class="headerImage" src="http://localhost:8000/bootProfile/'+data.managers[0].managerImage+'" />'
				+'</div>'
				+'<div class="stopFloat">'
				+'</div>';
				$('#bP_teamProfile_header').html(buildTeamHeader);
			});
			getTeamPlayers(data.teams[0].teamCode);
			$.getJSON(url2,param,function(data){
			achievementBuilder('bP_teamProfile_holder',data);
			});
	});	
}
function generateSearchURL(){
	var query=$('.search').val();
	QueryStr='?queryCode='+query ;
	window.location.href='http://localhost:8000/bootProfile/searchResultsPage.html'+QueryStr;
}
function getSearchParam(){
	var result="";
	var url=window.location.href;
	var QueryStr=url.split('?');
	if(QueryStr[1].length>0){
		var QueryStrKeyValue=QueryStr[1].split('=');
		result=QueryStrKeyValue[1];
		return result;
	}	
}
function searchResultsCount(){
	this.value=0;
}
function searchResultsIncrease(fnc,addval){
	fnc.value+=addval;
}
function getSearchResults(){
    var resultsCount=new searchResultsCount();
	$("#bP_SearchResults_holder").html("");
	var urlT='http://localhost:8000/bootProfile/phpScripts/teams.php';
	var urlP='http://localhost:8000/bootProfile/phpScripts/players.php';
	var param={queryCode:getSearchParam()};
	$.getJSON(urlT,param,function(data){
	searchResultsIncrease(resultsCount,data.teams.length);
	if(data.teams.length>0){
	$("#bP_SearchResults_holder").append('<hr />');
	$("#bP_SearchResults_holder").append('<div class="styleRecords"><p>Teams</p></div>');
	$("#bP_SearchResults_holder").append('<hr/>');
	$.each(data.teams, function(i,team){
	var teamCodeStr = team.teamCode;
	var buildTeams =
	'<div class="bP_team_container" onclick=createTeamQuery("'+teamCodeStr+'") >'
	+'<img class="divImage" src="http://localhost:8000/bootProfile/'+team.teamLogo+'" />'
	+'<div class="divName"><p>'+team.teamName+'</p></div>'
	+'</div>' ;
	
	$("#bP_SearchResults_holder").append(buildTeams);
	});
	$("#bP_SearchResults_holder").append('<div class="stopFloat"></div>');
	}
	});
	$.getJSON(urlP,param,function(data){
	searchResultsIncrease(resultsCount,data.players.length);
	if(data.players.length>0){
	$("#bP_SearchResults_holder").append('<div class="stopFloat"></div>');
	$("#bP_SearchResults_holder").append('<hr />');
	$("#bP_SearchResults_holder").append('<div class="styleRecords"><p>Players</p></div>');
	$("#bP_SearchResults_holder").append('<hr />');
	$.each(data.players, function(i,player){
	var playerCodeStr = player.playerCode;
	var buildPlayers =
	'<div class="bP_player_container" onclick=getPlayerStat("'+playerCodeStr+'") >'
	+'<img class="divImage" src="http://localhost:8000/bootProfile/'+player.playerImage+'" />'
	+'<div class="divName"><p>'+player.playerName+'</p></div>'
	+'</div>' ;
	
	$("#bP_SearchResults_holder").append(buildPlayers);
	});
	$("#bP_SearchResults_holder").append('<div class="stopFloat"></div>');
	}
	var buildPlayers =
	'<div class="styleRecords">'
	+'<p> There were '
	+ resultsCount.value
	+' search results found.</p>'
	+'<div>';
	$("#bP_SearchResults_Count").html(buildPlayers);
	});
	
}

function tabbedBehaviour(){
		jQuery('.tabs .tab-links a').on('click', function(e)  {
        var currentAttrValue = jQuery(this).attr('href');
        jQuery('.tabs ' + currentAttrValue).show().siblings().hide();
        jQuery(this).parent('li').addClass('active').siblings().removeClass('active');
        e.preventDefault();
    });
}

function userControl(){
if(localStorage.getItem('user')=== null){
}
else{
authenticateUser(localStorage.getItem('username'),localStorage.getItem('password'));		
}
}

function authenticateUser(username,password){
if(username != "" && password != ""){
		url='http://localhost:8000/bootProfile/phpScripts/login.php';
		param={"username":username,"password":password}
			$.getJSON(url,param,function(data){
				if(data.authentication==true){
					localStorage.setItem('user',data.userProfile[0].userId);
					localStorage.setItem('username',data.userProfile[0].username);
					localStorage.setItem('password',data.userProfile[0].password);
					addAdvancedLinks();
					var buildUser='<div><Label> WELCOME '
					+localStorage.getItem('username')
					+'<button class="loginBtn" onclick=signOut() > Sign-Out </button>'
					+'</Label></div>';
					$(".bP_bootProfile_Login").html(buildUser);	
				}
				else{
					
				}
			});
		}
}

function addAdvancedLinks(){
var buildStr='&nbsp;|&nbsp;<button onclick=manageTeam() >Add new Player</button>';
$(".bP_EditTeam_holder").append(buildStr);
}

function manageTeam(){
	var query=getTeamInfo()[0];
	QueryStr='?editTeamCode='+query ;
	window.location.href='http://localhost:8000/bootProfile/manageBootProfile.html'+QueryStr;
}
function signOut(){
localStorage.clear();
window.location.href='http://localhost:8000/bootProfile/Index.html';
}

function setHiddenFields(){
	var buildInput='<input type="hidden" id="teamCode" name="teamCode" value="'+ getTeamInfo() +'"/>';
	$('#hiddenField').html(buildInput);
}
function getPlayerStat(playerCode){
	var query=playerCode;
	QueryStr='?playerCode='+query ;
	window.location.href='http://localhost:8000/bootProfile/playerProfile.html'+QueryStr;
}
function getPlayerDetails(){
 var queryCode=getTeamInfo();
 var param={"playerCode":queryCode};
 url='http://localhost:8000/bootProfile/phpScripts/players.php'
 url1='http://localhost:8000/bootProfile/phpScripts/achievements.php'
 $.getJSON(url,param,function(data){
 var buildStr=
 '<div class="bP_player_container" >'
	+'<img class="divImage" src="http://localhost:8000/bootProfile/'+data.players[0].playerImage+'" />'
	+'<div class="divName"><p>'+data.players[0].playerName+'</p></div>'
	+'</div>' ;
	$('#bP_playerProfile_header').html(buildStr);
 });
 $.getJSON(url1,param,function(data){
 achievementBuilder('bP_playersAchievement_holder',data);
 });
}
function achievementBuilder(appendStr,data){
	buildStr=
	'<div class="styleRecords">'
	+'<p> English League Titles: '
	+data.achievements[0].englishLeague
	+'</p>'
	+'<p> FACup Titles: '
	+data.achievements[0].FACup
	+'</p>'
	+'<p> CapitalOne Cup trophies: '
	+data.achievements[0].leagueCup
	+'</p>'
	+'<p> Community Shield Titles: '
	+data.achievements[0].communityShield
	+'</p>'
	+'<p> laLiga Titles: '
	+data.achievements[0].laLiga
	+'</p>'
	+'<p> copa del Rey trophies: '
	+data.achievements[0].copadelRey
	+'</p>'
	+'<p> supercopade Espana trophies: '
	+data.achievements[0].supercopadeEspana
	+'</p>'
	+'<p> serieA Titles: '
	+data.achievements[0].serieA
	+'</p>'
	+'</div>';
	$('#'+appendStr).html(buildStr);
}