<?php
include 'dbConnection.php';
$var = array();
$sql="";
if(isset($_GET["divisionCode"])){
$divisionCode=$_GET["divisionCode"];
$sql = "SELECT * FROM team WHERE divisionCode ='".$divisionCode."';";
}
else if(isset($_GET["teamCode"])){
$teamCode=$_GET["teamCode"];
$sql = "SELECT * FROM team WHERE teamCode ='".$teamCode."';";
}
else if(isset($_GET["queryCode"])){
$queryCode=$_GET["queryCode"];
$sql = "SELECT * FROM team WHERE teamName Like '%".$queryCode."%';";
}
else{
$sql = "SELECT * FROM team";
}
$result = mysqli_query($con, $sql);

while($obj = mysqli_fetch_object($result)) {
$var[] = $obj;
}
echo '{"teams":'.json_encode($var).'}';
?>