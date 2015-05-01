<?php
include 'dbConnection.php';
$var = array();
$sql="";
if(isset($_GET["teamCode"])){
$teamCode=$_GET["teamCode"];
$sql = "SELECT * FROM achievement WHERE achievementCode ='".$teamCode."';";
}
else if(isset($_GET["playerCode"])){
$playerCode=$_GET["playerCode"];
$sql = "SELECT * FROM achievement WHERE achievementCode ='".$playerCode."';";
}

$result = mysqli_query($con, $sql);

while($obj = mysqli_fetch_object($result)) {
$var[] = $obj;
}
echo '{"achievements":'.json_encode($var).'}';
?>