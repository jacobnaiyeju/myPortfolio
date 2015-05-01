<?php
include 'dbConnection.php';
$var = array();
$sql="";
if(isset($_GET["teamCode"])){
$teamCode=$_GET["teamCode"];
$sql = "SELECT * FROM player WHERE teamCode ='".$teamCode."';";
}
else if(isset($_GET["queryCode"])){
$queryCode=$_GET["queryCode"];
$sql = "SELECT * FROM player WHERE playerName LIKE '%".$queryCode."%';";
}
else if(isset($_GET["playerCode"])){
$playerCode=$_GET["playerCode"];
$sql = "SELECT * FROM player WHERE playerCode = '".$playerCode."';";
}
else{
$sql = "SELECT * FROM player";
}
$result = mysqli_query($con, $sql);

while($obj = mysqli_fetch_object($result)) {
$var[] = $obj;
}
echo '{"players":'.json_encode($var).'}';
?>