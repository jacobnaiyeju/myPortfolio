<?php
include 'dbConnection.php';
$var = array();
$sql="";
if(isset($_GET["managerCode"])){
$managerCode=$_GET["managerCode"];
$sql = "SELECT * FROM manager WHERE managerCode ='".$managerCode."';";
}
else{
$sql = "SELECT * FROM manager";
}
$result = mysqli_query($con, $sql);

while($obj = mysqli_fetch_object($result)) {
$var[] = $obj;
}
echo '{"managers":'.json_encode($var).'}';
?>