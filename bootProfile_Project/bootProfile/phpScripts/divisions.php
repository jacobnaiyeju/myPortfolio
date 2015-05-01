<?php
include 'dbConnection.php';
$var = array();
$sql="";
if(isset($_GET["leagueCode"])){
$leagueCode=$_GET["leagueCode"];
$sql = "SELECT * FROM division WHERE leagueCode = '".$leagueCode."';";
}
else{
$sql = "SELECT * FROM division";
}
$result = mysqli_query($con, $sql);

while($obj = mysqli_fetch_object($result)) {
$var[] = $obj;
}
echo '{"divisions":'.json_encode($var).'}';
?>