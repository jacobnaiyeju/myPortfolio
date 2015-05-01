<?php
include 'dbConnection.php';
function gen_uuid() {
    return sprintf( '%04x%04x%04x%04x%04x%04x%04x%04x',
        mt_rand( 0, 0xffff ), mt_rand( 0, 0xffff ),
        mt_rand( 0, 0xffff ),
        mt_rand( 0, 0x0fff ) | 0x4000,
        mt_rand( 0, 0x3fff ) | 0x8000,
        mt_rand( 0, 0xffff ), mt_rand( 0, 0xffff ), mt_rand( 0, 0xffff )
    );
}
if(isset($_POST["forename"])
	&&isset($_POST["surname"])
	&&isset($_POST["position"])
	&&isset($_POST["teamCode"]))
	{
		$playerCode=gen_uuid();
		$firstname=$_POST["forename"];
		$surname=$_POST["surname"];
		$name=$firstname." ".$surname;
		$position=$_POST["position"];
		$teamCode=$_POST["teamCode"];
		$playerImage="default/playerImage/blank.jpg";
		$sql = "INSERT INTO bootprofile.player(playerCode,teamCode,playerImage,playerName,playerPosition) VALUES('".$playerCode."','".$teamCode."','".$playerImage."','".$name."','".$position."');";
		$sql1 = "INSERT INTO bootprofile.achievement(achievementCode,englishLeague,FACup,leagueCup,communityShield,laLiga,copadelRey,supercopadeEspana,serieA,coppaItalia,supercoppaItaliana,bundesLiga,DFBPokal,championsLeague,europaLeague,FIFAClubWorldCup,worldCup,ballondOr,europeanGoldenShoe,FIFAWorldPlayeroftheYear,PichichiTrophy,UEFABestPlayerinEuropeAward,PFAPlayersPlayeroftheYear,PremierLeagueGoldenBoot,BundesligatopSoccer,serieATopLeagueGoalSoccer,laLigaTopGoalSoccer) VALUES('".$playerCode."',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);";
		mysqli_query($con, $sql);
		mysqli_query($con, $sql1);
		header("location:../manageBootProfile.html?editTeamCode=".$teamCode);
	}
?>
